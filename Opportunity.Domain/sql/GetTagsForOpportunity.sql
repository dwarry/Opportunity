--DECLARE @id INT = 1;
SELECT t.Name
FROM OpportunityTag ot 
INNER JOIN Tag t on t.Id = ot.TagId
WHERE ot.OpportunityId = @id
