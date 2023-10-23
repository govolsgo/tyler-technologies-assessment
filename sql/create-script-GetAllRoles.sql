USE [S-Squared-Enterprises]

IF NOT EXISTS
(
    SELECT 1
    FROM [INFORMATION_SCHEMA].[ROUTINES] WITH (NOLOCK)
    WHERE [ROUTINE_NAME] = 'GetAllRoles'
    AND [ROUTINE_TYPE] = 'PROCEDURE'
)

BEGIN
    CREATE PROCEDURE [dbo].[GetAllRoles]
    AS
        SELECT *
        FROM [dbo].[Roles]
END