package main

import (
	"bufio"
	"log"
	"os"
	"strings"

	flag "github.com/ogier/pflag"
)

func main() {
	path := flag.StringP("patch", "p", "./0001-Add-all-game-patches.patch", "Path to patch file.")
	flag.Parse()

	file, err := os.Open(*path)
	defer file.Close()
	if err != nil {
		log.Fatal(err.Error())
		return
	}

	var currentFile *os.File
	scanner := bufio.NewScanner(file)
	for scanner.Scan() {
		text := scanner.Text()
		if strings.HasPrefix(text, "+++") {
			currentFile, err = os.Create(strings.TrimPrefix(text, "+++ b/"))
			if err != nil {
				log.Println("Couldn't create referenced file for writing:", err.Error())
				continue
			}
			continue
		}

		if currentFile == nil {
			continue
		}

		if strings.HasPrefix(text, "+") {
			_, err = currentFile.Write([]byte(strings.TrimPrefix(text, "+") + "\n"))
			if err != nil {
				log.Println("Couldn't write referenced file:", err.Error())
				continue
			}
		}
	}
}
