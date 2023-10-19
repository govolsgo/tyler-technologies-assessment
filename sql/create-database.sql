IF NOT EXISTS
(
    SELECT 1
    FROM [sys].[databases] WITH (NOLOCK)
    WHERE [name] = 'S-Squared-Enterprises'
)

BEGIN
    CREATE DATABASE [S-Squared-Enterprises]
END