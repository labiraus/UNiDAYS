CREATE PROCEDURE [dbo].[usp_FindUserById]
	@Id VARCHAR(50)
AS
	SELECT Id, UserName, NormalizedUserName, IsActive FROM Users WHERE Id = @Id
