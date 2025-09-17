# 1 Technologies

## 1.1 tech-unity

### 1.1.1 Id

tech-unity

### 1.1.2 Name

Unity Engine

### 1.1.3 Version

2022.3.x (LTS)

### 1.1.4 Category

🔹 Game Engine

### 1.1.5 Features

- High-quality 3D rendering pipeline
- Integrated development environment for game assets and logic
- C# scripting runtime
- Cross-platform build support (used for Windows target)

### 1.1.6 Requirements

- REQ-1-011
- REQ-1-002
- REQ-1-005
- REQ-1-017

### 1.1.7 Configuration

| Property | Value |
|----------|-------|
| Scripting Backend | Mono |
| Graphics Api | DirectX 11/12 |
| Target Platform | Windows Standalone (x64) |

### 1.1.8 License

#### 1.1.8.1 Type

🔹 Unity Personal

#### 1.1.8.2 Cost

Free (subject to revenue/funding limits)

## 1.2.0.0 tech-csharp

### 1.2.1.0 Id

tech-csharp

### 1.2.2.0 Name

C#

### 1.2.3.0 Version

12

### 1.2.4.0 Category

🔹 Programming Language

### 1.2.5.0 Features

- Strongly-typed, object-oriented language
- Primary language for Unity scripting and .NET development
- Modern language features (LINQ, async/await)
- Rich ecosystem and tooling support

### 1.2.6.0 Requirements

- REQ-1-011
- REQ-1-009

### 1.2.7.0 Configuration

#### 1.2.7.1 Language Version

latest

#### 1.2.7.2 Coding Standard

Microsoft C# Coding Conventions (REQ-1-024)

### 1.2.8.0 License

#### 1.2.8.1 Type

🔹 MIT

#### 1.2.8.2 Cost

Free

## 1.3.0.0 tech-dotnet

### 1.3.1.0 Id

tech-dotnet

### 1.3.2.0 Name

.NET

### 1.3.3.0 Version

8 (LTS)

### 1.3.4.0 Category

🔹 Core Framework

### 1.3.5.0 Features

- High-performance, modern development platform
- Comprehensive Base Class Library (BCL)
- Used for core game logic, data persistence, and testing
- Long-Term Support (LTS) ensures stability and security updates

### 1.3.6.0 Requirements

- REQ-1-009

### 1.3.7.0 Configuration

*No data available*

### 1.3.8.0 License

#### 1.3.8.1 Type

🔹 MIT

#### 1.3.8.2 Cost

Free

## 1.4.0.0 tech-vs2022

### 1.4.1.0 Id

tech-vs2022

### 1.4.2.0 Name

Visual Studio 2022

### 1.4.3.0 Version

Latest Stable

### 1.4.4.0 Category

🔹 Development Tool

### 1.4.5.0 Features

- Powerful IDE for C# and .NET development
- Advanced debugging capabilities
- Seamless integration with Unity and Git
- Test runner for NUnit

### 1.4.6.0 Requirements

- REQ-1-024

### 1.4.7.0 Configuration

#### 1.4.7.1 Edition

Community (recommended)

#### 1.4.7.2 Extensions

- Visual Studio Tools for Unity

### 1.4.8.0 License

#### 1.4.8.1 Type

🔹 Community Edition

#### 1.4.8.2 Cost

Free (for individuals and small teams)

## 1.5.0.0 tech-serilog

### 1.5.1.0 Id

tech-serilog

### 1.5.2.0 Name

Serilog

### 1.5.3.0 Version

3.1.x

### 1.5.4.0 Category

🔹 Logging Library

### 1.5.5.0 Features

- Structured logging capabilities
- Extensible sink architecture for various outputs (e.g., File)
- Message template support for rich log events
- High performance and low overhead

### 1.5.6.0 Requirements

- REQ-1-018
- REQ-1-019
- REQ-1-021

### 1.5.7.0 Configuration

| Property | Value |
|----------|-------|
| Sinks | Serilog.Sinks.File (for rolling file logging), Ser... |
| Minimum Level | Debug |
| Configuration Source | In-code configuration |

### 1.5.8.0 License

#### 1.5.8.1 Type

🔹 Apache 2.0

#### 1.5.8.2 Cost

Free

## 1.6.0.0 tech-sqlite

### 1.6.1.0 Id

tech-sqlite

### 1.6.2.0 Name

SQLite

### 1.6.3.0 Version

3.x

### 1.6.4.0 Category

🔹 Embedded Database

### 1.6.5.0 Features

- Serverless, self-contained, file-based database engine
- Transactional ACID compliance
- Ideal for local, single-user data storage
- Small footprint

### 1.6.6.0 Requirements

- REQ-1-089
- REQ-1-091
- REQ-1-033

### 1.6.7.0 Configuration

#### 1.6.7.1 Database File Location

`%APPDATA%/MonopolyTycoon/` as per REQ-1-089

### 1.6.8.0 License

#### 1.6.8.1 Type

🔹 Public Domain

#### 1.6.8.2 Cost

Free

## 1.7.0.0 tech-ms-sqlite

### 1.7.1.0 Id

tech-ms-sqlite

### 1.7.2.0 Name

Microsoft.Data.Sqlite

### 1.7.3.0 Version

8.0.x

### 1.7.4.0 Category

🔹 Data Access Library

### 1.7.5.0 Features

- Lightweight ADO.NET provider for SQLite
- Maintained by Microsoft, ensuring compatibility with .NET 8
- Provides a simple API for database interaction

### 1.7.6.0 Requirements

- REQ-1-089

### 1.7.7.0 Configuration

*No data available*

### 1.7.8.0 License

#### 1.7.8.1 Type

🔹 MIT

#### 1.7.8.2 Cost

Free

## 1.8.0.0 tech-nunit

### 1.8.1.0 Id

tech-nunit

### 1.8.2.0 Name

NUnit

### 1.8.3.0 Version

4.x

### 1.8.4.0 Category

🔹 Testing Framework

### 1.8.5.0 Features

- Popular, feature-rich unit testing framework for .NET
- Attribute-based test definitions
- Rich set of assertions
- Integrates with Visual Studio Test Explorer and Unity Test Framework

### 1.8.6.0 Requirements

- REQ-1-025

### 1.8.7.0 Configuration

#### 1.8.7.1 Adapter

NUnit3TestAdapter (for Visual Studio integration)

### 1.8.8.0 License

#### 1.8.8.1 Type

🔹 MIT

#### 1.8.8.2 Cost

Free

## 1.9.0.0 tech-system-text-json

### 1.9.1.0 Id

tech-system-text-json

### 1.9.2.0 Name

System.Text.Json

### 1.9.3.0 Version

8.0.x

### 1.9.4.0 Category

🔹 Serialization Library

### 1.9.5.0 Features

- High-performance JSON serialization and deserialization
- Built into the .NET framework
- Strongly integrated with modern C# features

### 1.9.6.0 Requirements

- REQ-1-087
- REQ-1-063
- REQ-1-084

### 1.9.7.0 Configuration

#### 1.9.7.1 Options

IncludeFields=true, WriteIndented=true (for debug builds)

### 1.9.8.0 License

#### 1.9.8.1 Type

🔹 MIT

#### 1.9.8.2 Cost

Free

## 1.10.0.0 tech-pandabt

### 1.10.1.0 Id

tech-pandabt

### 1.10.2.0 Name

Panda BT

### 1.10.3.0 Version

Latest Stable

### 1.10.4.0 Category

🔹 AI Library

### 1.10.5.0 Features

- Open-source Behavior Tree framework for Unity
- Allows defining AI logic in C# scripts
- Provides runtime debugging and visualization
- Lightweight and easy to integrate

### 1.10.6.0 Requirements

- REQ-1-063
- REQ-1-062

### 1.10.7.0 Configuration

*No data available*

### 1.10.8.0 License

#### 1.10.8.1 Type

🔹 MIT

#### 1.10.8.2 Cost

Free

## 1.11.0.0 tech-inno-setup

### 1.11.1.0 Id

tech-inno-setup

### 1.11.2.0 Name

Inno Setup

### 1.11.3.0 Version

6.2.x

### 1.11.4.0 Category

🔹 Installer Tool

### 1.11.5.0 Features

- Script-based creation of application installers for Windows
- Highly customizable user interface and installation steps
- Supports creation of desktop shortcuts and user-selectable directories
- Generates a single, standalone executable installer

### 1.11.6.0 Requirements

- REQ-1-012
- REQ-1-100

### 1.11.7.0 Configuration

*No data available*

### 1.11.8.0 License

#### 1.11.8.1 Type

🔹 Inno Setup License

#### 1.11.8.2 Cost

Free (for commercial and non-commercial use)

## 1.12.0.0 tech-git

### 1.12.1.0 Id

tech-git

### 1.12.2.0 Name

Git

### 1.12.3.0 Version

Latest Stable

### 1.12.4.0 Category

🔹 Version Control

### 1.12.5.0 Features

- Distributed version control system
- Industry standard for source code management
- Enables branching, merging, and collaboration
- Integrates with Visual Studio and Unity

### 1.12.6.0 Requirements

- REQ-1-024

### 1.12.7.0 Configuration

*No data available*

### 1.12.8.0 License

#### 1.12.8.1 Type

🔹 GPLv2

#### 1.12.8.2 Cost

Free

# 2.0.0.0 Configuration

| Property | Value |
|----------|-------|
| Summary | The technology stack is a monolithic architecture ... |
| Version Compatibility | All selected versions are compatible. .NET 8 is fu... |
| Long Term Viability | The stack is built on Long-Term Support (LTS) vers... |

