USE [S-Squared-Enterprises]

IF NOT EXISTS
(
    SELECT 1
    FROM [INFORMATION_SCHEMA].[ROUTINES] WITH (NOLOCK)
    WHERE [ROUTINE_NAME] = 'AddRoleMapping'
    AND [ROUTINE_TYPE] = 'PROCEDURE'
)

BEGIN
    CREATE PROCEDURE [dbo].[AddRoleMapping]
    (
        @ID UNIQUEIDENTIFIER,
        @Employee UNIQUEIDENTIFIER,
        @Role UNIQUEIDENTIFIER
    )
    AS
        INSERT INTO [dbo].[RoleMappings]
        (
            ID,
            UserID,
            Role
        )
        VALUES
        (
            @ID,
            @Employee,
            @Role
        )
END