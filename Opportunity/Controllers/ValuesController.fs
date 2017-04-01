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

