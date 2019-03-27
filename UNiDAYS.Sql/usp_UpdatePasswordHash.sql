CREATE PROCEDURE [dbo].[usp_UpdatePasswordHash]
	@Id VARCHAR(50),
	@PasswordHash VARCHAR(MAX)
AS
	UPDATE Users SET PasswordHash = @PasswordHash WHERE Id = @Id
RETURN 'Success'
