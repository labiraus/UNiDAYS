CREATE PROCEDURE [dbo].[usp_DeleteUser]
	@Id VARCHAR(50)
AS
	DELETE Users WHERE Id = @Id
	RETURN 'Success'
