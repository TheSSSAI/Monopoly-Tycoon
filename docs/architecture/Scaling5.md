# 1 System Overview

## 1.1 Analysis Date

2025-06-13

## 1.2 Technology Stack

- Windows Desktop
- Inno Setup
- .NET 8
- Unity Engine

## 1.3 Architecture Type

Standalone Monolithic Desktop Application

## 1.4 Deployment Target

End-User Windows PC (Windows 10/11 64-bit)

# 2.0 Deployment Strategy

| Property | Value |
|----------|-------|
| Approach | Standalone Executable Installer |
| Justification | The system is a completely offline, single-player ... |
| Update Mechanism | Manual download and execution of a new, full insta... |

# 3.0 Distribution Artifact

## 3.1 Type

🔹 Single Executable Installer (.exe)

## 3.2 Technology

Inno Setup

## 3.3 Technology Justification

Explicitly required by REQ-1-012 for creating a simple, user-friendly installation package.

## 3.4 Contents

- Compiled game executable and all required asset files from the Unity build.
- Required runtime dependencies (e.g., .NET 8 Desktop Runtime, if not bundled by Unity).
- Digital User Manual and other documentation files (e.g., Privacy Policy, EULA).
- Application icon file.

## 3.5 Security

### 3.5.1 Signing

The final installer executable must be signed with a valid Authenticode certificate to prevent Windows SmartScreen warnings and ensure authenticity.

### 3.5.2 Integrity

The installer will use internal checksums to verify its own integrity during execution.

# 4.0.0 Installation Process

## 4.1.0 User Interface

Standard wizard-based interface requiring minimal user interaction.

## 4.2.0 User Options

### 4.2.1 Option

#### 4.2.1.1 Option

Installation Directory

#### 4.2.1.2 Description

Allows the user to select the destination folder for the application files.

#### 4.2.1.3 Requirement Id

REQ-1-012

### 4.2.2.0 Option

#### 4.2.2.1 Option

Desktop Shortcut Creation

#### 4.2.2.2 Description

Allows the user to create a shortcut on their desktop.

#### 4.2.2.3 Requirement Id

REQ-1-012

### 4.2.3.0 Option

#### 4.2.3.1 Option

Start Menu Entry Creation

#### 4.2.3.2 Description

Creates a folder and shortcuts in the Windows Start Menu.

## 4.3.0.0 File Placement

### 4.3.1.0 Application Files

#### 4.3.1.1 Type

🔹 Application Files

#### 4.3.1.2 Location

User-selected directory (e.g., 'C:\Program Files\MonopolyTycoon')

#### 4.3.1.3 Permissions

Read-only for standard users.

### 4.3.2.0 User Data (Saves, Profile, Stats)

#### 4.3.2.1 Type

🔹 User Data (Saves, Profile, Stats)

#### 4.3.2.2 Location

'%APPDATA%\MonopolyTycoon'

#### 4.3.2.3 Details

This directory is NOT created by the installer. It is created by the application on its first run to ensure proper user permissions.

#### 4.3.2.4 Requirement Id

REQ-1-100

# 5.0.0.0 Update Process

| Property | Value |
|----------|-------|
| Method | Full Reinstallation via a new installer package. |
| Logic | The new installer will detect an existing installa... |
| User Data Handling | The update process must NOT modify or delete the u... |

# 6.0.0.0 Uninstallation Process

## 6.1.0.0 Method

Accessible via Windows 'Add or remove programs' feature.

## 6.2.0.0 File Removal

The uninstaller will completely remove all files and subdirectories from the application's installation directory, along with any registry keys created during installation.

## 6.3.0.0 User Data Handling

| Property | Value |
|----------|-------|
| Behavior | The uninstaller must present a clear, explicit dia... |
| Option Keep | If the user chooses to keep their data, the '%APPD... |
| Option Delete | If the user confirms deletion, the '%APPDATA%\Mono... |
| Requirement Id | REQ-1-100 |

# 7.0.0.0 Infrastructure Requirements

## 7.1.0.0 Compute

### 7.1.1.0 Component

End-User PC

### 7.1.2.0 Type

🔹 Compute

### 7.1.3.0 Details

No cloud or server infrastructure is required. The entire deployment runs on the end-user's local machine, which must meet the minimum system requirements specified in REQ-1-013.

## 7.2.0.0 Storage/Web

### 7.2.1.0 Component

Distribution Channel

### 7.2.2.0 Type

🔹 Storage/Web

### 7.2.3.0 Details

A simple, publicly accessible web server or digital distribution platform is required to host the installer executable for download. This is referenced by the in-game update notification link (REQ-1-097).

# 8.0.0.0 Project Specific Configuration

## 8.1.0.0 Inno Setup Script

### 8.1.1.0 Section

#### 8.1.1.1 Section

[Setup]

#### 8.1.1.2 Configuration

AppName=Monopoly Tycoon, AppVersion=1.0, DefaultDirName={autopf}\Monopoly Tycoon, OutputBaseFilename=MonopolyTycoon_Setup_v1_0, SignTool=MySignTool

### 8.1.2.0 Section

#### 8.1.2.1 Section

[Tasks]

#### 8.1.2.2 Configuration

Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}";

### 8.1.3.0 Section

#### 8.1.3.1 Section

[Code]

#### 8.1.3.2 Configuration

Implement a custom function for the uninstaller to prompt for user data deletion (`TNewCheckBox`) and perform the directory removal if checked, satisfying REQ-1-100.

