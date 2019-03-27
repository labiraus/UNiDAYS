CREATE PROCEDURE [dbo].[usp_UpdateUser]
	@Id VARCHAR(50),
	@UserName VARCHAR(MAX),
	@NormalizedUserName VARCHAR(MAX),
	@PasswordHash VARCHAR(MAX),
	@IsActive BIT
AS
	UPDATE Users 
	SET 
		UserName = @UserName, 
		NormalizedUserName = @NormalizedUserName,
		PasswordHash = @PasswordHash,
		IsActive = @IsActive
	WHERE Id = @Id
RETURN 'Success'
