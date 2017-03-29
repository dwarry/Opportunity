DECLARE @pageSize INT = 10, @pageIndex INT = 0, @endDate DATE = GETDATE()
SELECT
			Id,
			Name,
			Description,
			Link,
			LogoUrl,
			StartDate,
			EndDate,
			OrganizationalUnitId
FROM		Initiative
WHERE		EndDate >= @endDate
ORDER BY	StartDate DESC
OFFSET		(@pageSize * @pageIndex) ROWS
FETCH NEXT	@pageSize ROWS ONLY
