RequestExecutionLevel admin
Unicode true
AllowRootDirInstall true
!define MUI_ICON "..\..\Namava Direct Downloader\images\Namava Direct Downloader Logo.ico"
!include "MUI2.nsh"
!include "x64.nsh"

;Interface Settings
;--------------------------------
  !define MUI_ABORTWARNING
  !define MUI_HEADERIMAGE
  !define MUI_HEADERIMAGE_BITMAP       "..\..\Namava Direct Downloader\images\nsis3-metro-right-NDD.bmp"
  !define MUI_WELCOMEFINISHPAGE_BITMAP "..\..\Namava Direct Downloader\images\nsis3-metro-NDD.bmp"

;--------------------------------
;Pages
;--------------------------------
  !insertmacro MUI_PAGE_WELCOME
  !insertmacro MUI_PAGE_DIRECTORY
  !insertmacro MUI_PAGE_INSTFILES
  !insertmacro MUI_PAGE_FINISH
  
  !insertmacro MUI_UNPAGE_WELCOME
  !insertmacro MUI_UNPAGE_CONFIRM
  !insertmacro MUI_UNPAGE_INSTFILES
  !insertmacro MUI_UNPAGE_FINISH

;--------------------------------
;Languages

  !insertmacro MUI_LANGUAGE "English"

;--------------------------------
UninstPage uninstConfirm
UninstPage instfiles