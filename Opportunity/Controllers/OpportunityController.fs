namespace Opportunity.Controllers
open System
open System.Collections.Generic
open System.Linq
open System.Net.Http
open System.Web.Http
open System.Web.Http.Cors

open Opportunity
open Opportunity.Domain
open Opportunity.DataTransferObjects
open Opportunity.Filters
open System.Runtime.InteropServices

/// Retrieves values.
[<Authorize>]
[<RoutePrefix("api")>]
[<DefaultParameterValueFixupFilter>]
[<EnableCors("http://localhost:9000","*", "*", SupportsCredentials=true)>]
type OpportunityController() =
    inherit ApiController()

    [<HttpGet;Route("opportunities/{id}", Name="GetOpportunity")>]
    member this.GetOpportunity (id: int): IHttpActionResult =
        let result = OpportunityDataAccess.getOpportunity id
        match result with 
        | Some opp ->  let tags = OpportunityDataAccess.getTagsForOpportunity id
                                  |> OptionHelpers.defaultIfNone Array.empty<string>
                       let init = match opp.InitiativeId with
                                  | Some x -> Some { InitiativeSummary.Id = x
                                                     Name =  opp.InitiativeName
                                                     Link =  opp.InitiativeLink
                                                     LogoUrl = opp.InitiativeLogoUrl
                                                   }
                                  | None -> None
        
                       this.Ok ({ OpportunityDetail.Id = opp.Id
                                  OrgUnitId = opp.OrganizationalUnitId
                                  Initiative = init
                                  Title = opp.Title
                                  Description = opp.Description
                                  EstimatedWorkload = opp.EstimatedWorkload
                                  Outcomes = opp.Outcomes
                                  StartDate = opp.StartDate
                                  EndDate = opp.EndDate
                                  Vacancies = opp.Vacancies
                                  CategoryId = opp.CategoryId
                                  Tags = tags
                                  UpdatedAt = opp.UpdatedAt
                                  UpdatedBy = opp.UpdatedBy
                                  Version = System.Convert.ToBase64String(opp.Version)
                                }) :> _
        | None -> this.NotFound () :> _


    [<HttpGet;Route("opportunities/my")>]
    member this.GetMyOpportunities([<Optional;DefaultParameterValue(0)>]pageIndex: Nullable<int>, 
                                   [<Optional;DefaultParameterValue(20)>]pageSize: Nullable<int>): IHttpActionResult =

        let parameters = this.Request.GetQueryNameValuePairs

        let opportunities = 
            OpportunityDataAccess.getMyOpportunities (pageSize.GetValueOrDefault(20))
                                                     (pageIndex.GetValueOrDefault(0))
                                                     this.User.Identity.Name
        
        let results = 
            match opportunities with
            | None -> Array.empty<MyOpportunity>
            | Some arr -> arr |> Array.map (fun opp -> let tags = OpportunityDataAccess.getTagsForOpportunity opp.Id
                                                                  |> OptionHelpers.defaultIfNone Array.empty<string>
                                                       { MyOpportunity.Id = opp.Id
                                                         Title = opp.Title
                                                         Description = opp.Description
                                                         StartDate = opp.StartDate
                                                         EndDate = opp.EndDate
                                                         CategoryId = opp.CategoryId
                                                         ApplicationCount = opp.ApplicationCount
                                                         SuccessfulCount = opp.SuccessfulCount
                                                         Tags = tags
                                                        })
        this.Ok (results) :> _

    [<HttpGet;Route("opportunities/open")>]
    member this.GetOpenOpportunities([<Optional;DefaultParameterValue(0)>]pageIndex: Nullable<int>, 
                                     [<Optional;DefaultParameterValue(20)>]pageSize: Nullable<int>): IHttpActionResult = 

        let parameters = this.Request.GetQueryNameValuePairs

        let opportunities = 
            OpportunityDataAccess.getOpenOpportunities (pageSize.GetValueOrDefault(20))
                                                       (pageIndex.GetValueOrDefault(0))
                                                       this.User.Identity.Name
                                                       DateTime.Today

        let results = 
            match opportunities with
            | None -> Array.empty<OpenOpportunity> 
            | Some arr -> arr |> Array.map (fun opp -> let tags = OpportunityDataAccess.getTagsForOpportunity opp.Id
                                                                  |> OptionHelpers.defaultIfNone Array.empty<string>
                                                       { OpenOpportunity.Id = opp.Id
                                                         Title = opp.Title
                                                         Description = opp.Description
                                                         EstimatedWorkload = opp.EstimatedWorkload
                                                         Outcomes= opp.Outcomes
                                                         StartDate = opp.StartDate
                                                         EndDate = opp.EndDate
                                                         Vacancies = opp.Vacancies
                                                         CategoryId = opp.CategoryId
                                                         OwnerId = opp.OwnerId
                                                         Tags = tags
                                                         IsApplicationSubmitted = opp.IsSubmitted
                                                       })

        this.Ok (results) :> _

    [<HttpPost; Route("opportunities")>]
    member this.PostNewOpportunity([<FromBody>]opportunity: NewOpportunity) : IHttpActionResult = 

        if this.ModelState.IsValid then
            let result = OpportunityDataAccess.createOpportunity this.User.Identity.Name 
                                                                 opportunity.OrgUnitId
                                                                 opportunity.InitiativeId
                                                                 opportunity.Title
                                                                 opportunity.Description
                                                                 opportunity.EstimatedWorkload
                                                                 opportunity.Outcomes
                                                                 opportunity.StartDate
                                                                 opportunity.EndDate
                                                                 opportunity.Vacancies
                                                                 opportunity.CategoryId
                                                                 opportunity.Tags
            match result with
            | Some id -> this.CreatedAtRoute("GetOpportunity", {id = id}, "") :> _
            | None -> this.InternalServerError() :> _
        else
            this.BadRequest(this.ModelState) :> _

