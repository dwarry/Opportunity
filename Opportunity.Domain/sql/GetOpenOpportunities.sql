--declare @asAt DATE = GETDATE(), @userName NVARCHAR(32) = 'SILICON\davew', @pageSize INT = 10, @pageIndex INT = 0;

declare @size INT = @pageSize;
declare @index INT = @pageIndex;
declare @offset INT = @size * @index;
declare @dt DATE = @asAt;
declare @userId INT;

SELECT @userId = Id
FROM   dbo.[User]
WHERE  [AccountName] = @userName;


select	opp.[Id],
		opp.[Title],
		opp.[Description],
		opp.[EstimatedWorkload],
		opp.[Outcomes],
		opp.[Vacancies],
		opp.[StartDate],
		opp.[EndDate],
		opp.[InitiativeId],
		opp.[CategoryId],
		opp.[OwnerId],
		app.[IsSubmitted]
FROM		dbo.[Opportunity] opp
LEFT JOIN   dbo.[Application] app on app.[OpportunityId] = opp.Id
WHERE	opp.[StartDate] <= @dt AND opp.[EndDate] >= @dt
AND		opp.[OwnerId] <> @userId
AND		app.[UserId] = @userId
ORDER BY opp.[EndDate] DESC
OFFSET		@offset ROWS
FETCH NEXT	@size ROWS ONLY
