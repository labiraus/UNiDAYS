CREATE PROCEDURE [dbo].[usp_CreateUser]
	@Id VARCHAR(50),
	@UserName VARCHAR(MAX),
	@NormalizedUserName VARCHAR(MAX),
	@PasswordHash VARCHAR(MAX),
	@IsActive BIT
AS
	MERGE Users as target
	using (SELECT @Id AS Id, @UserName as UserName, @NormalizedUserName AS NormalizedUserName, @PasswordHash AS PasswordHash, @IsActive AS IsActive) as source
	ON (target.NormalizedUserName = source.NormalizedUserName OR target.UserName = source.UserName)
	WHEN NOT MATCHED THEN INSERT (Id, UserName, NormalizedUserName, PasswordHash, IsActive) VALUES (source.Id, source.UserName, source.NormalizedUserName, source.PasswordHash, source.IsActive);
	SELECT Id, UserName, NormalizedUserName, IsActive FROM Users WHERE Id = @Id
