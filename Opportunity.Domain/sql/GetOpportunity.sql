--declare @id INT = 1;
SELECT	opp.[Id],
        opp.[OwnerId],
        opp.[OrganizationalUnitId], 
        opp.[InitiativeId], 
        opp.[Title],
        opp.[Description],
        opp.[EstimatedWorkload],
        opp.[Outcomes],
        opp.[StartDate], 
        opp.[EndDate],
        opp.[Vacancies],
        opp.[CategoryId],
        opp.[UpdatedAt],
        opp.[UpdatedBy],
        opp.[Version],
        init.[Name]    as InitiativeName,
        init.[Link]    as InitiativeLink,
        init.[LogoUrl] as InitiativeLogoUrl
FROM dbo.[Opportunity] opp
INNER JOIN dbo.[Initiative] init on init.Id = opp.InitiativeId 
WHERE opp.[Id] = @id
