--declare @accountName NVARCHAR(32) = 'SILICON\davew', @pageSize INT = 10, @pageIndex INT = 0;

DECLARE  @size INT = @pageSize;
DECLARE  @index INT = @pageIndex;
DECLARE  @offset INT = @size * @index;
SELECT		opp.[Id], 
			opp.[Title], 
			opp.[Description], 
			opp.[StartDate], 
			opp.[EndDate], 
			opp.[CategoryId], 
			(SELECT COUNT(*) FROM dbo.[Application] app WHERE app.OpportunityId = opp.Id ) AS ApplicationCount,
			(SELECT COUNT(*) FROM dbo.[Application] app WHERE app.OpportunityId = opp.Id AND app.IsSuccessful = 1) as SuccessfulCount
FROM		dbo.[opportunity] opp
INNER JOIN  dbo.[User] usr on opp.[OwnerId] = usr.[Id]
WHERE		usr.[AccountName] = @accountName
ORDER BY	EndDate DESC
OFFSET		@offset ROWS
FETCH NEXT	@size ROWS ONLY
