namespace Components

open Feliz
open Feliz.MaterialUI
open Components

module CustomerTable =

  let customerRow (customer : Shared.Customer) viewFunction editFunction =
    let backgroundColour =
        match customer.AccountStatus with
        | Some "ACTIVE" -> "green"
        | Some "INACTIVE" -> "red"
        | _ -> "None"

    Mui.tableRow[
      tableRow.classes.root backgroundColour
      tableRow.children [
        Mui.tableCell [ tableCell.children [
          Buttons.primaryButton customer.CustomerId "VIEW" viewFunction
          Buttons.secondaryButton customer.CustomerId "EDIT" editFunction ] ]
        Mui.tableCell [ tableCell.children [ customer.CustomerAccountNumber ] ]
        Mui.tableCell [ tableCell.children [ customer.CustomerAccountName |> Option.defaultValue "" ] ]
        Mui.tableCell [ tableCell.children [ (if customer.CustomerIsOnHold then "Yes" else "" ) ] ]
        Mui.tableCell [ tableCell.children [ customer.AccountStatus |> Option.defaultValue "" ] ]
        Mui.tableCell [ tableCell.children [ customer.Telephone     |> Option.defaultValue "" ] ]
        Mui.tableCell [ tableCell.children [ customer.Suburb |> Option.defaultValue "" ] ]
        Mui.tableCell [ tableCell.children [ customer.ContactPerson |> Option.defaultValue "" ] ]
      ] ]

  let customerList (customers: Shared.Customer seq) viewFunction editFunction =
    Mui.table [
      table.stickyHeader true
      table.size.small
      table.children [
        Mui.tableHead [
          tableHead.classes.root "tableHead"
          tableHead.children [
            Mui.tableRow [
              tableRow.children [
                Mui.tableCell [ tableCell.variant.head; tableCell.children ["Actions"] ]
                Mui.tableCell [ tableCell.variant.head; tableCell.children ["Customer Code"] ]
                Mui.tableCell [ tableCell.variant.head; tableCell.children ["Customer Name"] ]
                Mui.tableCell [ tableCell.variant.head; tableCell.children ["On hold?"] ]
                Mui.tableCell [ tableCell.variant.head; tableCell.children ["Status"] ]
                Mui.tableCell [ tableCell.variant.head; tableCell.children ["Telephone"] ]
                Mui.tableCell [ tableCell.variant.head; tableCell.children ["Suburb"] ]
                Mui.tableCell [ tableCell.variant.head; tableCell.children ["Contact Person"] ]
          ] ] ] ]
        Mui.tableBody [
          tableBody.children (
              customers
              |> Seq.map (fun customer -> customerRow customer (viewFunction customer.CustomerId) (editFunction customer.CustomerId)))
          ]
      ] ]