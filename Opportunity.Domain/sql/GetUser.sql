--use Opportunity
--declare @accountName NVARCHAR(50) = 'Silicon\davew'

SELECT Id, AccountName, FirstName, FamilyName, EmailAddress, ProfileUrl
FROM [User]
WHERE AccountName=@accountName