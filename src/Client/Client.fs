module Client

open Elmish
open Elmish.React
open Fable.React
open Fable.React.Props
open Thoth.Json
open Feliz
open Feliz.MaterialUI
open Components
open API

open Shared
open Pages
open Drawer

type PageState =
    | HomePageState of Home.State
    | ListingPageState of Listings.State
    | CustomerEditState of CustomerEdit.State
    | CustomerViewState of CustomerView.State
    | CustomerCreateState of CustomerCreate.State
    | ChangeHistoryState of ChangeHistory.State
    | LoginPageState of Login.State
    | NotFoundState

type SearchFields = {
  AreaList: Area list option
  GroupList: Group list option
  PriceListList: PriceList list option
  SalesRepList: SalesRep list option
  RepVisitFrequencies : string list option
  MarketSegments : string list option }

type State = {
    DrawerState: Drawer.State
    PageState: PageState
    SearchFields : SearchFields option
    UserAuthenticated : bool
    LoggedOnUser : Shared.User
    Loading : bool
  }

type Message =
    | DrawerMessage              of Drawer.Message
    | HomePageMessage            of Drawer.MenuItem
    | ListingsMessage            of Listings.Message
    | CustomerEditMessage        of CustomerEdit.Message
    | CustomerCreateMessage      of CustomerCreate.Message
    | CustomerViewMessage        of CustomerView.Message
    | LoginPageMessage           of Login.Message
    | AreaListUpdated            of Area list
    | GroupListUpdated           of Group list
    | PriceListListUpdated       of PriceList list
    | SalesRepListUpdate         of SalesRep list
    | MarketSegmentsUpdated      of string list
    | RepVisitFrequenciesUpdated of string list
    | UserLoggedOn

let init () : State * Cmd<Message> =
    let initialDrawerState, _ = Drawer.init()
    let initialLoginPageState = Login.init
    let initialModel = {
        UserAuthenticated = false
        LoggedOnUser = { UserId = 0; Username = "Unknown"}
        DrawerState = initialDrawerState
        PageState = LoginPageState initialLoginPageState
        SearchFields = None
        Loading = false }
    initialModel, Cmd.none

let update (msg : Message) (currentState : State) : State * Cmd<Message> =
  match msg, currentState.PageState with
  | HomePageMessage msg, HomePageState state ->
    match msg with
    | MenuItem.Home -> currentState, Cmd.ofMsg <| DrawerMessage (ChangeMenu MenuItem.Home)
    | MenuItem.CustomerCreate -> currentState, Cmd.ofMsg <| DrawerMessage (ChangeMenu MenuItem.CustomerCreate)
    | MenuItem.CustomerListings -> currentState, Cmd.ofMsg <| DrawerMessage (ChangeMenu MenuItem.CustomerListings)
    | MenuItem.ChangeHistory -> currentState, Cmd.ofMsg <| DrawerMessage (ChangeMenu MenuItem.ChangeHistory)
  | LoginPageMessage msg, LoginPageState state ->
    let nextState, nextCommand = Login.update msg state
    match nextState.LoginSuccess with
    | Some success ->
        if success then
          let initHome, _  = Home.init()
          {currentState with PageState = HomePageState initHome; LoggedOnUser = { UserId = 0; Username = nextState.Username}; UserAuthenticated = true} ,
          Cmd.OfAsync.perform Server.api.getAreaList () AreaListUpdated
        else { currentState with PageState = LoginPageState { nextState with LoginSuccess = None }}, Toast.errorMessage 5000 (System.Exception("Login failed"))
    | None -> { currentState with PageState = LoginPageState nextState }, Cmd.map Message.LoginPageMessage nextCommand
  | DrawerMessage drawerMessage, _  ->
    let nextDrawerState, nextDrawerCommand = Drawer.update drawerMessage currentState.DrawerState
    match nextDrawerState.CurrentMenu with
    | MenuItem.Home ->
      let homeState, _ =
        match currentState.SearchFields with
        | Some x -> {
          Home.State.AreaList = x.AreaList
          Home.State.GroupList = x.GroupList
          Home.State.PriceListList = x.PriceListList
          Home.State.SalesRepList = x.SalesRepList
          Home.State.MarketSegmentList = x.MarketSegments
          Home.State.RepVisitFrequencies = x.RepVisitFrequencies }, Cmd.none
        | None -> Home.init()
      { currentState with PageState = HomePageState homeState; DrawerState = nextDrawerState }, nextDrawerCommand
    | MenuItem.CustomerListings ->
      let listingState, _ =
        match currentState.SearchFields with
          | Some x -> {
            Listings.State.isLoading = false
            Listings.State.AreaList = x.AreaList            |> Option.get
            Listings.State.GroupList = x.GroupList          |> Option.get
            Listings.State.PriceListList = x.PriceListList  |> Option.get
            Listings.State.SalesRepList = x.SalesRepList    |> Option.get
            Listings.State.RepVisitFrequencies = x.RepVisitFrequencies |> Option.get
            Listings.State.MarketSegments = x.MarketSegments |> Option.get
            Listings.State.SelectedArea = None
            Listings.State.AreaCode = ""
            Listings.State.SelectedGroup = None
            Listings.State.GroupCode = ""
            Listings.State.SelectedRep = None
            Listings.State.RepCode = ""
            Listings.State.CustomerList = None
            Listings.State.CurrentCustomerId = None
            Listings.State.ShowEditScreen = false
            Listings.State.CustomerSearch = None
            Listings.State.ShowViewScreen = false }, Cmd.none
          | None -> Listings.init()
      { currentState with PageState = ListingPageState listingState; DrawerState = nextDrawerState }, nextDrawerCommand
    | MenuItem.CustomerCreate ->
      let createState, _ =
        match currentState.SearchFields with
          | Some x -> {
            CustomerCreate.State.isLoading                 = false
            CustomerCreate.State.AreaList                  = x.AreaList                  |> Option.get
            CustomerCreate.State.GroupList                 = x.GroupList                 |> Option.get
            CustomerCreate.State.PriceListList             = x.PriceListList             |> Option.get
            CustomerCreate.State.SalesRepList              = x.SalesRepList              |> Option.get
            CustomerCreate.State.MarketSegments            = x.MarketSegments            |> Option.get
            CustomerCreate.State.RepVisitFrequencies       = x.RepVisitFrequencies       |> Option.get
            CustomerCreate.State.SelectedArea              = None
            CustomerCreate.State.AreaCode                  = ""
            CustomerCreate.State.SelectedGroup             = None
            CustomerCreate.State.GroupCode                 = ""
            CustomerCreate.State.SelectedRep               = None
            CustomerCreate.State.RepCode                   = ""
            CustomerCreate.State.SelectedPricelist         = None
            CustomerCreate.State.PricelistCode             = ""
            CustomerCreate.State.CurrentCustomerId         = None
            CustomerCreate.State.CustomerCode              = ""
            CustomerCreate.State.DetailForm                = CustomerCreate.emptyForm
            CustomerCreate.State.SelectedRepVisitFrequency = ""
            CustomerCreate.State.SelectedMarketSegment = ""
            CustomerCreate.State.ResponseFromDatabase = None }, Cmd.none
          | None -> CustomerCreate.init()
      { currentState with PageState = CustomerCreateState createState; DrawerState = nextDrawerState }, nextDrawerCommand
    | MenuItem.ChangeHistory ->
      let state, _ = ChangeHistory.init ()
      { currentState with PageState = ChangeHistoryState state; DrawerState = nextDrawerState }, nextDrawerCommand
  | ListingsMessage msg, ListingPageState state ->
    let nextListingState, nextListingCommand = Listings.update msg state
    match nextListingState.ShowEditScreen, nextListingState.ShowViewScreen, nextListingState.CurrentCustomerId with
    | true ,_,  Some customerId ->
      let nextEditState, nextEditCommand =
        match currentState.SearchFields with
        | Some x -> {
          CustomerEdit.State.isLoading = false
          CustomerEdit.State.AreaList = x.AreaList                        |> Option.get
          CustomerEdit.State.GroupList = x.GroupList                      |> Option.get
          CustomerEdit.State.PriceListList = x.PriceListList              |> Option.get
          CustomerEdit.State.SalesRepList = x.SalesRepList                |> Option.get
          CustomerEdit.State.MarketSegments = x.MarketSegments            |> Option.get
          CustomerEdit.State.RepVisitFrequencies = x.RepVisitFrequencies  |> Option.get
          CustomerEdit.State.SelectedArea = None
          CustomerEdit.State.AreaCode = ""
          CustomerEdit.State.SelectedGroup = None
          CustomerEdit.State.GroupCode = ""
          CustomerEdit.State.SelectedRep = None
          CustomerEdit.State.RepCode = ""
          CustomerEdit.State.SelectedPricelist = None
          CustomerEdit.State.PricelistCode = ""
          CustomerEdit.State.CurrentCustomerId = None
          CustomerEdit.State.CustomerCode = ""
          CustomerEdit.State.DetailForm = CustomerEdit.emptyForm
          CustomerEdit.State.SelectedRepVisitFrequency = ""
          CustomerEdit.State.SelectedMarketSegment = ""
          CustomerEdit.State.ResponseFromDatabase = None }, Cmd.none
        | None -> CustomerEdit.init()
      { currentState with PageState = CustomerEditState nextEditState }, Cmd.ofMsg (CustomerEdit.CustomerIdFound customerId) |> Cmd.map CustomerEditMessage
    | _, true, Some customerId ->
      let nextViewState, _ = CustomerView.init()
      {currentState with PageState = CustomerViewState nextViewState }, Cmd.ofMsg (CustomerView.LoadCustomer customerId) |> Cmd.map CustomerViewMessage
    | _ , _ , _-> { currentState with PageState = ListingPageState nextListingState }, Cmd.map Message.ListingsMessage nextListingCommand
  | CustomerEditMessage msg, CustomerEditState state ->
    let nextState, nextCommand = CustomerEdit.update msg state
    { currentState with PageState = CustomerEditState nextState }, Cmd.map Message.CustomerEditMessage nextCommand
  | CustomerCreateMessage msg, CustomerCreateState state ->
    let nextState, nextCommand = CustomerCreate.update msg state
    { currentState with PageState = CustomerCreateState nextState }, Cmd.map Message.CustomerCreateMessage nextCommand
  | CustomerViewMessage msg, CustomerViewState state ->
    let nextState, nextCommand = CustomerView.update msg state
    { currentState with PageState = CustomerViewState nextState }, nextCommand |> Cmd.map CustomerViewMessage
  | AreaListUpdated areaList, HomePageState pageState ->
      { currentState with PageState = HomePageState { pageState with AreaList = Some areaList } },
      Cmd.OfAsync.perform Server.api.getGroupList () GroupListUpdated
  | GroupListUpdated groupList, HomePageState pageState ->
      { currentState with PageState = HomePageState { pageState with GroupList = Some groupList } },
      Cmd.OfAsync.perform Server.api.getPricelist () PriceListListUpdated
  | PriceListListUpdated priceListList, HomePageState pageState ->
      { currentState with PageState = HomePageState { pageState with PriceListList = Some priceListList } },
      Cmd.OfAsync.perform Server.api.getMarketSegments () MarketSegmentsUpdated
  | MarketSegmentsUpdated list, HomePageState state ->
      { currentState with PageState = HomePageState { state with MarketSegmentList = Some list } },
      Cmd.OfAsync.perform Server.api.getRepVisitFrequencies () RepVisitFrequenciesUpdated
  | RepVisitFrequenciesUpdated list, HomePageState state ->
      { currentState with PageState = HomePageState { state with RepVisitFrequencies = Some list } },
      Cmd.OfAsync.perform Server.api.getSalesRepList () SalesRepListUpdate
  | SalesRepListUpdate salesRepList, HomePageState pageState ->
      { currentState with
          PageState = HomePageState { pageState with SalesRepList = Some salesRepList }; Loading = false
          SearchFields = Some { AreaList = pageState.AreaList
                                SalesRepList = Some salesRepList
                                GroupList = pageState.GroupList
                                PriceListList = pageState.PriceListList
                                RepVisitFrequencies = pageState.RepVisitFrequencies
                                MarketSegments = pageState.MarketSegmentList }},
      Cmd.none
  | UserLoggedOn , _ -> { currentState with Loading = false; UserAuthenticated = true} , Cmd.none
  | _, _ -> currentState, Cmd.none


let view (state : State) (dispatch : Message -> unit) =
  match state.UserAuthenticated with
  | true ->
     Mui.paper [
      paper.elevation 12
      paper.children [
        AppBar.view state.DrawerState (DrawerMessage >> dispatch)
        Drawer.view state.DrawerState (DrawerMessage >> dispatch)
        Mui.paper [
            if state.Loading then
                yield Mui.typography [
                     typography.variant.h5
                     typography.color.primary
                     typography.classes.root "AppTitle"
                     typography.children("Welcome, please wait while application loads") ]
                yield Mui.circularProgress[]
            else match state.PageState with
                  | HomePageState state ->
                    match state.AreaList with
                    | Some _ -> yield Home.view state (HomePageMessage >> dispatch)
                    | None -> yield Mui.card [
                      card.variant.outlined
                      card.elevation 5
                      card.children [Mui.circularProgress []]]
                  | ListingPageState    state -> yield Listings.view       state (ListingsMessage       >> dispatch)
                  | CustomerEditState   state -> yield CustomerEdit.view   state (CustomerEditMessage   >> dispatch)
                  | CustomerCreateState state -> yield CustomerCreate.view state (CustomerCreateMessage >> dispatch)
                  | CustomerViewState   state -> yield CustomerView.view   state (CustomerViewMessage   >> dispatch)
                  | LoginPageState      state -> yield Login.view          state (LoginPageMessage      >> dispatch)
                  | ChangeHistoryState  state -> yield ChangeHistory.view  state
                  | NotFoundState             -> yield Mui.typography "This page is not available"
            ]
      ] ]
  | false ->
    if state.Loading then Mui.linearProgress[ linearProgress.color.secondary ]
    else Login.view (Login.init) (LoginPageMessage >> dispatch)

#if DEBUG
open Elmish.Debug
open Elmish.HMR
#endif

Program.mkProgram init update view
#if DEBUG
|> Program.withConsoleTrace
#endif
|> Program.withReactBatched "success-app"
#if DEBUG
|> Program.withDebugger
#endif
|> Program.run
