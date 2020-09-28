namespace Pages

open Components

module CustomerEdit =
  open Elmish
  open Feliz
  open Feliz.MaterialUI
  open Shared
  open API

  type formData = {
    CustomerCode : string
    CustomerName : string
    Contact1: string
    Contact2: string
    Telephone: string
    Cell: string
    Email: string
    Physical1: string
    Physical2: string
    Physical3: string
    DeliveryEmail: string
  }

  let emptyForm = {
    CustomerCode = ""
    CustomerName = ""
    Contact1 = ""
    Contact2 = ""
    Telephone = ""
    Cell = ""
    Email = ""
    Physical1 = ""
    Physical2 = ""
    Physical3 = ""
    DeliveryEmail = ""  }

  type State = {
    isLoading: bool
    CurrentCustomerId: int option
    CustomerCode: string
    AreaList: Area list
    GroupList: Group list
    PriceListList: PriceList list
    SalesRepList: SalesRep list
    SelectedArea: int option
    AreaCode: string
    SelectedRep: int option
    RepCode: string
    SelectedGroup: int option
    PricelistCode: string
    SelectedPricelist: int option
    DetailForm : formData
    RepVisitFrequencies : string list
    SelectedRepVisitFrequency : string
    MarketSegments : string list
    SelectedMarketSegment : string
    ResponseFromDatabase : string option
  }

  type Message =
    | AreaCodeChanged of string
    | AreaCodeSelected of int
    | SalesRepChanged of string
    | SalesRepSelected of int
    | GroupSelected of int
    | PricelistChanged of string
    | PricelistSelected of int
    | CustomerChanged of string
    | ListsUpdated
    | CustomerIdFound of int
    | CustomerDetailLoaded of Customer
    | LookupCustomerId
    | CustomerNameChanged of string
    | Contact1Changed of string
    | Contact2Changed of string
    | TelephoneChanged of string
    | CellChanged of string
    | EmailChanged of string
    | Physical1Changed of string
    | Physical2Changed of string
    | Physical3Changed of string
    | DeliveryEmailChanged of string
    | RepVisitFrequencySelected of string
    | MarketSegmentSelected of string
    | SaveChangesToDatabase
    | SaveChangesCompleted of string
    | ExceptionReceived of System.Exception
    | CloseScreen

  let init () : State * _ =
    { AreaList = []
      GroupList = []
      PriceListList = []
      SalesRepList = []
      SelectedArea = None
      AreaCode = ""
      CurrentCustomerId = None
      CustomerCode = ""
      SelectedRep = None
      RepCode = ""
      SelectedGroup = None
      SelectedPricelist = None
      PricelistCode = ""
      isLoading = false
      DetailForm = emptyForm
      RepVisitFrequencies = List.empty
      SelectedRepVisitFrequency = ""
      MarketSegments = List.empty
      SelectedMarketSegment = ""
      ResponseFromDatabase = None
    }, Cmd.none

  let update (msg : Message) (currentState : State) : State * _ =
    match msg with
    | AreaCodeChanged       s ->  if s.Length = 0 then { currentState with SelectedArea   = None; AreaCode = ""}, Cmd.none  else { currentState with AreaCode = s}, Cmd.none
    | SalesRepChanged       s ->  if s.Length = 0 then { currentState with SelectedRep    = None; RepCode = ""}, Cmd.none    else { currentState with RepCode = s}, Cmd.none
    | PricelistChanged      s ->  if s.Length = 0 then { currentState with SelectedPricelist = None; PricelistCode = s}, Cmd.none  else { currentState with PricelistCode = s}, Cmd.none
    | AreaCodeSelected      s ->  { currentState with SelectedArea = Some s }, Cmd.none
    | SalesRepSelected      s ->  { currentState with SelectedRep = Some s }, Cmd.none
    | GroupSelected         s ->  { currentState with SelectedGroup = Some s }, Cmd.none
    | PricelistSelected     s ->  { currentState with SelectedPricelist = Some s }, Cmd.none
    | CustomerChanged       s ->  { currentState with CurrentCustomerId = None; CustomerCode = s }, Cmd.none
    | CustomerDetailLoaded  s ->  { currentState with DetailForm = {  CustomerCode = s.CustomerAccountNumber
                                                                      CustomerName = s.CustomerAccountName  |> Option.defaultValue ""
                                                                      Contact1 = s.ContactPerson  |> Option.defaultValue ""
                                                                      Contact2 = s.DeliveryContact  |> Option.defaultValue ""
                                                                      Telephone = s.Telephone  |> Option.defaultValue ""
                                                                      Cell = s.Cellphone  |> Option.defaultValue ""
                                                                      Email = s.Email  |> Option.defaultValue ""
                                                                      Physical1 = s.Physical  |> Option.defaultValue ""
                                                                      Physical2 = s.Physical2  |> Option.defaultValue ""
                                                                      Physical3 = s.Suburb  |> Option.defaultValue ""
                                                                      DeliveryEmail = s.DeliveryEmail |> Option.defaultValue "" };
                                                                      SelectedArea = s.AreaId;
                                                                      SelectedRep = s.SalesRepId;
                                                                      SelectedGroup = s.GroupId;
                                                                      SelectedPricelist = s.PriceListId;
                                                                      SelectedRepVisitFrequency = s.RepVisitFrequency |> Option.defaultValue "";
                                                                      SelectedMarketSegment = s.MarketSegment |> Option.defaultValue "" }, Cmd.none
    | CustomerNameChanged   s ->  { currentState with DetailForm = { currentState.DetailForm with CustomerName = s }}, Cmd.none
    | Contact1Changed       s ->  { currentState with DetailForm = { currentState.DetailForm with Contact1 = s }}, Cmd.none
    | Contact2Changed       s ->  { currentState with DetailForm = { currentState.DetailForm with Contact2 = s }}, Cmd.none
    | TelephoneChanged      s ->  { currentState with DetailForm = { currentState.DetailForm with Telephone = s }}, Cmd.none
    | CellChanged           s ->  { currentState with DetailForm = { currentState.DetailForm with Cell = s }}, Cmd.none
    | EmailChanged          s ->  { currentState with DetailForm = { currentState.DetailForm with Email = s }}, Cmd.none
    | Physical1Changed      s ->  { currentState with DetailForm = { currentState.DetailForm with Physical1 = s }}, Cmd.none
    | Physical2Changed      s ->  { currentState with DetailForm = { currentState.DetailForm with Physical2 = s }}, Cmd.none
    | Physical3Changed      s ->  { currentState with DetailForm = { currentState.DetailForm with Physical3 = s }}, Cmd.none
    | DeliveryEmailChanged  s ->  { currentState with DetailForm = { currentState.DetailForm with DeliveryEmail = s }}, Cmd.none
    | LookupCustomerId -> currentState, Cmd.OfAsync.perform Server.api.getCustomerIdFromCode currentState.CustomerCode CustomerIdFound
    | CustomerIdFound id -> { currentState with CurrentCustomerId = Some id }, Cmd.OfAsync.perform Server.api.getCustomerById id CustomerDetailLoaded
    | RepVisitFrequencySelected s -> {currentState with SelectedRepVisitFrequency = s}, Cmd.none
    | MarketSegmentSelected s -> {currentState with SelectedMarketSegment = s }, Cmd.none
    | SaveChangesToDatabase -> currentState,
                                Cmd.OfAsyncImmediate.either Server.api.updateCustomerMaster {
                                      CustomerId = currentState.CurrentCustomerId |> Option.defaultValue 0
                                      CustomerName = currentState.DetailForm.CustomerName
                                      GroupId = currentState.SelectedGroup |> Option.defaultValue 0
                                      RepId = currentState.SelectedRep |> Option.defaultValue 0
                                      PricelistId = currentState.SelectedPricelist |> Option.defaultValue 0
                                      Contact1  = currentState.DetailForm.Contact1
                                      Contact2 =  currentState.DetailForm.Contact2
                                      Telephone =  currentState.DetailForm.Telephone
                                      Cell =  currentState.DetailForm.Cell
                                      Email =  currentState.DetailForm.Email
                                      Physical1 =  currentState.DetailForm.Physical1
                                      Physical2 =  currentState.DetailForm.Physical2
                                      Physical3 =  currentState.DetailForm.Physical3
                                      DeliveryEmail =  currentState.DetailForm.DeliveryEmail
                                      MarketSegment =  currentState.SelectedMarketSegment
                                      RepVisitFreq =  currentState.SelectedRepVisitFrequency
                                   } SaveChangesCompleted ExceptionReceived
    | SaveChangesCompleted s -> { currentState with ResponseFromDatabase = Some s }, Toast.successMessage 5000 "Changes saved to database"
    | ExceptionReceived exn -> currentState, Toast.errorMessage 5000 exn
    | _ -> currentState, Cmd.none

  let selectedArea state =
    state.SelectedArea |> Option.bind (fun i -> state.AreaList |> List.tryFind(fun item -> item.Id = i))

  let selectedRep state =
    state.SelectedRep |> Option.bind (fun i -> state.SalesRepList |> List.tryFind(fun item -> item.Id = i))

  let selectedGroup state =
    state.SelectedGroup |> Option.bind (fun i -> state.GroupList |> List.tryFind(fun item -> item.Id = i))

  let selectedPriceList state =
    state.SelectedPricelist |> Option.bind (fun i -> state.PriceListList |> List.tryFind(fun item -> item.Id = i))

  let view (state : State) (dispatch : Message -> unit) =
    match state.CurrentCustomerId with
    | Some customerId ->
      Mui.formGroup [
        Mui.card [
          Mui.cardContent [
          cardContent.children [
            Buttons.secondaryButtonLarge "BACK" (fun _ -> dispatch CloseScreen)
            Mui.typography [
              typography.variant.h4
              typography.color.secondary
              typography.children ( sprintf "Editing customer: %s" state.DetailForm.CustomerCode ) ]
            FormFields.textInput "Name" state.DetailForm.CustomerName (fun x -> dispatch (CustomerNameChanged x) )

            Mui.container [
              prop.className "paper"
              container.children [
                Mui.typography (sprintf "Currently: %A, choose new option below" (selectedGroup state |> string))
                Mui.autocomplete [
                  autocomplete.id "GroupId"
                  autocomplete.options (state.GroupList |> List.toArray)
                  autocomplete.value (selectedGroup state)
                  autocomplete.getOptionLabel (function Some (e: Group) -> e.Code | None -> "Unknown")
                  autocomplete.onChange (fun (item: Group) -> dispatch (GroupSelected item.Id))
                  autocomplete.renderInput (fun props -> Mui.textField [
                    textField.fullWidth true
                    textField.required true
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
                        listItemText.secondary option.Description ] ] ) ] ] ]
            Mui.container [
              prop.className "paper"
              container.children [
              Mui.typography (sprintf "Currently: %A, choose new option below" (selectedRep state |> string))
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
                      listItemText.secondary option.Description ] ] ) ] ] ]
            Mui.container [
              prop.className "paper"
              container.children [
            Mui.typography (sprintf "Currently: %A, choose new option below" (selectedPriceList state |> string))
            Mui.autocomplete [
              autocomplete.id "Pricelist"
              autocomplete.options (state.PriceListList |> List.toArray)
              autocomplete.value (selectedPriceList state)
              autocomplete.getOptionLabel (function Some (e: PriceList) -> e.Name | None -> "Unknown")
              autocomplete.onInputChange (fun x -> dispatch (PricelistChanged x))
              autocomplete.onChange (fun (item: PriceList) -> dispatch (PricelistSelected item.Id))
              autocomplete.renderInput (fun props -> Mui.textField [
                textField.fullWidth true
                textField.required true
                textField.value (selectedPriceList state)
                textField.helperText " . "
                textField.label "Pricelist"
                textField.variant.outlined
                yield! props.felizProps
              ] )
              autocomplete.renderOption (fun (option: PriceList) _ ->
                Mui.listItem [
                  Mui.listItemText [
                    listItemText.primary (
                                          Html.span [
                                            prop.key option.Id
                                            prop.text option.Name ] )
                    listItemText.secondary option.Description ] ] ) ] ] ]

            FormFields.textInput "Contact 1"  state.DetailForm.Contact1     (fun x -> dispatch ( Contact1Changed x) )
            FormFields.textInput "Contact 2"  state.DetailForm.Contact2     (fun x -> dispatch ( Contact2Changed x) )
            FormFields.textInput "Telephone"  state.DetailForm.Telephone    (fun x -> dispatch ( TelephoneChanged x) )
            FormFields.textInput "Cellular"   state.DetailForm.Cell         (fun x -> dispatch ( CellChanged x) )
            FormFields.emailInput "Email"      state.DetailForm.Email        (fun x -> dispatch ( EmailChanged x) )
            FormFields.textInput "Physical 1" state.DetailForm.Physical1    (fun x -> dispatch ( Physical1Changed x) )
            FormFields.textInput "Physical 2" state.DetailForm.Physical2    (fun x -> dispatch ( Physical2Changed x) )
            FormFields.textInput "Physical 3" state.DetailForm.Physical3    (fun x -> dispatch ( Physical3Changed x) )
            FormFields.emailInput "Delivery email" state.DetailForm.DeliveryEmail (fun x -> dispatch ( DeliveryEmailChanged x) )

            Mui.container [
              prop.className "paper"
              container.children [
                Mui.autocomplete [
                  autocomplete.id "MarketSegment"
                  autocomplete.options (state.MarketSegments |> List.toArray)
                  autocomplete.value state.SelectedMarketSegment
                  autocomplete.inputValue state.SelectedMarketSegment
                  autocomplete.onInputChange (fun x -> dispatch (MarketSegmentSelected x))
                  autocomplete.onChange (fun x -> dispatch (MarketSegmentSelected x))
                  autocomplete.renderInput (fun props -> Mui.textField [
                    textField.fullWidth true
                    textField.required true
                    textField.helperText " . "
                    textField.label "Market segment"
                    textField.variant.outlined
                    yield! props.felizProps
                  ] )
                  autocomplete.renderOption (fun (option: string) _ ->
                    Mui.listItem [
                      Mui.listItemText [
                        listItemText.primary (
                                              Html.span [
                                                prop.key option
                                                prop.text option ] ) ] ] ) ] ] ]
            Mui.container [
              prop.className "paper"
              container.children [
              Mui.autocomplete [
                autocomplete.id "RepVisit"
                autocomplete.options (state.RepVisitFrequencies |> List.toArray)
                autocomplete.value state.SelectedRepVisitFrequency
                autocomplete.inputValue state.SelectedRepVisitFrequency
                autocomplete.onChange (fun item -> dispatch (RepVisitFrequencySelected item))
                autocomplete.renderInput (fun props -> Mui.textField [
                  textField.fullWidth true
                  textField.required true
                  textField.helperText " . "
                  textField.label "Rep visit frequency"
                  textField.variant.outlined
                  yield! props.felizProps
                ] )
                autocomplete.renderOption (fun (option: string) _ ->
                  Mui.listItem [
                    Mui.listItemText [
                      listItemText.primary (
                                            Html.span [
                                              prop.key option
                                              prop.text option ] )  ] ] ) ] ] ]
            Buttons.secondaryButtonLarge "SAVE CHANGES TO EVOLUTION" (fun _ -> dispatch SaveChangesToDatabase)
          ] ] ] ]
    | None ->
      Mui.card [
        card.children [
          Mui.container [container.children [ if state.isLoading then Mui.linearProgress[] else Mui.hidden [hidden.xsUp true] ] ]
          FormFields.textInput "Customer code" state.CustomerCode (fun x -> dispatch (CustomerChanged x))
          Buttons.primaryButtonLarge "Load customer" (fun _ -> dispatch LookupCustomerId)
        ]
      ]