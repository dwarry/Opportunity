--DECLARE @ownerName NVARCHAR(32) = 'SILICON\davew'
declare @userId INT;
declare @userName NVARCHAR(32) = @ownerName;

SELECT @userId = Id
FROM   dbo.[User]
WHERE  [AccountName] = @userName;

INSERT INTO opportunity 
(
    OwnerId, 
    OrganizationalUnitId,
    InitiativeId,
    Title,
    Description,
    EstimatedWorkload,
    Outcomes,
    StartDate, 
    EndDate,
    Vacancies, 
    FurtherDetailsUrl, 
    CategoryId,
    UpdatedAt,
    UpdatedBy
)
OUTPUT INSERTED.Id
VALUES 
(
    @userId,
    @orgUnitId,
    @initiativeId,
    @title,
    @description,
    @estimatedWorkload,
    @outcomes,
    @startDate, 
    @endDate,
    @vacancies,
    @furtherDetailsUrl,
    @categoryId,
    SYSDATETIME(),
    @userName
)