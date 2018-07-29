package main

import (
	"bytes"
	"encoding/gob"
	"io"
	"io/ioutil"
	"log"
	"net/http"
	"os"
	"path/filepath"
	"strings"

	"github.com/lxn/win"
	ziphelper "github.com/pierrre/archivefile/zip"
	"github.com/pietroglyph/walk"
	. "github.com/pietroglyph/walk/declarative"
	"github.com/pietroglyph/xferspdy"
	"github.com/skratchdot/open-golang/open"
)

const (
	windowWidth  = 400
	windowHeight = 100

	gameDirSelectInfo = ` Please select Garfield Kart's directory. Example locations:
"C:\Program Files (x86)\Steam\steamapps\common\Garfield Kart"
"E:\SteamLibrary\steamapps\common\Garfield Kart"`

	garfieldKartGameID       = "362930"
	patchFileURL             = "https://lasagne.margo.ml"
	binaryPatchFileBlockSize = 40960

	// Windows-focused for now
	defaultGamePath    = `C:\Program Files (x86)\Steam\steamapps\common\Garfield Kart`
	fileToPatchSubPath = `GarfieldKartNoMulti_Data\Managed\Assembly-CSharp.dll`
)

type patchState int

const (
	waitingToStart patchState = iota
	readingData
	writingData
)

func main() {
	var progressBar *walk.ProgressBar
	var button *walk.PushButton
	var window *walk.MainWindow
	state := waitingToStart

	declarativeWindow := MainWindow{
		Title:            "Garfield Kart Patcher",
		MinSize:          Size{Width: windowWidth, Height: windowHeight},
		Layout:           VBox{},
		FixedSize:        true,
		MaximizeDisabled: true,
		Children: []Widget{
			ProgressBar{AssignTo: &progressBar},
			PushButton{
				Text: "Begin Patching",
				OnClicked: func() {
					go func() {
						pi := window.ProgressIndicator()
						pi.SetState(walk.PINormal)
						pi.SetTotal(100)
						setProgress(0, progressBar, pi)

						gamePath := defaultGamePath

						buttonRedoText := "Retry Patching"
						button.SetEnabled(false)
						defer func() {
							state = waitingToStart
							button.SetEnabled(true)
							button.SetText(buttonRedoText)
						}()

						button.SetText("Locating Garfield Kart")
						state = readingData
						for {
							_, err := os.Stat(filepath.Join(gamePath, fileToPatchSubPath))
							if !os.IsNotExist(err) && err == nil {
								break
							}
							// Show an error message, and allow the user to abort
							if walk.MsgBox(window, "Couldn't Locate Garfield Kart", "Garfield Kart couldn't be located. You will now be prompted to locate it manually.", walk.MsgBoxOKCancel|walk.MsgBoxIconExclamation) == win.IDCANCEL {
								pi.SetState(walk.PIError)
								return
							}

							// Prompt the user to find Garfield Kart's folder
							fd := walk.FileDialog{
								Title: gameDirSelectInfo, // This actually shows up in the body of the dialog, so we can put helpful info here
							}
							fd.ShowBrowseFolder(window)
							gamePath = fd.FilePath
							log.Println(fd.FilePath)
						}
						setProgress(10, progressBar, pi)

						button.SetText("Downloading patch bundle")
						resp, err := http.Get(patchFileURL)
						setProgress(30, progressBar, pi)
						var body io.ReaderAt
						var bodySize int64
						if err != nil {
							pi.SetState(walk.PIPaused)
							if walk.MsgBox(window, "Error Downloading Patch Bundle", "Couldn't download the patch bundle ("+err.Error()+")... Would you like to select one locally?", walk.MsgBoxYesNo|walk.MsgBoxIconExclamation) == win.IDNO {
								pi.SetState(walk.PIError)
								return
							}

							picker := walk.FileDialog{
								Title:  "Select a Patch Bundle",
								Filter: "Compressed Patch Bundle (*.patchbundle)",
							}
							for {
								picker.ShowOpen(window)

								// The user can escape the loop if they press cancel
								if picker.FilePath == "" {
									return
								}

								file, err := os.Open(picker.FilePath)
								if err != nil {
									continue
								}

								fileStat, err := file.Stat()
								if err == nil {
									body = file
									bodySize = fileStat.Size()
									break
								}
							}
						} else {
							buf, err := ioutil.ReadAll(resp.Body)
							if err != nil {
								walk.MsgBox(window, "Error Reading Downloaded Patch Bundle", "Couldn't read downloaded patch bundle ("+err.Error()+").", walk.MsgBoxIconExclamation)
								pi.SetState(walk.PIError)
								return
							}
							bodySize = int64(len(buf))
							body = bytes.NewReader(buf)
						}
						setProgress(40, progressBar, window.ProgressIndicator())

						button.SetText("Writing assets to disk")
						var binaryPatchFilePaths []string
						state = writingData
						ziphelper.Unarchive(body, bodySize, gamePath, func(archivePath string) {
							if strings.HasSuffix(archivePath, ".patch") {
								binaryPatchFilePaths = append(binaryPatchFilePaths, filepath.Join(gamePath, archivePath))
							}
						})
						setProgress(70, progressBar, pi)

						button.SetText("Patching binary files")
						for _, v := range binaryPatchFilePaths {
							pi.SetState(walk.PINormal)
							binaryToPatch, err := os.OpenFile(strings.TrimSuffix(v, ".patch"), os.O_RDWR, 0755)
							if err != nil {
								walk.MsgBox(window, "Error Opening Corresponding Binary", "A binary corresponding to a patch file was not found or could not be opened. Installation will continue, and this error may be spurious, or your game may be corrupted.\n"+err.Error(), walk.MsgBoxOK|walk.MsgBoxIconWarning)
								pi.SetState(walk.PIError)
								continue
							}
							patchFile, err := os.Open(v)
							if err != nil {
								walk.MsgBox(window, "Error Opening Constituent Binary Patch File", "Although the main patch bundle was downloaded and read successfully, one of it's constituent binary patch files could not be opened. Installation will continue, but your download may be corrupted.\n"+err.Error(), walk.MsgBoxOK|walk.MsgBoxIconWarning)
								pi.SetState(walk.PIError)
								continue
							}

							var delta []xferspdy.Block
							err = gob.NewDecoder(patchFile).Decode(&delta)
							if err != nil {
								walk.MsgBox(window, "Error Decoding Constituent Binary Patch File", "Although the main patch bundle was downloaded and read successfully, one of it's constituent binary patch files could not be decoded. Installation will continue, but your download may be corrupted.\n"+err.Error(), walk.MsgBoxOK|walk.MsgBoxIconWarning)
								pi.SetState(walk.PIError)
								continue
							}
							err = xferspdy.PatchOpenedFile(delta, binaryToPatch, binaryToPatch)
							if err != nil {
								walk.MsgBox(window, "Error Patching Binary File", "Although the main patch bundle was downloaded and read successfully, one of it's constituent binary patches could not be applied. Installation will continue, but your download may be corrupted.\n"+err.Error(), walk.MsgBoxOK|walk.MsgBoxIconWarning)
								pi.SetState(walk.PIError)
								continue
							}
							os.Remove(v) // This returns an error, but we're going to ignore it because this isn't a very important step
						}
						setProgress(100, progressBar, pi)

						button.SetText("Done patching")
						pi.SetState(walk.PIPaused)
						if walk.MsgBox(window, "Done Patching", "Garfield Kart has been patched succsessfully. Would you like to run it?", walk.MsgBoxYesNo|walk.MsgBoxIconQuestion) == win.IDYES {
							open.Run("steam://rungameid/" + garfieldKartGameID)
						}

						pi.SetState(walk.PINoProgress)
						buttonRedoText = "Patch Again"
						state = waitingToStart
					}()
				},
				AssignTo: &button,
			},
		},
		AssignTo: &window,
	}

	go func() {
		for {
			if window != nil {
				break
			}
		}
		window.Closing().Attach(func(canceled *bool, reason walk.CloseReason) {
			if state != waitingToStart {
				message := "Would you like to abort the operation? "
				if state == readingData {
					message += "The patch hasn't been written yet, so your game probably won't be corrupted."
				} else if state == writingData {
					message += "The patch is being written, and your game will probably be corrupted if you abort. Steam's game validation and repair function will be launched if you abort. Wait for it to finish."
					open.Start("steam://validate/" + garfieldKartGameID)
				}

				if walk.MsgBox(window, "Confirm Abort", message, walk.MsgBoxYesNo|walk.MsgBoxIconAsterisk) == win.IDYES {
					*canceled = false
				} else {
					*canceled = true
				}
			}
		})
	}()

	declarativeWindow.Run()
}

func setProgress(value int, progressBar *walk.ProgressBar, progressIndicator *walk.ProgressIndicator) {
	progressBar.SetValue(value)
	progressIndicator.SetCompleted(uint32(value))
	progressIndicator.SetState(walk.PINormal)
}
