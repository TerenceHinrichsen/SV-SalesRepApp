namespace Pages

open Components

module Home =
  open Elmish
  open Elmish.React
  open Feliz.MaterialUI
  open Shared

  type State = {
    AreaList: Area list option
    GroupList: Group list option
    PriceListList: PriceList list option
    SalesRepList: SalesRep list option
    MarketSegmentList: string list option
    RepVisitFrequencies: string list option
  }

  type Message =
    ChangeMenu of Drawer.MenuItem

  let init () : State * _ =
    { AreaList = None
      GroupList = None
      PriceListList = None
      SalesRepList = None
      MarketSegmentList = None
      RepVisitFrequencies = None
    }, Cmd.none

  let update _ (currentState : State) : State * _ =
    currentState, Cmd.none

  let view (state : State) dispatch =
    if  state.AreaList.IsNone ||
        state.GroupList.IsNone ||
        state.PriceListList.IsNone ||
        state.SalesRepList.IsNone ||
        state.MarketSegmentList.IsNone ||
        state.RepVisitFrequencies.IsNone
    then Mui.paper [ Mui.circularProgress []; Strings.header3 "Loading . . . " ; Strings.subtitle "If this message is still displayed after 5 minutes, refresh the page to try again" ]
    else
        Mui.paper [
          Mui.card [
            Mui.cardContent [
              Mui.typography [
                typography.variant.h5
                typography.color.primary
                typography.classes.root "AppTitle"
                typography.children("Welcome, choose option or use menu on left")
              ]
              Mui.buttonGroup [
               buttonGroup.size.large
               buttonGroup.orientation.vertical
               buttonGroup.fullWidth true
               buttonGroup.classes.root "Padded"
               buttonGroup.children [
                Buttons.primaryButtonLarge "Customer listings" (fun _ -> dispatch Drawer.MenuItem.CustomerListings)
                ]]
              Mui.buttonGroup [
               buttonGroup.size.large
               buttonGroup.orientation.vertical
               buttonGroup.fullWidth true
               buttonGroup.classes.root "Padded"
               buttonGroup.children [
                Buttons.primaryButtonLarge "Add new customer" (fun _ -> dispatch Drawer.MenuItem.CustomerCreate)
                ]]
              Mui.buttonGroup [
               buttonGroup.size.large
               buttonGroup.orientation.vertical
               buttonGroup.fullWidth true
               buttonGroup.classes.root "Padded"
               buttonGroup.children [
                Buttons.primaryButtonLarge "Instructions / Help" (fun _ -> dispatch Drawer.MenuItem.ChangeHistory)
                ]]        ]
      ]
    ]
