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
type UserController() =
    inherit ApiController()

    /// Gets all values.
    [<Route("users/current")>]
    member x.GetCurrentUser(): IHttpActionResult = 
        let acc = x.User.Identity.Name
        let u = UserDataAccess.getUser acc
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
