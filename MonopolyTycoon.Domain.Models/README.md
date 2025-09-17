# MonopolyTycoon.Domain.Models

This foundational repository contains the Plain Old C# Object (POCO) data models that form the canonical representation of the game state. It was extracted from the original `MonopolyTycoon.Domain` repository to serve as a shared, dependency-free contract library.

## Purpose

This project includes core entities like `GameState`, `PlayerState`, and property definitions, as specified in REQ-1-041 and REQ-1-031. By isolating these models, any other component in the system can reference the core data structures without inheriting the entire domain's business logic.

## Architectural Role

This separation is critical for decoupling, allowing the rule engine, application services, and persistence layers to operate on a common, stable set of objects. It has **zero dependencies** on other project code, maximizing its reusability and establishing a stable foundation for the entire solution's architecture.

This library acts as the "Shared Kernel" or "Contracts" layer in a Clean Architecture approach, providing the data structures used by all other layers.

## Technology

- **Framework**: .NET 8
- **Language**: C# 12
- **Project Type**: .NET Class Library