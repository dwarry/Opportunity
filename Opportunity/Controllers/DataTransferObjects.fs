module Opportunity.DataTransferObjects

open System
open System.ComponentModel.DataAnnotations

[<CLIMutable>]
type Application = {
    OpportunityId: int
    
    UserId: int
    
    IsSubmitted: bool

    ApplicationDate: Nullable<DateTime>
    
    [<StringLength(1024)>]
    ApplicationText: string
    
    MeetingTime: Nullable<DateTime>

    MeetingDuration: Nullable<int>

    IsSuccessful: Nullable<bool>

    [<StringLength(400)>]
    OwnerComments: string

    UpdatedAt: DateTime

    [<StringLength(32)>]
    UpdatedBy: string
}

[<CLIMutable>]
type NewApplication = {
    OpportunityId: int
    
    UserId: int
    
    IsSubmitted: bool

    ApplicationDate: Nullable<DateTime>
    
    [<StringLength(1024)>]
    ApplicationText: string
}

[<CLIMutable>]
type ApplicationMeetingRequest = {
    OpportunityId: int
    
    UserId: int
    
    MeetingTime: Nullable<DateTime>

    MeetingDuration: Nullable<int>

    [<StringLength(400)>]
    OwnerComments: string
}

[<CLIMutable>]
type ApplicationOutcome = {
    OpportunityId: int
    
    UserId: int
    
    IsSuccessful: Nullable<bool>

    [<StringLength(400)>]
    OwnerComments: string
}

[<CLIMutable>]
type Category = {
    Id: int


    [<Required; StringLength(64)>]
    Name: string

    [<Required; StringLength(32)>]
    Icon: string
}

[<CLIMutable>]
type NewInitiative = {
    Name: string
    Description: string
    Link: string
    LogoUrl: string
    StartDate: DateTime
    EndDate: DateTime
    OrganizationalUnitId: Nullable<int>
}

[<CLIMutable>]
type Initiative = {
    Id: int

    [<Required; StringLength(50)>]
    Name: string

    [<StringLength(200)>]
    Description: string
    
    [<StringLength(100)>]
    Link: string
    
    [<StringLength(80)>]
    LogoUrl: string

    StartDate: DateTime
    
    EndDate: DateTime

    OrganizationalUnitId: int
    
    UpdatedAt: DateTime
    
    [<StringLength(32)>]
    UpdatedBy: string

    Version: string
}

[<CLIMutable>]
type UserDetails = {
    Id: int
    Account: string
    FirstName: string
    FamilyName: string
    Email: string
    ProfileUrl: string
    PhotoUrl: string
    HasOpenApplications: bool
    HasOpenOpportunities: bool
    CanManageOpportunities: bool
}
