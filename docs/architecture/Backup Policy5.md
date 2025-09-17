# 1 System Overview

## 1.1 Analysis Date

2025-06-13

## 1.2 Technology Stack

- Unity Engine
- C#
- .NET 8
- NUnit
- Inno Setup
- Git

## 1.3 Architecture Patterns

- Monolithic Desktop Application

## 1.4 Scope

Design of CI, Release Candidate, and final Release pipelines for a Windows standalone game, focusing on build, test, and package automation as per the Software Requirements Specification.

# 2.0 Pipelines

## 2.1 ci-pipeline

### 2.1.1 Id

ci-pipeline

### 2.1.2 Name

CI Pipeline (Pull Request Validation)

### 2.1.3 Description

Provides rapid feedback on feature branches and pull requests by performing essential code compilation, quality checks, and unit testing. This pipeline is optimized for speed to not block developers.

### 2.1.4 Trigger

#### 2.1.4.1 Type

🔹 code-commit

#### 2.1.4.2 Branches

- feature/*
- bugfix/*
- hotfix/*

#### 2.1.4.3 Events

- pull_request_opened
- pull_request_synchronized

### 2.1.5.0 Stages

#### 2.1.5.1 Setup Environment

##### 2.1.5.1.1 Name

Setup Environment

##### 2.1.5.1.2 Steps

###### 2.1.5.1.2.1 Checkout Code

####### 2.1.5.1.2.1.1 Name

Checkout Code

####### 2.1.5.1.2.1.2 Description

Clones the specific branch or pull request source from the Git repository.

####### 2.1.5.1.2.1.3 Tool

Git

###### 2.1.5.1.2.2.0 Install Build Agent Dependencies

####### 2.1.5.1.2.2.1 Name

Install Build Agent Dependencies

####### 2.1.5.1.2.2.2 Description

Ensures the build agent has the required .NET 8 SDK and Unity Engine version installed.

####### 2.1.5.1.2.2.3 Tool

Build Agent Provisioning

#### 2.1.5.2.0.0.0 Build & Analyze

##### 2.1.5.2.1.0.0 Name

Build & Analyze

##### 2.1.5.2.2.0.0 Steps

###### 2.1.5.2.2.1.0 Restore Dependencies

####### 2.1.5.2.2.1.1 Name

Restore Dependencies

####### 2.1.5.2.2.1.2 Description

Restores all required NuGet packages for the .NET solution.

####### 2.1.5.2.2.1.3 Tool

dotnet CLI

###### 2.1.5.2.2.2.0 Compile Core Logic

####### 2.1.5.2.2.2.1 Name

Compile Core Logic

####### 2.1.5.2.2.2.2 Description

Compiles the C# solution containing the core game logic and tests.

####### 2.1.5.2.2.2.3 Tool

dotnet CLI

###### 2.1.5.2.2.3.0 Static Code Analysis

####### 2.1.5.2.2.3.1 Name

Static Code Analysis

####### 2.1.5.2.2.3.2 Description

Scans C# code to enforce Microsoft C# Coding Conventions (REQ-1-024). Fails the build on critical issues.

####### 2.1.5.2.2.3.3 Tool

Roslyn Analyzer / SonarScanner

###### 2.1.5.2.2.4.0 Dependency Vulnerability Scan

####### 2.1.5.2.2.4.1 Name

Dependency Vulnerability Scan

####### 2.1.5.2.2.4.2 Description

Scans NuGet packages for known security vulnerabilities. Fails the build on high or critical severity findings.

####### 2.1.5.2.2.4.3 Tool

OWASP Dependency-Check / Snyk

#### 2.1.5.3.0.0.0 Test & Verify

##### 2.1.5.3.1.0.0 Name

Test & Verify

##### 2.1.5.3.2.0.0 Steps

###### 2.1.5.3.2.1.0 Run Unit Tests

####### 2.1.5.3.2.1.1 Name

Run Unit Tests

####### 2.1.5.3.2.1.2 Description

Executes all unit tests using the NUnit framework as required by REQ-1-025.

####### 2.1.5.3.2.1.3 Tool

dotnet test CLI

###### 2.1.5.3.2.2.0 Calculate & Publish Code Coverage

####### 2.1.5.3.2.2.1 Name

Calculate & Publish Code Coverage

####### 2.1.5.3.2.2.2 Description

Generates a code coverage report from the unit test run. This data is used by a quality gate.

####### 2.1.5.3.2.2.3 Tool

Coverlet

###### 2.1.5.3.2.3.0 Publish Test Results

####### 2.1.5.3.2.3.1 Name

Publish Test Results

####### 2.1.5.3.2.3.2 Description

Uploads unit test results in a standard format (e.g., JUnit XML) for review in the CI/CD system UI.

####### 2.1.5.3.2.3.3 Tool

CI/CD Platform Plugin

### 2.1.6.0.0.0.0 Quality Gates

#### 2.1.6.1.0.0.0 Code Compilation Gate

##### 2.1.6.1.1.0.0 Name

Code Compilation Gate

##### 2.1.6.1.2.0.0 Description

The C# solution must compile successfully.

##### 2.1.6.1.3.0.0 Metric

Build Status

##### 2.1.6.1.4.0.0 Threshold

SUCCESS

#### 2.1.6.2.0.0.0 Unit Test Gate

##### 2.1.6.2.1.0.0 Name

Unit Test Gate

##### 2.1.6.2.2.0.0 Description

100% of unit tests must pass.

##### 2.1.6.2.3.0.0 Metric

Test Pass Rate

##### 2.1.6.2.4.0.0 Threshold

100%

#### 2.1.6.3.0.0.0 Code Coverage Gate

##### 2.1.6.3.1.0.0 Name

Code Coverage Gate

##### 2.1.6.3.2.0.0 Description

Unit test coverage for core game logic must meet or exceed the requirement from REQ-1-025.

##### 2.1.6.3.3.0.0 Metric

Line Coverage

##### 2.1.6.3.4.0.0 Threshold

>= 70%

#### 2.1.6.4.0.0.0 Code Quality Gate

##### 2.1.6.4.1.0.0 Name

Code Quality Gate

##### 2.1.6.4.2.0.0 Description

Static analysis must report zero critical or blocker-level issues.

##### 2.1.6.4.3.0.0 Metric

Static Analysis Issues

##### 2.1.6.4.4.0.0 Threshold

0

#### 2.1.6.5.0.0.0 Security Gate

##### 2.1.6.5.1.0.0 Name

Security Gate

##### 2.1.6.5.2.0.0 Description

Dependency vulnerability scan must report zero critical or high-severity vulnerabilities.

##### 2.1.6.5.3.0.0 Metric

Vulnerability Count

##### 2.1.6.5.4.0.0 Threshold

0

## 2.2.0.0.0.0.0 rc-pipeline

### 2.2.1.0.0.0.0 Id

rc-pipeline

### 2.2.2.0.0.0.0 Name

Release Candidate Pipeline (Main Branch)

### 2.2.3.0.0.0.0 Description

Builds a complete, installable, and testable version of the game upon every merge to the main branch. The output is a release candidate artifact.

### 2.2.4.0.0.0.0 Trigger

#### 2.2.4.1.0.0.0 Type

🔹 code-commit

#### 2.2.4.2.0.0.0 Branches

- main

#### 2.2.4.3.0.0.0 Events

- push

### 2.2.5.0.0.0.0 Stages

#### 2.2.5.1.0.0.0 Run CI Checks

##### 2.2.5.1.1.0.0 Name

Run CI Checks

##### 2.2.5.1.2.0.0 Steps

- {'name': 'Execute CI Pipeline', 'description': 'Runs all stages from the CI Pipeline to ensure the main branch meets all quality standards before proceeding.', 'tool': 'Pipeline Orchestrator'}

#### 2.2.5.2.0.0.0 Integration Test

##### 2.2.5.2.1.0.0 Name

Integration Test

##### 2.2.5.2.2.0.0 Steps

###### 2.2.5.2.2.1.0 Run Integration Tests

####### 2.2.5.2.2.1.1 Name

Run Integration Tests

####### 2.2.5.2.2.1.2 Description

Executes end-to-end tests covering key workflows like save/load and bankruptcy cycle, as per REQ-1-026. Uses predefined test data files from REQ-1-027.

####### 2.2.5.2.2.1.3 Tool

dotnet test CLI

###### 2.2.5.2.2.2.0 Publish Integration Test Results

####### 2.2.5.2.2.2.1 Name

Publish Integration Test Results

####### 2.2.5.2.2.2.2 Description

Uploads integration test results for review.

####### 2.2.5.2.2.2.3 Tool

CI/CD Platform Plugin

#### 2.2.5.3.0.0.0 Package Application

##### 2.2.5.3.1.0.0 Name

Package Application

##### 2.2.5.3.2.0.0 Steps

###### 2.2.5.3.2.1.0 Build Unity Project

####### 2.2.5.3.2.1.1 Name

Build Unity Project

####### 2.2.5.3.2.1.2 Description

Builds the Unity project into a standalone Windows x64 executable.

####### 2.2.5.3.2.1.3 Tool

Unity Engine (CLI)

###### 2.2.5.3.2.2.0 Create Installer

####### 2.2.5.3.2.2.1 Name

Create Installer

####### 2.2.5.3.2.2.2 Description

Runs an Inno Setup script to package the game build, assets, and dependencies into a single installer executable, fulfilling REQ-1-012.

####### 2.2.5.3.2.2.3 Tool

Inno Setup Compiler

#### 2.2.5.4.0.0.0 Publish Artifact

##### 2.2.5.4.1.0.0 Name

Publish Artifact

##### 2.2.5.4.2.0.0 Steps

###### 2.2.5.4.2.1.0 Version Artifact

####### 2.2.5.4.2.1.1 Name

Version Artifact

####### 2.2.5.4.2.1.2 Description

Versions the installer using a semantic versioning scheme with a pre-release tag (e.g., MonopolyTycoon-1.2.0-rc.123.exe).

####### 2.2.5.4.2.1.3 Tool

Custom Script

###### 2.2.5.4.2.2.0 Upload to Repository

####### 2.2.5.4.2.2.1 Name

Upload to Repository

####### 2.2.5.4.2.2.2 Description

Publishes the versioned installer to an artifact repository for QA testing and potential promotion to a final release.

####### 2.2.5.4.2.2.3 Tool

Artifact Repository CLI (e.g., GitHub CLI, JFrog CLI)

### 2.2.6.0.0.0.0 Quality Gates

#### 2.2.6.1.0.0.0 CI Pipeline Success Gate

##### 2.2.6.1.1.0.0 Name

CI Pipeline Success Gate

##### 2.2.6.1.2.0.0 Description

The integrated CI pipeline run must pass all its quality gates.

##### 2.2.6.1.3.0.0 Metric

Pipeline Status

##### 2.2.6.1.4.0.0 Threshold

SUCCESS

#### 2.2.6.2.0.0.0 Integration Test Gate

##### 2.2.6.2.1.0.0 Name

Integration Test Gate

##### 2.2.6.2.2.0.0 Description

100% of integration tests must pass.

##### 2.2.6.2.3.0.0 Metric

Test Pass Rate

##### 2.2.6.2.4.0.0 Threshold

100%

#### 2.2.6.3.0.0.0 Packaging Gate

##### 2.2.6.3.1.0.0 Name

Packaging Gate

##### 2.2.6.3.2.0.0 Description

The Unity build and Inno Setup packaging process must complete successfully.

##### 2.2.6.3.3.0.0 Metric

Build Status

##### 2.2.6.3.4.0.0 Threshold

SUCCESS

## 2.3.0.0.0.0.0 release-pipeline

### 2.3.1.0.0.0.0 Id

release-pipeline

### 2.3.2.0.0.0.0 Name

Official Release Pipeline

### 2.3.3.0.0.0.0 Description

Promotes a validated Release Candidate artifact to an official release. This pipeline is manually triggered and includes a formal approval step.

### 2.3.4.0.0.0.0 Trigger

#### 2.3.4.1.0.0.0 Type

🔹 manual

#### 2.3.4.2.0.0.0 Branches

*No items available*

#### 2.3.4.3.0.0.0 Events

- git_tag_creation (e.g., v1.2.0)

### 2.3.5.0.0.0.0 Stages

#### 2.3.5.1.0.0.0 Release Approval

##### 2.3.5.1.1.0.0 Name

Release Approval

##### 2.3.5.1.2.0.0 Steps

- {'name': 'Project Lead Go/No-Go', 'description': 'A manual intervention gate requiring explicit approval from the Project Lead before proceeding, as specified in REQ-13.5.', 'tool': 'CI/CD Platform Approval UI'}

#### 2.3.5.2.0.0.0 Prepare Release Artifact

##### 2.3.5.2.1.0.0 Name

Prepare Release Artifact

##### 2.3.5.2.2.0.0 Steps

###### 2.3.5.2.2.1.0 Download Release Candidate

####### 2.3.5.2.2.1.1 Name

Download Release Candidate

####### 2.3.5.2.2.1.2 Description

Fetches the specific installer artifact from the repository that corresponds to the Git tag that triggered the release.

####### 2.3.5.2.2.1.3 Tool

Artifact Repository CLI

###### 2.3.5.2.2.2.0 Code Sign Installer

####### 2.3.5.2.2.2.1 Name

Code Sign Installer

####### 2.3.5.2.2.2.2 Description

Applies a digital signature to the installer executable to ensure authenticity and comply with Windows desktop application best practices (REQ-1-100).

####### 2.3.5.2.2.2.3 Tool

Windows SignTool

#### 2.3.5.3.0.0.0 Publish

##### 2.3.5.3.1.0.0 Name

Publish

##### 2.3.5.3.2.0.0 Steps

###### 2.3.5.3.2.1.0 Finalize Versioning

####### 2.3.5.3.2.1.1 Name

Finalize Versioning

####### 2.3.5.3.2.1.2 Description

Renames the artifact to remove the pre-release tag, resulting in the final version (e.g., MonopolyTycoon-1.2.0.exe).

####### 2.3.5.3.2.1.3 Tool

Custom Script

###### 2.3.5.3.2.2.0 Publish to Distribution Channel

####### 2.3.5.3.2.2.1 Name

Publish to Distribution Channel

####### 2.3.5.3.2.2.2 Description

Uploads the final, signed installer to the official public download location, fulfilling the cutover plan from REQ-12.4.

####### 2.3.5.3.2.2.3 Tool

Distribution Platform CLI (e.g., GitHub Releases)

### 2.3.6.0.0.0.0 Quality Gates

- {'name': 'Manual Approval Gate', 'description': 'The release must be explicitly approved by the designated stakeholder.', 'metric': 'Approval Status', 'threshold': 'APPROVED'}

# 3.0.0.0.0.0.0 Artifact Management

## 3.1.0.0.0.0.0 Repository

GitHub Releases or JFrog Artifactory

## 3.2.0.0.0.0.0 Versioning Strategy

Semantic Versioning (Major.Minor.Patch)

## 3.3.0.0.0.0.0 Artifact Handling

Artifacts are immutable. Release Candidate artifacts are tagged with '-rc.<build_number>'. Release artifacts have no pre-release tag.

## 3.4.0.0.0.0.0 Retention Policy

| Property | Value |
|----------|-------|
| Release Candidates | Retain for 30 days. |
| Official Releases | Retain indefinitely. |
| Ci Builds | Artifacts are not generated or retained. |

# 4.0.0.0.0.0.0 Rollback Strategy

## 4.1.0.0.0.0.0 Approach

Re-release of a Previous Version

## 4.2.0.0.0.0.0 Description

As a desktop application, rollback is managed by the end-user. The CI/CD system's role is to ensure all previously released, stable installer versions are retained in the artifact repository and are accessible for re-publication, as outlined in the fallback plan (REQ-12.4).

