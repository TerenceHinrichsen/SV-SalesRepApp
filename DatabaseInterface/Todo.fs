namespace DatabaseInterface 
open Configuration
open Dapper
open System.Data.SqlClient
open Helpers

module Todo = 


  let CreateNew customerId assignee message promisedDate =
    registerOptionTypes()
    let sql = SqlScripts.CreateNewTodo
    let connection = new SqlConnection(sqlConnectionString)
    do connection.Open()
    let transaction = connection.BeginTransaction("ReadOneCustomers")

    let parameters = dict [ "CustomerId"    => customerId
                            "Assignee"      => assignee
                            "Message"       => message
                            "PromisedDate"  => promisedDate ]
    try
      try
        connection.Query<unit>(sql, parameters, transaction = transaction) |> ignore
        transaction.Commit()
        "Success"
      with
        | exn -> sprintf "Could not insert into datase %A" exn
    finally
      connection.Close()
