namespace Opportunity

open System
open System.Web

/// <summary>
/// Handles CORS pre-flight OPTIONS requests 
/// </summary>
/// <remarks>
/// Adapted from https://stackoverflow.com/questions/31290317/handling-cors-preflight-in-asp-net-web-api
/// </remarks>
type CorsPreflightOptionModule() =
    

    interface IHttpModule with
        member this.Init(context: HttpApplication) = 
            let handler = fun (sender: obj) (arg: EventArgs) ->
                              let app = sender :?> HttpApplication
                              if app.Request.HttpMethod = "OPTIONS" then
                                  let resp = app.Response
                                  resp.AddHeader("Access-Control-Allow-Headers", "content-type")
                                  resp.AddHeader("Access-Control-Allow-Origin", "http://localhost:9000")
                                  resp.AddHeader("Access-Control-Allow-Credentials", "true")
                                  resp.AddHeader("Access-Control-Allow-Methods", "GET,DELETE,OPTIONS,POST,PUT")
                                  resp.AddHeader("Content-Type", "application/json")
                                  resp.StatusCode <- 200
                                  resp.Flush()
                              ()
            context.BeginRequest.AddHandler(new EventHandler(handler))
        
        member this.Dispose() = ()
            
