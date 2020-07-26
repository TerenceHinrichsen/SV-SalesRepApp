namespace Pages

open Components

module Listings =
  open Elmish
  open Elmish.React
  open Feliz.MaterialUI
  open Feliz
  open Shared
  open API

  type State = {
    isLoading: bool
    AreaList: Area list
    GroupList: Group list
    PriceListList: PriceList list
    SalesRepList: SalesRep list
    SelectedArea: int option
    AreaCode: string
    SelectedRep: int option
    RepCode: string
    SelectedGroup: int option
    GroupCode: string
    CustomerList: Customer list option
    ShowEditScreen: bool
    ShowViewScreen : bool
    CurrentCustomerId: int option
    RepVisitFrequencies : string list
    MarketSegments : string list 
    CustomerSearch : string option
  }

  type Message =
    | AreaCodeChanged of string
    | AreaCodeSelected of int
    | SalesRepChanged of string
    | SalesRepSelected of int
    | GroupChanged of string
    | GroupSelected of int
    | SearchForCustomers
    | CustomerListReceived of Customer list
    | ListsUpdated
    | ToggleEditScreen of int
    | ToggleViewScreen of int
    | CustomerSearchChanged of string

  let init () : State * _ =
    { AreaList = []
      GroupList = []
      PriceListList = []
      SalesRepList = []
      SelectedArea = None
      AreaCode = ""
      SelectedRep = None
      RepCode = ""
      SelectedGroup = None
      GroupCode = ""
      CustomerList= None
      isLoading = false
      ShowEditScreen = false
      ShowViewScreen = false
      CurrentCustomerId = None
      RepVisitFrequencies = []
      MarketSegments = []
      CustomerSearch = None
    }, Cmd.none

  let update (msg : Message) (currentState : State) : State * _ =
    match msg with
    | AreaCodeChanged   s ->  if s.Length = 0 then { currentState with SelectedArea   = None; AreaCode = ""}, Cmd.none  else { currentState with AreaCode = s}, Cmd.none
    | SalesRepChanged   s ->  if s.Length = 0 then { currentState with SelectedRep    = None; RepCode = s}, Cmd.none    else { currentState with RepCode = s}, Cmd.none
    | GroupChanged      s ->  if s.Length = 0 then { currentState with SelectedGroup  = None; GroupCode = s}, Cmd.none  else { currentState with GroupCode = s}, Cmd.none
    | AreaCodeSelected  s ->  { currentState with SelectedArea = Some s }, Cmd.none
    | SalesRepSelected  s ->  { currentState with SelectedRep = Some s }, Cmd.none
    | GroupSelected     s ->  { currentState with SelectedGroup = Some s }, Cmd.none
    | SearchForCustomers  ->  {currentState with isLoading = true }, Cmd.OfAsync.perform Server.api.getCustomerListByOneOfRepGroupArea (currentState.SelectedArea, currentState.SelectedRep, currentState.SelectedGroup) CustomerListReceived
    | CustomerListReceived s -> { currentState with CustomerList = Some s; isLoading = false}, Cmd.none
    | ToggleEditScreen s -> { currentState with ShowEditScreen = (not currentState.ShowEditScreen); CurrentCustomerId = Some s }, Cmd.none
    | ToggleViewScreen s -> { currentState with ShowViewScreen = (not currentState.ShowViewScreen); CurrentCustomerId = Some s }, Cmd.none
    | CustomerSearchChanged s -> { currentState with CustomerSearch = Some s }, Cmd.none
    | _ -> currentState, Cmd.none

  let selectedArea state =
    state.SelectedArea |> Option.bind (fun i -> state.AreaList |> List.tryFind(fun item -> item.Id = i))

  let selectedRep state =
    state.SelectedRep |> Option.bind (fun i -> state.SalesRepList |> List.tryFind(fun item -> item.Id = i))

  let selectedGroup state =
    state.SelectedGroup |> Option.bind (fun i -> state.GroupList |> List.tryFind(fun item -> item.Id = i))

  let ViewCustomer id _ =
    printfn "View clicked %A" id 

  let view (state : State) (dispatch : Message -> unit) =
    Mui.grid [
      grid.spacing._2
      grid.container true
      grid.children [
        Mui.grid [
          grid.item true
          grid.xs._4
          grid.children [
            Mui.card [
              Mui.cardContent [
              cardContent.children [
                Mui.autocomplete [
                  autocomplete.id "AreaId"
                  autocomplete.options (state.AreaList |> List.toArray)
                  autocomplete.value (selectedArea state)
                  autocomplete.getOptionLabel (function Some (e: Area) -> e.Code | None -> "Unknown")
                  autocomplete.inputValue state.AreaCode
                  autocomplete.onInputChange (fun x -> dispatch (AreaCodeChanged x))
                  autocomplete.onChange (fun (item: Area) -> dispatch (AreaCodeSelected item.Id))
                  autocomplete.renderInput (fun props -> Mui.textField [
                    textField.fullWidth true
                    textField.required true
                    textField.helperText "Please select area from dropdown" 
                    textField.label "Area code"
                    textField.variant.outlined
                    yield! props.felizProps
                  ] )
                  autocomplete.renderOption (fun (option: Area) _ ->
                    Mui.listItem [
                      Mui.listItemText [
                        listItemText.primary (
                                              Html.span [
                                                prop.key option.Id
                                                prop.text option.Code ] )
                        listItemText.secondary option.Description ] ] ) ]
              ] ] ]
            ]
        ]
        Mui.grid [
          grid.item true
          grid.xs._4
          grid.children [
            Mui.card [
              Mui.cardContent [
              cardContent.children [
                Mui.autocomplete [
                  autocomplete.id "SalesRep"
                  autocomplete.options (state.SalesRepList |> List.toArray)
                  autocomplete.value (selectedRep state)
                  autocomplete.getOptionLabel (function Some (e: SalesRep) -> e.Code | None -> "Unknown")
                  autocomplete.inputValue state.RepCode
                  autocomplete.onInputChange (fun x -> dispatch (SalesRepChanged x))
                  autocomplete.onChange (fun (item: SalesRep) -> dispatch (SalesRepSelected item.Id))
                  autocomplete.renderInput (fun props -> Mui.textField [
                    textField.fullWidth true
                    textField.required true
                    textField.helperText "Please select rep from dropdown" 
                    textField.label "Sales rep code"
                    textField.variant.outlined
                    yield! props.felizProps
                  ] )
                  autocomplete.renderOption (fun (option: SalesRep) _ ->
                    Mui.listItem [
                      Mui.listItemText [
                        listItemText.primary (
                                              Html.span [
                                                prop.key option.Id
                                                prop.text option.Code ] )
                        listItemText.secondary option.Description ] ] ) ]
              ] ] ]
            ]
        ]
        Mui.grid [
          grid.item true
          grid.xs._4
          grid.children [
            Mui.card [
              Mui.cardContent [
              cardContent.children [
                Mui.autocomplete [
                  autocomplete.id "GroupId"
                  autocomplete.options (state.GroupList |> List.toArray)
                  autocomplete.value (selectedGroup state)
                  autocomplete.getOptionLabel (function Some (e: Group) -> e.Code | None -> "Unknown")
                  autocomplete.inputValue state.GroupCode
                  autocomplete.onInputChange (fun x -> dispatch (GroupChanged x))
                  autocomplete.onChange (fun (item: Group) -> dispatch (GroupSelected item.Id))
                  autocomplete.renderInput (fun props -> Mui.textField [
                    textField.fullWidth true
                    textField.required true
                    textField.helperText "Please select group from dropdown" 
                    textField.label "Group code"
                    textField.variant.outlined
                    yield! props.felizProps
                  ] )
                  autocomplete.renderOption (fun (option: Group) _ ->
                    Mui.listItem [
                      Mui.listItemText [
                        listItemText.primary (
                                              Html.span [
                                                prop.key option.Id
                                                prop.text option.Code ] )
                        listItemText.secondary option.Description ] ] ) ]
              ] ] ]
            ]
        ]
        Mui.grid [
          grid.item true
          grid.xs._12
          grid.children [
            Mui.button [
              button.variant.contained
              button.color.primary
              prop.onClick (fun _ -> dispatch SearchForCustomers)
              button.fullWidth true
              button.children [
                "SEARCH"
              ]
            ]
            Mui.container [
              container.children [
                if state.isLoading then Mui.linearProgress[] else Mui.hidden [hidden.xsUp true]
              ]
            ]
            CustomerTable.customerList (state.CustomerList |> Option.defaultValue List.empty) ( fun id _ -> dispatch (ToggleViewScreen id)) ( fun id _ -> dispatch (ToggleEditScreen id)) ]
        ]
      ] ]