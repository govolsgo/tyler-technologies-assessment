USE [S-Squared-Enterprises]

IF EXISTS
(
    SELECT 1
    FROM [INFORMATION_SCHEMA].[ROUTINES] WITH (NOLOCK)
    WHERE [ROUTINE_NAME] = 'AddRoleMapping'
    AND [ROUTINE_TYPE] = 'PROCEDURE'
)
BEGIN
	DROP PROCEDURE [AddRoleMapping]
END
GO

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
    RoleID
)
VALUES
(
    @ID,
    @Employee,
    @Role
)