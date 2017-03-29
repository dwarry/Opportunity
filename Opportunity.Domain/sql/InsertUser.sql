
--DECLARE @AccountName NVARCHAR(32) = N'Test',
--        @FirstName   NVARCHAR(32) = N'Test',
--		@FamilyName  NVARCHAR(32) = N'TEST',
--		@EmailAddress NVARCHAR(50) = N'Test@example.com',
--		@ProfileUrl   NVARCHAR(80) = N'https://profile.example.com/Test',
--		@ImageUrl     NVARCHAR(80) = N'https://images.example.com/Test',
--		@UpdatedAt    DATETIME2(7) = SYSDATETIME(),
--		@UpdatedBy    NVARCHAR(32) = N'TEST';

INSERT INTO [User]
(
	AccountName, FirstName, FamilyName, EmailAddress, ProfileUrl, ImageUrl, UpdatedAt, UpdatedBy
)
OUTPUT Inserted.Id
VALUES
(
	@accountName, @firstName, @familyName, @emailAddress, @profileUrl, @imageUrl, @updatedAt, @updatedBy
)