module User

open Configuration
open Dapper
open Helpers
open System.Data.SqlClient


let login username password = 
  registerOptionTypes()
  let sql = SqlScripts.LoginUser
  let connection = new SqlConnection(sqlConnectionString)
  do connection.Open()
  let transaction = connection.BeginTransaction("ReadAllCustomers")
  let parameters = dict ["Username" => username
                         "Password" => password ]

  connection.Query<bool>(sql, parameters,  transaction = transaction) |> List.ofSeq |> List.head