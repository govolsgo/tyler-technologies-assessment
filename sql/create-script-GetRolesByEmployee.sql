USE [S-Squared-Enterprises]

IF NOT EXISTS
(
    SELECT 1
    FROM [INFORMATION_SCHEMA].[ROUTINES] WITH (NOLOCK)
    WHERE [ROUTINE_NAME] = 'GetRolesByEmployee'
    AND [ROUTINE_TYPE] = 'PROCEDURE'
)

BEGIN
    CREATE PROCEDURE [dbo].[GetRolesByEmployee]
    (
        @EmployeeGUID UNIQUEIDENTIFIER
    )
    AS
        SELECT *
        FROM [dbo].[RoleMappings]
        WHERE [UserID] = @EmployeeGUID
END