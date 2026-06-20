; ===========================================================================
;  Disk Benchmark - NSIS Installer Script
;  Author : Ari Sohandri Putra (program) / Installer wrapper by NSIS
;  Output : DiskBenchmark-1.0-Vista7-Default(EN).exe
;  Tested : NSIS 3.10 - 3.12 (Unicode)
; ===========================================================================

; ---------------------------------------------------------------------------
;  Compression & Unicode
; ---------------------------------------------------------------------------
Unicode true
SetCompressor /SOLID lzma
SetCompressorDictSize 32

; ---------------------------------------------------------------------------
;  Basic installer info (visible in "Add / Remove Programs")
; ---------------------------------------------------------------------------
Name               "Disk Benchmark"
BrandingText      "Disk Benchmark 1.0"
OutFile           "DiskBenchmark-1.0-Vista7-Default(EN).exe"

InstallDir        "$PROGRAMFILES32\Disk Benchmark"
InstallDirRegKey  HKLM "Software\Disk Benchmark" "InstallDir"

ShowInstDetails   show
ShowUnInstDetails show

RequestExecutionLevel admin

; Version info embedded into the final .exe (right-click -> Properties)
VIProductVersion "1.0.0.0"
VIAddVersionKey "ProductName"      "Disk Benchmark"
VIAddVersionKey "CompanyName"      "Ari Sohandri Putra"
VIAddVersionKey "LegalCopyright"   "Copyright (C) 2026 Ari Sohandri Putra"
VIAddVersionKey "FileDescription"  "Disk Benchmark Setup"
VIAddVersionKey "FileVersion"      "1.0.0.0"
VIAddVersionKey "ProductVersion"   "1.0.0.0"

; ===========================================================================
;  MUI (legacy wizard UI)
; ===========================================================================
!include "MUI.nsh"
!include "LogicLib.nsh"
!include "Sections.nsh"

; ---------------------------------------------------------------------------
;  MUI defines - icons & bitmaps shipped with NSIS 3.x
;  Icons use the "orange" set for a fresh look distinct from the default
;  grey modern-install.ico.
; ---------------------------------------------------------------------------
!define MUI_ABORTWARNING
!define MUI_ICON                 "${NSISDIR}\Contrib\Graphics\Icons\orange-install.ico"
!define MUI_UNICON               "${NSISDIR}\Contrib\Graphics\Icons\orange-uninstall.ico"
!define MUI_HEADERIMAGE
!define MUI_HEADERIMAGE_BITMAP   "${NSISDIR}\Contrib\Graphics\Header\win.bmp"
!define MUI_HEADERIMAGE_UNBITMAP "${NSISDIR}\Contrib\Graphics\Header\win.bmp"
!define MUI_WELCOMEFINISHPAGE_BITMAP   "${NSISDIR}\Contrib\Graphics\Wizard\win.bmp"
!define MUI_UNWELCOMEFINISHPAGE_BITMAP "${NSISDIR}\Contrib\Graphics\Wizard\win.bmp"

; ===========================================================================
;  Page order
;  TWO license pages are inserted back-to-back: NSIS MUI supports calling
;  MUI_PAGE_LICENSE multiple times. Each call creates a new license page.
; ===========================================================================

; --- LICENSE #1: Disk Benchmark (LICENSE.txt) -------------------------------
; Defines below apply ONLY to the next MUI_PAGE_LICENSE insert.
!define MUI_LICENSEPAGE_TEXT_TOP    "Please review the license terms for Disk Benchmark before continuing."
!define MUI_LICENSEPAGE_TEXT_BOTTOM "If you accept the terms of the agreement, click I Agree to continue. You must accept the agreement to install Disk Benchmark."
!define MUI_LICENSEPAGE_BUTTON      "I &Agree"
!define MUI_LICENSEPAGE_CHECKBOX    true
!define MUI_PAGE_HEADER_TEXT         "License Agreement"
!define MUI_PAGE_HEADER_SUBTEXT      "Disk Benchmark - Copyright (C) 2026 Ari Sohandri Putra"

; --- Page 1: Welcome
!insertmacro MUI_PAGE_WELCOME

; --- Page 2: License 1 (Disk Benchmark)
!insertmacro MUI_PAGE_LICENSE "LICENSE.txt"

; ---------------------------------------------------------------------------
; The MUI_LICENSEPAGE_* defines above are consumed by the previous macro.
; We now redefine them for the SECOND license page (DiskSpd).
; ---------------------------------------------------------------------------

; --- LICENSE #2: DiskSpd (DiskSpd-LICENSE.txt) ------------------------------
; NSIS MUI auto-undefs these defines after each MUI_PAGE_LICENSE, so we use
; /nonfatal here to silently skip the !undef if the symbol is already gone.
!undef /nonfatal MUI_LICENSEPAGE_TEXT_TOP
!undef /nonfatal MUI_LICENSEPAGE_TEXT_BOTTOM
!undef /nonfatal MUI_LICENSEPAGE_BUTTON
!undef /nonfatal MUI_LICENSEPAGE_CHECKBOX
!undef /nonfatal MUI_PAGE_HEADER_TEXT
!undef /nonfatal MUI_PAGE_HEADER_SUBTEXT

!define MUI_LICENSEPAGE_TEXT_TOP    "Please review the license terms for DiskSpd before continuing."
!define MUI_LICENSEPAGE_TEXT_BOTTOM "If you accept the terms of the agreement, click I Agree to continue. DiskSpd is (C) Microsoft, distributed under the MIT license."
!define MUI_LICENSEPAGE_BUTTON      "I &Agree"
!define MUI_LICENSEPAGE_CHECKBOX    true
!define MUI_PAGE_HEADER_TEXT         "License Agreement (DiskSpd)"
!define MUI_PAGE_HEADER_SUBTEXT      "DiskSpd - Copyright (c) 2014 Microsoft"

; --- Page 3: License 2 (DiskSpd)
!insertmacro MUI_PAGE_LICENSE "DiskSpd-LICENSE.txt"

; Clean up the per-page defines so they don't leak into other pages.
!undef /nonfatal MUI_LICENSEPAGE_TEXT_TOP
!undef /nonfatal MUI_LICENSEPAGE_TEXT_BOTTOM
!undef /nonfatal MUI_LICENSEPAGE_BUTTON
!undef /nonfatal MUI_LICENSEPAGE_CHECKBOX
!undef /nonfatal MUI_PAGE_HEADER_TEXT
!undef /nonfatal MUI_PAGE_HEADER_SUBTEXT

; --- Page 4: Components (pick desktop shortcut, etc.)
!insertmacro MUI_PAGE_COMPONENTS

; --- Page 5: Directory
!insertmacro MUI_PAGE_DIRECTORY

; --- Page 6: Installing
!insertmacro MUI_PAGE_INSTFILES

; --- Page 7: Finish (with "Run now" + "View help.html" checkboxes)
!define MUI_FINISHPAGE_RUN               "$INSTDIR\DiskBenchmark.exe"
!define MUI_FINISHPAGE_RUN_TEXT          "&Run Disk Benchmark now"
!define MUI_FINISHPAGE_RUN_NOTCHECKED
!define MUI_FINISHPAGE_SHOWREADME        "$INSTDIR\help.html"
!define MUI_FINISHPAGE_SHOWREADME_TEXT   "&View help.html"
!define MUI_FINISHPAGE_SHOWREADME_NOTCHECKED
!define MUI_FINISHPAGE_NOAUTOCLOSE
!insertmacro MUI_PAGE_FINISH

; --- Uninstaller pages
!insertmacro MUI_UNPAGE_WELCOME
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES
!insertmacro MUI_UNPAGE_FINISH

; --- Language
!insertmacro MUI_LANGUAGE "English"

; ===========================================================================
;  Section descriptions (shown on the Components page)
; ===========================================================================
LangString DESC_Core      ${LANG_ENGLISH} "Main Disk Benchmark executable and required files (required)."
LangString DESC_Desktop   ${LANG_ENGLISH} "Create a shortcut to Disk Benchmark on the Windows Desktop."
LangString DESC_StartMenu ${LANG_ENGLISH} "Create Start Menu shortcuts for Disk Benchmark, Help, and Uninstall."

; ===========================================================================
;  Sections
; ===========================================================================

; --- Section: Core files (required) ----------------------------------------
Section "Disk Benchmark (required)" SecCore
    SectionIn RO
    SetOutPath "$INSTDIR"
    SetOverwrite on

    ; Main executable
    File "DiskBenchmark.exe"

    ; Help file
    File "help.html"

    ; License files - ship them in the install ROOT (matching the original
    ; program layout). DiskSpd-LICENSE.txt is NOT placed inside DiskSpd\.
    File "LICENSE.txt"
    File "DiskSpd-LICENSE.txt"

    ; settings\ subfolder - preserved verbatim from the source tree.
    SetOutPath "$INSTDIR\settings"
    File /r "settings\*.*"

    ; DiskSpd\ subfolder - installed automatically (no checkbox), preserving
    ; the original program folder layout. DiskSpd-LICENSE.txt stays in the
    ; install ROOT, NOT inside DiskSpd\.
    SetOutPath "$INSTDIR\DiskSpd"
    File /r "DiskSpd\*.*"

    ; Write uninstaller back in root
    SetOutPath "$INSTDIR"
    WriteUninstaller "$INSTDIR\Uninstall.exe"

    ; Register uninstall info in Add/Remove Programs
    WriteRegStr   HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Disk Benchmark" "DisplayName"           "Disk Benchmark"
    WriteRegStr   HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Disk Benchmark" "DisplayVersion"        "1.0.0.0"
    WriteRegStr   HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Disk Benchmark" "Publisher"             "Ari Sohandri Putra"
    WriteRegStr   HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Disk Benchmark" "DisplayIcon"           "$INSTDIR\DiskBenchmark.exe"
    WriteRegStr   HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Disk Benchmark" "InstallLocation"       "$INSTDIR"
    WriteRegStr   HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Disk Benchmark" "UninstallString"       "$\"$INSTDIR\Uninstall.exe$\""
    WriteRegStr   HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Disk Benchmark" "QuietUninstallString"  "$\"$INSTDIR\Uninstall.exe$\" /S"
    WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Disk Benchmark" "NoModify" 1
    WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Disk Benchmark" "NoRepair" 1

    ; Persist install dir for upgrades
    WriteRegStr HKLM "Software\Disk Benchmark" "InstallDir" "$INSTDIR"
SectionEnd

; --- Section: Start Menu shortcuts -----------------------------------------
Section "Start Menu shortcuts" SecStartMenu
    CreateDirectory "$SMPROGRAMS\Disk Benchmark"
    CreateShortCut  "$SMPROGRAMS\Disk Benchmark\Disk Benchmark.lnk" "$INSTDIR\DiskBenchmark.exe" "" "$INSTDIR\DiskBenchmark.exe" 0
    CreateShortCut  "$SMPROGRAMS\Disk Benchmark\Help.lnk"           "$INSTDIR\help.html"          "" "" 0
    CreateShortCut  "$SMPROGRAMS\Disk Benchmark\Uninstall.lnk"      "$INSTDIR\Uninstall.exe"      "" "" 0
SectionEnd

; --- Section: Desktop shortcut ---------------------------------------------
Section "Desktop shortcut" SecDesktop
    CreateShortCut "$DESKTOP\Disk Benchmark.lnk" "$INSTDIR\DiskBenchmark.exe" "" "$INSTDIR\DiskBenchmark.exe" 0
SectionEnd

; ===========================================================================
;  Section descriptions shown on the Components page
; ===========================================================================
!insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
!insertmacro MUI_DESCRIPTION_TEXT ${SecCore}      $(DESC_Core)
!insertmacro MUI_DESCRIPTION_TEXT ${SecStartMenu} $(DESC_StartMenu)
!insertmacro MUI_DESCRIPTION_TEXT ${SecDesktop}   $(DESC_Desktop)
!insertmacro MUI_FUNCTION_DESCRIPTION_END

; ===========================================================================
;  .onInit - default section states
; ===========================================================================
Function .onInit
    ; Start Menu pre-checked; Desktop unchecked by default.
    ; (DiskSpd is installed automatically as part of the required Core section.)
    SectionSetFlags ${SecStartMenu} ${SF_SELECTED}
    SectionSetFlags ${SecDesktop}   0
FunctionEnd

; ===========================================================================
;  Uninstaller
; ===========================================================================
Function un.onInit
    MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 \
      "Are you sure you want to completely remove Disk Benchmark and all of its components?" \
      IDYES +2
    Abort
FunctionEnd

Section "Uninstall"
    ; --- Files in root ---
    Delete "$INSTDIR\DiskBenchmark.exe"
    Delete "$INSTDIR\help.html"
    Delete "$INSTDIR\LICENSE.txt"
    Delete "$INSTDIR\DiskSpd-LICENSE.txt"
    Delete "$INSTDIR\Uninstall.exe"

    ; --- settings\ and DiskSpd\ folders - remove recursively.
    ;     This preserves the original program folder layout and cleans
    ;     any extra files added later inside these folders too.
    RMDir /r "$INSTDIR\settings"
    RMDir /r "$INSTDIR\DiskSpd"

    ; --- Shortcuts ---
    Delete "$SMPROGRAMS\Disk Benchmark\Disk Benchmark.lnk"
    Delete "$SMPROGRAMS\Disk Benchmark\Help.lnk"
    Delete "$SMPROGRAMS\Disk Benchmark\Uninstall.lnk"
    RmDir  "$SMPROGRAMS\Disk Benchmark"

    Delete "$DESKTOP\Disk Benchmark.lnk"

    ; --- Registry ---
    DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Disk Benchmark"
    DeleteRegKey HKLM "Software\Disk Benchmark"

    ; --- Empty install folder ---
    RmDir "$INSTDIR"
SectionEnd

; ===========================================================================
;  End of script
; ===========================================================================
