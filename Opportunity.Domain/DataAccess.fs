module Opportunity.Domain.DataAccess

open FSharp.Data

[<Literal>]
let private sqlFolder = __SOURCE_DIRECTORY__ + "\\SQL"

type private DB = SqlProgrammabilityProvider<"name=Opportunity", 
                                             ConfigFile="DesignTime.config">

type private GetUser = SqlCommandProvider<"GetUser.sql", 
                                          "name=Opportunity", 
                                          ConfigFile="DesignTime.config",
                                          SingleRow=true,
                                          ResolutionFolder=sqlFolder>

type private InsertUser = SqlCommandProvider<"InsertUser.sql",
                                             "name=Opportunity",
                                             ConfigFile="DesignTime.config",
                                             SingleRow=true,
                                             ResolutionFolder=sqlFolder>

type private GetActiveInitiatives = SqlCommandProvider<"GetActiveInitiatives.sql",
                                                       "name=Opportunity",
                                                       ConfigFile="DesignTime.config",
                                                       ResolutionFolder=sqlFolder>



let getUser (accountName: string) = 
    use cmd = new GetUser()
    cmd.Execute(accountName)

let createUser (accountName: string) 
               (firstName:string) 
               (familyName:string)
               (emailAddress:string)
               (profileUrl:string)
               (imageUrl:string)
               (updatedBy:string) = 
    use cmd = new InsertUser()
    let now = System.DateTime.Now
    cmd.Execute(accountName, firstName, familyName, emailAddress, profileUrl, imageUrl, now, updatedBy)
    