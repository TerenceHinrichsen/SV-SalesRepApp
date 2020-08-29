namespace Pages

open Components

module CustomerView =
  open Elmish
  open Feliz
  open Feliz.MaterialUI
  open Fable.MaterialUI
  open Shared
  open API
  open Feliz.Recharts

  type State = {
    CurrentCustomerId : int option
    Name : string
    GroupCode : string
    AreaCode :  string
    AgeingPeriod: string
    Rotation : string
    GYPercentage : string
    GPPercentage : string
    OtherSupplier : string
    LastRepVisit : string
    LastInvoice : string
    Comments : string
    IsLoading : bool
    SalesGraphLoading : bool
    InvoiceList : TransDetail list
    CreditList : TransDetail list
    Sales : SalesGraphPoint list
    SalesGraphLoaded : bool
    VisitForm : CustomerVisit.State
    CustomerCode : string
    TodoState : Todo option
    ShowToDoScreen : bool
    ShowCustomerVisit : bool
    CustomerSpecials : CustomerSpecial list
    ProductMix : ProductMixDatapoint list
   }

  type Message =
    | CustomerDetailLoaded of CustomerDisplayDetail
    | LoadLastTransactions of TransDetail list
    | LoadLastCredits of TransDetail list
    | LoadCustomer of int
    | LoadSalesHistory
    | SalesHistoryLoaded of SalesGraphPoint list
    | CustomerVisitMessage of CustomerVisit.Message
    | CustomerChanged of string
    | LookupCustomerId
    | AddTodo of int
    | ShowCustomerVisitScreen
    | SaveToDo
    | TodoSaved of string
    | ExceptionReceived of System.Exception
    | CloseToDo
    | CloseCustomerVisit
    | AssigneeChanged of string
    | PromisedDateChanged of string
    | MessageChanged of string
    | LoadSpecials of CustomerSpecial list
    | FetchSpecials
    | FetchProductMix
    | LoadProductMix of ProductMixDatapoint list
    | SaveCustomerVisit


  let init () =
    let initialCustomerVisitState, _ = CustomerVisit.init()
    { CurrentCustomerId = None
      Name = "##UNKNOWN##"
      GroupCode = "##UNKNOWN##"
      AreaCode  = "##UNKNOWN##"
      AgeingPeriod = "##UNKNOWN##"
      Rotation = "##UNKNOWN##"
      GYPercentage = "##UNKNOWN##"
      GPPercentage = "##UNKNOWN##"
      OtherSupplier = "##UNKNOWN##"
      LastRepVisit = "##UNKNOWN##"
      LastInvoice = "##UNKNOWN##"
      Comments = "##UNKNOWN##"
      InvoiceList = List.Empty
      CreditList = List.Empty
      IsLoading = true
      Sales = List.Empty
      SalesGraphLoading = true
      SalesGraphLoaded = false
      VisitForm = initialCustomerVisitState
      CustomerCode = ""
      TodoState = None
      ShowToDoScreen = false
      ShowCustomerVisit = false
      CustomerSpecials = List.Empty
      ProductMix = List.empty
    }, Cmd.none

  let update (msg: Message) (state: State) =
    match msg with
    | LoadCustomer customerId -> { state with IsLoading = true }, Cmd.OfAsync.perform Server.api.getCustomerViewDetail customerId CustomerDetailLoaded
    | CustomerDetailLoaded detail -> { state with IsLoading = false
                                                  CurrentCustomerId = Some detail.Id
                                                  Name          = detail.Name             |> Option.defaultValue "##ERROR##"
                                                  GroupCode     = detail.Group            |> Option.defaultValue "##ERROR##"
                                                  AreaCode      = detail.Area             |> Option.defaultValue "##ERROR##"
                                                  AgeingPeriod  = detail.AgeingPeriod     |> Option.defaultValue "##ERROR##"
                                                  Rotation      = detail.Rotation         |> Option.defaultValue "##ERROR##"
                                                  GYPercentage  = detail.GYPercentage     |> Option.defaultValue "##ERROR##"
                                                  GPPercentage  = detail.GPPercentage     |> Option.defaultValue "##ERROR##"
                                                  OtherSupplier = detail.OtherSuppliers   |> Option.defaultValue "##ERROR##"
                                                  LastInvoice   = detail.LastInvoiceDate  |> Option.defaultValue "##ERROR##"
                                                  LastRepVisit  = detail.LastRepVisit     |> Option.defaultValue "##ERROR##"
                                                  Comments      = detail.GeneralComments  |> Option.defaultValue "##ERROR##"
                                                  InvoiceList   = detail.Last5Invoices
                                                  CreditList    = detail.Last5Credits
                                                  }, Cmd.OfAsyncImmediate.perform Server.api.getLast5Invoices detail.Id LoadLastTransactions
    | LoadLastTransactions invoiceList -> {state with InvoiceList = invoiceList}, Cmd.OfAsync.perform Server.api.getLast5Credits (state.CurrentCustomerId |> Option.defaultValue 0) LoadLastCredits
    | LoadLastCredits creditList -> {state with CreditList = creditList} , Cmd.none
    | LoadSalesHistory ->
      if state.Sales.Length = 0
      then
        printfn "Fetching sales history"
        { state with SalesGraphLoading = true }, Cmd.OfAsync.perform Server.api.getSalesGraphData (state.CurrentCustomerId |> Option.defaultValue 0) SalesHistoryLoaded
      else
        printfn "Not necessary"
        state, Cmd.none
    | SalesHistoryLoaded list -> { state with Sales = list; SalesGraphLoaded = true; SalesGraphLoading = false },Cmd.none
    | FetchSpecials ->
        match state.CustomerSpecials.Length with
        | 0 -> state, Cmd.OfAsync.perform Server.api.getCustomerSpecials (state.CurrentCustomerId |> Option.defaultValue 0) LoadSpecials
        | _ -> state, Cmd.none
    | LoadSpecials customerSpecials -> {state with CustomerSpecials = customerSpecials}, Cmd.ofMsg FetchProductMix
    | FetchProductMix ->
        match state.ProductMix.Length with
        | 0 -> state, Cmd.OfAsync.perform Server.api.getProductMix (state.CurrentCustomerId |> Option.defaultValue 0) LoadProductMix
        | _ -> state, Cmd.none
    | LoadProductMix productMix -> { state with ProductMix = productMix }, Cmd.none //Toast.infoMessage 2000 "Specials loaded"
    | ShowCustomerVisitScreen -> {state with ShowCustomerVisit = true }, Cmd.none
    | CustomerVisitMessage msg ->
        let (nextState, nextCommand) = CustomerVisit.update msg state.VisitForm
        { state with VisitForm = nextState } , nextCommand
    | CustomerChanged s -> { state with CustomerCode = s }, Cmd.none
    | LookupCustomerId -> { state with IsLoading = true }, Cmd.OfAsync.perform Server.api.getCustomerIdFromCode state.CustomerCode LoadCustomer
    | AddTodo customerId -> { state with ShowToDoScreen = true;
                                         TodoState = Some {
                                            CustomerId = customerId;
                                            Assignee = ""
                                            Message = ""
                                            PromisedDate = System.DateTime.Now } }, Cmd.none
    | SaveToDo -> { state with ShowToDoScreen = false }, Cmd.OfAsync.either Server.api.addNewTodo state.TodoState.Value TodoSaved ExceptionReceived
    | TodoSaved string -> { state with TodoState = None }, Toast.successMessage 5000 string
    | ExceptionReceived exn -> state, Toast.errorMessage 5000 exn
    | CloseToDo -> { state with ShowToDoScreen = false },  Cmd.none
    | CloseCustomerVisit -> { state with ShowCustomerVisit = false },  Cmd.none
    | AssigneeChanged s ->
        match state.TodoState with
        | Some tds -> { state with TodoState = Some { tds with Assignee = s} }, Cmd.none
        | None -> { state with TodoState = Some { Assignee = s; Message = ""; PromisedDate = System.DateTime.Now; CustomerId = 0 }}, Cmd.none
    | MessageChanged s ->
        match state.TodoState with
        | Some tds -> { state with TodoState = Some { tds with Message = s} }, Cmd.none
        | None -> { state with TodoState = Some { Assignee = ""; Message = s; PromisedDate = System.DateTime.Now; CustomerId = 0 }}, Cmd.none
    | PromisedDateChanged s ->
        match state.TodoState with
        | Some tds -> { state with TodoState = Some { tds with PromisedDate = System.DateTime.Parse s} }, Cmd.none
        | None -> { state with TodoState = Some { Assignee = ""; Message = ""; PromisedDate = System.DateTime.Parse s; CustomerId = 0 }}, Cmd.none
    | SaveCustomerVisit ->
        state, Toast.successMessage 5000 "Saved to database"

  let tableRows (list : TransDetail list) =
    list |> List.map ( fun x -> Mui.tableRow [
      tableRow.children [
        Mui.tableCell (x.Date |> Option.defaultValue "")
        Mui.tableCell (x.Number |> Option.defaultValue "")
        Mui.tableCell [tableCell.align.right; tableCell.children (x.Total |> string)  ] ] ] )

  let specialsTable ( list : CustomerSpecial list) =
    Mui.container [
        container.children [
            Mui.table [
                table.stickyHeader true
                table.children [
        Mui.tableHead [
            Mui.tableCell "Item description"
            Mui.tableCell "Start date"
            Mui.tableCell "Dozen price"
            Mui.tableCell "Unit price"
        ]
        Mui.tableBody (
            list
            |> List.sortBy (fun x -> x.ItemCode)
            |> List.map (fun x -> Mui.tableRow [
              tableRow.children [
                Mui.tableCell (x.ItemDescription)
                Mui.tableCell (x.EffectiveDate.ToShortDateString())
                Mui.tableCell (x.DozenPrice.ToString("0.00"))
                Mui.tableCell (x.UnitPrice.ToString("0.00")) ]
    ]) ) ] ] ] ]

  type PieSlice = { name: string; value : int }

  let productMixGraph ( list : ProductMixDatapoint list) =
    let productMixList datapoint =
        Mui.tableRow [
            Mui.tableCell datapoint.ProductCode
            Mui.tableCell (datapoint.TotalBoxes.ToString("0.00"))
        ]
    let cells =
        list
        |> List.map (fun x -> { name = x.ProductCode; value = x.TotalBoxes |> int})
    Mui.container [
        container.children [
    Recharts.pieChart [
      pieChart.width 300
      pieChart.height 250
      pieChart.data cells
      pieChart.children [
        Recharts.pie [
          pie.data cells
          pie.dataKey (fun point -> point.value)
          pie.cx 150
          pie.cy 100
          pie.label true
          pie.fill  "#82ca9d" ] ] ]
    Mui.table [
        table.stickyHeader true
        table.children [
            Mui.tableHead [
                Mui.tableRow [
                    Mui.tableCell "Product code"
                    Mui.tableCell "TotalBoxes (for last 8 weeks)" ] ]
            Mui.tableBody (List.map productMixList list )
        ] ]]]

  let view (state : State) (dispatch : Message -> unit) =
    Mui.paper[
      paper.children [
        Mui.dialog [
          dialog.open' state.ShowToDoScreen
          dialog.maxWidth.xl
          dialog.onBackdropClick (fun _ -> dispatch CloseToDo)
          dialog.children [
            Mui.dialogContent [
              match state.TodoState with
              | Some todoState ->
                  Mui.formControl [
                    Mui.textField [textField.type' "date"; textField.onChange (PromisedDateChanged >> dispatch)]
                    Mui.textField [textField.label "Responsible person"; textField.value todoState.Assignee ;textField.onChange (AssigneeChanged >> dispatch) ]
                    Mui.textField [textField.label "Message" ;textField.multiline true; textField.rows 4; textField.onChange(MessageChanged >> dispatch)] ]
              | None -> Mui.typography "Could not load todo" ]
            Mui.dialogActions [
            Buttons.primaryButton 1 "Save" (fun _ -> dispatch SaveToDo)
            Buttons.secondaryButton 2 "Cancel" (fun _ -> dispatch CloseToDo)
            ] ] ]
        Mui.dialog [
          dialog.open' state.ShowCustomerVisit
          dialog.maxWidth.xl
          dialog.onBackdropClick (fun _ -> dispatch CloseCustomerVisit)
          dialog.children [
            Mui.dialogContent [ CustomerVisit.view state.VisitForm (CustomerVisitMessage >> dispatch) ]
            Mui.dialogActions [
            Buttons.primaryButton 1 "Save" (fun _ -> dispatch SaveCustomerVisit)
            Buttons.secondaryButton 2 "Cancel" (fun _ -> dispatch CloseCustomerVisit)
            ] ] ]
        match state.IsLoading with
        | true -> Mui.circularProgress [ circularProgress.size 50; circularProgress.color.secondary ]
        | false ->
            match state.CurrentCustomerId with
            | Some customerId ->
                Mui.paper [
                    Mui.expansionPanel [
                      expansionPanel.variant.outlined
                      expansionPanel.children [
                        Mui.expansionPanelSummary [
                          expansionPanelSummary.expandIcon (MaterialDesignIcons.arrowExpandDownIcon "")
                          expansionPanelSummary.children (Strings.header5 "Customer detail") ]
                        Mui.expansionPanelDetails [
                          expansionPanelDetails.classes.root "wrappingContainer"
                          expansionPanelDetails.children [
                            Buttons.primaryButton customerId "Add reminder" (fun _ -> dispatch (AddTodo customerId))
                            Buttons.primaryButton customerId "Record visit" (fun _ -> dispatch (ShowCustomerVisitScreen))
                            Buttons.primaryButton customerId "Mark for deletion" (fun _ -> dispatch (AddTodo customerId))
                            Mui.card [ card.square true; card.raised true ; card.classes.root "FullCard";
                              card.children [
                                            Mui.table [
                                              table.size.small
                                              table.children [
                                              Mui.tableRow [ tableRow.children [ Mui.tableCell "Customer name"; Mui.tableCell state.Name] ]
                                              Mui.tableRow [ tableRow.children [ Mui.tableCell "Group" ; Mui.tableCell state.GroupCode ] ]
                                              Mui.tableRow [ tableRow.children [ Mui.tableCell "Area" ; Mui.tableCell state.AreaCode ] ]
                                              Mui.tableRow [ tableRow.children [ Mui.tableCell "Ageing period" ; Mui.tableCell state.AgeingPeriod ] ]
                                              ] ] ] ]
                            Mui.card [ card.square true; card.raised true ; card.classes.root "FullCard";
                              card.children [
                                            Mui.table [
                                               table.size.small
                                               table.children [
                                               Mui.tableRow [ tableRow.children [ Mui.tableCell "Rotation"; Mui.tableCell state.Rotation] ]
                                               Mui.tableRow [ tableRow.children [ Mui.tableCell "GY %" ; Mui.tableCell state.GYPercentage ] ]
                                               Mui.tableRow [ tableRow.children [ Mui.tableCell "GP %" ; Mui.tableCell  state.GPPercentage] ]
                                               Mui.tableRow [ tableRow.children [ Mui.tableCell "Other supplier" ; Mui.tableCell state.OtherSupplier ] ]
                                              ] ] ] ]
                            Mui.card [ card.square true; card.raised true ; card.classes.root "FullCard";
                              card.children [
                                            Mui.table [
                                              table.size.small
                                              table.children [
                                              Mui.tableRow [ tableRow.children [ Mui.tableCell "Last rep visit"; Mui.tableCell state.LastRepVisit] ]
                                              Mui.tableRow [ tableRow.children [ Mui.tableCell "Last invoice" ; Mui.tableCell state.LastInvoice ] ]
                                              Mui.tableRow [ tableRow.children [ Mui.tableCell "General comments" ; Mui.tableCell state.Comments ] ]
                                              ] ]
                                            ]
                                          ]
                            Mui.card [ card.square true; card.raised true ; card.classes.root "FullCard";
                              card.children [
                                            Strings.subtitle "Last 5 invoices for this customer"
                                            Mui.table [table.size.small ;table.children (tableRows state.InvoiceList) ]
                                            ]
                                            ]
                            Mui.card [ card.square true; card.raised true ; card.classes.root "FullCard";
                              card.children [
                                            Strings.subtitle "Last 5 returns for this customer"
                                            Mui.table [
                                              table.size.small ;table.children (tableRows state.CreditList) ]
                                            ]
                                            ]
                              ] ] ] ]

                    Mui.expansionPanel [
                      expansionPanel.variant.outlined
                      prop.onClick (fun x -> dispatch LoadSalesHistory)
                      expansionPanel.children [
                        Mui.expansionPanelSummary [
                          expansionPanelSummary.expandIcon (MaterialDesignIcons.arrowExpandDownIcon "")
                          expansionPanelSummary.children (Strings.header5 "Sales trend") ]
                        Mui.expansionPanelDetails [
                          expansionPanelDetails.children [
                            if not state.SalesGraphLoaded then Mui.linearProgress[]
                            elif state.SalesGraphLoading then Mui.circularProgress[]
                            else
                              Mui.grid [
                                grid.container true
                                grid.children [
                                  Mui.card [ card.square true; card.raised true ; card.classes.root "FullCard";
                                    card.children [ SalesGraph.chart state.Sales]
                                              ]
                                ] ] ] ]
                      ] ]

                    Mui.expansionPanel [
                      expansionPanel.variant.outlined
                      prop.onClick(fun x -> dispatch FetchSpecials)
                      expansionPanel.defaultExpanded false
                      expansionPanel.children [
                        Mui.expansionPanelSummary [
                          expansionPanelSummary.expandIcon (MaterialDesignIcons.arrowExpandDownIcon "")
                          expansionPanelSummary.children (Strings.header5 "Specials and purchases") ]
                        Mui.expansionPanelDetails [
                          expansionPanelDetails.classes.root "wrappingContainer"
                          expansionPanelDetails.children [
                            Mui.card [ card.square true; card.raised true ; card.classes.root "FullCard";
                              card.children [ productMixGraph state.ProductMix] ]
                            Mui.card [ card.square true; card.raised true ; card.classes.root "FullCard";
                              card.children (specialsTable state.CustomerSpecials)
                              ] ] ] ] ]
                  ]
            | None -> Mui.card [
                card.children [
                      Mui.container [container.children [ if state.IsLoading then Mui.linearProgress[] else Mui.hidden [hidden.xsUp true] ] ]
                      FormFields.textInput "Customer code" state.CustomerCode (CustomerChanged >> dispatch )
                      Buttons.primaryButtonLarge "Load customer" (fun _ -> dispatch LookupCustomerId)
            ]
          ]
      ] ]

