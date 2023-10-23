USE [S-Squared-Enterprises]

IF EXISTS
(
    SELECT 1
    FROM [INFORMATION_SCHEMA].[ROUTINES] WITH (NOLOCK)
    WHERE [ROUTINE_NAME] = 'GetManagers'
    AND [ROUTINE_TYPE] = 'PROCEDURE'
)
BEGIN
	DROP PROCEDURE [GetManagers]
END

CREATE PROCEDURE [dbo].[GetManagers]
AS
SELECT DISTINCT [Manager]
INTO #ManagerGuidList
FROM [dbo].[Employees]

SELECT *
FROM [dbo].[Employees] [e]
INNER JOIN [#ManagerGuidList] [mgl]
ON [mgl].[Manager] = [e].[ID]