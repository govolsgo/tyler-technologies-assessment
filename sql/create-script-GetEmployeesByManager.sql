USE [S-Squared-Enterprises]

IF NOT EXISTS
(
    SELECT 1
    FROM [INFORMATION_SCHEMA].[ROUTINES] WITH (NOLOCK)
    WHERE [ROUTINE_NAME] = 'GetEmployeesByManager'
    AND [ROUTINE_TYPE] = 'PROCEDURE'
)

BEGIN
    CREATE PROCEDURE [dbo].[GetEmployeesByManager]
    (
        @Manager UNIQUEIDENTIFIER
    )
    AS
        SELECT *
        FROM [dbo].[Employees]
        WHERE [Manager] = @Manager
END