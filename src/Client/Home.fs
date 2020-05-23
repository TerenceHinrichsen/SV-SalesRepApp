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
    
  let view (state : State) =
    Mui.paper [
      Mui.card [
        Mui.cardContent [
          Mui.typography [
            typography.variant.h5
            typography.color.secondary
            typography.children("Welcome, please wait for all items to finish loading before proceeding!")
          ]
          Mui.typography(
            sprintf "Number of areas loaded: %s" (
                match state.AreaList with
                | Some x -> x.Length.ToString()
                | None -> "No areas loaded"))
          Mui.typography(
            sprintf "Number of groups loaded: %s" (
                match state.GroupList with
                | Some x -> x.Length.ToString()
                | None -> "No groups loaded"))
          Mui.typography(
            sprintf "Number of pricelists loaded: %s" (
                match state.PriceListList with
                | Some x -> x.Length.ToString()
                | None -> "No pricelists loaded"))
          Mui.typography(
            sprintf "Number of sales reps loaded: %s" (
                match state.SalesRepList with
                | Some x -> x.Length.ToString()
                | None -> "No Sales reps loaded"))
          Mui.typography(
            sprintf "Number of market segments loaded: %s" (
                match state.SalesRepList with
                | Some x -> x.Length.ToString()
                | None -> "No market segments loaded"))
          Mui.typography(
            sprintf "Number of rep visit frequencies loaded: %s" (
                match state.RepVisitFrequencies with
                | Some x -> x.Length.ToString()
                | None -> "No rep visit frequencies loaded"))

        ]
      ]
    ]
    