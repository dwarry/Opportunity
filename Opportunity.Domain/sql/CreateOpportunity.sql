DECLARE @ownerId INT = 2
DECLARE @userId INT = @ownerId
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
    @userId
)