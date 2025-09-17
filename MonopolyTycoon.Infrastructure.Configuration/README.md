# Monopoly Tycoon

This repository contains the source code for the "Monopoly Tycoon" digital board game, a single-player, Windows-exclusive game developed using .NET 8 and Unity.

## Solution Structure

The solution follows the principles of **Clean Architecture** to ensure a clear separation of concerns, maintainability, and testability. The projects are organized into distinct layers:

-   **`src/Core`**: Contains the core business logic and domain entities (Domain and Application layers).
    -   `MonopolyTycoon.Domain`: Entities, aggregates, and domain services.
    -   `MonopolyTycoon.Application`: Application services, use cases, DTOs, and interfaces.
-   **`src/Infrastructure`**: Contains implementations for external concerns like data persistence, file access, and logging.
    -   `MonopolyTycoon.Infrastructure.Configuration`: A reusable library for loading configuration from external files (e.g., JSON).
    -   (Other infrastructure projects for persistence, logging, etc.)
-   **`src/Presentation`**: The Unity game client.
    -   (Unity project will be located here)
-   **`tests`**: Contains all unit and integration tests for the solution, mirroring the `src` structure.

## Getting Started

### Prerequisites

-   [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
-   [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) with the ".NET desktop development" workload installed.

### Building the Solution

1.  Clone the repository:
    ```sh
    git clone https://github.com/user/monopoly-tycoon.git
    cd monopoly-tycoon
    ```

2.  Restore NuGet packages:
    ```sh
    dotnet restore
    ```

3.  Build the solution:
    ```sh
    dotnet build --configuration Release
    ```

### Running Tests

To run all the tests from the command line:

```sh
dotnet test
```

This will execute all tests in the solution and generate a code coverage report.

## CI/CD

This repository is configured with CI/CD pipelines for both GitHub Actions (`.github/workflows/dotnet.yml`) and Azure DevOps (`azure-pipelines.yml`). These pipelines automatically build the solution and run all tests on every push to the `main` branch.