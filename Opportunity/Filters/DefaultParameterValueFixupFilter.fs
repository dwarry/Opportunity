namespace Opportunity.Filters

open System.Reflection
open System.Web.Http.Filters
open System.Web.Http.Controllers
open System.Runtime.InteropServices

/// From https://gist.github.com/bentayloruk/92dd56bdc0adcb6dec45
/// Responsible for populating missing action arguments from DefaultParameterValueAttribute values.
/// Created to handle this issue https://github.com/aspnet/Mvc/issues/1923
/// Note: This is for later version of System.Web.Http but could be back-ported.
type DefaultParameterValueFixupFilter() =
  inherit ActionFilterAttribute()

  /// Get list of (paramInfo, defValue) tuples for params where DefaultParameterValueAttribute is present.
  let getDefParamVals (parameters:ParameterInfo array) =
    [ for param in parameters do
        let defParamValAttrs = param.GetCustomAttributes<DefaultParameterValueAttribute>() |> List.ofSeq 
        match defParamValAttrs with
        // Review: we are ignoring null defaults.  Is this correct?
        | [x] -> if x.Value = null then () else yield param, x.Value
        | [] -> () 
        | _ -> failwithf "Multiple DefaultParameterValueAttribute on param '%s'!" param.Name
    ]
  
  /// Add action arg default values where specified in DefaultParameterValueAttribute attrs.
  let addActionArgDefsFromDefParamValAttrs (context:HttpActionContext) =  
    match context.ActionDescriptor with
    | :? ReflectedHttpActionDescriptor as ad ->
      let defParamVals = getDefParamVals (ad.MethodInfo.GetParameters())
      for (param, value) in defParamVals do
        match context.ActionArguments.TryGetValue(param.Name) with
        | true, :? System.Reflection.Missing
        | false, _ ->
          // Remove is null-op if key not found, so we handle both match cases OK.
          let _ = context.ActionArguments.Remove(param.Name)
          context.ActionArguments.Add(param.Name, value)
        | _, _ -> ()
    | _ -> ()

  /// Override adding suport for DefaultParameterValueAttribute values.
  override x.OnActionExecuting(context) =
    addActionArgDefsFromDefParamValAttrs context
    base.OnActionExecuting(context)
  
  /// Override adding suport for DefaultParameterValueAttribute values.
  override x.OnActionExecutingAsync(context, cancellationToken) =
    addActionArgDefsFromDefParamValAttrs context
    base.OnActionExecutingAsync(context, cancellationToken)