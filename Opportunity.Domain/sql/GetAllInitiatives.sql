--DECLARE @pageSize INT = 10, @pageIndex INT = 0, @asAtDate DATE = GETDATE()
declare  @size INT = @pageSize;
declare  @index INT = @pageIndex;
declare  @offset INT = @size * @index;

SELECT
            [Id],
            [Name],
            [Description],
            [Link],
            [LogoUrl],
            [StartDate],
            [EndDate],
            [OrganizationalUnitId],
            [UpdatedAt], 
            [UpdatedBy],
            [Version]
FROM		Initiative
ORDER BY	[StartDate] DESC
OFFSET		@offset ROWS
FETCH NEXT	@size ROWS ONLY
