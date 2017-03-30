--declare @follower int = 1;

DECLARE @userId INT = @follower;

SELECT 'Initiative' as SubjectType,
       init.Id as SubjectId,
       init.Name as SubjectName,
       ifol.CreatedAt as CreatedAt
FROM InitiativeFollower ifol 
INNER JOIN Initiative init on init.Id = ifol.InitiativeId
WHERE ifol.FollowerId = @userId
UNION
SELECT 'Owner',
       oo.Id as SubjectId,
       oo.FirstName + ' ' + oo.FamilyName as SubjectName,
       oof.CreatedAt
FROM   OpportunityOwnerFollower oof
inner join [User] oo on oo.id = oof.OpportunityOwnerId
where oof.FollowerId = @userId
UNION
SELECT 'OrgUnit',
       ou.Id,
       ou.Name,
       ouf.CreatedAt
from   OrganizationalUnitFollower ouf
inner join OrganizationalUnit ou on ou.id = ouf.OrganizationalUnitId
where ouf.Followerid = @userId
