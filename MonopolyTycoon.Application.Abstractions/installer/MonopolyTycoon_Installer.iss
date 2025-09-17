; Monopoly Tycoon Inno Setup Script
; REQ-1-012: The application shall be packaged and distributed as a standalone installer created with Inno Setup.
; REQ-1-100: The system's uninstaller must perform a clean removal... with an explicit choice to... delete personal data.

#define MyAppName "Monopoly Tycoon"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "Monopoly Tycoon Dev Team"
#define MyAppURL "https://www.example.com"
#define MyAppExeName "MonopolyTycoon.exe"

[Setup]
; NOTE: The AppId is a unique identifier for your application.
; It is recommended to use a GUID.
AppId={{F0A8C2B3-D4E5-4F6A-B7C8-D9E0A1B2C3D4}}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
DisableProgramGroupPage=yes
LicenseFile=EULA.txt
OutputDir=.\Output
OutputBaseFilename=MonopolyTycoon_Setup_{#MyAppVersion}
Compression=lzma
SolidCompression=yes
WizardStyle=modern
PrivilegesRequired=admin
SignedUninstaller=yes
; SignTool=... (configure signing tool path and parameters in CI/CD pipeline)

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: checkablealone

[Files]
; This path should point to the published output of the Unity build
Source: "..\UnityProject\Builds\Windows\MonopolyTycoon.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\UnityProject\Builds\Windows\*_Data\*"; DestDir: "{app}\{sourcebasename}_Data"; Flags: ignoreversion recursesubdirs createallsubdirs
; Add other necessary files like DLLs or configuration files here
Source: "..\UnityProject\Assets\StreamingAssets\appsettings.json"; DestDir: "{app}\MonopolyTycoon_Data\StreamingAssets"; Flags: ignoreversion

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[UninstallDelete]
Type: filesandordirs; Name: "{app}"

[Code]
var
  DeleteUserDataPage: TInputQueryWizardPage;
  DeleteUserDataCheckBox: TNewCheckBox;

procedure InitializeWizard();
begin
  DeleteUserDataPage := CreateInputQueryPage(wpUninstallProgress,
    'Delete Personal Data', 'Do you want to delete your personal data?',
    'This includes save games, player profiles, and statistics.'#13#10#13#10 +
    'If you plan to reinstall {#MyAppName} later, you should keep this data.');
  
  DeleteUserDataCheckBox := TNewCheckBox.Create(DeleteUserDataPage);
  DeleteUserDataCheckBox.Caption := 'Yes, delete my personal data.';
  DeleteUserDataCheckBox.Checked := False;
  DeleteUserDataCheckBox.Parent := DeleteUserDataPage.Surface;
end;

function ShouldSkipPage(PageID: Integer): Boolean;
begin
  Result := False;
  if PageID = DeleteUserDataPage.ID then
  begin
    // REQ-1-100: Only show this page if user data actually exists.
    if not DirExists(ExpandConstant('{userappdata}\MonopolyTycoon')) then
      Result := True;
  end;
end;

procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
var
  UserDataPath: string;
begin
  if (CurUninstallStep = usPostUninstall) and (DeleteUserDataPage.Values[0] = '1') then
  begin
    UserDataPath := ExpandConstant('{userappdata}\MonopolyTycoon');
    if DirExists(UserDataPath) then
    begin
      Log('User chose to delete personal data. Removing directory: ' + UserDataPath);
      DelTree(UserDataPath, True, True, True);
    end
    else
    begin
      Log('User chose to delete personal data, but directory not found: ' + UserDataPath);
    end;
  end;
end;