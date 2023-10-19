USE [S-Squared-Enterprises]

IF NOT EXISTS
(
    SELECT 1
    FROM [INFORMATION_SCHEMA].[TABLES] WITH (NOLOCK)
    WHERE [TABLE_NAME] = N'S-Squared-Enterprises'
)

BEGIN
    CREATE TABLE [Employees]
    (
        [ID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
        [EmployeeID] VARCHAR(100) NOT NULL,
        [FirstName] VARCHAR(100) NOT NULL,
        [LastName] VARCHAR(100) NOT NULL,
        [Manager] UNIQUEIDENTIFIER NOT NULL,
        [Roles] UNIQUEIDENTIFIER NOT NULL
    )
END