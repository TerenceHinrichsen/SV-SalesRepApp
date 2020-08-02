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
        { Version = "1.0.2"; Change = """
        * Quantity on sales graph changed to Boxes instead of dozens.
        * Added new Customers screen - add manage new customer accounts screen.
        * Changed font on App title to Star wars font.
        * Changed login credentials verification (removed bug)
        """; DateTime = System.DateTime(2020,06,21)}
        { Version = "1.0.3"; Change = "Added Reminders to customer View"; DateTime = System.DateTime(2020,07,20)}
        { Version = "1.0.4"; Change = "Added ability to search for specific customer based on a search 'string'"; DateTime = System.DateTime(2020,07,28)}
        { Version = "1.0.5"; Change = "Added Toastr to enhance user feedback on completed / failed requests"; DateTime = System.DateTime(2020,07,29)}
        { Version = "1.0.6"; Change = "Modifying customers - move to auto approval of non-critical changes"; DateTime = System.DateTime(2020,08,01)}

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
    
