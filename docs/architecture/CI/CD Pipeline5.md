# 1 Pipelines

- {'id': 'pipeline-monopoly-tycoon-release', 'name': 'Monopoly Tycoon - Windows Build & Release', 'description': 'Builds, tests, and packages the Monopoly Tycoon Windows game client, culminating in a signed installer. This pipeline is triggered on merge to the main branch.', 'stages': [{'name': 'Code Validation & Unit Testing', 'steps': ['dotnet restore MonopolyTycoon.sln', 'dotnet build --no-restore', 'dotnet test --no-build --collect:"XPlat Code Coverage" --results-directory ./TestResults/'], 'environment': {'DOTNET_VERSION': '8.0'}, 'qualityGates': [{'name': 'Unit Test Coverage', 'criteria': ['coverage >= 70%'], 'blocking': True}]}, {'name': 'Build Game Client', 'steps': ['Unity -batchmode -quit -projectPath . -buildTarget Win64 -logFile ./unity_build.log -executeMethod CIBuilder.PerformWindowsBuild'], 'environment': {'UNITY_VERSION': '2022.3.x', 'BUILD_OUTPUT_PATH': './Build/Windows/'}, 'qualityGates': [{'name': 'Unity Build Success', 'criteria': ['Exit code is 0'], 'blocking': True}]}, {'name': 'Package Installer & Archive Artifacts', 'steps': ['iscc "InstallerScript.iss"', 'archive-artifact -path ./Output/MonopolyTycoon-Setup.exe', 'archive-artifact -path ./Build/Windows/Symbols/'], 'environment': {'ARTIFACT_VERSION': '1.0.${{BUILD_NUMBER}}'}, 'qualityGates': []}, {'name': 'Manual Release Approval', 'steps': ['wait-for-approval -approver "Project Lead" -timeout 7d -instructions "Perform full manual regression testing on the installer artifact from the previous stage. Approve to publish."'], 'environment': {}, 'qualityGates': [{'name': 'Go/No-Go Review', 'criteria': ['Manual sign-off from Project Lead based on full regression test results per REQ-1-101 and REQ-1-102'], 'blocking': True}]}, {'name': 'Publish Release', 'steps': ['git tag -a v1.0.${{BUILD_NUMBER}} -m "Release v1.0.${{BUILD_NUMBER}}"', 'git push origin v1.0.${{BUILD_NUMBER}}', 'upload-to-distribution -file ./Output/MonopolyTycoon-Setup.exe'], 'environment': {}, 'qualityGates': []}]}

# 2 Configuration

| Property | Value |
|----------|-------|
| Artifact Repository | InternalBuildServer/MonopolyTycoon |
| Default Branch | main |
| Retention Policy | 90d |
| Notification Channel | email:project-lead@monopoly.dev |

