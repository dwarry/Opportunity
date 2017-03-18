module Opportunity.Domain.DataAccess

open FSharp.Data

[<Literal>]
let private sqlFolder = __SOURCE_DIRECTORY__ + "\\SQL"

type private DB = SqlProgrammabilityProvider<"name=Opportunity", ConfigFile="DesignTime.config">

type private GetUser = SqlCommandProvider<"GetUser.sql", 
                                          "name=Opportunity", 
                                          ConfigFile="DesignTime.config",
                                          SingleRow=true,
                                          ResolutionFolder=sqlFolder>

let getUser (accountName: string) = 
    use cmd = new GetUser()
    cmd.Execute(accountName)