erDiagram
    GameState {
        Guid gameStateId PK "Corresponds to SavedGame.savedGameId"
        VARCHAR gameVersion "Used for data migration logic (REQ-1-090)"
        Array_PlayerState_ playerStates "Contains an array of PlayerState objects as defined in REQ-1-032."
        JSON_Object boardState "Represents ownership and development level (houses/hotel) of all properties."
        JSON_Object bankState "Represents number of houses and hotels remaining in the bank."
        JSON_Object deckStates "Represents the current order of cards in the Chance and Community Chest decks."
        JSON_Object gameMetadata "Contains current turn number, active player ID, session timestamp, etc."
    }