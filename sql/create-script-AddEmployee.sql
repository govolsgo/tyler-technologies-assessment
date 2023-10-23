USE [S-Squared-Enterprises]

IF EXISTS
(
    SELECT 1
    FROM [INFORMATION_SCHEMA].[ROUTINES] WITH (NOLOCK)
    WHERE [ROUTINE_NAME] = 'AddEmployee'
    AND [ROUTINE_TYPE] = 'PROCEDURE'
)
BEGIN
	DROP PROCEDURE [AddEmployee]
END
GO

CREATE PROCEDURE [dbo].[AddEmployee]
(
    @ID UNIQUEIDENTIFIER,
    @Manager UNIQUEIDENTIFIER = NULL,
    @EmployeeID VARCHAR(100),
    @FirstName VARCHAR(100),
    @LastName VARCHAR(100)
)
AS
INSERT INTO [dbo].[Employees]
(
    ID,
    Manager,
    EmployeeID,
    FirstName,
    LastName
)
VALUES
(
    @ID,
    @Manager,
    @EmployeeID,
    @FirstName,
    @LastName
)