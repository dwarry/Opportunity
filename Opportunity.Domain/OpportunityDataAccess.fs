module Opportunity.Domain.OpportunityDataAccess

open FSharp.Data

open System

open System.Data
open System.Data.SqlClient

open Opportunity.Domain.DataAccess


let private doInTransaction = DataAccess.doInTransaction


type private GetOpportunity = SqlCommandProvider<"GetOpportunity.sql", "name=Opportunity", SingleRow=true, ConfigFile="DesignTime.config", ResolutionFolder=sqlFolder >

type private GetOpportunityRecord = GetOpportunity.Record

let getOpportunity (id: int): GetOpportunityRecord option = 
    doInTransaction IsolationLevel.ReadCommitted
                    (fun tran -> let conn = tran.Connection
                                 use cmd = new GetOpportunity(conn, transaction = tran)
                                 (true, cmd.Execute (id)))



type private GetMyOpportunities = SqlCommandProvider<"GetMyOpportunities.sql", "name=Opportunity", ConfigFile="DesignTime.config", ResolutionFolder=sqlFolder >

type private GetMyOpportunitiesRecord = GetMyOpportunities.Record

let getMyOpportunities (pageSize:int) (pageIndex: int) (userName:string): GetMyOpportunitiesRecord[] option = 
    doInTransaction IsolationLevel.ReadCommitted
                    (fun tran -> let conn = tran.Connection
                                 use cmd = new GetMyOpportunities(conn, transaction = tran)
                                 (true, cmd.Execute (pageSize, pageIndex, userName) |> Seq.toArray |> Some) )



type private GetOpenOpportunities = SqlCommandProvider<"GetOpenOpportunities.sql", "name=Opportunity", ConfigFile="DesignTime.config", ResolutionFolder=sqlFolder >

type private GetOpenOpportunitiesRecord = GetOpenOpportunities.Record

let getOpenOpportunities (pageSize:int) (pageIndex: int) (userName:string) (asAt:DateTime): GetOpenOpportunitiesRecord[] option= 
    doInTransaction IsolationLevel.ReadCommitted
                    (fun tran -> let conn = tran.Connection
                                 use cmd = new GetOpenOpportunities(conn, transaction = tran)
                                 (true, cmd.Execute (pageSize, pageIndex, asAt, userName) |> Seq.toArray |> Some) )



type private CreateOpportunity = SqlCommandProvider<"CreateOpportunity.sql", "name=Opportunity", AllParametersOptional = true, SingleRow=true, ConfigFile="DesignTime.config", ResolutionFolder=sqlFolder >


let createOpportunity (ownerName:string)
                      (orgUnitId:int)
                      (initiativeId:int option)
                      (title:string)
                      (description:string)
                      (estimatedWorkload:string)
                      (outcomes:string)
                      (startDate:DateTime)
                      (endDate:DateTime)
                      (vacancies:string)
                      (categoryId: int) : int option = 
    doInTransaction IsolationLevel.ReadCommitted
                    (fun tran -> let conn = tran.Connection
                                 use cmd = new CreateOpportunity(conn, transaction = tran)
                                 (true, cmd.Execute (Some ownerName,
                                                     Some orgUnitId,
                                                     initiativeId,
                                                     Some title,
                                                     Some description,
                                                     Some estimatedWorkload,
                                                     Some outcomes,
                                                     Some startDate,
                                                     Some endDate,
                                                     Some vacancies,
                                                     None,
                                                     Some categoryId)))



type private GetTagsForOpportunity = SqlCommandProvider<"GetTagsForOpportunity.sql", "name=Opportunity", ConfigFile="DesignTime.config", ResolutionFolder=sqlFolder >


let getTagsForOpportunity (opportunityId: int): string[] option = 
    doInTransaction IsolationLevel.ReadCommitted
                    (fun tran -> let conn = tran.Connection
                                 use cmd = new GetTagsForOpportunity(conn, transaction = tran)
                                 (true, cmd.Execute (opportunityId) |> Seq.toArray |> Some ) )


let setTagsForOpportunity (opportunityId: int) (tags: string[]) = 
    doInTransaction IsolationLevel.RepeatableRead
                    (fun tran -> let conn = tran.Connection
                                 use cmd = new DB.dbo.SetTagsForOpportunity(conn, tran)
                                 let tagt = Array.map (fun t -> new DB.dbo.``User-Defined Table Types``.TagTable(t)) tags
                                 let retVal = cmd.Execute(opportunityId, tagt)
                                 if retVal <> 0 then failwithf "[SetTagsForOpportunity] returned %d" retVal
                                 (true, Some 1)) 
    |> ignore
    