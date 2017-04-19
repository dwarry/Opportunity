--declare @initiativeId INT = 2;
declare @id INT = @initiativeId;
declare @opportunityId INT;

SELECT TOP 1 @opportunityId = Id
FROM Opportunity
WHERE InitiativeId = @id;

IF @opportunityId IS NULL
	DELETE FROM Initiative
	OUTPUT DELETED.Id
	WHERE Id = @id and Version = @version;
ELSE
	SELECT NULL;


