module PriceList

open Configuration
open Dapper
open Helpers
open System.Data.SqlClient

type PriceList = {
  Id: int
  Name: string
  Description: string
}

let FetchAllFromDatabase = 
  registerOptionTypes()
  let sql = SqlScripts.PriceListSelect
  let connection = new SqlConnection(sqlConnectionString)
  do connection.Open()
  let transaction = connection.BeginTransaction("ReadAllCustomers")
  connection.Query<PriceList>(sql, transaction = transaction) |> List.ofSeq