module Opportunity.Domain.UserDataAccess

open FSharp.Data

open System

open System.Data
open System.Data.SqlClient

open System.DirectoryServices
open System.DirectoryServices.AccountManagement

open Opportunity.Domain.DataAccess

type private GetUser = SqlCommandProvider<"GetUser.sql", 
                                          "name=Opportunity", 
                                          ConfigFile="DesignTime.config",
                                          SingleRow=true,
                                          ResolutionFolder=DataAccess.sqlFolder>
type GetUserRecord = GetUser.Record

type private InsertUser = SqlCommandProvider<"InsertUser.sql",
                                             "name=Opportunity",
                                             ConfigFile="DesignTime.config",
                                             SingleRow=true,
                                             ResolutionFolder=DataAccess.sqlFolder>

let private doInTransaction = DataAccess.doInTransaction

let createUser (accountName: string) 
               (firstName:string) 
               (familyName:string)
               (emailAddress:string)
               (profileUrl:string)
               (imageUrl:string)
               (updatedBy:string): int option =
    doInTransaction IsolationLevel.ReadCommitted
                    (fun tran -> let conn = tran.Connection
                                 use cmd = new InsertUser(conn, transaction = tran)
                                 let newId = cmd.Execute(accountName, firstName, familyName, emailAddress, profileUrl, imageUrl, DateTime.Now, updatedBy)
                                 match newId with
                                 | Some id -> (true, newId)
                                 | _       -> (false, None))

    
let rec getUser (accountName: string) = 
    let createUserFromActiveDirectory () = 
        // TODO: get the details from AD
        use context = new PrincipalContext(ContextType.Domain)
        let up = UserPrincipal.FindByIdentity(context, accountName)
        if Object.ReferenceEquals(up, null) then
            let firstName = up.GivenName
            let familyName = up.Surname
            let email = up.EmailAddress
            let profile = sprintf "http://mysite/person.aspx?accountname=%s" (System.Net.WebUtility.UrlEncode(accountName)) 
            let image = sprintf "http://mysite/User%%20Photos/Profile%%20Pictures/%sp_LThumb.jpg" 
                                (accountName.Split([|'\\'|]) |> Array.tryLast |> Option.get)

            createUser accountName firstName familyName email profile image accountName |> ignore
        else
            failwith ("Unknown user: " + accountName)

        
    use cmd = new GetUser()
    let result = cmd.Execute(accountName)
    match result with
    | Some x -> x
    | None -> createUserFromActiveDirectory () 
              getUser accountName
