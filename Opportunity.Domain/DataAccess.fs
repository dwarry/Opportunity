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

type internal DB = SqlProgrammabilityProvider<"name=Opportunity", 
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



