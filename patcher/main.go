package main

import (
	"log"
	"os"

	"github.com/andlabs/ui"
)

const (
	windowWidth  = 400
	windowHeight = 256

	popupWidth  = 200
	popupHeight = 100

	// Windows-focused for now
	defaultGamePath    = `C:\Program Files (x86)\Steam\steamapps\common\Garfield Kart`
	fileToPatchSubPath = `\GarfieldKartNoMulti_Data\Managed\Assembly-CSharp.dll`
)

var isLoading bool = true

func main() {
	err := ui.Main(func() {
		var gamePath string = defaultGamePath

		progressBar := ui.NewProgressBar()
		startPatchButton := ui.NewButton("Initializing...")
		startPatchButton.Disable()

		controlsContainer := ui.NewVerticalBox()
		controlsContainer.Append(progressBar, false)
		controlsContainer.Append(startPatchButton, false)

		window := ui.NewWindow("Garfield Kart Patcher", windowWidth, windowHeight, false)
		window.SetMargined(false)
		window.SetChild(controlsContainer)
		window.Show()

		// First we find the game
		startPatchButton.SetText("Locating Garfield Kart...")
		for {
			_, err := os.Stat(gamePath + fileToPatchSubPath)
			if !os.IsNotExist(err) && err != nil {
				break
			}
			shouldAbort, _ := twoChoiceBox("Couldn't locate Garfield Kart",
				`Garfield Kart couldn't be found. You will now be prompted to locate it manually.
			Example locations include:
			"C:\Program Files (x86)\Steam\steamapps\common\Garfield Kart"
			"E:\SteamLibrary\steamapps\common\Garfield Kart"`, "Abort", "Locate Manually")
			if shouldAbort {
				ui.Quit()
			}
			gamePath = ui.OpenFile(window)
		}
		progressBar.SetValue(10)

		window.OnClosing(func(_ *ui.Window) bool {
			return exitWarningAndCleanup()
		})
		ui.OnShouldQuit(func() bool {
			return exitWarningAndCleanup()
		})
	})
	if err != nil {
		log.Panic(err)
	}
}

func exitWarningAndCleanup() bool {
	if isLoading {
		return false
	}
	ui.Quit()
	return true
}

func twoChoiceBox(title, description, choiceOne, choiceTwo string) (choiceOneChosen bool, choiceTwoChosen bool) {
	w := ui.NewWindow(title, popupWidth, popupHeight, false)
	mainContainer := ui.NewVerticalBox()
	buttonContainer := ui.NewHorizontalBox()
	descriptionLabel := ui.NewLabel(description)
	choiceOneButton := ui.NewButton(choiceOne)
	choiceTwoButton := ui.NewButton(choiceTwo)

	w.SetChild(mainContainer)

	buttonContainer.Append(choiceOneButton, true)
	buttonContainer.Append(choiceTwoButton, true)

	mainContainer.Append(descriptionLabel, true)
	mainContainer.Append(buttonContainer, true)

	needsToClose := false
	w.OnClosing(func(_ *ui.Window) bool {
		needsToClose = true
		return true
	})

	choiceOneClicked := true // If they close the window manually, choice one is "chosen" by default
	choiceTwoClicked := false
	choiceOneButton.OnClicked(func(_ *ui.Button) {
		choiceOneClicked = true
		choiceTwoClicked = false
		needsToClose = true
	})
	choiceTwoButton.OnClicked(func(_ *ui.Button) {
		choiceOneClicked = false
		choiceTwoClicked = true
		needsToClose = true
	})

	w.Show()

	// for {
	if needsToClose {
		return choiceOneClicked, choiceTwoClicked
	}
	return false, false
	// }
}
