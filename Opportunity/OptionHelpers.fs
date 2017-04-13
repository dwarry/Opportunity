[<AutoOpen>]
module Opportunity.OptionHelpers

let defaultIfNone<'T> (defaultValue: 'T) (value: 'T option)  = 
    match value with
    | Some t -> t
    | None   -> defaultValue

