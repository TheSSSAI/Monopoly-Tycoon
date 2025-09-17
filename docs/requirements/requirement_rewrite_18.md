# Monopoly Tycoon - Software Requirements Specification

### 1.0 Introduction

#### 1.1 Project Vision
To develop a high-quality, single-player digital adaptation of the classic Monopoly board game, named "Monopoly Tycoon," for the Windows platform. The game will pit a human player against challenging and engaging AI opponents, featuring polished 3D graphics and strict adherence to the official game rules.

#### 1.2 Core Objectives
*   **Authenticity:** Replicate the standard Monopoly gameplay mechanics and rules with high fidelity.
*   **Challenge:** Implement a tunable, algorithmic AI that provides a compelling experience for a human player of varying skill levels.
*   **Visual Appeal:** Deliver a modern, visually immersive experience using high-quality 3D graphics and an isometric perspective.
*   **Replayability:** Encourage repeated playthroughs via features like a persistent player profile with statistics, variable AI difficulty, and a dynamic theme system.

#### 1.3 Scope

##### 1.3.1 In-Scope Features:
*   Full implementation of standard Monopoly rules for one human player against 1 to 3 AI opponents.
*   Player profile with persistent historical statistics.
*   Core mechanics: property acquisition, auctions, rent, building, mortgages, trading.
*   All standard board spaces and card decks (Chance, Community Chest).
*   Tunable AI difficulty levels for each opponent.
*   Save/Load game functionality with multiple slots.
*   Persistent high score tracking for human player victories.
*   Visual and audio theme customization system.
*   Offline, single-player (human vs. AI) gameplay exclusively.

##### 1.3.2 Out-of-Scope Features:
*   Any form of online or local multiplayer (human vs. human).
*   House rules or non-standard game variations.
*   In-game purchases or microtransactions.
*   Network connectivity for any purpose.
*   Support for platforms other than Windows OS.

### 2.0 System and Technical Requirements

#### 2.1 Target Platform and Technology
*   The game shall be developed using the .NET 8 framework.
*   The game shall be playable on the Windows OS.
*   The game shall be developed using the Unity game engine with C# scripting to meet the 3D graphics, visual polish, and cross-component integration requirements.

#### 2.2 Installation and Distribution
*   The game shall be distributed via a standalone installer package created using Inno Setup.
*   The installation process shall be simple, requiring minimal user intervention and providing options for installation directory and desktop shortcut creation.

#### 2.3 System Requirements
*   **Minimum System Requirements:**
    *   OS: Windows 10 (64-bit)
    *   Processor: Dual-core CPU @ 2.4 GHz
    *   Memory: 4 GB RAM
    *   Graphics: DirectX 11 compatible GPU with 1 GB VRAM
    *   Storage: 2 GB available space
*   **Recommended System Requirements:**
    *   OS: Windows 11 (64-bit)
    *   Processor: Quad-core CPU @ 3.0 GHz
    *   Memory: 8 GB RAM
    *   Graphics: DirectX 12 compatible GPU with 4 GB VRAM
    *   Storage: 2 GB available space

#### 2.4 Performance Requirements
*   The game must maintain a stable 60 frames per second (FPS), with no drops below 45 FPS, at 1920x1080 resolution under typical gameplay conditions on a system meeting the recommended hardware specifications.
*   Asset loading times when starting a new game or loading a save file from the main menu shall not exceed 10 seconds on a system with an SSD meeting recommended specifications.

#### 2.5 Graphics and Visuals
*   The game shall feature high-quality 3D graphics with modern visual fidelity and artistic polish, aiming for an immersive and visually appealing experience.
*   The game shall utilize an isometric view for gameplay.
*   The UI and game view shall be scalable to support common aspect ratios (e.g., 16:9, 16:10, 21:9) without visual artifacts or loss of functionality.
*   Interactive graphical elements shall be implemented to enhance user experience (e.g., animated dice, moving player tokens, property highlights, visual effects for transactions).

#### 2.6 Operational Requirements
*   **Logging:** The game shall implement the Serilog framework for structured, configurable logging.
    *   Logs shall capture key game events (INFO), AI decisions (DEBUG), and errors (ERROR) to a local file in a structured JSON format.
    *   The log file shall be stored in a user-accessible location (`%APPDATA%/MonopolyTycoon/logs`).
    *   The logging system shall use a rolling file mechanism, retaining the last 7 days of logs or a maximum of 50 MB of log files, whichever is smaller, to prevent excessive disk usage.
    *   No personally identifiable information (PII), other than the user-provided profile name, shall be written to logs.
*   **Error Handling:** The game shall feature a user-friendly error dialog for unhandled exceptions. This dialog will inform the user of the error, provide a unique error ID that correlates to a specific log entry, and give instructions on how to locate the log file for support purposes.

#### 2.7 Development and Quality Assurance
*   **Version Control:** The project source code shall be managed using the Git version control system.
*   **IDE:** Development shall be conducted using Visual Studio 2022.
*   **Code Style:** All C# code shall adhere to the Microsoft C# Coding Conventions to ensure consistency and maintainability.
*   **Unit Testing:** The core game logic (rule engine, economic model, AI decision stubs) shall have a minimum of 70% unit test coverage using the NUnit framework.
*   **Integration Testing:** Key end-to-end workflows, including the save/load process, trade negotiation sequences, and the bankruptcy-to-asset-transfer cycle, shall be covered by integration tests.
*   **Test Data Management:** A suite of predefined game state files shall be created and maintained to facilitate testing of specific edge cases and complex scenarios (e.g., a game state on the verge of bankruptcy, a housing shortage scenario).

#### 2.8 Auditability
*   Key economic transactions (e.g., property purchase, rent payment, trade completion, mortgage actions) must be logged at the INFO level.
*   Each logged transaction must include the turn number, the involved player IDs, the type of transaction, and relevant values (e.g., cash amount, property IDs) to create a verifiable audit trail for debugging and analysis.

### 3.0 Game Setup and Player Management

#### 3.1 Player Configuration
*   The game shall support a total of 2 to 4 players, consisting of one human player and 1 to 3 computer-controlled (AI) opponents.
*   The game setup screen shall allow the human player to configure the number of AI opponents (1, 2, or 3) and set the difficulty level (Easy, Medium, Hard) for each individual AI opponent.

#### 3.2 Player State Object
*   Each player, whether human or AI, shall be represented by a data object containing:
    *   `player_id`: Unique identifier.
    *   `player_name`: Display name (e.g., user profile name or "AI 1").
    *   `is_human`: Boolean flag to distinguish the human player from AI.
    *   `ai_difficulty`: The assigned difficulty level (null for human player).
    *   `token_id`: Identifier for the selected visual game piece.
    *   `cash`: Current monetary balance.
    *   `properties_owned`: A list of owned property IDs.
    *   `current_position`: Integer representing the board space index (0-39).
    *   `get_out_of_jail_cards`: Integer count of cards held.
    *   `status`: An enum representing the player's current state (e.g., `Active`, `InJail`, `Bankrupt`).
    *   `jail_turns_remaining`: Integer count for turns in jail.

#### 3.3 Player Profile
*   The system shall support a simple profile for the human player, which includes a display name entered by the user. This name will be used in UI elements and in the Top Score History.
*   The system shall assign a persistent, unique `profile_id` for internal data tracking.
*   The display name input field shall be validated to have a length between 3 and 16 characters and disallow special characters that could interfere with file systems or display logic.

#### 3.4 Player Historical Statistics
*   The system shall track and persist historical gameplay statistics for the human player's profile, linked via `profile_id`. This includes, at a minimum:
    *   Total games played.
    *   Total wins.
    *   Win/Loss ratio.
    *   Average game duration.
    *   Most profitable property (by total rent collected).
    *   Total rent paid and collected.
    *   Total properties acquired.
    *   Largest trade deal (by combined value).

#### 3.5 Player Piece Selection
*   During game setup, the human player shall be able to select their visual game piece (token) from a predefined set of at least 8 classic options (e.g., Top Hat, Car, Thimble). AI opponents will be assigned the remaining pieces automatically and randomly.

### 4.0 Core Gameplay Mechanics

#### 4.1 Rule Set Adherence
*   The game logic shall strictly implement the official rules of the Monopoly board game as published by Hasbro (using the standard U.S. rulebook version as of 2024 as the baseline). All game events, player actions, and economic calculations must conform to this rule set.

#### 4.2 Game Board and Spaces
*   The game board shall accurately represent the standard 40 spaces, including:
    *   28 Properties: 22 colored properties, 4 Railroads, and 2 Utilities.
    *   3 Chance spaces.
    *   3 Community Chest spaces.
    *   2 Tax spaces (Income Tax, Luxury Tax).
    *   Special corner spaces: Go, Jail/Just Visiting, Free Parking, Go to Jail.

#### 4.3 Game Flow
*   The game shall proceed in a turn-based manner, cycling through the human player and all active AI players.
*   A turn sequence shall consist of:
    1.  **Pre-Turn Phase:** Handle start-of-turn events, such as jail options (pay fine, use card, attempt to roll doubles).
    2.  **Pre-Roll Management Phase:**
        *   **Human Player:** The game shall present the UI for property management (building, trading, mortgaging) and wait for the player to signal they are ready to roll.
        *   **AI Player:** The AI's logic will evaluate and perform any property management actions.
    3.  **Roll Phase:** Player rolls two six-sided dice. For the human player, this is initiated by a UI button click.
    4.  **Movement Phase:** Player's token moves clockwise around the board, triggering a 'Pass Go' event if applicable.
    5.  **Action Phase:** The game executes the action associated with the destination space.
        *   **Human Player:** If the action requires a decision (e.g., buying a property), the game shall display a UI prompt and wait for user input.
        *   **AI Player:** The AI's logic will make the decision automatically.
    6.  **Post-Roll Phase:** If the player rolled doubles, they take another turn (returning to Step 2). If it was their third consecutive double, they go to jail. Otherwise, the turn passes to the next player.

#### 4.4 Game State Management
*   The system shall maintain a comprehensive `GameState` object, which encapsulates the entire state of a match and is the primary subject of the save/load functionality. This object shall include:
    *   A list of all `PlayerState` objects.
    *   `board_state`: Ownership and development level (houses/hotel) of all properties.
    *   `bank_state`: Number of houses and hotels remaining in the bank.
    *   `deck_states`: The current order of cards in the Chance and Community Chest decks, including the discard pile.
    *   `game_metadata`: Current turn number, active player ID, and game session timestamp.

#### 4.5 Dice Mechanics
*   The game shall simulate the rolling of two six-sided dice using a cryptographically secure random number generator to ensure fairness.
*   The rule for rolling doubles (allowing an extra turn) and the consequence of rolling three consecutive doubles (going to Jail) shall be implemented.

#### 4.6 Special Space Rules
*   **Go:** Players passing or landing on 'Go' shall collect $200 from the Bank.
*   **Jail:** Players go to Jail by landing on 'Go to Jail', drawing a 'Go to Jail' card, or rolling three consecutive doubles. Players can get out by paying a $50 fine, using a 'Get Out of Jail Free' card, or rolling doubles on one of their next three turns. If they fail to roll doubles on the third turn, they must pay the $50 fine.
*   **Free Parking:** Landing on Free Parking shall have no effect on the player.
*   **Chance & Community Chest:** Landing on these spaces shall trigger a card draw from the top of the respective shuffled deck, and its instructions shall be executed. Used cards are placed at the bottom of the deck.
*   **Tax Spaces:** Players landing on Income Tax must choose to pay either a flat $200 or 10% of their total net worth. Total net worth is defined as cash on hand + the printed price of all owned properties + the purchase price of all buildings. Players landing on Luxury Tax must pay the fixed amount.

### 5.0 Property and Economic Management

#### 5.1 The Bank and Game Economy
*   Each player shall start with $1500, distributed according to the official rules.
*   The Bank shall manage all monetary transactions, including initial funds, property purchases, rent payments, taxes, building costs, and mortgage transactions.

#### 5.2 Property Acquisition
*   **Purchase Decision:** When a player lands on an unowned property:
    *   **Human Player:** A UI dialog shall be presented with options to 'Buy' the property for its list price or 'Auction' it.
    *   **AI Player:** Its decision-making algorithm shall determine whether to purchase it.
*   **Auction:** If a player declines to purchase, the property shall be immediately put up for auction. The auction starts with the player who declined the purchase. Bidding proceeds clockwise. The minimum starting bid is $1. Any player can bid, including the one who originally declined. The auction proceeds until no player is willing to increase the bid. The highest bidder pays the Bank and receives the property.

#### 5.3 Building and Development
*   **Monopoly Requirement:** A player may only build houses or hotels on properties belonging to a complete color set (a monopoly).
*   **Even Building Rule:** The system shall enforce the even building rule. A player cannot build a second house on a property until all other properties in that color set have one house. This rule applies up to four houses per property. Hotels can only be built after four houses are on each property in the set.
*   **Bank Supply:** The game shall manage a finite supply of 32 houses and 12 hotels. If a player wishes to build but the supply is exhausted, they cannot do so until another player sells buildings back to the Bank. The UI must clearly communicate when a building shortage prevents construction.
*   **Building Shortages:** If multiple players wish to buy the last available building(s), the buildings shall be sold via auction to the highest bidder, starting with the player whose turn it is.

#### 5.4 Mortgaging
*   A player can mortgage any undeveloped property to the Bank to receive its mortgage value. No rent can be collected on mortgaged properties.
*   To unmortgage a property, the player must pay the Bank the mortgage value plus 10% interest.

#### 5.5 Player Trading
*   **Human-Initiated Trades:** The human player shall be able to initiate a trade with any AI opponent via a dedicated trading UI at any point during their turn.
*   **AI-Initiated Trades:** AI players can propose trades to the human player. A UI dialog shall be presented to the human player with options to 'Accept', 'Decline', or 'Propose Counter-Offer'.
*   **AI-to-AI Trades:** AI shall be capable of proposing, evaluating, and responding to trade offers with other AI players based on its difficulty setting. These trades shall be announced to the human player via a non-intrusive UI notification.

#### 5.6 Rent Collection
*   Rent shall be automatically collected from a player landing on an owned property. Rent amounts shall vary based on the property, development level, and ownership of sets (for Railroads/Utilities).

### 6.0 AI and Difficulty

#### 6.1 Difficulty Levels
*   The game shall provide at least three distinct AI difficulty levels: Easy, Medium, and Hard.

#### 6.2 Tunable AI Behavior Parameters
*   The AI's behavior shall be controlled by a set of parameters tuned for each difficulty level, including:
    *   Property Purchase Threshold (based on property value and strategic importance)
    *   Building Strategy (when and where to build)
    *   Trading Aggressiveness and Logic (what constitutes a 'good' trade)
    *   Mortgage Management (when to mortgage/unmortgage)
    *   Risk Aversion (propensity to spend cash vs. save for rent)
*   The AI's decision-making logic shall be implemented using a Behavior Tree architecture to allow for modular and scalable strategy definition for each difficulty level.
*   AI behavior parameters for each difficulty level shall be stored in an external configuration file (JSON) to allow for tuning and balancing without requiring a full application rebuild.

#### 6.3 Difficulty Level Characteristics
*   **Easy:** AI will play sub-optimally. It may decline to buy properties needed for a monopoly, rarely initiate trades, and accept trades that are not in its favor.
*   **Medium:** AI will play competently. It will prioritize completing monopolies, make reasonable property purchase decisions, and propose and accept relatively fair trades.
*   **Hard:** AI will play strategically. It will aggressively pursue monopolies, use trades to block opponents, manage its cash flow efficiently to avoid bankruptcy, and attempt to cause housing shortages by building to four houses on key monopolies.

### 7.0 Game End Conditions

#### 7.1 Bankruptcy Condition
*   A player is declared bankrupt if they cannot cover a debt after selling all houses/hotels and mortgaging all properties.

#### 7.2 Asset Transfer
*   **Debt to Another Player:** If a player goes bankrupt to another player, the bankrupt player's entire portfolio (cash, properties, Get Out of Jail Free cards) is transferred to the creditor. Mortgaged properties remain mortgaged and the new owner must pay the 10% interest fee if they choose to unmortgage them later.
*   **Debt to Bank:** If a player goes bankrupt to the Bank, all their properties are returned to the Bank and immediately put up for auction, one by one, to the remaining players.

#### 7.3 Win and Lose Conditions
*   **Win Condition:** The human player is the last remaining participant, with all AI opponents having gone bankrupt. The system shall display a victory screen.
*   **Lose Condition:** The human player is declared bankrupt. The game shall display a game-over screen for the human player. The game will then continue to simulate the remaining AI players' turns at high speed until a single winner is determined to ensure accurate final game statistics are recorded.
*   **Game Summary Screen:** Following either a win or lose condition, a detailed game summary screen shall be displayed, showing key statistics for all players from that specific match (e.g., final net worth, properties owned, total rent collected).

### 8.0 User Interface and Experience

#### 8.1 Game Setup Screen
*   The system shall display a setup screen allowing the human player to enter a profile name, select a game piece, choose the number of AI opponents, and set the difficulty for each.

#### 8.2 Main Game HUD
*   The main game screen shall feature a persistent Heads-Up Display (HUD) showing at-a-glance information for all players, including name, token, cash, and a clear indicator for the current turn.
*   The game board shall visually indicate property ownership using color-coded markers or borders.

#### 8.3 Player Action Prompts
*   The system shall use modal dialogs for critical, turn-halting player decisions (e.g., buying property, responding to a trade).
*   The system shall use non-intrusive, temporary notifications for informational events (e.g., an AI-to-AI trade, another player paying rent).

#### 8.4 Property Management Interface
*   The game shall provide a dedicated, easily accessible interface for the human player to view their properties, build houses/hotels, manage mortgages, and initiate trades.

#### 8.5 Trading Interface
*   The system shall implement a clear, two-panel trading interface for proposing and receiving trades, allowing for the selection and exchange of multiple properties, cash, and 'Get Out of Jail Free' cards in a single offer.

#### 8.6 Game Settings
*   The game shall include a settings menu accessible during gameplay.
*   **8.6.1 Game Speed:** This menu shall feature a 'Game Speed' setting (e.g., Normal, Fast, Instant) that controls animation durations and AI decision delays. The selected game speed shall be temporarily overridden and set to 'Normal' during any auction involving the human player. The user-selected speed will resume after the auction concludes.
*   **8.6.2 Audio Settings:** The menu shall provide separate volume sliders for Master Volume, Music, and Sound Effects.
*   **8.6.3 Data Management Settings:** The settings menu shall provide options for the user to:
    *   Reset all historical player statistics and high scores to their default state after a confirmation prompt.
    *   Delete all saved game files after a confirmation prompt.

#### 8.7 Player Onboarding
*   **Tutorial:** The game shall include an optional, interactive, step-by-step tutorial mode that guides new players through the basic rules and actions of a game.
*   **Digital Rulebook:** A searchable digital rulebook shall be accessible from the main menu and the in-game pause menu to allow players to look up specific rules. The rulebook content shall be sourced from a structured data file (JSON) to facilitate easy updates.
*   **Localization Readiness:** All user-facing strings (UI text, card descriptions, rulebook content) shall be stored in external resource files (JSON) and referenced by a key, not hardcoded. The initial release will only include English, but the architecture must support future localization.

### 9.0 Game Features

#### 9.1 Save/Load Game
*   The system shall allow the game state to be saved at any point during the human player's turn (before the dice roll).
*   The system shall provide at least five save game slots.
*   **9.1.1 Data Management:**
    *   Save files shall use a versioned JSON format, serialized using the `System.Text.Json` library to ensure backward compatibility where possible.
    *   Save files shall be stored in the user's local application data folder (`%APPDATA%/MonopolyTycoon/saves`).
    *   Player statistics and high scores shall be stored in a local SQLite database file, also located in `%APPDATA%/MonopolyTycoon/`.
    *   The system shall implement checksums to detect file corruption. If a corrupted or incompatible (due to version mismatch) file is detected, it will be clearly marked as unusable in the load game menu.
*   **9.1.2 Backup and Recovery:**
    *   On successful application startup, the system shall automatically create a backup of the player statistics SQLite database if it has changed since the last backup.
    *   The system shall retain the three most recent backups, deleting the oldest backup when a new one is created. This provides a recovery path in case of primary data file corruption.
*   **9.1.3 Data Migration:**
    *   The application must include logic to detect and handle older versions of save files and the statistics database.
    *   Where feasible, an automatic migration process shall be implemented to upgrade older data files to the current version upon first load, preserving user progress across game updates.

#### 9.2 Top Score History
*   The game shall persist a list of the top 10 game victories achieved by the human player, read from the local SQLite database and sorted by final net worth descending.
*   Each entry shall record the player's profile name, final net worth, game duration, and total turns.
*   The system shall provide a button to export the Top Score History to a local, human-readable text file (`.txt`).

#### 9.3 Theme System
*   The game shall support a theme system for dynamic replacement of visual and audio assets (e.g., board, tokens, music, UI skin).
*   The game must ship with a minimum of two distinct themes (e.g., 'Classic' and 'Futuristic').

#### 9.4 Audio
*   The game shall incorporate sound effects for key actions (e.g., dice rolls, property purchases, paying rent, going to jail).
*   The system shall provide distinct audio cues for positive (e.g., collecting rent), negative (e.g., paying a tax), and neutral events.
*   The game shall feature a context-aware background music system that changes based on the game state (e.g., calm music for early game, more tense music when players are near bankruptcy).

### 10.0 Documentation Requirements

#### 10.1 User Documentation
*   A digital User Manual shall be included and accessible from the main menu. This manual will cover software-specific features such as game setup, UI navigation, save/load functionality, and settings configuration.

#### 10.2 Technical Documentation
*   Internal technical documentation shall be maintained for development and support purposes, including:
    *   High-level system architecture diagrams.
    *   Data schemas for the JSON save file format and the SQLite statistics database.
    *   Documentation of the configurable AI behavior parameters.

### 11.0 Support and Maintenance

#### 11.1 Update Notification
*   The application shall include a non-intrusive mechanism to check for new versions upon startup. If an update is available, it will notify the user and provide a link to the project's download page. The application will not perform automatic updates.

#### 11.2 Data Privacy
*   A formal data privacy statement shall be accessible from the main menu.
*   The statement shall clarify that the application operates entirely offline and does not collect, store, or transmit any personally identifiable information (PII) or gameplay telemetry to any external server. All user-generated data (profiles, save files, statistics) is stored exclusively on the user's local machine.

### 12.0 Transition Requirements

#### 12.1 Implementation Approach
*   The initial release (Version 1.0) of the software shall be deployed using a "Big Bang" approach, where the full application is made available to all users simultaneously.
*   Subsequent updates (patches, minor versions) shall be delivered as complete installer packages that replace the existing installation.

#### 12.2 Data Migration Strategy
*   The application shall support forward migration of user data (player profiles, statistics, and save files) as defined in section 9.1.3.
*   The migration process must be atomic. If migration fails, the original data files shall be left untouched and the user will be notified of the failure.
*   No backward migration (from a newer version to an older one) will be supported.

#### 12.3 Training Requirements
*   No formal end-user training sessions are required.
*   Player onboarding shall be facilitated through the in-game interactive tutorial and the digital rulebook, as specified in section 8.7.

#### 12.4 Cutover Plan
*   The cutover for the initial release is defined as the moment the official installer is made available for download on the designated distribution channel.
*   Success criteria for the cutover include:
    *   The installer can be successfully downloaded.
    *   The application installs, launches, and is playable on systems meeting the minimum requirements.
    *   Core features (new game, save/load, player profile) are functional.
*   In the event of a critical failure in a post-launch patch, the fallback plan is to remove the faulty patch and direct users to reinstall the last known stable version.

#### 12.5 Legacy System Decommissioning
*   As this is a new product, there is no legacy system to decommission.
*   For future updates, the installation process will automatically overwrite and replace the previous version of the application, rendering it decommissioned. User data will be preserved and migrated as per section 12.2.

### 13.0 Business Rules and Constraints

#### 13.1 Core Business Rules
*   The core gameplay logic is governed by the official Monopoly rule set as defined in section 4.1.
*   The Bank is an entity that cannot be bankrupted and has an infinite supply of cash.
*   All financial transactions must be validated to ensure players have sufficient funds or assets to cover their debts.

#### 13.2 Regulatory Compliance
*   The game's content, features, and marketing materials shall be designed to meet the criteria for an ESRB (Entertainment Software Rating Board) rating of "E for Everyone".
*   This includes the prohibition of realistic gambling, explicit content, and excessive violence.

#### 13.3 Legal and Intellectual Property Constraints
*   All game assets, including but not limited to the "Monopoly" brand name, board design, property names, card text, and token designs, must strictly adhere to the terms of the licensing agreement with Hasbro, Inc.
*   No unlicensed third-party intellectual property shall be used in the application.
*   The application shall display all required copyright and trademark notices as specified in the licensing agreement.

#### 13.4 Industry Standards
*   The application must comply with standard Windows desktop application best practices.
*   The installer must provide a clean uninstallation process that removes all application files and registry entries, with the explicit option for the user to retain or delete their personal data (save files, profiles).
*   The application must store user-specific data in the correct user directory (`%APPDATA%`) and not in the installation folder or system-wide locations.

#### 13.5 Organizational Policies
*   Every official release build must be approved through a formal go/no-go review process by the project lead.
*   Prior to the review, the release candidate must pass a full manual and automated regression testing suite to ensure quality and stability.
*   All source code changes must be submitted through pull requests and require at least one peer review before being merged into the main development branch.