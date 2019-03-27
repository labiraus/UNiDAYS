CREATE TABLE [dbo].[Users]
(
	[Id] VARCHAR(50) NOT NULL PRIMARY KEY, 
    [UserName] VARCHAR(MAX) NOT NULL, 
    [NormalizedUserName] VARCHAR(MAX) NOT NULL, 
    [PasswordHash] VARCHAR(MAX) NOT NULL, 
    [IsActive] BIT NOT NULL
)
