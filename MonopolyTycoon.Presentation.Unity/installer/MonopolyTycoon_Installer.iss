; Monopoly Tycoon Installer Script for Inno Setup
; Fulfills requirements: REQ-1-012, REQ-1-100
; Fulfills user stories: US-001, US-002, US-003, US-006, US-007

#define MyAppName "Monopoly Tycoon"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "Monopoly Tycoon Dev Team"
#define MyAppURL "https://www.example.com"
#define MyAppExeName "MonopolyTycoon.exe"
#define UserDataPath "MonopolyTycoon"

[Setup]
; NOTE: The AppId is a unique identifier for your application.
; It must be different for every application you create.
AppId={{C6E0A4E1-2D29-4A7B-9D09-0C343D1A2F16}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=yes
LicenseFile=license.txt
; Required for checking if the app is running
AppMutex=MonopolyTycoonGameMutex
OutputBaseFilename=MonopolyTycoon_Setup_v{#MyAppVersion}
OutputDir=Output
Compression=lzma
SolidCompression=yes
WizardStyle=modern
SignTool=signtool
SignedUninstaller=yes
MinVersion=10.0.17763

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: checkablealone

[Files]
Source: "build\windows\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "build\windows\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[UninstallDelete]
Type: filesandordirs; Name: "{app}"
Type: dirifempty; Name: "{userappdata}\{#UserDataPath}"

[Code]
var
  DeleteUserData: Boolean;

// This function is called when the uninstaller starts.
procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
begin
  if CurUninstallStep = usUninstall then
  begin
    // Check if the user data directory exists
    if DirExists(ExpandConstant('{userappdata}\{#UserDataPath}')) then
    begin
      // Prompt the user to keep or delete their data
      if MsgBox('Do you want to remove all your personal data, including save games, player profiles, and statistics?' + #13#10 + #13#10 + 'If you plan to reinstall Monopoly Tycoon later, you should choose No.', mbConfirmation, MB_YESNO or MB_DEFBUTTON2) = IDYES then
      begin
        DeleteUserData := True;
      end
      else
      begin
        DeleteUserData := False;
      end;
    end
    else
    begin
      // No data directory found, so no need to ask or delete
      DeleteUserData := False;
    end;
  end;
end;

// This function is called after the main uninstallation is complete
procedure CurStepChanged(CurStep: TSetupStep);
begin
  if (CurStep = ssPostUninstall) and (DeleteUserData) then
  begin
    Log('User chose to delete personal data. Removing directory: ' + ExpandConstant('{userappdata}\{#UserDataPath}'));
    DelTree(ExpandConstant('{userappdata}\{#UserDataPath}'), True, True, True);
  end;
end;

// Function to check disk space and prevent installation if insufficient
function GetSpaceRequired(const Path: String): Int64;
var
  Space: Int64;
begin
  Space := 2 * 1024 * 1024 * 1024; // 2 GB as per REQ-1-013
  Result := Space;
end;

function NextButtonClick(CurPageID: Integer): Boolean;
var
  FreeSpace, RequiredSpace: Int64;
begin
  Result := True;
  if CurPageID = wpSelectDir then
  begin
    RequiredSpace := GetSpaceRequired(WizardDirValue);
    FreeSpace := GetSpaceOnDisk(WizardDirValue);
    if FreeSpace < RequiredSpace then
    begin
      MsgBox('Insufficient disk space on the selected drive.' + #13#10 +
        'Required: ' + IntToStr(RequiredSpace div (1024*1024)) + ' MB' + #13#10 +
        'Available: ' + IntToStr(FreeSpace div (1024*1024)) + ' MB',
        mbError, MB_OK);
      Result := False;
    end;
  end;
end;