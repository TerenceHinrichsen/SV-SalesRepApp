namespace Pages

module ChangeHistory =
  open Elmish
  open Feliz.MaterialUI
  open Components

  type ChangeData = {
   Version: string
   Change: string
   DateTime: System.DateTime }

  type State = {
    Changes: ChangeData list}

  let init () : State * _ =
    { Changes = [
        { Version = "1.0.0"; Change = "View / Edit customers added"; DateTime = System.DateTime(2020,05,31)}
        { Version = "1.0.1"; Change = "Migrated to Azure SQL database"; DateTime = System.DateTime(2020,06,10)}
    ]
    }, Cmd.none

  let changeCard (changeData: ChangeData) =
      Mui.card [
        Mui.cardContent [
          Strings.header5 changeData.Version
          Strings.body1 changeData.Change
          Strings.subtitle (changeData.DateTime.ToString("yyyy-MM-dd"))
          ] ]
      
  let view (state : State) =
    Mui.paper [
      paper.children (state.Changes |> List.rev |>List.map changeCard)
    ] 
    
