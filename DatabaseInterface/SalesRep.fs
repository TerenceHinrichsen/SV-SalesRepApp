module SalesRep

open Configuration
open Dapper

type SalesRep = {
  Id: int
  Code: string
  Description: string
}

let FetchAllFromDatabase = 
  Helpers.registerOptionTypes()
  let sql = SqlScripts.SalesRepSelect
  let connection = new System.Data.SqlClient.SqlConnection(sqlConnectionString)
  do connection.Open()
  let transaction = connection.BeginTransaction("ReadAllAreas")
  connection.Query<SalesRep> (sql, transaction = transaction) |> List.ofSeq

