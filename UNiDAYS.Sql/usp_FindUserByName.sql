CREATE PROCEDURE [dbo].[usp_FindUserByName]
	@NormalizedUserName VARCHAR(MAX)
AS
	SELECT Id, UserName, NormalizedUserName, IsActive FROM Users WHERE NormalizedUserName = @NormalizedUserName
