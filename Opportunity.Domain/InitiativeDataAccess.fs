module Opportunity.Domain.InitiativeDataAccess

open FSharp.Data

open System

open System.Data
open System.Data.SqlClient

open Opportunity.Domain.DataAccess


let private doInTransaction = DataAccess.doInTransaction

type private CreateInitiative = SqlCommandProvider<"CreateInitiative.sql",
                                                   "name=Opportunity",
                                                   ConfigFile="DesignTime.config",
                                                   ResolutionFolder=sqlFolder,
                                                   SingleRow=true>

type CreateInitiativeRecord = CreateInitiative.Record

/// Creates a new Initiative.
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



type private GetActiveInitiatives = SqlCommandProvider<"GetActiveInitiatives.sql",
                                                       "name=Opportunity",
                                                       ConfigFile="DesignTime.config",
                                                       ResolutionFolder=sqlFolder>

type GetActiveInitiativesRecord = GetActiveInitiatives.Record

/// <summary>
/// Retrieves a page of Initiatives that are active on a particular date.
/// </summary>
/// <param name="pageSize">The maximum number of items to be returned</param>
/// <param name="pageIndex">Zero-based index of the page to be returned.</param>
/// <param name="asAt">Target date</param>
let getActiveInitiatives (pageSize: int) (pageIndex: int) (asAt: DateTime): GetActiveInitiativesRecord[] = 
    doInTransaction IsolationLevel.ReadCommitted
                    (fun tran -> let conn = tran.Connection
                                 use cmd = new GetActiveInitiatives(conn, transaction = tran)
                                 (true, cmd.Execute (pageSize, pageIndex, asAt.Date) |> Seq.toArray))

type private GetAllInitiatives = SqlCommandProvider<"GetAllInitiatives.sql", "name=Opportunity", ConfigFile="DesignTime.config", ResolutionFolder=sqlFolder >

type GetAllInitiativesRecord = GetAllInitiatives.Record

/// Retrieves a page of all initiatives, whether currently active or not.
let getAllInitiatives (pageSize: int) (pageIndex: int): GetAllInitiativesRecord[] = 
    doInTransaction IsolationLevel.ReadCommitted
                    (fun tran -> let conn = tran.Connection
                                 use cmd = new GetAllInitiatives(conn, transaction = tran)
                                 (true, cmd.Execute(pageSize, pageIndex) |> Seq.toArray ) )


type private GetInitiative = SqlCommandProvider<"GetInitiative.sql", "name=Opportunity", SingleRow=true, ConfigFile="DesignTime.config", ResolutionFolder=sqlFolder >

type GetInitiativeRecord = GetInitiative.Record

/// Retrieves an individual Initiative
let getInitiative (id: int): GetInitiativeRecord option = 
    doInTransaction IsolationLevel.ReadCommitted
                    (fun tran -> let conn = tran.Connection
                                 use cmd = new GetInitiative(conn, transaction = tran)
                                 (true, cmd.Execute (id) ) )



type private DeleteInitiative = SqlCommandProvider<"DeleteInitiative.sql",
                                                   "name=Opportunity",
                                                   SingleRow=true,
                                                   ConfigFile="DesignTime.config",
                                                   ResolutionFolder=sqlFolder >




              
    
    
/// Deletes an initiative
let deleteInitiative (id: int) = 
    doInTransaction IsolationLevel.ReadCommitted
                    (fun tran -> let conn = tran.Connection
                                 use cmd = new DeleteInitiative(conn, transaction = tran)
                                 let result = cmd.Execute(id)
                                 match result with
                                 | Some (Some x) -> (true, Some x)
                                 | _             -> (false, None))

