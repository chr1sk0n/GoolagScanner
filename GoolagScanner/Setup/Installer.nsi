; NSIS
; Install Script for Goolag Scan
; Cult of the Dead Cow 2008
;
;    Because you are attenuated, and 
;    neither one nor zero, I will expel
;    thee from my pasture. BOV 3:16
;

!include WordFunc.nsh
!include LogicLib.nsh
!include MUI.nsh
!include x64.nsh
!insertmacro VersionCompare

Name "GoolagScan"
OutFile "GoolagScan Setup.exe"
BrandingText "Bow to the Cow"
SetCompressor /SOLID LZMA
RequestExecutionLevel admin

!define MUI_ICON "Installer.ico"
!define MUI_UNICON "UnInstaller.ico"
!define LICENSE "License.txt"
!define DISCLAIMER "Disclaimer.txt"
!define NETX86_URL "http://www.microsoft.com/downloads/info.aspx?na=90&p=&SrcDisplayLang=en&SrcCategoryId=&SrcFamilyId=0856eacb-4362-4b0d-8edd-aab15c5e04f5&u=http%3a%2f%2fdownload.microsoft.com%2fdownload%2f5%2f6%2f7%2f567758a3-759e-473e-bf8f-52154438565a%2fdotnetfx.exe"
!define NETX64_URL "http://www.microsoft.com/downloads/info.aspx?na=90&p=&SrcDisplayLang=en&SrcCategoryId=&SrcFamilyId=b44a0000-acf8-4fa1-affb-40e78d788b00&u=http%3a%2f%2fdownload.microsoft.com%2fdownload%2fa%2f3%2ff%2fa3f1bf98-18f3-4036-9b68-8e6de530ce0a%2fNetFx64.exe"
!define VER "1.0.0.37"
!define FULLNAME "GoolagScan Setup"
!define WEBSITE "www.cultdeadcow.com"
!define NAME "GoolagScan"
!define FRAMEWORK "$DESKTOP\Framework.EXE"

VIProductVersion "${VER}"
;VIAddVersionKey FileDescription ""
VIAddVersionKey LegalCopyright "2008 Cult of the Dead Cow"
VIAddVersionKey Comments "${WEBSITE}"
VIAddVersionKey CompanyName "Cult of the Dead Cow"
VIAddVersionKey FileVersion "${VER}"

VAR NET_URL
VAR PROXY_ADDRESS 	;Incase of future development
VAR PROXY_USER		;Incase of future development
VAR PROXY_PASSWORD	;Incase of future development
VAR PROXY_TYPE		;Incase of future development
VAR MUI_TEMP
VAR STARTMENU_FOLDER


Function .onInit
;Initialize Settings
StrCpy $PROXY_ADDRESS ""
StrCpy $PROXY_USER ""
StrCpy $PROXY_PASSWORD ""
StrCpy $PROXY_TYPE ""

  Call GetDotNETVersion
  Pop $0
  ${If} $0 == "not found" ;If .Net Framework wasn't installed...
    MessageBox MB_OK|MB_ICONSTOP ".NET runtime library is not installed.$\rWould you like to download .NET v2.0?" IDYES YesPlease IDNO NoThanks
	 YesPlease:
	  ${If} ${FileExists} ${FRAMEWORK} ;If we know we already downloaded it... 
		ExecWait ${FRAMEWORK} ;lets try again.
		Delete ${FRAMEWORK} ;always clean your plate.
	  ${Else} ;If it doesn't exist
	    Call DownloadDotNet ;Then download it.
	  ${EndIf}
	  Call .onInit ;Now check again to see if it is installed
	 NoThanks:
	  Abort    
  ${EndIf}
 
  StrCpy $0 $0 "" 1 # skip "v"
 
  ${VersionCompare} $0 "2.0" $1
  ${If} $1 == 2 ; If .NET framework was installed, but was a lower version than 2.0...
    MessageBox MB_YESNO|MB_ICONSTOP ".NET runtime library v2.0 or newer is required. You have $0.$\rWould you like to download .NET v2.0?" IDYES Yes IDNO No
     Yes:
	  ${If} ${FileExists} ${FRAMEWORK} ;If we know we already downloaded it... 
		ExecWait ${FRAMEWORK} ;lets try again.
		Delete ${FRAMEWORK} ;always clean your plate
	  ${Else} ;If it doesn't exist
	    Call DownloadDotNet ;Then download it.
	  ${EndIf}
	  Call .onInit ;Now check again to see if it is installed
	 No:
	   Abort
  ${EndIf}
FunctionEnd
 
InstallDir "$PROGRAMFILES\GoolagScan" ;Default installation folder
InstallDirRegKey HKCU "Software\GoolagScan" "" ;Get installation folder from registry if available

!define MUI_ABORTWARNING

  !insertmacro MUI_PAGE_LICENSE "${DISCLAIMER}"
    !define MUI_LICENSEPAGE_RADIOBUTTONS
  !insertmacro MUI_PAGE_LICENSE "${LICENSE}"
  !insertmacro MUI_PAGE_DIRECTORY
  !define MUI_STARTMENUPAGE_REGISTRY_ROOT "HKCU" 
  !define MUI_STARTMENUPAGE_REGISTRY_KEY "Software\GoolagScan" 
  !define MUI_STARTMENUPAGE_REGISTRY_VALUENAME "Start Menu Folder"
  !insertmacro MUI_PAGE_STARTMENU Application $STARTMENU_FOLDER
  !insertmacro MUI_PAGE_INSTFILES
  !insertmacro MUI_UNPAGE_CONFIRM
  !insertmacro MUI_UNPAGE_INSTFILES
  !insertmacro MUI_LANGUAGE "English"

Section "Dummy Section" SecDummy

  SetOutPath "$INSTDIR\bin\Release"
  File "GoolagScan.exe"
  File "GoolagScan.exe.config"
  File "GoolagScan.exe.manifest"
  File ${LICENSE}

	SetOutPath "$INSTDIR\Dorkdata"
	
	File "gdorks.xml"
	
  ;Store installation folder
  WriteRegStr HKCU "Software\GoolagScan" "" $INSTDIR
  
  ;Create uninstaller
  WriteUninstaller "$INSTDIR\Uninstall.exe"

  !insertmacro MUI_STARTMENU_WRITE_BEGIN Application
    
    ;Create shortcuts
    CreateDirectory "$SMPROGRAMS\$STARTMENU_FOLDER"
    CreateShortCut "$SMPROGRAMS\$STARTMENU_FOLDER\GoolagScan.lnk" "$INSTDIR\bin\Release\GoolagScan.exe"
    CreateShortCut "$SMPROGRAMS\$STARTMENU_FOLDER\Uninstall.lnk" "$INSTDIR\Uninstall.exe"
  
  !insertmacro MUI_STARTMENU_WRITE_END

SectionEnd


  LangString DESC_SecDummy ${LANG_ENGLISH} "A test section."
  !insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
    !insertmacro MUI_DESCRIPTION_TEXT ${SecDummy} $(DESC_SecDummy)
  !insertmacro MUI_FUNCTION_DESCRIPTION_END

Section "Uninstall"

  RMDir /r "$INSTDIR\bin\Release"
  RMDir /r "$INSTDIR\bin"
  RMDir /r "$INSTDIR\Dorkdata"
  Delete "$INSTDIR\Uninstall.exe"
  RMDir "$INSTDIR"
  !insertmacro MUI_STARTMENU_GETFOLDER Application $MUI_TEMP
  Delete "$SMPROGRAMS\$MUI_TEMP\GoolagScan.lnk"
  Delete "$SMPROGRAMS\$MUI_TEMP\Uninstall.lnk"
  RMDir "$SMPROGRAMS\$MUI_TEMP"
  StrCpy $MUI_TEMP "$SMPROGRAMS\$MUI_TEMP"
 
  startMenuDeleteLoop:
	ClearErrors
    RMDir $MUI_TEMP
    GetFullPathName $MUI_TEMP "$MUI_TEMP\.."
    IfErrors startMenuDeleteLoopDone
    StrCmp $MUI_TEMP $SMPROGRAMS startMenuDeleteLoopDone startMenuDeleteLoop

  startMenuDeleteLoopDone:
      DeleteRegKey /ifempty HKCU "Software\GoolagScan"

SectionEnd

!macro Download URL FILE
	${If} $PROXY_TYPE == "auto"
	${OrIf} $PROXY_TYPE == ""
		inetc::get  /BANNER "GoolagScan Installer$\n$\nDownloading Microsoft .NET Framework..." /RESUME "Error Connecting To The Internet.$\rPlease Check Your Connection and Settings." /USERAGENT "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.1.12) Gecko/20080201 Firefox/2.0.0.12" "${URL}" "${FILE}" /END
	${ElseIf} $PROXY_TYPE == "proxy"
		${If} $PROXY_USER == ""
			inetc::get  /BANNER "GoolagScan Installer$\n$\nDownloading Microsoft .NET Framework..." /RESUME "Error Connecting To The Internet.$\rPlease Check Your Connection and Settings." /USERAGENT "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.1.12) Gecko/20080201 Firefox/2.0.0.12" /PROXY $PROXY_ADDRESS "${URL}" "${FILE}" /END
		${Else}
			inetc::get  /BANNER "GoolagScan Installer$\n$\nDownloading Microsoft .NET Framework..." /RESUME"Error Connecting To The Internet.$\rPlease Check Your Connection and Settings." /USERAGENT "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.1.12) Gecko/20080201 Firefox/2.0.0.12" /PROXY $PROXY_ADDRESS /USER $PROXY_USER /PASSWORD $PROXY_PASSWORD "${URL}" "${FILE}" /END
		${EndIf}
	${EndIf}
!macroend

!define Download `!insertmacro Download`

Function GetDotNETVersion
  Push $0
  Push $1
 
  System::Call "mscoree::GetCORVersion(w .r0, i ${NSIS_MAX_STRLEN}, *i) i .r1 ?u"
  StrCmp $1 "error" 0 +2
    StrCpy $0 "not found"
 
  Pop $1
  Exch $0
FunctionEnd

Function DownloadDotNet
  System::Call "kernel32::GetCurrentProcess() i .s"		;**
  System::Call "kernel32::IsWow64Process(i s, *i .r0)"	;**Load a check for 64-bit OS
  ${If} ${RunningX64} ;If we are running on a 64bit system...
  ${OrIf} $0 == "1" ;Check the earlier test for 64-bit incase RunningX64 didn't work out.
	StrCpy $NET_URL ${NETX64_URL}
  ${Else} ;We are running a 32bit system...
    StrCpy $NET_URL ${NETX86_URL}
  ${EndIf}
  ${Download} "$NET_URL" ${FRAMEWORK}
  ${If} ${FileExists} ${FRAMEWORK}
    ExecWait ${FRAMEWORK}
	Sleep 500 ;Hope we aren't still accessing it. Try to delete it.
	Delete ${FRAMEWORK}
  ${EndIf}
FunctionEnd