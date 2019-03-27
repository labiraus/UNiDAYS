CREATE PROCEDURE [dbo].[usp_GetPasswordHash]
	@Id VARCHAR(50)
AS
	SELECT PasswordHash FROM Users where Id = @Id
RETURN 0
