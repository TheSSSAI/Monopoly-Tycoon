; Monopoly Tycoon Inno Setup Script
; Fulfills REQ-1-012, US-001, US-002, US-003, US-006, US-007

#define MyAppName "Monopoly Tycoon"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "Tycoon Games"
#define MyAppURL "https://www.tycoongames.com/monopolytycoon"
#define MyAppExeName "MonopolyTycoon.exe"
#define MyAppId "{{C1A2B3D4-E5F6-7890-1234-567890ABCDEF}}"
#define MinRequiredWindowsVersion "10.0.17763" ; Windows 10 (1809)

[Setup]
AppId={#MyAppId}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}
AllowNoIcons=yes
LicenseFile=.\EULA.txt
OutputDir=.\Builds
OutputBaseFilename=MonopolyTycoon_Setup_v{#MyAppVersion}
Compression=lzma2/ultra64
SolidCompression=yes
WizardStyle=modern
VISTAFIED
MinVersion=0,6.1.7601
PrivilegesRequired=admin
WizardImageFile=.\wizard-image.bmp
WizardSmallImageFile=.\wizard-small-image.bmp

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: checkablealone

[Files]
Source: "..\Builds\Windows\MonopolyTycoon.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\Builds\Windows\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: ..\Builds\Windows\ is the assumed output directory of the Unity build.

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[UninstallDelete]
Type: filesandordirs; Name: "{app}"
Type: dirifempty; Name: "{userappdata}\MonopolyTycoon"

[Code]
var
  UserDataPath: String;
  DeleteUserData: Boolean;

function IsGameRunning(): Boolean;
begin
  Result := FindWindowByClassName('UnityWndClass') <> 0;
end;

procedure InitializeWizard();
begin
  UserDataPath := ExpandConstant('{userappdata}\MonopolyTycoon');
  DeleteUserData := False;
end;

function InitializeUninstall(): Boolean;
begin
  if IsGameRunning() then
  begin
    MsgBox('The game is currently running. Please close Monopoly Tycoon before uninstalling.', mbError, MB_OK);
    Result := False;
  end
  else
  begin
    Result := True;
  end;
end;

procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
begin
  if (CurUninstallStep = usUninstall) then
  begin
    if DirExists(UserDataPath) then
    begin
      if MsgBox('Do you want to remove all your personal data, including save games, player profiles, and statistics?'#13#10#13#10'If you plan to reinstall Monopoly Tycoon later, you should choose No.', mbConfirmation, MB_YESNO or MB_DEFBUTTON2) = IDYES then
      begin
        DeleteUserData := True;
      end;
    end;
  end;
end;

procedure CurStepChanged(CurStep: TSetupStep);
var
  SpaceRequired, FreeSpace: Int64;
begin
  if CurStep = ssInstall then
  begin
    // Check for min required Windows version (REQ-1-013)
    if not IsWindowsVersionOrGreater(10, 0, 17763) then
    begin
      MsgBox('This game requires Windows 10 (version 1809) or later.', mbError, MB_OK);
      WizardForm.Close;
    end;
  end;
end;

procedure DeinitializeUninstall();
begin
  if DeleteUserData then
  begin
    if DirExists(UserDataPath) then
    begin
      Log(Format('User opted to delete personal data. Removing directory: %s', [UserDataPath]));
      DelTree(UserDataPath, True, True, True);
    end;
  end;
end;