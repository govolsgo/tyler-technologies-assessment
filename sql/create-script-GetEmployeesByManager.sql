USE [S-Squared-Enterprises]

IF EXISTS
(
    SELECT 1
    FROM [INFORMATION_SCHEMA].[ROUTINES] WITH (NOLOCK)
    WHERE [ROUTINE_NAME] = 'GetEmployeesByManager'
    AND [ROUTINE_TYPE] = 'PROCEDURE'
)
BEGIN
	DROP PROCEDURE [GetEmployeesByManager]
END
GO

CREATE PROCEDURE [dbo].[GetEmployeesByManager]
(
    @Manager UNIQUEIDENTIFIER
)
AS
SELECT *
FROM [dbo].[Employees]
WHERE [Manager] = @Manager