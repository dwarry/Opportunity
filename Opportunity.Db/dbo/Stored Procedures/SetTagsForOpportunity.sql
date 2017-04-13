CREATE PROCEDURE [dbo].[SetTagsForOpportunity]
	@opportunityId int,
	@tags TagTable readonly 
AS
	SET NOCOUNT ON;

	INSERT INTO Tag
	(
		Name
	)
	SELECT Tag
	FROM   @tags oppTags
	LEFT JOIN Tag t2 on t2.Name = oppTags.Tag
	WHERE t2.Id IS NULL;	

	DELETE FROM OpportunityTag
	WHERE OpportunityId = @opportunityId;

	SET NOCOUNT ON;
	INSERT INTO OpportunityTag (OpportunityId, TagId)
	SELECT @opportunityId as OpportunityId, t.Id as TagId
	FROM Tag t
	INNER JOIN @tags newTags on t.Name = newTags.Tag;

RETURN 0