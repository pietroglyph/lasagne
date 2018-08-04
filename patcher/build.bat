@ECHO OFF
go get -u github.com/akavel/rsrc
go get -u ./
%GOPATH%\bin\rsrc.exe -ico icon.ico -manifest patcher.exe.manifest -o rsrc.syso
go build -ldflags="-H windowsgui"
