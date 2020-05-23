namespace DatabaseInterface 
module Area = 
  open Configuration
  open System.Data.SqlClient
  open Dapper

  type Area = {
    AreaId: int
    AreaCode: string
    AreaDescription: string
  }

  let FetchAllFromDatabase = 
    Helpers.registerOptionTypes()
    let sql = SqlScripts.AreaSelect
    let connection = new SqlConnection(sqlConnectionString)
    do connection.Open()
    printfn "Connection to database: %A" connection.State
    let transaction = connection.BeginTransaction("ReadAllAreas")
    connection.Query<Area>(sql, transaction = transaction) |> List.ofSeq

