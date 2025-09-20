; Monopoly Tycoon Inno Setup Script
; REQ-1-012: The application shall be packaged and distributed as a standalone installer created with Inno Setup.

[Setup]
; NOTE: The AppId is a unique identifier for your application.
; It is recommended to generate a new GUID for your application.
AppId={{C1B7A9FD-E084-4B4E-A8D7-7B4L9D7C4C6B}}
AppName=Monopoly Tycoon
AppVersion=1.0.0
AppPublisher=Monopoly Tycoon Dev Team
AppPublisherURL=https://www.example.com/
AppSupportURL=https://www.example.com/support
AppUpdatesURL=https://www.example.com/updates
DefaultDirName={autopf}\Monopoly Tycoon
DefaultGroupName=Monopoly Tycoon
DisableProgramGroupPage=yes
LicenseFile=eula.rtf
OutputBaseFilename=MonopolyTycoon_Setup_v1.0.0
Compression=lzma
SolidCompression=yes
WizardStyle=modern
PrivilegesRequired=admin ; Required for writing to Program Files
OutputDir=Output
SetupIconFile=game_icon.ico

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
; REQ-1-012: ...create a desktop shortcut.
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: checkedonce

[Files]
; This assumes the build output is placed in a 'release_build' directory relative to this script.
Source: "release_build\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: The game executable is assumed to be "MonopolyTycoon.exe"
; The EULA and icon files must be present in the same directory as this script.

[Icons]
Name: "{group}\Monopoly Tycoon"; Filename: "{app}\MonopolyTycoon.exe"
Name: "{autodesktop}\Monopoly Tycoon"; Filename: "{app}\MonopolyTycoon.exe"; Tasks: desktopicon

[Run]
Filename: "{app}\MonopolyTycoon.exe"; Description: "{cm:LaunchProgram,Monopoly Tycoon}"; Flags: nowait postinstall skipifsilent

[UninstallDelete]
; REQ-1-100: Handle user data directory on uninstall
Type: dirifempty; Name: "{userappdata}\MonopolyTycoon\saves"
Type: dirifempty; Name: "{userappdata}\MonopolyTycoon\logs"
Type: dirifempty; Name: "{userappdata}\MonopolyTycoon"

[Code]
// REQ-1-100: During the uninstallation process, the user must be presented with an explicit choice
// to either keep or delete their personal data located in the `%APPDATA%/MonopolyTycoon/` directory.

procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
var
  DataPath: String;
  ResultCode: Integer;
begin
  if CurUninstallStep = usPostUninstall then
  begin
    DataPath := ExpandConstant('{userappdata}\MonopolyTycoon');
    if DirExists(DataPath) then
    begin
      // The default option is 'No' (IDNO) to prevent accidental data loss.
      ResultCode := MsgBox('Do you want to remove all your personal data, including save games, player profiles, and statistics?'#13#10#13#10 +
        'If you plan to reinstall Monopoly Tycoon later, you should choose No.', mbConfirmation, MB_YESNO or MB_DEFBUTTON2);
      
      if ResultCode = IDYES then
      begin
        Log('User chose to delete personal data directory: ' + DataPath);
        DelTree(DataPath, True, True, True);
      end
      else
      begin
        Log('User chose to keep personal data directory.');
      end;
    end;
  end;
end;