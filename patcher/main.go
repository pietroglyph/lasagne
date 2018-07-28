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

	"github.com/lxn/walk"
	. "github.com/lxn/walk/declarative"
	"github.com/lxn/win"
	ziphelper "github.com/pierrre/archivefile/zip"
	"github.com/pietroglyph/xferspdy"
	"github.com/skratchdot/open-golang/open"
)

const (
	windowWidth  = 400
	windowHeight = 256

	gameDirSelectInfo = ` Please select Garfield Kart's directory. Example locations:
"C:\Program Files (x86)\Steam\steamapps\common\Garfield Kart"
"E:\SteamLibrary\steamapps\common\Garfield Kart"`

	garfieldKartGameID       = "362930"
	patchFileURL             = "https://patches.lasagne.margo.ml"
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
		Title:   "Garfield Kart Patcher",
		MinSize: Size{Width: windowWidth, Height: windowHeight},
		Layout:  VBox{},
		Children: []Widget{
			ProgressBar{AssignTo: &progressBar},
			PushButton{
				Text: "Begin Patching",
				OnClicked: func() {
					go func() {
						gamePath := defaultGamePath
						progressBar.SetValue(0)

						button.SetEnabled(false)
						button.SetText("Locating Garfield Kart")
						state = readingData
						defer func() {
							state = waitingToStart
							button.SetEnabled(true)
							button.SetText("Retry Patching")
						}()
						for {
							_, err := os.Stat(filepath.Join(gamePath, fileToPatchSubPath))
							if !os.IsNotExist(err) && err == nil {
								break
							}
							// Show an error message, and allow the user to abort
							if walk.MsgBox(window, "Couldn't Locate Garfield Kart", "Garfield Kart couldn't be located. You will now be prompted to locate it manually.", walk.MsgBoxOKCancel|walk.MsgBoxIconExclamation) == win.IDCANCEL {
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
						progressBar.SetValue(10)

						button.SetText("Downloading patches")
						resp, err := http.Get(patchFileURL)
						progressBar.SetValue(30)
						var body io.ReaderAt
						var bodySize int64
						if err != nil {
							if walk.MsgBox(window, "Error Downloading Patches", "Couldn't download a patch file ("+err.Error()+")... Would you like to select one locally?", walk.MsgBoxYesNo|walk.MsgBoxIconExclamation) == win.IDNO {
								return
							}
							picker := walk.FileDialog{
								Title:  "Select a Patch File",
								Filter: "Compressed Patch File (*.cpatch)",
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
								walk.MsgBox(window, "Error Reading Downloaded Patches", "Couldn't read downloaded patches ("+err.Error()+").", walk.MsgBoxIconExclamation)
								return
							}
							bodySize = int64(len(buf))
							body = bytes.NewReader(buf)
						}
						progressBar.SetValue(40)

						button.SetText("Writing assets to disk")
						var binaryPatchFilePaths []string
						state = writingData
						ziphelper.Unarchive(body, bodySize, gamePath, func(archivePath string) {
							if strings.HasSuffix(archivePath, ".patch") {
								binaryPatchFilePaths = append(binaryPatchFilePaths, filepath.Join(gamePath, archivePath))
							}
						})
						progressBar.SetValue(70)

						button.SetText("Patching binary files, and cleaning up")
						for _, v := range binaryPatchFilePaths {
							binaryToPatch, err := os.Open(strings.TrimSuffix(v, ".patch"))
							if err != nil {
								walk.MsgBox(window, "Error Opening Corresponding Binary", "A binary corresponding to a patch file was not found or could not be opened. Installation will continue, and this error may be spurious, or your game may be corrupted.\n"+err.Error(), walk.MsgBoxOK|walk.MsgBoxIconWarning)
								continue
							}
							patchFile, err := os.Open(v)
							if err != nil {
								walk.MsgBox(window, "Error Opening Constituent Binary Patch File", "Although the main (non-binary) patch file was downloaded and read successfully, one of it's constituent binary patch files could not be opened. Installation will continue, but your download may be corrupted.\n"+err.Error(), walk.MsgBoxOK|walk.MsgBoxIconWarning)
								continue
							}

							var delta []xferspdy.Block
							err = gob.NewDecoder(patchFile).Decode(&delta)
							if err != nil {
								walk.MsgBox(window, "Error Decoding Constituent Binary Patch File", "Although the main (non-binary) patch file was downloaded and read successfully, one of it's constituent binary patch files could not be decoded. Installation will continue, but your download may be corrupted.\n"+err.Error(), walk.MsgBoxOK|walk.MsgBoxIconError)
								return
							}
							err = xferspdy.PatchOpenedFile(delta, binaryToPatch, binaryToPatch)
							if err != nil {
								walk.MsgBox(window, "Error Patching Binary File", "Although the main (non-binary) patch file was downloaded and read successfully, one of it's constituent binary patches could not be applied. Installation will continue, but your download may be corrupted.\n"+err.Error(), walk.MsgBoxOK|walk.MsgBoxIconError)
							}
						}
						progressBar.SetValue(100)

						progressBar.SetMarqueeMode(true)
						if walk.MsgBox(window, "Done Patching", "Garfield Kart has been patched succsessfully. Would you like to run it?", walk.MsgBoxYesNo|walk.MsgBoxIconQuestion) == win.IDYES {
							open.Run("steam://rungameid/" + garfieldKartGameID)
						}

						progressBar.SetMarqueeMode(false)
						button.SetText("Patch again")
						button.SetEnabled(true)
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
					message += "The patch is being written, and your game will be corrupted if you abort. Steam's game validation and repair function will be launched if you abort. Wait for it to finish."
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
