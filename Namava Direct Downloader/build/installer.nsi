Name "{Name}"
OutFile "{Name} installer.exe"
!include "BaseInstall.nsh"
Function .onInit
  ${If} ${RunningX64}
    SetRegView 64
  ${Else}
    SetRegView 32
  ${EndIf}
  SetOutPath $INSTDIR
FunctionEnd
InstallDir "$PROGRAMFILES\Namava Direct Downloader"
InstallDirRegKey HKCR "nmvopdl\shell\open\command" ""

Section
  File /r "..\..\Namava Direct Downloader\bin\Release\Namava Direct Downloader.exe"
  File /r "..\..\Namava Direct Downloader\bin\Release\System.Json.dll"
  File /r "..\..\Namava Direct Downloader\build\ffmpeg.exe"
  WriteRegStr HKCR "nmvopdl" "" "URL:nmvopdl"
  WriteRegStr HKCR "nmvopdl" "URL Protocol" ""
  WriteRegStr HKCR "nmvopdl\shell\open\command" "" "$\"$instdir\Namava Direct Downloader.exe$\" $\"%1$\""
SectionEnd