namespace Opportunity.Controllers
open System
open System.Collections.Generic
open System.Linq
open System.Net.Http
open System.Web.Http

open Opportunity.Domain
open Opportunity.DataTransferObjects
open System.Runtime.InteropServices

/// Retrieves values.
[<Authorize>]
[<RoutePrefix("api")>]
type ValuesController() =
    inherit ApiController()

    [<Route("referenceData")>]
    member x.GetReferenceData (): IHttpActionResult = 
        x.Ok(Opportunity.ReferenceData.RefData) :> _

    /// Gets all values.
    [<Route("users/current")>]
    member x.Get(): IHttpActionResult = 
        let acc = x.User.Identity.Name
        let u = DataAccess.getUser acc
        x.Ok({UserDetails.Id = u.Id
              Account = u.AccountName
              FirstName = u.FirstName; 
              FamilyName=u.FamilyName; 
              Email = u.EmailAddress; 
              ProfileUrl = u.ProfileUrl
              PhotoUrl = u.ImageUrl
              HasOpenApplications = Option.isSome u.AppId
              HasOpenOpportunities = Option.isSome u.OpId
              CanManageOpportunities = true
              Following = [||]}) :> _

    [<Route("initiatives/{id:int}", Name="GetInitiative")>]
    member x.GetInitiative (id: int) =
        let init = DataAccess.GetInitiative id
        match init with
        | None -> x.NotFound()
        | Some initiative -> x.Ok()

    [<Route("initiatives/current")>]
    member x.GetCurrentInitiatives([<Optional;DefaultParameterValue(0)>]pageIndex: Nullable<int>, 
                                   [<Optional;DefaultParameterValue(20)>]pageSize: Nullable<int>): IHttpActionResult =
        let inits = DataAccess.getActiveInitiatives (pageSize.GetValueOrDefault(20)) 
                                                    (pageIndex.GetValueOrDefault(0)) 
                                                    DateTime.Today
                    |> Array.map (fun init -> {Initiative.Id = init.Id
                                               Name = init.Name
                                               Description = init.Description
                                               Link  = init.Link
                                               LogoUrl = init.LogoUrl
                                               StartDate = init.StartDate
                                               EndDate = init.EndDate
                                               OrganizationalUnitId = init.OrganizationalUnitId
                                               UpdatedAt = init.UpdatedAt
                                               UpdatedBy = init.UpdatedBy 
                                               Version = Convert.ToBase64String(init.Version) })
        x.Ok(inits) :> _
    
    
    [<Route("initiatives/all")>]
    member x.GetAllInitiatives ([<Optional;DefaultParameterValue(0)>]pageIndex: Nullable<int>, 
                                [<Optional;DefaultParameterValue(20)>]pageSize: Nullable<int>): IHttpActionResult =
        let inits = DataAccess.getAllInitiatives (pageSize.GetValueOrDefault(20)) 
                                                 (pageIndex.GetValueOrDefault(0)) 
                                                 DateTime.Today
                    |> Array.map (fun x -> {Initiative.Id = x.Id
                                            Name = x.Name
                                            Description = x.Description
                                            Link  = x.Link
                                            LogoUrl = x.LogoUrl
                                            StartDate = x.StartDate
                                            EndDate = x.EndDate
                                            OrganizationalUnitId = x.OrganizationalUnitId
                                            UpdatedAt = x.UpdatedAt
                                            UpdatedBy = x.UpdatedBy 
                                            Version = Convert.ToBase64String(x.Version) })
        x.Ok(inits) :> _

    [<Route("initiatives/new")>]
    member x.PostNewInitiative (initiative: NewInitiative): IHttpActionResult =
        let newId = DataAccess.createInitiative initiative.Name 
                                                initiative.Description
                                                initiative.Link
                                                initiative.LogoUrl
                                                initiative.StartDate
                                                initiative.EndDate
                                                x.User.Identity.Name
                                                initiative.OrganizationalUnitId

        match newId with
        | Some y -> x.Created()

        x.InternalServerError() :> _

