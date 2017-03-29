--use Opportunity
--declare @accountName NVARCHAR(50) 
--set @accountName = 'SILICON\davew'
DECLARE @today DATE = GETDATE()

SELECT  
        u.Id, 
        u.AccountName, 
        u.FirstName, 
        u.FamilyName, 
        u.EmailAddress, 
        u.ProfileUrl,
        u.ImageUrl,
        (SELECT TOP 1 o.Id
           FROM dbo.Opportunity o 
          WHERE o.OwnerId = u.Id 
            AND o.EndDate >= @today) as OpId,
        (SELECT TOP 1 a.OpportunityId
           FROM dbo.Application a 
         INNER JOIN dbo.Opportunity o2 on o2.Id = a.OpportunityId
         WHERE a.UserId = u.Id AND o2.EndDate >= @today) as AppId 
    FROM    dbo.[User] u
LEFT JOIN dbo.[Opportunity] o on o.OwnerId = u.Id
WHERE u.AccountName=@accountName