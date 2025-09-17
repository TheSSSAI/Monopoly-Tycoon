# 1 Entities

## 1.1 PlayerProfile

### 1.1.1 Name

PlayerProfile

### 1.1.2 Description

Represents the human player's persistent profile, including their chosen display name and creation date. Based on REQ-1-032.

### 1.1.3 Attributes

#### 1.1.3.1 Guid

##### 1.1.3.1.1 Name

profileId

##### 1.1.3.1.2 Type

🔹 Guid

##### 1.1.3.1.3 Is Required

✅ Yes

##### 1.1.3.1.4 Is Primary Key

✅ Yes

##### 1.1.3.1.5 Is Unique

✅ Yes

##### 1.1.3.1.6 Index Type

UniqueIndex

##### 1.1.3.1.7 Size

0

##### 1.1.3.1.8 Constraints

*No items available*

##### 1.1.3.1.9 Default Value



##### 1.1.3.1.10 Is Foreign Key

❌ No

##### 1.1.3.1.11 Precision

0

##### 1.1.3.1.12 Scale

0

#### 1.1.3.2.0 VARCHAR

##### 1.1.3.2.1 Name

displayName

##### 1.1.3.2.2 Type

🔹 VARCHAR

##### 1.1.3.2.3 Is Required

✅ Yes

##### 1.1.3.2.4 Is Primary Key

❌ No

##### 1.1.3.2.5 Is Unique

✅ Yes

##### 1.1.3.2.6 Index Type

UniqueIndex

##### 1.1.3.2.7 Size

16

##### 1.1.3.2.8 Constraints

- LENGTH_3_TO_16
- NO_SPECIAL_CHARS

##### 1.1.3.2.9 Default Value



##### 1.1.3.2.10 Is Foreign Key

❌ No

##### 1.1.3.2.11 Precision

0

##### 1.1.3.2.12 Scale

0

#### 1.1.3.3.0 DateTime

##### 1.1.3.3.1 Name

createdAt

##### 1.1.3.3.2 Type

🔹 DateTime

##### 1.1.3.3.3 Is Required

✅ Yes

##### 1.1.3.3.4 Is Primary Key

❌ No

##### 1.1.3.3.5 Is Unique

❌ No

##### 1.1.3.3.6 Index Type

Index

##### 1.1.3.3.7 Size

0

##### 1.1.3.3.8 Constraints

*No items available*

##### 1.1.3.3.9 Default Value

CURRENT_TIMESTAMP

##### 1.1.3.3.10 Is Foreign Key

❌ No

##### 1.1.3.3.11 Precision

0

##### 1.1.3.3.12 Scale

0

#### 1.1.3.4.0 DateTime

##### 1.1.3.4.1 Name

updatedAt

##### 1.1.3.4.2 Type

🔹 DateTime

##### 1.1.3.4.3 Is Required

✅ Yes

##### 1.1.3.4.4 Is Primary Key

❌ No

##### 1.1.3.4.5 Is Unique

❌ No

##### 1.1.3.4.6 Index Type

None

##### 1.1.3.4.7 Size

0

##### 1.1.3.4.8 Constraints

*No items available*

##### 1.1.3.4.9 Default Value

CURRENT_TIMESTAMP

##### 1.1.3.4.10 Is Foreign Key

❌ No

##### 1.1.3.4.11 Precision

0

##### 1.1.3.4.12 Scale

0

### 1.1.4.0.0 Primary Keys

- profileId

### 1.1.5.0.0 Unique Constraints

- {'name': 'UC_PlayerProfile_DisplayName', 'columns': ['displayName']}

### 1.1.6.0.0 Indexes

- {'name': 'IX_PlayerProfile_CreatedAt', 'columns': ['createdAt'], 'type': 'BTree'}

## 1.2.0.0.0 PlayerStatistic

### 1.2.1.0.0 Name

PlayerStatistic

### 1.2.2.0.0 Description

Stores aggregated historical gameplay statistics for a player profile, as required by REQ-1-033 and REQ-1-034. This table is a one-to-one extension of PlayerProfile.

### 1.2.3.0.0 Attributes

#### 1.2.3.1.0 Guid

##### 1.2.3.1.1 Name

playerStatisticId

##### 1.2.3.1.2 Type

🔹 Guid

##### 1.2.3.1.3 Is Required

✅ Yes

##### 1.2.3.1.4 Is Primary Key

✅ Yes

##### 1.2.3.1.5 Is Unique

✅ Yes

##### 1.2.3.1.6 Index Type

UniqueIndex

##### 1.2.3.1.7 Size

0

##### 1.2.3.1.8 Constraints

*No items available*

##### 1.2.3.1.9 Default Value



##### 1.2.3.1.10 Is Foreign Key

❌ No

##### 1.2.3.1.11 Precision

0

##### 1.2.3.1.12 Scale

0

#### 1.2.3.2.0 Guid

##### 1.2.3.2.1 Name

profileId

##### 1.2.3.2.2 Type

🔹 Guid

##### 1.2.3.2.3 Is Required

✅ Yes

##### 1.2.3.2.4 Is Primary Key

❌ No

##### 1.2.3.2.5 Is Unique

✅ Yes

##### 1.2.3.2.6 Index Type

UniqueIndex

##### 1.2.3.2.7 Size

0

##### 1.2.3.2.8 Constraints

*No items available*

##### 1.2.3.2.9 Default Value



##### 1.2.3.2.10 Is Foreign Key

✅ Yes

##### 1.2.3.2.11 Precision

0

##### 1.2.3.2.12 Scale

0

#### 1.2.3.3.0 INT

##### 1.2.3.3.1 Name

totalGamesPlayed

##### 1.2.3.3.2 Type

🔹 INT

##### 1.2.3.3.3 Is Required

✅ Yes

##### 1.2.3.3.4 Is Primary Key

❌ No

##### 1.2.3.3.5 Is Unique

❌ No

##### 1.2.3.3.6 Index Type

None

##### 1.2.3.3.7 Size

0

##### 1.2.3.3.8 Constraints

- NON_NEGATIVE

##### 1.2.3.3.9 Default Value

0

##### 1.2.3.3.10 Is Foreign Key

❌ No

##### 1.2.3.3.11 Precision

0

##### 1.2.3.3.12 Scale

0

#### 1.2.3.4.0 INT

##### 1.2.3.4.1 Name

totalWins

##### 1.2.3.4.2 Type

🔹 INT

##### 1.2.3.4.3 Is Required

✅ Yes

##### 1.2.3.4.4 Is Primary Key

❌ No

##### 1.2.3.4.5 Is Unique

❌ No

##### 1.2.3.4.6 Index Type

None

##### 1.2.3.4.7 Size

0

##### 1.2.3.4.8 Constraints

- NON_NEGATIVE

##### 1.2.3.4.9 Default Value

0

##### 1.2.3.4.10 Is Foreign Key

❌ No

##### 1.2.3.4.11 Precision

0

##### 1.2.3.4.12 Scale

0

#### 1.2.3.5.0 INT

##### 1.2.3.5.1 Name

averageGameDurationSeconds

##### 1.2.3.5.2 Type

🔹 INT

##### 1.2.3.5.3 Is Required

✅ Yes

##### 1.2.3.5.4 Is Primary Key

❌ No

##### 1.2.3.5.5 Is Unique

❌ No

##### 1.2.3.5.6 Index Type

None

##### 1.2.3.5.7 Size

0

##### 1.2.3.5.8 Constraints

- NON_NEGATIVE

##### 1.2.3.5.9 Default Value

0

##### 1.2.3.5.10 Is Foreign Key

❌ No

##### 1.2.3.5.11 Precision

0

##### 1.2.3.5.12 Scale

0

#### 1.2.3.6.0 VARCHAR

##### 1.2.3.6.1 Name

mostProfitableProperty

##### 1.2.3.6.2 Type

🔹 VARCHAR

##### 1.2.3.6.3 Is Required

❌ No

##### 1.2.3.6.4 Is Primary Key

❌ No

##### 1.2.3.6.5 Is Unique

❌ No

##### 1.2.3.6.6 Index Type

None

##### 1.2.3.6.7 Size

50

##### 1.2.3.6.8 Constraints

*No items available*

##### 1.2.3.6.9 Default Value



##### 1.2.3.6.10 Is Foreign Key

❌ No

##### 1.2.3.6.11 Precision

0

##### 1.2.3.6.12 Scale

0

#### 1.2.3.7.0 DECIMAL

##### 1.2.3.7.1 Name

totalRentPaid

##### 1.2.3.7.2 Type

🔹 DECIMAL

##### 1.2.3.7.3 Is Required

✅ Yes

##### 1.2.3.7.4 Is Primary Key

❌ No

##### 1.2.3.7.5 Is Unique

❌ No

##### 1.2.3.7.6 Index Type

None

##### 1.2.3.7.7 Size

0

##### 1.2.3.7.8 Constraints

- NON_NEGATIVE

##### 1.2.3.7.9 Default Value

0.00

##### 1.2.3.7.10 Is Foreign Key

❌ No

##### 1.2.3.7.11 Precision

18

##### 1.2.3.7.12 Scale

2

#### 1.2.3.8.0 DECIMAL

##### 1.2.3.8.1 Name

totalRentCollected

##### 1.2.3.8.2 Type

🔹 DECIMAL

##### 1.2.3.8.3 Is Required

✅ Yes

##### 1.2.3.8.4 Is Primary Key

❌ No

##### 1.2.3.8.5 Is Unique

❌ No

##### 1.2.3.8.6 Index Type

None

##### 1.2.3.8.7 Size

0

##### 1.2.3.8.8 Constraints

- NON_NEGATIVE

##### 1.2.3.8.9 Default Value

0.00

##### 1.2.3.8.10 Is Foreign Key

❌ No

##### 1.2.3.8.11 Precision

18

##### 1.2.3.8.12 Scale

2

#### 1.2.3.9.0 INT

##### 1.2.3.9.1 Name

totalPropertiesAcquired

##### 1.2.3.9.2 Type

🔹 INT

##### 1.2.3.9.3 Is Required

✅ Yes

##### 1.2.3.9.4 Is Primary Key

❌ No

##### 1.2.3.9.5 Is Unique

❌ No

##### 1.2.3.9.6 Index Type

None

##### 1.2.3.9.7 Size

0

##### 1.2.3.9.8 Constraints

- NON_NEGATIVE

##### 1.2.3.9.9 Default Value

0

##### 1.2.3.9.10 Is Foreign Key

❌ No

##### 1.2.3.9.11 Precision

0

##### 1.2.3.9.12 Scale

0

#### 1.2.3.10.0 DECIMAL

##### 1.2.3.10.1 Name

largestTradeDealValue

##### 1.2.3.10.2 Type

🔹 DECIMAL

##### 1.2.3.10.3 Is Required

✅ Yes

##### 1.2.3.10.4 Is Primary Key

❌ No

##### 1.2.3.10.5 Is Unique

❌ No

##### 1.2.3.10.6 Index Type

None

##### 1.2.3.10.7 Size

0

##### 1.2.3.10.8 Constraints

- NON_NEGATIVE

##### 1.2.3.10.9 Default Value

0.00

##### 1.2.3.10.10 Is Foreign Key

❌ No

##### 1.2.3.10.11 Precision

18

##### 1.2.3.10.12 Scale

2

#### 1.2.3.11.0 DateTime

##### 1.2.3.11.1 Name

updatedAt

##### 1.2.3.11.2 Type

🔹 DateTime

##### 1.2.3.11.3 Is Required

✅ Yes

##### 1.2.3.11.4 Is Primary Key

❌ No

##### 1.2.3.11.5 Is Unique

❌ No

##### 1.2.3.11.6 Index Type

None

##### 1.2.3.11.7 Size

0

##### 1.2.3.11.8 Constraints

*No items available*

##### 1.2.3.11.9 Default Value

CURRENT_TIMESTAMP

##### 1.2.3.11.10 Is Foreign Key

❌ No

##### 1.2.3.11.11 Precision

0

##### 1.2.3.11.12 Scale

0

### 1.2.4.0.0 Primary Keys

- playerStatisticId

### 1.2.5.0.0 Unique Constraints

- {'name': 'UC_PlayerStatistic_ProfileId', 'columns': ['profileId']}

### 1.2.6.0.0 Indexes

*No items available*

## 1.3.0.0.0 GameResult

### 1.3.1.0.0 Name

GameResult

### 1.3.2.0.0 Description

Records the outcome and high-level statistics of a single completed game session. Used for game summaries (REQ-1-070) and the Top Scores list (REQ-1-091).

### 1.3.3.0.0 Attributes

#### 1.3.3.1.0 Guid

##### 1.3.3.1.1 Name

gameResultId

##### 1.3.3.1.2 Type

🔹 Guid

##### 1.3.3.1.3 Is Required

✅ Yes

##### 1.3.3.1.4 Is Primary Key

✅ Yes

##### 1.3.3.1.5 Is Unique

✅ Yes

##### 1.3.3.1.6 Index Type

UniqueIndex

##### 1.3.3.1.7 Size

0

##### 1.3.3.1.8 Constraints

*No items available*

##### 1.3.3.1.9 Default Value



##### 1.3.3.1.10 Is Foreign Key

❌ No

##### 1.3.3.1.11 Precision

0

##### 1.3.3.1.12 Scale

0

#### 1.3.3.2.0 Guid

##### 1.3.3.2.1 Name

profileId

##### 1.3.3.2.2 Type

🔹 Guid

##### 1.3.3.2.3 Is Required

✅ Yes

##### 1.3.3.2.4 Is Primary Key

❌ No

##### 1.3.3.2.5 Is Unique

❌ No

##### 1.3.3.2.6 Index Type

Index

##### 1.3.3.2.7 Size

0

##### 1.3.3.2.8 Constraints

*No items available*

##### 1.3.3.2.9 Default Value



##### 1.3.3.2.10 Is Foreign Key

✅ Yes

##### 1.3.3.2.11 Precision

0

##### 1.3.3.2.12 Scale

0

#### 1.3.3.3.0 BOOLEAN

##### 1.3.3.3.1 Name

didHumanWin

##### 1.3.3.3.2 Type

🔹 BOOLEAN

##### 1.3.3.3.3 Is Required

✅ Yes

##### 1.3.3.3.4 Is Primary Key

❌ No

##### 1.3.3.3.5 Is Unique

❌ No

##### 1.3.3.3.6 Index Type

Index

##### 1.3.3.3.7 Size

0

##### 1.3.3.3.8 Constraints

*No items available*

##### 1.3.3.3.9 Default Value

false

##### 1.3.3.3.10 Is Foreign Key

❌ No

##### 1.3.3.3.11 Precision

0

##### 1.3.3.3.12 Scale

0

#### 1.3.3.4.0 INT

##### 1.3.3.4.1 Name

gameDurationSeconds

##### 1.3.3.4.2 Type

🔹 INT

##### 1.3.3.4.3 Is Required

✅ Yes

##### 1.3.3.4.4 Is Primary Key

❌ No

##### 1.3.3.4.5 Is Unique

❌ No

##### 1.3.3.4.6 Index Type

None

##### 1.3.3.4.7 Size

0

##### 1.3.3.4.8 Constraints

- NON_NEGATIVE

##### 1.3.3.4.9 Default Value

0

##### 1.3.3.4.10 Is Foreign Key

❌ No

##### 1.3.3.4.11 Precision

0

##### 1.3.3.4.12 Scale

0

#### 1.3.3.5.0 INT

##### 1.3.3.5.1 Name

totalTurns

##### 1.3.3.5.2 Type

🔹 INT

##### 1.3.3.5.3 Is Required

✅ Yes

##### 1.3.3.5.4 Is Primary Key

❌ No

##### 1.3.3.5.5 Is Unique

❌ No

##### 1.3.3.5.6 Index Type

None

##### 1.3.3.5.7 Size

0

##### 1.3.3.5.8 Constraints

- NON_NEGATIVE

##### 1.3.3.5.9 Default Value

0

##### 1.3.3.5.10 Is Foreign Key

❌ No

##### 1.3.3.5.11 Precision

0

##### 1.3.3.5.12 Scale

0

#### 1.3.3.6.0 DateTime

##### 1.3.3.6.1 Name

endTimestamp

##### 1.3.3.6.2 Type

🔹 DateTime

##### 1.3.3.6.3 Is Required

✅ Yes

##### 1.3.3.6.4 Is Primary Key

❌ No

##### 1.3.3.6.5 Is Unique

❌ No

##### 1.3.3.6.6 Index Type

Index

##### 1.3.3.6.7 Size

0

##### 1.3.3.6.8 Constraints

*No items available*

##### 1.3.3.6.9 Default Value

CURRENT_TIMESTAMP

##### 1.3.3.6.10 Is Foreign Key

❌ No

##### 1.3.3.6.11 Precision

0

##### 1.3.3.6.12 Scale

0

#### 1.3.3.7.0 DECIMAL

##### 1.3.3.7.1 Name

humanFinalNetWorth

##### 1.3.3.7.2 Type

🔹 DECIMAL

##### 1.3.3.7.3 Is Required

❌ No

##### 1.3.3.7.4 Is Primary Key

❌ No

##### 1.3.3.7.5 Is Unique

❌ No

##### 1.3.3.7.6 Index Type

None

##### 1.3.3.7.7 Size

0

##### 1.3.3.7.8 Constraints

*No items available*

##### 1.3.3.7.9 Default Value



##### 1.3.3.7.10 Is Foreign Key

❌ No

##### 1.3.3.7.11 Precision

18

##### 1.3.3.7.12 Scale

2

### 1.3.4.0.0 Primary Keys

- gameResultId

### 1.3.5.0.0 Unique Constraints

*No items available*

### 1.3.6.0.0 Indexes

- {'name': 'idx_gameresult_profile_win_ts', 'columns': ['profileId', 'didHumanWin', 'endTimestamp'], 'type': 'BTree'}

## 1.4.0.0.0 GameParticipant

### 1.4.1.0.0 Name

GameParticipant

### 1.4.2.0.0 Description

Represents a single player's (human or AI) performance in a specific completed game, linked to a GameResult. Fulfills REQ-1-070.

### 1.4.3.0.0 Attributes

#### 1.4.3.1.0 Guid

##### 1.4.3.1.1 Name

gameParticipantId

##### 1.4.3.1.2 Type

🔹 Guid

##### 1.4.3.1.3 Is Required

✅ Yes

##### 1.4.3.1.4 Is Primary Key

✅ Yes

##### 1.4.3.1.5 Is Unique

✅ Yes

##### 1.4.3.1.6 Index Type

UniqueIndex

##### 1.4.3.1.7 Size

0

##### 1.4.3.1.8 Constraints

*No items available*

##### 1.4.3.1.9 Default Value



##### 1.4.3.1.10 Is Foreign Key

❌ No

##### 1.4.3.1.11 Precision

0

##### 1.4.3.1.12 Scale

0

#### 1.4.3.2.0 Guid

##### 1.4.3.2.1 Name

gameResultId

##### 1.4.3.2.2 Type

🔹 Guid

##### 1.4.3.2.3 Is Required

✅ Yes

##### 1.4.3.2.4 Is Primary Key

❌ No

##### 1.4.3.2.5 Is Unique

❌ No

##### 1.4.3.2.6 Index Type

Index

##### 1.4.3.2.7 Size

0

##### 1.4.3.2.8 Constraints

*No items available*

##### 1.4.3.2.9 Default Value



##### 1.4.3.2.10 Is Foreign Key

✅ Yes

##### 1.4.3.2.11 Precision

0

##### 1.4.3.2.12 Scale

0

#### 1.4.3.3.0 VARCHAR

##### 1.4.3.3.1 Name

participantName

##### 1.4.3.3.2 Type

🔹 VARCHAR

##### 1.4.3.3.3 Is Required

✅ Yes

##### 1.4.3.3.4 Is Primary Key

❌ No

##### 1.4.3.3.5 Is Unique

❌ No

##### 1.4.3.3.6 Index Type

None

##### 1.4.3.3.7 Size

16

##### 1.4.3.3.8 Constraints

*No items available*

##### 1.4.3.3.9 Default Value



##### 1.4.3.3.10 Is Foreign Key

❌ No

##### 1.4.3.3.11 Precision

0

##### 1.4.3.3.12 Scale

0

#### 1.4.3.4.0 BOOLEAN

##### 1.4.3.4.1 Name

isHuman

##### 1.4.3.4.2 Type

🔹 BOOLEAN

##### 1.4.3.4.3 Is Required

✅ Yes

##### 1.4.3.4.4 Is Primary Key

❌ No

##### 1.4.3.4.5 Is Unique

❌ No

##### 1.4.3.4.6 Index Type

None

##### 1.4.3.4.7 Size

0

##### 1.4.3.4.8 Constraints

*No items available*

##### 1.4.3.4.9 Default Value

false

##### 1.4.3.4.10 Is Foreign Key

❌ No

##### 1.4.3.4.11 Precision

0

##### 1.4.3.4.12 Scale

0

#### 1.4.3.5.0 INT

##### 1.4.3.5.1 Name

aiDifficulty

##### 1.4.3.5.2 Type

🔹 INT

##### 1.4.3.5.3 Is Required

❌ No

##### 1.4.3.5.4 Is Primary Key

❌ No

##### 1.4.3.5.5 Is Unique

❌ No

##### 1.4.3.5.6 Index Type

None

##### 1.4.3.5.7 Size

0

##### 1.4.3.5.8 Constraints

*No items available*

##### 1.4.3.5.9 Default Value



##### 1.4.3.5.10 Is Foreign Key

❌ No

##### 1.4.3.5.11 Precision

0

##### 1.4.3.5.12 Scale

0

#### 1.4.3.6.0 DECIMAL

##### 1.4.3.6.1 Name

finalNetWorth

##### 1.4.3.6.2 Type

🔹 DECIMAL

##### 1.4.3.6.3 Is Required

✅ Yes

##### 1.4.3.6.4 Is Primary Key

❌ No

##### 1.4.3.6.5 Is Unique

❌ No

##### 1.4.3.6.6 Index Type

Index

##### 1.4.3.6.7 Size

0

##### 1.4.3.6.8 Constraints

*No items available*

##### 1.4.3.6.9 Default Value

0.00

##### 1.4.3.6.10 Is Foreign Key

❌ No

##### 1.4.3.6.11 Precision

18

##### 1.4.3.6.12 Scale

2

#### 1.4.3.7.0 INT

##### 1.4.3.7.1 Name

propertiesOwnedCount

##### 1.4.3.7.2 Type

🔹 INT

##### 1.4.3.7.3 Is Required

✅ Yes

##### 1.4.3.7.4 Is Primary Key

❌ No

##### 1.4.3.7.5 Is Unique

❌ No

##### 1.4.3.7.6 Index Type

None

##### 1.4.3.7.7 Size

0

##### 1.4.3.7.8 Constraints

- NON_NEGATIVE

##### 1.4.3.7.9 Default Value

0

##### 1.4.3.7.10 Is Foreign Key

❌ No

##### 1.4.3.7.11 Precision

0

##### 1.4.3.7.12 Scale

0

#### 1.4.3.8.0 DECIMAL

##### 1.4.3.8.1 Name

totalRentCollected

##### 1.4.3.8.2 Type

🔹 DECIMAL

##### 1.4.3.8.3 Is Required

✅ Yes

##### 1.4.3.8.4 Is Primary Key

❌ No

##### 1.4.3.8.5 Is Unique

❌ No

##### 1.4.3.8.6 Index Type

None

##### 1.4.3.8.7 Size

0

##### 1.4.3.8.8 Constraints

- NON_NEGATIVE

##### 1.4.3.8.9 Default Value

0.00

##### 1.4.3.8.10 Is Foreign Key

❌ No

##### 1.4.3.8.11 Precision

18

##### 1.4.3.8.12 Scale

2

#### 1.4.3.9.0 BOOLEAN

##### 1.4.3.9.1 Name

isWinner

##### 1.4.3.9.2 Type

🔹 BOOLEAN

##### 1.4.3.9.3 Is Required

✅ Yes

##### 1.4.3.9.4 Is Primary Key

❌ No

##### 1.4.3.9.5 Is Unique

❌ No

##### 1.4.3.9.6 Index Type

None

##### 1.4.3.9.7 Size

0

##### 1.4.3.9.8 Constraints

*No items available*

##### 1.4.3.9.9 Default Value

false

##### 1.4.3.9.10 Is Foreign Key

❌ No

##### 1.4.3.9.11 Precision

0

##### 1.4.3.9.12 Scale

0

### 1.4.4.0.0 Primary Keys

- gameParticipantId

### 1.4.5.0.0 Unique Constraints

- {'name': 'UC_GameParticipant_Result_Name', 'columns': ['gameResultId', 'participantName']}

### 1.4.6.0.0 Indexes

#### 1.4.6.1.0 BTree

##### 1.4.6.1.1 Name

IX_GameParticipant_GameResultId

##### 1.4.6.1.2 Columns

- gameResultId

##### 1.4.6.1.3 Type

🔹 BTree

#### 1.4.6.2.0 BTree

##### 1.4.6.2.1 Name

idx_gameparticipant_networth_desc

##### 1.4.6.2.2 Columns

- finalNetWorth

##### 1.4.6.2.3 Type

🔹 BTree

## 1.5.0.0.0 SavedGame

### 1.5.1.0.0 Name

SavedGame

### 1.5.2.0.0 Description

Stores metadata for a saved game state file, linking it to a player profile. The actual game state is in a separate file. Fulfills REQ-1-085, REQ-1-086, REQ-1-087.

### 1.5.3.0.0 Attributes

#### 1.5.3.1.0 Guid

##### 1.5.3.1.1 Name

savedGameId

##### 1.5.3.1.2 Type

🔹 Guid

##### 1.5.3.1.3 Is Required

✅ Yes

##### 1.5.3.1.4 Is Primary Key

✅ Yes

##### 1.5.3.1.5 Is Unique

✅ Yes

##### 1.5.3.1.6 Index Type

UniqueIndex

##### 1.5.3.1.7 Size

0

##### 1.5.3.1.8 Constraints

*No items available*

##### 1.5.3.1.9 Default Value



##### 1.5.3.1.10 Is Foreign Key

❌ No

##### 1.5.3.1.11 Precision

0

##### 1.5.3.1.12 Scale

0

#### 1.5.3.2.0 Guid

##### 1.5.3.2.1 Name

profileId

##### 1.5.3.2.2 Type

🔹 Guid

##### 1.5.3.2.3 Is Required

✅ Yes

##### 1.5.3.2.4 Is Primary Key

❌ No

##### 1.5.3.2.5 Is Unique

❌ No

##### 1.5.3.2.6 Index Type

Index

##### 1.5.3.2.7 Size

0

##### 1.5.3.2.8 Constraints

*No items available*

##### 1.5.3.2.9 Default Value



##### 1.5.3.2.10 Is Foreign Key

✅ Yes

##### 1.5.3.2.11 Precision

0

##### 1.5.3.2.12 Scale

0

#### 1.5.3.3.0 INT

##### 1.5.3.3.1 Name

slotNumber

##### 1.5.3.3.2 Type

🔹 INT

##### 1.5.3.3.3 Is Required

✅ Yes

##### 1.5.3.3.4 Is Primary Key

❌ No

##### 1.5.3.3.5 Is Unique

❌ No

##### 1.5.3.3.6 Index Type

Index

##### 1.5.3.3.7 Size

0

##### 1.5.3.3.8 Constraints

- RANGE_1_TO_5

##### 1.5.3.3.9 Default Value

0

##### 1.5.3.3.10 Is Foreign Key

❌ No

##### 1.5.3.3.11 Precision

0

##### 1.5.3.3.12 Scale

0

#### 1.5.3.4.0 VARCHAR

##### 1.5.3.4.1 Name

saveName

##### 1.5.3.4.2 Type

🔹 VARCHAR

##### 1.5.3.4.3 Is Required

❌ No

##### 1.5.3.4.4 Is Primary Key

❌ No

##### 1.5.3.4.5 Is Unique

❌ No

##### 1.5.3.4.6 Index Type

None

##### 1.5.3.4.7 Size

100

##### 1.5.3.4.8 Constraints

*No items available*

##### 1.5.3.4.9 Default Value

Game Save

##### 1.5.3.4.10 Is Foreign Key

❌ No

##### 1.5.3.4.11 Precision

0

##### 1.5.3.4.12 Scale

0

#### 1.5.3.5.0 DateTime

##### 1.5.3.5.1 Name

saveTimestamp

##### 1.5.3.5.2 Type

🔹 DateTime

##### 1.5.3.5.3 Is Required

✅ Yes

##### 1.5.3.5.4 Is Primary Key

❌ No

##### 1.5.3.5.5 Is Unique

❌ No

##### 1.5.3.5.6 Index Type

Index

##### 1.5.3.5.7 Size

0

##### 1.5.3.5.8 Constraints

*No items available*

##### 1.5.3.5.9 Default Value

CURRENT_TIMESTAMP

##### 1.5.3.5.10 Is Foreign Key

❌ No

##### 1.5.3.5.11 Precision

0

##### 1.5.3.5.12 Scale

0

#### 1.5.3.6.0 VARCHAR

##### 1.5.3.6.1 Name

gameVersion

##### 1.5.3.6.2 Type

🔹 VARCHAR

##### 1.5.3.6.3 Is Required

✅ Yes

##### 1.5.3.6.4 Is Primary Key

❌ No

##### 1.5.3.6.5 Is Unique

❌ No

##### 1.5.3.6.6 Index Type

None

##### 1.5.3.6.7 Size

20

##### 1.5.3.6.8 Constraints

*No items available*

##### 1.5.3.6.9 Default Value



##### 1.5.3.6.10 Is Foreign Key

❌ No

##### 1.5.3.6.11 Precision

0

##### 1.5.3.6.12 Scale

0

### 1.5.4.0.0 Primary Keys

- savedGameId

### 1.5.5.0.0 Unique Constraints

- {'name': 'UC_SavedGame_Profile_Slot', 'columns': ['profileId', 'slotNumber']}

### 1.5.6.0.0 Indexes

- {'name': 'idx_savedgame_profile_ts_desc', 'columns': ['profileId', 'saveTimestamp'], 'type': 'BTree'}

# 2.0.0.0.0 Relations

## 2.1.0.0.0 OneToOne

### 2.1.1.0.0 Name

PlayerProfile_PlayerStatistic

### 2.1.2.0.0 Id

REL_PLAYERPROFILE_PLAYERSTATISTIC_001

### 2.1.3.0.0 Source Entity

PlayerProfile

### 2.1.4.0.0 Target Entity

PlayerStatistic

### 2.1.5.0.0 Type

🔹 OneToOne

### 2.1.6.0.0 Source Multiplicity

1

### 2.1.7.0.0 Target Multiplicity

1

### 2.1.8.0.0 Cascade Delete

✅ Yes

### 2.1.9.0.0 Is Identifying

✅ Yes

### 2.1.10.0.0 On Delete

Cascade

### 2.1.11.0.0 On Update

Cascade

### 2.1.12.0.0 Join Table

#### 2.1.12.1.0 Name

FK_PlayerStatistic_PlayerProfile

#### 2.1.12.2.0 Columns

- {'name': 'profileId', 'type': 'Guid', 'references': 'PlayerProfile.profileId'}

## 2.2.0.0.0 OneToMany

### 2.2.1.0.0 Name

PlayerProfile_GameResults

### 2.2.2.0.0 Id

REL_PLAYERPROFILE_GAMERESULT_001

### 2.2.3.0.0 Source Entity

PlayerProfile

### 2.2.4.0.0 Target Entity

GameResult

### 2.2.5.0.0 Type

🔹 OneToMany

### 2.2.6.0.0 Source Multiplicity

1

### 2.2.7.0.0 Target Multiplicity

0..*

### 2.2.8.0.0 Cascade Delete

✅ Yes

### 2.2.9.0.0 Is Identifying

❌ No

### 2.2.10.0.0 On Delete

Cascade

### 2.2.11.0.0 On Update

Cascade

### 2.2.12.0.0 Join Table

#### 2.2.12.1.0 Name

FK_GameResult_PlayerProfile

#### 2.2.12.2.0 Columns

- {'name': 'profileId', 'type': 'Guid', 'references': 'PlayerProfile.profileId'}

## 2.3.0.0.0 OneToMany

### 2.3.1.0.0 Name

PlayerProfile_SavedGames

### 2.3.2.0.0 Id

REL_PLAYERPROFILE_SAVEDGAME_001

### 2.3.3.0.0 Source Entity

PlayerProfile

### 2.3.4.0.0 Target Entity

SavedGame

### 2.3.5.0.0 Type

🔹 OneToMany

### 2.3.6.0.0 Source Multiplicity

1

### 2.3.7.0.0 Target Multiplicity

0..*

### 2.3.8.0.0 Cascade Delete

✅ Yes

### 2.3.9.0.0 Is Identifying

❌ No

### 2.3.10.0.0 On Delete

Cascade

### 2.3.11.0.0 On Update

Cascade

### 2.3.12.0.0 Join Table

#### 2.3.12.1.0 Name

FK_SavedGame_PlayerProfile

#### 2.3.12.2.0 Columns

- {'name': 'profileId', 'type': 'Guid', 'references': 'PlayerProfile.profileId'}

## 2.4.0.0.0 OneToMany

### 2.4.1.0.0 Name

GameResult_GameParticipants

### 2.4.2.0.0 Id

REL_GAMERESULT_GAMEPARTICIPANT_001

### 2.4.3.0.0 Source Entity

GameResult

### 2.4.4.0.0 Target Entity

GameParticipant

### 2.4.5.0.0 Type

🔹 OneToMany

### 2.4.6.0.0 Source Multiplicity

1

### 2.4.7.0.0 Target Multiplicity

1..*

### 2.4.8.0.0 Cascade Delete

✅ Yes

### 2.4.9.0.0 Is Identifying

✅ Yes

### 2.4.10.0.0 On Delete

Cascade

### 2.4.11.0.0 On Update

Cascade

### 2.4.12.0.0 Join Table

#### 2.4.12.1.0 Name

FK_GameParticipant_GameResult

#### 2.4.12.2.0 Columns

- {'name': 'gameResultId', 'type': 'Guid', 'references': 'GameResult.gameResultId'}

