module Opportunity.Validation
    open System
    open Microsoft.FSharp.Quotations

    type 'e Test = Test of ('e -> (string*string) option) 
   
    let CreateValidator (f: 'e -> ('e Test list -> 'e Test list)) =  
        let entries = f Unchecked.defaultof<_> []
        fun entity -> List.choose (fun (Test test) -> test entity) entries

    let private add (propQ:'x Expr) args message fx (xs: 'e Test list) = 
        let propName, eval =
            match propQ with
            | Patterns.PropertyGet (_,p,_) -> p.Name, fun x -> p.GetValue(x,[||])
            | Patterns.Value (_, ty) when ty = typeof<'e> -> "x", box 
            | _ -> failwith "Unsupported expression"
        let test entity =
            let value = eval entity
            if fx (unbox value) then None
            else  Some (propName, String.Format(message, Array.ofList (value::args)))
        Test(test) :: xs
    
    let email propQ = 
        let regex = Text.RegularExpressions.Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
        add propQ [] "Please enter a valid email address" regex.IsMatch
         
    let required propQ = 
        add propQ [] "Is a required field" (String.IsNullOrWhiteSpace >> not)
    
    let between propQ min max = 
        add propQ [box min; box max] "Must be between {1} and {2}"  
            (fun v -> v >= min && v <= max)
    