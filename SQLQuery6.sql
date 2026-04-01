-- Step 1: drop PK
ALTER TABLE RiskScores DROP CONSTRAINT PK_RiskScores;
 
-- Step 2: convert column
ALTER TABLE RiskScores
ALTER COLUMN ScoreId INT NOT NULL;
 ALTER TABLE RiskScores
ADD CONSTRAINT PK_RiskScores PRIMARY KEY (ScoreId);
 -- Add new column with identity
ALTER TABLE RiskScores
ADD TempId INT IDENTITY(1,1);
 
-- Drop old PK
ALTER TABLE RiskScores DROP CONSTRAINT PK_RiskScores;
 
-- Drop old column
ALTER TABLE RiskScores DROP COLUMN ScoreId;
 
-- Rename new column
EXEC sp_rename 'RiskScores.TempId', 'ScoreId', 'COLUMN';
 
-- Add PK again
ALTER TABLE RiskScores
ADD CONSTRAINT PK_RiskScores PRIMARY KEY (ScoreId);
 