﻿Insert INTO Initiative
(
	Name,
	Description,
	Link,
	LogoUrl,
	StartDate,
	EndDate,
	UpdatedAt,
	UpdatedBy,
	OrganizationalUnitId

)
OUTPUT INSERTED.Id, INSERTED.UpdatedAt, INSERTED.Version
VALUES 
(
	@name,
	@description,
	@link,
	@logoUrl,
	@startDate,
	@endDate,
	SYSDATETIME(),
	@updatedBy,
	@orgUnitId	

) 
