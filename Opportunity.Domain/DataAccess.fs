module Opportunity.Domain.DataAccess

open FSharp.Data

open System

open System.Data
open System.Data.SqlClient

open System.DirectoryServices
open System.DirectoryServices.AccountManagement

[<Literal>]
let private sqlFolder = __SOURCE_DIRECTORY__ + "\\SQL"

let connectionString = System.Configuration.ConfigurationManager.ConnectionStrings.["Opportunity"].ConnectionString

type private DB = SqlProgrammabilityProvider<"name=Opportunity", 
                                             ConfigFile="DesignTime.config">


type private GetCategories = SqlCommandProvider<"GetCategories.sql",
                                                "name=Opportunity",
                                                ConfigFile="DesignTime.config",
                                                ResolutionFolder=sqlFolder >

type GetCategoriesRecord = GetCategories.Record

type private GetOrgUnits = SqlCommandProvider<"GetOrgUnits.sql", 
                                              "name=Opportunity", 
                                              ConfigFile="DesignTime.config", 
                                              ResolutionFolder=sqlFolder >

type GetOrgUnitsRecord = GetOrgUnits.Record

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


type private CreateInitiative = SqlCommandProvider<"CreateInitiative.sql",
                                                   "name=Opportunity",
                                                   ConfigFile="DesignTime.config",
                                                   ResolutionFolder=sqlFolder,
                                                   SingleRow=true>


type private DeleteInitiative = SqlCommandProvider<"DeleteInitiative.sql",
                                                   "name=Opportunity",
                                                   SingleRow=true,
                                                   ConfigFile="DesignTime.config",
                                                   ResolutionFolder=sqlFolder >



let private doInTransaction<'TResult> (isolationLevel: IsolationLevel) 
                                      (action: SqlTransaction -> bool * 'TResult) =
    use conn = new SqlConnection(connectionString)
    conn.Open()
    use tran = conn.BeginTransaction(isolationLevel)
    let shouldCommit, result = action tran
    if shouldCommit then tran.Commit() else tran.Rollback()
    result 

let getCategories () = 
    doInTransaction IsolationLevel.ReadCommitted
                    (fun tran -> let conn = tran.Connection
                                 use cmd = new GetCategories(conn, transaction = tran)
                                 (true, cmd.Execute () |> Seq.toArray ) )
let getOrgUnits () = 
    doInTransaction IsolationLevel.ReadCommitted
                    (fun tran -> let conn = tran.Connection
                                 use cmd = new GetOrgUnits(conn, transaction = tran)
                                 (true, cmd.Execute () |> Seq.toArray ) )

let createUser (accountName: string) 
               (firstName:string) 
               (familyName:string)
               (emailAddress:string)
               (profileUrl:string)
               (imageUrl:string)
               (updatedBy:string) =
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
              
    
    
let createInitiative (name: string) 
                     (description: string)
                     (link: string)
                     (logoUrl: string)
                     (startDate: DateTime)
                     (endDate: DateTime)
                     (updatedBy: string)
                     (orgUnitId: int) =
    doInTransaction IsolationLevel.ReadCommitted
                    (fun tran -> let conn = tran.Connection
                                 use cmd = new CreateInitiative(conn, transaction=tran)
                                 let newId = cmd.Execute(name, 
                                                         description, 
                                                         link, 
                                                         logoUrl, 
                                                         startDate, 
                                                         endDate, 
                                                         updatedBy, 
                                                         orgUnitId)

                                 match newId with
                                 | Some x -> (true, newId)
                                 | _      -> (false, None))

let deleteInitiative (id: int) = 
    doInTransaction IsolationLevel.ReadCommitted
                    (fun tran -> let conn = tran.Connection
                                 use cmd = new DeleteInitiative(conn, transaction = tran)
                                 let result = cmd.Execute(id)
                                 match result with
                                 | Some (Some x) -> (true, Some x)
                                 | _             -> (false, None))

