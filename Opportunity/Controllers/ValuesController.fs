namespace Opportunity.Controllers
open System
open System.Collections.Generic
open System.Linq
open System.Net.Http
open System.Web.Http

open Opportunity.Domain
open Opportunity.DataTransferObjects

/// Retrieves values.
[<Authorize>]
[<RoutePrefix("api/values")>]
type ValuesController() =
    inherit ApiController()
    let values = [|"value1";"value2"|]

    /// Gets all values.
    [<Route("")>]
    member x.Get(): IHttpActionResult = 
        let acc = x.User.Identity.Name
        let domUser = DataAccess.getUser acc
        match domUser with
        | None -> x.NotFound () :> _
        | Some u -> x.Ok({UserDetails.id = u.Id
                          account = u.AccountName
                          firstName = u.FirstName; 
                          familyName=u.FamilyName; 
                          email = u.EmailAddress; 
                          profileUrl = u.ProfileUrl }) :> _

    /// Gets the value with index id.
    [<Route("{id:int}")>]
    member x.Get(id) : IHttpActionResult =
        if id > values.Length - 1 then
            x.BadRequest() :> _
        else x.Ok(values.[id]) :> _