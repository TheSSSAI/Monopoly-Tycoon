# 1 Title

Game State Save Files

# 2 Name

game_saves

# 3 Db Type

- document

# 4 Db Technology

System.Text.Json on File System

# 5 Entities

- {'name': 'GameState', 'description': "Represents the complete, serializable state of an in-progress game. Each instance is stored as a distinct JSON file and is managed by the Infrastructure Layer's GameSaveRepository. Its metadata is stored in the 'SavedGame' table of the relational database. Fulfills REQ-1-041 and REQ-1-087.", 'attributes': [{'name': 'gameStateId', 'type': 'Guid', 'isRequired': True, 'isPrimaryKey': True, 'size': 0, 'isUnique': True, 'constraints': ['Corresponds to SavedGame.savedGameId'], 'precision': 0, 'scale': 0, 'isForeignKey': True}, {'name': 'gameVersion', 'type': 'VARCHAR', 'isRequired': True, 'isPrimaryKey': False, 'size': 20, 'isUnique': False, 'constraints': ['Used for data migration logic (REQ-1-090)'], 'precision': 0, 'scale': 0, 'isForeignKey': False}, {'name': 'playerStates', 'type': 'Array<PlayerState>', 'isRequired': True, 'isPrimaryKey': False, 'size': 0, 'isUnique': False, 'constraints': ['Contains an array of PlayerState objects as defined in REQ-1-032.'], 'precision': 0, 'scale': 0, 'isForeignKey': False}, {'name': 'boardState', 'type': 'JSON_Object', 'isRequired': True, 'isPrimaryKey': False, 'size': 0, 'isUnique': False, 'constraints': ['Represents ownership and development level (houses/hotel) of all properties.'], 'precision': 0, 'scale': 0, 'isForeignKey': False}, {'name': 'bankState', 'type': 'JSON_Object', 'isRequired': True, 'isPrimaryKey': False, 'size': 0, 'isUnique': False, 'constraints': ['Represents number of houses and hotels remaining in the bank.'], 'precision': 0, 'scale': 0, 'isForeignKey': False}, {'name': 'deckStates', 'type': 'JSON_Object', 'isRequired': True, 'isPrimaryKey': False, 'size': 0, 'isUnique': False, 'constraints': ['Represents the current order of cards in the Chance and Community Chest decks.'], 'precision': 0, 'scale': 0, 'isForeignKey': False}, {'name': 'gameMetadata', 'type': 'JSON_Object', 'isRequired': True, 'isPrimaryKey': False, 'size': 0, 'isUnique': False, 'constraints': ['Contains current turn number, active player ID, session timestamp, etc.'], 'precision': 0, 'scale': 0, 'isForeignKey': False}], 'primaryKeys': ['gameStateId'], 'uniqueConstraints': [], 'indexes': []}

