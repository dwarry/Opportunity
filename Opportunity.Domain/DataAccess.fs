module Opportunity.Domain.DataAccess

open FSharp.Data

open System

open System.Data
open System.Data.SqlClient

open System.DirectoryServices
open System.DirectoryServices.AccountManagement

[<Literal>]
let internal sqlFolder = __SOURCE_DIRECTORY__ + "\\SQL"

let internal connectionString = System.Configuration.ConfigurationManager.ConnectionStrings.["Opportunity"].ConnectionString

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


type private GetActiveInitiatives = SqlCommandProvider<"GetActiveInitiatives.sql",
                                                       "name=Opportunity",
                                                       ConfigFile="DesignTime.config",
                                                       ResolutionFolder=sqlFolder>

type GetActiveInitiativesRecord = GetActiveInitiatives.Record



type private CreateInitiative = SqlCommandProvider<"CreateInitiative.sql",
                                                   "name=Opportunity",
                                                   ConfigFile="DesignTime.config",
                                                   ResolutionFolder=sqlFolder,
                                                   SingleRow=true>

type CreateInitiativeRecord = CreateInitiative.Record

type private DeleteInitiative = SqlCommandProvider<"DeleteInitiative.sql",
                                                   "name=Opportunity",
                                                   SingleRow=true,
                                                   ConfigFile="DesignTime.config",
                                                   ResolutionFolder=sqlFolder >



let internal doInTransaction<'TResult> (isolationLevel: IsolationLevel) 
                                      (action: SqlTransaction -> bool * 'TResult) =
    use conn = new SqlConnection(connectionString)
    conn.Open()
    use tran = conn.BeginTransaction(isolationLevel)
    let shouldCommit, result = action tran
    if shouldCommit then tran.Commit() else tran.Rollback()
    result 

let getCategories (): GetCategoriesRecord[] = 
    doInTransaction IsolationLevel.ReadCommitted
                    (fun tran -> let conn = tran.Connection
                                 use cmd = new GetCategories(conn, transaction = tran)
                                 (true, cmd.Execute () |> Seq.toArray ) )

let getOrgUnits (): GetOrgUnitsRecord[] = 
    doInTransaction IsolationLevel.ReadCommitted
                    (fun tran -> let conn = tran.Connection
                                 use cmd = new GetOrgUnits(conn, transaction = tran)
                                 (true, cmd.Execute () |> Seq.toArray ) )

let getActiveInitiatives (pageSize: int) (pageIndex: int) (asAt: DateTime): GetActiveInitiativesRecord[] = 
    doInTransaction IsolationLevel.ReadCommitted
                    (fun tran -> let conn = tran.Connection
                                 use cmd = new GetActiveInitiatives(conn, transaction = tran)
                                 (true, cmd.Execute (pageSize, pageIndex, asAt.Date) |> Seq.toArray))



              
    
    
let createInitiative (name: string) 
                     (description: string)
                     (link: string)
                     (logoUrl: string)
                     (startDate: DateTime)
                     (endDate: DateTime)
                     (updatedBy: string)
                     (orgUnitId: int) : CreateInitiativeRecord option =
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
                                 | Some y -> (true, newId)
                                 | _      -> (false, None))

let deleteInitiative (id: int) = 
    doInTransaction IsolationLevel.ReadCommitted
                    (fun tran -> let conn = tran.Connection
                                 use cmd = new DeleteInitiative(conn, transaction = tran)
                                 let result = cmd.Execute(id)
                                 match result with
                                 | Some (Some x) -> (true, Some x)
                                 | _             -> (false, None))



type private GetAllInitiatives = SqlCommandProvider<"GetAllInitiatives.sql", "name=Opportunity", ConfigFile="DesignTime.config", ResolutionFolder=sqlFolder >

type private GetAllInitiativesRecord = GetAllInitiatives.Record

let getAllInitiatives (pageSize: int) (pageIndex: int): GetAllInitiativesRecord[] = 
    doInTransaction IsolationLevel.ReadCommitted
                    (fun tran -> let conn = tran.Connection
                                 use cmd = new GetAllInitiatives(conn, transaction = tran)
                                 (true, cmd.Execute(pageSize, pageIndex) |> Seq.toArray ) )


type private GetInitiative = SqlCommandProvider<"GetInitiative.sql", "name=Opportunity", SingleRow=true, ConfigFile="DesignTime.config", ResolutionFolder=sqlFolder >

type private GetInitiativeRecord = GetInitiative.Record

let getInitiative (id: int): GetInitiativeRecord option = 
    doInTransaction IsolationLevel.ReadCommitted
                    (fun tran -> let conn = tran.Connection
                                 use cmd = new GetInitiative(conn, transaction = tran)
                                 (true, cmd.Execute (id) ) )
