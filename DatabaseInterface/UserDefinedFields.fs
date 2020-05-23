module UserDefinedFields

open Configuration
open Dapper
open Helpers
open System.Data.SqlClient

let MarketSegments = 
  registerOptionTypes()
  let sql = SqlScripts.MarketSegments
  let connection = new SqlConnection(sqlConnectionString)
  do connection.Open()
  let transaction = connection.BeginTransaction("ReadAllCustomers")
  let answer = connection.Query<string>(sql, transaction = transaction) |> Seq.head
  let separator = [|';'|]
  answer.Split(separator,50)

let RepVisitFrequencies = 
  registerOptionTypes()
  let sql = SqlScripts.RepVisitFrequency
  let connection = new SqlConnection(sqlConnectionString)
  do connection.Open()
  let transaction = connection.BeginTransaction("ReadAllCustomers")
  let answer = connection.Query<string>(sql, transaction = transaction) |> Seq.head
  let separator = [|';'|]
  answer.Split(separator,50)