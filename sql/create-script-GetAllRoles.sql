USE [S-Squared-Enterprises]

IF EXISTS
(
    SELECT 1
    FROM [INFORMATION_SCHEMA].[ROUTINES] WITH (NOLOCK)
    WHERE [ROUTINE_NAME] = 'GetAllRoles'
    AND [ROUTINE_TYPE] = 'PROCEDURE'
)
BEGIN
	DROP PROCEDURE [GetAllRoles]
END
GO

CREATE PROCEDURE [dbo].[GetAllRoles]
AS
SELECT *
FROM [dbo].[Roles]