namespace DatabaseInterface

module Group = 

  open Configuration
  open Dapper
  open System.Data.SqlClient
  open Helpers

  type Group = {
    GroupId: int
    GroupCode: string
    GroupDescription: string
  }

  let FetchAllFromDatabase =
    registerOptionTypes()
    let sql = SqlScripts.CustomerGroupSelect
    let connection = new SqlConnection(sqlConnectionString)
    do connection.Open()
    let transaction = connection.BeginTransaction("ReadAllGroups")
    connection.Query<Group>(sql, transaction = transaction) |> List.ofSeq
