# 1 Title

Monopoly Tycoon Player Data Store

# 2 Name

player_data_store

# 3 Db Type

- relational

# 4 Db Technology

SQLite

# 5 Entities

## 5.1 PlayerProfile

### 5.1.1 Name

PlayerProfile

### 5.1.2 Description

Represents the human player's persistent profile, including their chosen display name and creation date. Based on REQ-1-032 and REQ-1-033.

### 5.1.3 Attributes

#### 5.1.3.1 Guid

##### 5.1.3.1.1 Name

profileId

##### 5.1.3.1.2 Type

🔹 Guid

##### 5.1.3.1.3 Is Required

✅ Yes

##### 5.1.3.1.4 Is Primary Key

✅ Yes

##### 5.1.3.1.5 Size

0

##### 5.1.3.1.6 Is Unique

✅ Yes

##### 5.1.3.1.7 Constraints

*No items available*

##### 5.1.3.1.8 Precision

0

##### 5.1.3.1.9 Scale

0

##### 5.1.3.1.10 Is Foreign Key

❌ No

#### 5.1.3.2.0 VARCHAR

##### 5.1.3.2.1 Name

displayName

##### 5.1.3.2.2 Type

🔹 VARCHAR

##### 5.1.3.2.3 Is Required

✅ Yes

##### 5.1.3.2.4 Is Primary Key

❌ No

##### 5.1.3.2.5 Size

16

##### 5.1.3.2.6 Is Unique

✅ Yes

##### 5.1.3.2.7 Constraints

- LENGTH_3_TO_16
- NO_SPECIAL_CHARS

##### 5.1.3.2.8 Precision

0

##### 5.1.3.2.9 Scale

0

##### 5.1.3.2.10 Is Foreign Key

❌ No

#### 5.1.3.3.0 DateTime

##### 5.1.3.3.1 Name

createdAt

##### 5.1.3.3.2 Type

🔹 DateTime

##### 5.1.3.3.3 Is Required

✅ Yes

##### 5.1.3.3.4 Is Primary Key

❌ No

##### 5.1.3.3.5 Size

0

##### 5.1.3.3.6 Is Unique

❌ No

##### 5.1.3.3.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.1.3.3.8 Precision

0

##### 5.1.3.3.9 Scale

0

##### 5.1.3.3.10 Is Foreign Key

❌ No

#### 5.1.3.4.0 DateTime

##### 5.1.3.4.1 Name

updatedAt

##### 5.1.3.4.2 Type

🔹 DateTime

##### 5.1.3.4.3 Is Required

✅ Yes

##### 5.1.3.4.4 Is Primary Key

❌ No

##### 5.1.3.4.5 Size

0

##### 5.1.3.4.6 Is Unique

❌ No

##### 5.1.3.4.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.1.3.4.8 Precision

0

##### 5.1.3.4.9 Scale

0

##### 5.1.3.4.10 Is Foreign Key

❌ No

### 5.1.4.0.0 Primary Keys

- profileId

### 5.1.5.0.0 Unique Constraints

- {'name': 'UC_PlayerProfile_DisplayName', 'columns': ['displayName']}

### 5.1.6.0.0 Indexes

- {'name': 'IX_PlayerProfile_CreatedAt', 'columns': ['createdAt'], 'type': 'BTree'}

## 5.2.0.0.0 PlayerStatistic

### 5.2.1.0.0 Name

PlayerStatistic

### 5.2.2.0.0 Description

Stores aggregated historical gameplay statistics for a player profile, as required by REQ-1-034. This table is a one-to-one extension of PlayerProfile.

### 5.2.3.0.0 Attributes

#### 5.2.3.1.0 Guid

##### 5.2.3.1.1 Name

playerStatisticId

##### 5.2.3.1.2 Type

🔹 Guid

##### 5.2.3.1.3 Is Required

✅ Yes

##### 5.2.3.1.4 Is Primary Key

✅ Yes

##### 5.2.3.1.5 Size

0

##### 5.2.3.1.6 Is Unique

✅ Yes

##### 5.2.3.1.7 Constraints

*No items available*

##### 5.2.3.1.8 Precision

0

##### 5.2.3.1.9 Scale

0

##### 5.2.3.1.10 Is Foreign Key

❌ No

#### 5.2.3.2.0 Guid

##### 5.2.3.2.1 Name

profileId

##### 5.2.3.2.2 Type

🔹 Guid

##### 5.2.3.2.3 Is Required

✅ Yes

##### 5.2.3.2.4 Is Primary Key

❌ No

##### 5.2.3.2.5 Size

0

##### 5.2.3.2.6 Is Unique

✅ Yes

##### 5.2.3.2.7 Constraints

*No items available*

##### 5.2.3.2.8 Precision

0

##### 5.2.3.2.9 Scale

0

##### 5.2.3.2.10 Is Foreign Key

✅ Yes

#### 5.2.3.3.0 INT

##### 5.2.3.3.1 Name

totalGamesPlayed

##### 5.2.3.3.2 Type

🔹 INT

##### 5.2.3.3.3 Is Required

✅ Yes

##### 5.2.3.3.4 Is Primary Key

❌ No

##### 5.2.3.3.5 Size

0

##### 5.2.3.3.6 Is Unique

❌ No

##### 5.2.3.3.7 Constraints

- NON_NEGATIVE
- DEFAULT 0

##### 5.2.3.3.8 Precision

0

##### 5.2.3.3.9 Scale

0

##### 5.2.3.3.10 Is Foreign Key

❌ No

#### 5.2.3.4.0 INT

##### 5.2.3.4.1 Name

totalWins

##### 5.2.3.4.2 Type

🔹 INT

##### 5.2.3.4.3 Is Required

✅ Yes

##### 5.2.3.4.4 Is Primary Key

❌ No

##### 5.2.3.4.5 Size

0

##### 5.2.3.4.6 Is Unique

❌ No

##### 5.2.3.4.7 Constraints

- NON_NEGATIVE
- DEFAULT 0

##### 5.2.3.4.8 Precision

0

##### 5.2.3.4.9 Scale

0

##### 5.2.3.4.10 Is Foreign Key

❌ No

#### 5.2.3.5.0 INT

##### 5.2.3.5.1 Name

averageGameDurationSeconds

##### 5.2.3.5.2 Type

🔹 INT

##### 5.2.3.5.3 Is Required

✅ Yes

##### 5.2.3.5.4 Is Primary Key

❌ No

##### 5.2.3.5.5 Size

0

##### 5.2.3.5.6 Is Unique

❌ No

##### 5.2.3.5.7 Constraints

- NON_NEGATIVE
- DEFAULT 0

##### 5.2.3.5.8 Precision

0

##### 5.2.3.5.9 Scale

0

##### 5.2.3.5.10 Is Foreign Key

❌ No

#### 5.2.3.6.0 VARCHAR

##### 5.2.3.6.1 Name

mostProfitableProperty

##### 5.2.3.6.2 Type

🔹 VARCHAR

##### 5.2.3.6.3 Is Required

❌ No

##### 5.2.3.6.4 Is Primary Key

❌ No

##### 5.2.3.6.5 Size

50

##### 5.2.3.6.6 Is Unique

❌ No

##### 5.2.3.6.7 Constraints

*No items available*

##### 5.2.3.6.8 Precision

0

##### 5.2.3.6.9 Scale

0

##### 5.2.3.6.10 Is Foreign Key

❌ No

#### 5.2.3.7.0 DECIMAL

##### 5.2.3.7.1 Name

totalRentPaid

##### 5.2.3.7.2 Type

🔹 DECIMAL

##### 5.2.3.7.3 Is Required

✅ Yes

##### 5.2.3.7.4 Is Primary Key

❌ No

##### 5.2.3.7.5 Size

0

##### 5.2.3.7.6 Is Unique

❌ No

##### 5.2.3.7.7 Constraints

- NON_NEGATIVE
- DEFAULT 0.00

##### 5.2.3.7.8 Precision

18

##### 5.2.3.7.9 Scale

2

##### 5.2.3.7.10 Is Foreign Key

❌ No

#### 5.2.3.8.0 DECIMAL

##### 5.2.3.8.1 Name

totalRentCollected

##### 5.2.3.8.2 Type

🔹 DECIMAL

##### 5.2.3.8.3 Is Required

✅ Yes

##### 5.2.3.8.4 Is Primary Key

❌ No

##### 5.2.3.8.5 Size

0

##### 5.2.3.8.6 Is Unique

❌ No

##### 5.2.3.8.7 Constraints

- NON_NEGATIVE
- DEFAULT 0.00

##### 5.2.3.8.8 Precision

18

##### 5.2.3.8.9 Scale

2

##### 5.2.3.8.10 Is Foreign Key

❌ No

#### 5.2.3.9.0 INT

##### 5.2.3.9.1 Name

totalPropertiesAcquired

##### 5.2.3.9.2 Type

🔹 INT

##### 5.2.3.9.3 Is Required

✅ Yes

##### 5.2.3.9.4 Is Primary Key

❌ No

##### 5.2.3.9.5 Size

0

##### 5.2.3.9.6 Is Unique

❌ No

##### 5.2.3.9.7 Constraints

- NON_NEGATIVE
- DEFAULT 0

##### 5.2.3.9.8 Precision

0

##### 5.2.3.9.9 Scale

0

##### 5.2.3.9.10 Is Foreign Key

❌ No

#### 5.2.3.10.0 DECIMAL

##### 5.2.3.10.1 Name

largestTradeDealValue

##### 5.2.3.10.2 Type

🔹 DECIMAL

##### 5.2.3.10.3 Is Required

✅ Yes

##### 5.2.3.10.4 Is Primary Key

❌ No

##### 5.2.3.10.5 Size

0

##### 5.2.3.10.6 Is Unique

❌ No

##### 5.2.3.10.7 Constraints

- NON_NEGATIVE
- DEFAULT 0.00

##### 5.2.3.10.8 Precision

18

##### 5.2.3.10.9 Scale

2

##### 5.2.3.10.10 Is Foreign Key

❌ No

#### 5.2.3.11.0 DateTime

##### 5.2.3.11.1 Name

updatedAt

##### 5.2.3.11.2 Type

🔹 DateTime

##### 5.2.3.11.3 Is Required

✅ Yes

##### 5.2.3.11.4 Is Primary Key

❌ No

##### 5.2.3.11.5 Size

0

##### 5.2.3.11.6 Is Unique

❌ No

##### 5.2.3.11.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.2.3.11.8 Precision

0

##### 5.2.3.11.9 Scale

0

##### 5.2.3.11.10 Is Foreign Key

❌ No

### 5.2.4.0.0 Primary Keys

- playerStatisticId

### 5.2.5.0.0 Unique Constraints

- {'name': 'UC_PlayerStatistic_ProfileId', 'columns': ['profileId']}

### 5.2.6.0.0 Indexes

*No items available*

## 5.3.0.0.0 GameResult

### 5.3.1.0.0 Name

GameResult

### 5.3.2.0.0 Description

Records the outcome and high-level statistics of a single completed game session. Used for game summaries (REQ-1-070) and the Top Scores list (REQ-1-091).

### 5.3.3.0.0 Attributes

#### 5.3.3.1.0 Guid

##### 5.3.3.1.1 Name

gameResultId

##### 5.3.3.1.2 Type

🔹 Guid

##### 5.3.3.1.3 Is Required

✅ Yes

##### 5.3.3.1.4 Is Primary Key

✅ Yes

##### 5.3.3.1.5 Size

0

##### 5.3.3.1.6 Is Unique

✅ Yes

##### 5.3.3.1.7 Constraints

*No items available*

##### 5.3.3.1.8 Precision

0

##### 5.3.3.1.9 Scale

0

##### 5.3.3.1.10 Is Foreign Key

❌ No

#### 5.3.3.2.0 Guid

##### 5.3.3.2.1 Name

profileId

##### 5.3.3.2.2 Type

🔹 Guid

##### 5.3.3.2.3 Is Required

✅ Yes

##### 5.3.3.2.4 Is Primary Key

❌ No

##### 5.3.3.2.5 Size

0

##### 5.3.3.2.6 Is Unique

❌ No

##### 5.3.3.2.7 Constraints

*No items available*

##### 5.3.3.2.8 Precision

0

##### 5.3.3.2.9 Scale

0

##### 5.3.3.2.10 Is Foreign Key

✅ Yes

#### 5.3.3.3.0 BOOLEAN

##### 5.3.3.3.1 Name

didHumanWin

##### 5.3.3.3.2 Type

🔹 BOOLEAN

##### 5.3.3.3.3 Is Required

✅ Yes

##### 5.3.3.3.4 Is Primary Key

❌ No

##### 5.3.3.3.5 Size

0

##### 5.3.3.3.6 Is Unique

❌ No

##### 5.3.3.3.7 Constraints

- DEFAULT false

##### 5.3.3.3.8 Precision

0

##### 5.3.3.3.9 Scale

0

##### 5.3.3.3.10 Is Foreign Key

❌ No

#### 5.3.3.4.0 INT

##### 5.3.3.4.1 Name

gameDurationSeconds

##### 5.3.3.4.2 Type

🔹 INT

##### 5.3.3.4.3 Is Required

✅ Yes

##### 5.3.3.4.4 Is Primary Key

❌ No

##### 5.3.3.4.5 Size

0

##### 5.3.3.4.6 Is Unique

❌ No

##### 5.3.3.4.7 Constraints

- NON_NEGATIVE
- DEFAULT 0

##### 5.3.3.4.8 Precision

0

##### 5.3.3.4.9 Scale

0

##### 5.3.3.4.10 Is Foreign Key

❌ No

#### 5.3.3.5.0 INT

##### 5.3.3.5.1 Name

totalTurns

##### 5.3.3.5.2 Type

🔹 INT

##### 5.3.3.5.3 Is Required

✅ Yes

##### 5.3.3.5.4 Is Primary Key

❌ No

##### 5.3.3.5.5 Size

0

##### 5.3.3.5.6 Is Unique

❌ No

##### 5.3.3.5.7 Constraints

- NON_NEGATIVE
- DEFAULT 0

##### 5.3.3.5.8 Precision

0

##### 5.3.3.5.9 Scale

0

##### 5.3.3.5.10 Is Foreign Key

❌ No

#### 5.3.3.6.0 DateTime

##### 5.3.3.6.1 Name

endTimestamp

##### 5.3.3.6.2 Type

🔹 DateTime

##### 5.3.3.6.3 Is Required

✅ Yes

##### 5.3.3.6.4 Is Primary Key

❌ No

##### 5.3.3.6.5 Size

0

##### 5.3.3.6.6 Is Unique

❌ No

##### 5.3.3.6.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.3.3.6.8 Precision

0

##### 5.3.3.6.9 Scale

0

##### 5.3.3.6.10 Is Foreign Key

❌ No

#### 5.3.3.7.0 DECIMAL

##### 5.3.3.7.1 Name

humanFinalNetWorth

##### 5.3.3.7.2 Type

🔹 DECIMAL

##### 5.3.3.7.3 Is Required

✅ Yes

##### 5.3.3.7.4 Is Primary Key

❌ No

##### 5.3.3.7.5 Size

0

##### 5.3.3.7.6 Is Unique

❌ No

##### 5.3.3.7.7 Constraints

- DEFAULT 0.00

##### 5.3.3.7.8 Precision

18

##### 5.3.3.7.9 Scale

2

##### 5.3.3.7.10 Is Foreign Key

❌ No

### 5.3.4.0.0 Primary Keys

- gameResultId

### 5.3.5.0.0 Unique Constraints

*No items available*

### 5.3.6.0.0 Indexes

#### 5.3.6.1.0 BTree

##### 5.3.6.1.1 Name

IX_GameResult_TopScores

##### 5.3.6.1.2 Columns

- profileId
- didHumanWin
- humanFinalNetWorth

##### 5.3.6.1.3 Type

🔹 BTree

#### 5.3.6.2.0 BTree

##### 5.3.6.2.1 Name

IX_GameResult_History

##### 5.3.6.2.2 Columns

- profileId
- endTimestamp

##### 5.3.6.2.3 Type

🔹 BTree

## 5.4.0.0.0 GameParticipant

### 5.4.1.0.0 Name

GameParticipant

### 5.4.2.0.0 Description

Represents a single player's (human or AI) performance in a specific completed game, linked to a GameResult. Fulfills REQ-1-070.

### 5.4.3.0.0 Attributes

#### 5.4.3.1.0 Guid

##### 5.4.3.1.1 Name

gameParticipantId

##### 5.4.3.1.2 Type

🔹 Guid

##### 5.4.3.1.3 Is Required

✅ Yes

##### 5.4.3.1.4 Is Primary Key

✅ Yes

##### 5.4.3.1.5 Size

0

##### 5.4.3.1.6 Is Unique

✅ Yes

##### 5.4.3.1.7 Constraints

*No items available*

##### 5.4.3.1.8 Precision

0

##### 5.4.3.1.9 Scale

0

##### 5.4.3.1.10 Is Foreign Key

❌ No

#### 5.4.3.2.0 Guid

##### 5.4.3.2.1 Name

gameResultId

##### 5.4.3.2.2 Type

🔹 Guid

##### 5.4.3.2.3 Is Required

✅ Yes

##### 5.4.3.2.4 Is Primary Key

❌ No

##### 5.4.3.2.5 Size

0

##### 5.4.3.2.6 Is Unique

❌ No

##### 5.4.3.2.7 Constraints

*No items available*

##### 5.4.3.2.8 Precision

0

##### 5.4.3.2.9 Scale

0

##### 5.4.3.2.10 Is Foreign Key

✅ Yes

#### 5.4.3.3.0 VARCHAR

##### 5.4.3.3.1 Name

participantName

##### 5.4.3.3.2 Type

🔹 VARCHAR

##### 5.4.3.3.3 Is Required

✅ Yes

##### 5.4.3.3.4 Is Primary Key

❌ No

##### 5.4.3.3.5 Size

16

##### 5.4.3.3.6 Is Unique

❌ No

##### 5.4.3.3.7 Constraints

*No items available*

##### 5.4.3.3.8 Precision

0

##### 5.4.3.3.9 Scale

0

##### 5.4.3.3.10 Is Foreign Key

❌ No

#### 5.4.3.4.0 BOOLEAN

##### 5.4.3.4.1 Name

isHuman

##### 5.4.3.4.2 Type

🔹 BOOLEAN

##### 5.4.3.4.3 Is Required

✅ Yes

##### 5.4.3.4.4 Is Primary Key

❌ No

##### 5.4.3.4.5 Size

0

##### 5.4.3.4.6 Is Unique

❌ No

##### 5.4.3.4.7 Constraints

- DEFAULT false

##### 5.4.3.4.8 Precision

0

##### 5.4.3.4.9 Scale

0

##### 5.4.3.4.10 Is Foreign Key

❌ No

#### 5.4.3.5.0 INT

##### 5.4.3.5.1 Name

aiDifficulty

##### 5.4.3.5.2 Type

🔹 INT

##### 5.4.3.5.3 Is Required

❌ No

##### 5.4.3.5.4 Is Primary Key

❌ No

##### 5.4.3.5.5 Size

0

##### 5.4.3.5.6 Is Unique

❌ No

##### 5.4.3.5.7 Constraints

*No items available*

##### 5.4.3.5.8 Precision

0

##### 5.4.3.5.9 Scale

0

##### 5.4.3.5.10 Is Foreign Key

❌ No

#### 5.4.3.6.0 DECIMAL

##### 5.4.3.6.1 Name

finalNetWorth

##### 5.4.3.6.2 Type

🔹 DECIMAL

##### 5.4.3.6.3 Is Required

✅ Yes

##### 5.4.3.6.4 Is Primary Key

❌ No

##### 5.4.3.6.5 Size

0

##### 5.4.3.6.6 Is Unique

❌ No

##### 5.4.3.6.7 Constraints

- DEFAULT 0.00

##### 5.4.3.6.8 Precision

18

##### 5.4.3.6.9 Scale

2

##### 5.4.3.6.10 Is Foreign Key

❌ No

#### 5.4.3.7.0 INT

##### 5.4.3.7.1 Name

propertiesOwnedCount

##### 5.4.3.7.2 Type

🔹 INT

##### 5.4.3.7.3 Is Required

✅ Yes

##### 5.4.3.7.4 Is Primary Key

❌ No

##### 5.4.3.7.5 Size

0

##### 5.4.3.7.6 Is Unique

❌ No

##### 5.4.3.7.7 Constraints

- NON_NEGATIVE
- DEFAULT 0

##### 5.4.3.7.8 Precision

0

##### 5.4.3.7.9 Scale

0

##### 5.4.3.7.10 Is Foreign Key

❌ No

#### 5.4.3.8.0 DECIMAL

##### 5.4.3.8.1 Name

totalRentCollected

##### 5.4.3.8.2 Type

🔹 DECIMAL

##### 5.4.3.8.3 Is Required

✅ Yes

##### 5.4.3.8.4 Is Primary Key

❌ No

##### 5.4.3.8.5 Size

0

##### 5.4.3.8.6 Is Unique

❌ No

##### 5.4.3.8.7 Constraints

- NON_NEGATIVE
- DEFAULT 0.00

##### 5.4.3.8.8 Precision

18

##### 5.4.3.8.9 Scale

2

##### 5.4.3.8.10 Is Foreign Key

❌ No

#### 5.4.3.9.0 BOOLEAN

##### 5.4.3.9.1 Name

isWinner

##### 5.4.3.9.2 Type

🔹 BOOLEAN

##### 5.4.3.9.3 Is Required

✅ Yes

##### 5.4.3.9.4 Is Primary Key

❌ No

##### 5.4.3.9.5 Size

0

##### 5.4.3.9.6 Is Unique

❌ No

##### 5.4.3.9.7 Constraints

- DEFAULT false

##### 5.4.3.9.8 Precision

0

##### 5.4.3.9.9 Scale

0

##### 5.4.3.9.10 Is Foreign Key

❌ No

### 5.4.4.0.0 Primary Keys

- gameParticipantId

### 5.4.5.0.0 Unique Constraints

- {'name': 'UC_GameParticipant_Result_Name', 'columns': ['gameResultId', 'participantName']}

### 5.4.6.0.0 Indexes

- {'name': 'IX_GameParticipant_GameResultId', 'columns': ['gameResultId'], 'type': 'BTree'}

## 5.5.0.0.0 SavedGame

### 5.5.1.0.0 Name

SavedGame

### 5.5.2.0.0 Description

Stores metadata for a saved game state file, linking it to a player profile. The actual game state is in a separate file. Fulfills REQ-1-085, REQ-1-086, REQ-1-087.

### 5.5.3.0.0 Attributes

#### 5.5.3.1.0 Guid

##### 5.5.3.1.1 Name

savedGameId

##### 5.5.3.1.2 Type

🔹 Guid

##### 5.5.3.1.3 Is Required

✅ Yes

##### 5.5.3.1.4 Is Primary Key

✅ Yes

##### 5.5.3.1.5 Size

0

##### 5.5.3.1.6 Is Unique

✅ Yes

##### 5.5.3.1.7 Constraints

*No items available*

##### 5.5.3.1.8 Precision

0

##### 5.5.3.1.9 Scale

0

##### 5.5.3.1.10 Is Foreign Key

❌ No

#### 5.5.3.2.0 Guid

##### 5.5.3.2.1 Name

profileId

##### 5.5.3.2.2 Type

🔹 Guid

##### 5.5.3.2.3 Is Required

✅ Yes

##### 5.5.3.2.4 Is Primary Key

❌ No

##### 5.5.3.2.5 Size

0

##### 5.5.3.2.6 Is Unique

❌ No

##### 5.5.3.2.7 Constraints

*No items available*

##### 5.5.3.2.8 Precision

0

##### 5.5.3.2.9 Scale

0

##### 5.5.3.2.10 Is Foreign Key

✅ Yes

#### 5.5.3.3.0 INT

##### 5.5.3.3.1 Name

slotNumber

##### 5.5.3.3.2 Type

🔹 INT

##### 5.5.3.3.3 Is Required

✅ Yes

##### 5.5.3.3.4 Is Primary Key

❌ No

##### 5.5.3.3.5 Size

0

##### 5.5.3.3.6 Is Unique

❌ No

##### 5.5.3.3.7 Constraints

- RANGE_1_TO_5

##### 5.5.3.3.8 Precision

0

##### 5.5.3.3.9 Scale

0

##### 5.5.3.3.10 Is Foreign Key

❌ No

#### 5.5.3.4.0 VARCHAR

##### 5.5.3.4.1 Name

saveName

##### 5.5.3.4.2 Type

🔹 VARCHAR

##### 5.5.3.4.3 Is Required

❌ No

##### 5.5.3.4.4 Is Primary Key

❌ No

##### 5.5.3.4.5 Size

100

##### 5.5.3.4.6 Is Unique

❌ No

##### 5.5.3.4.7 Constraints

- DEFAULT 'Game Save'

##### 5.5.3.4.8 Precision

0

##### 5.5.3.4.9 Scale

0

##### 5.5.3.4.10 Is Foreign Key

❌ No

#### 5.5.3.5.0 DateTime

##### 5.5.3.5.1 Name

saveTimestamp

##### 5.5.3.5.2 Type

🔹 DateTime

##### 5.5.3.5.3 Is Required

✅ Yes

##### 5.5.3.5.4 Is Primary Key

❌ No

##### 5.5.3.5.5 Size

0

##### 5.5.3.5.6 Is Unique

❌ No

##### 5.5.3.5.7 Constraints

- DEFAULT CURRENT_TIMESTAMP

##### 5.5.3.5.8 Precision

0

##### 5.5.3.5.9 Scale

0

##### 5.5.3.5.10 Is Foreign Key

❌ No

#### 5.5.3.6.0 VARCHAR

##### 5.5.3.6.1 Name

gameVersion

##### 5.5.3.6.2 Type

🔹 VARCHAR

##### 5.5.3.6.3 Is Required

✅ Yes

##### 5.5.3.6.4 Is Primary Key

❌ No

##### 5.5.3.6.5 Size

20

##### 5.5.3.6.6 Is Unique

❌ No

##### 5.5.3.6.7 Constraints

*No items available*

##### 5.5.3.6.8 Precision

0

##### 5.5.3.6.9 Scale

0

##### 5.5.3.6.10 Is Foreign Key

❌ No

#### 5.5.3.7.0 VARCHAR

##### 5.5.3.7.1 Name

fileChecksum

##### 5.5.3.7.2 Type

🔹 VARCHAR

##### 5.5.3.7.3 Is Required

✅ Yes

##### 5.5.3.7.4 Is Primary Key

❌ No

##### 5.5.3.7.5 Size

64

##### 5.5.3.7.6 Is Unique

❌ No

##### 5.5.3.7.7 Constraints

*No items available*

##### 5.5.3.7.8 Precision

0

##### 5.5.3.7.9 Scale

0

##### 5.5.3.7.10 Is Foreign Key

❌ No

### 5.5.4.0.0 Primary Keys

- savedGameId

### 5.5.5.0.0 Unique Constraints

- {'name': 'UC_SavedGame_Profile_Slot', 'columns': ['profileId', 'slotNumber']}

### 5.5.6.0.0 Indexes

- {'name': 'IX_SavedGame_Profile_Timestamp', 'columns': ['profileId', 'saveTimestamp'], 'type': 'BTree'}

