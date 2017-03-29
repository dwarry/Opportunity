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

    /// Gets all values.
    [<Route("users/current")>]
    member x.Get(): IHttpActionResult = 
        let acc = x.User.Identity.Name
        let domUser = DataAccess.getUser acc
        match domUser with
        | None -> x.NotFound () :> _
        | Some u -> x.Ok({UserDetails.Id = u.Id
                          Account = u.AccountName
                          FirstName = u.FirstName; 
                          FamilyName=u.FamilyName; 
                          Email = u.EmailAddress; 
                          ProfileUrl = u.ProfileUrl
                          PhotoUrl = u.ImageUrl
                          HasOpenApplications = Option.isSome u.AppId
                          HasOpenOpportunities = Option.isSome u.OpId
                          CanManageOpportunities = true
                           }) :> _

    [<Route("initiatives/current")>]
    member x.GetCurrentInitiatives([<Optional;DefaultParameterValue(0)>]pageIndex: Nullable<int>, 
                                   [<Optional;DefaultParameterValue(20)>]pageSize: Nullable<int>): IHttpActionResult =
        x.NotFound() :> _

    [<Route("initiatives/all")>]
    member x.GetAllInitiatives ([<Optional;DefaultParameterValue(0)>]pageIndex: Nullable<int>, 
                                [<Optional;DefaultParameterValue(20)>]pageSize: Nullable<int>): IHttpActionResult =
        x.NotFound() :> _

    [<Route("initiatives/new")>]
    member x.PostNewInitiative (initiative: NewInitiative): IHttpActionResult =
        x.InternalServerError() :> _

