--DECLARE @id INT = 1;

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
WHERE		[Id] = @id