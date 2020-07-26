namespace Pages

open Components

module CustomerView =
  open Elmish
  open Feliz
  open Feliz.MaterialUI
  open Fable.MaterialUI
  open Shared
  open API

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
    | CustomerVisitMessage msg ->
        let (nextState, nextCommand) = CustomerVisit.update msg state.VisitForm
        { state with VisitForm = nextState } , nextCommand
    | CustomerChanged s -> { state with CustomerCode = s }, Cmd.none
    | LookupCustomerId -> { state with IsLoading = true }, Cmd.OfAsync.perform Server.api.getCustomerIdFromCode state.CustomerCode LoadCustomer


  let tableRows (list : TransDetail list) =
    list |> List.map ( fun x -> Mui.tableRow [
      tableRow.children [
        Mui.tableCell (x.Date |> Option.defaultValue "")
        Mui.tableCell (x.Number |> Option.defaultValue "")
        Mui.tableCell (x.Total |> string) ] ] )

  let view (state : State) (dispatch : Message -> unit) =
    match state.IsLoading with
    | true -> Mui.circularProgress [ circularProgress.size 50; circularProgress.color.secondary ]
    | false ->
        match state.CurrentCustomerId with
        | Some customerId ->
            Mui.paper [
                Mui.expansionPanel [
                  expansionPanel.variant.outlined
                  expansionPanel.defaultExpanded true
                  expansionPanel.children [
                    Mui.expansionPanelSummary [
                      expansionPanelSummary.expandIcon (MaterialDesignIcons.arrowExpandDownIcon "")
                      expansionPanelSummary.children (Strings.header5 "Customer detail") ]
                    Mui.expansionPanelDetails [
                      expansionPanelDetails.classes.root "wrappingContainer"
                      expansionPanelDetails.children [
                        Mui.card [ card.square true; card.raised true ; card.classes.root "ThirdCard";
                          card.children [
                                        Mui.table [
                                          table.children [
                                          Mui.tableRow [ tableRow.children [ Mui.tableCell "Customer name"; Mui.tableCell state.Name] ]
                                          Mui.tableRow [ tableRow.children [ Mui.tableCell "Group" ; Mui.tableCell state.GroupCode ] ]
                                          Mui.tableRow [ tableRow.children [ Mui.tableCell "Area" ; Mui.tableCell state.AreaCode ] ]
                                          Mui.tableRow [ tableRow.children [ Mui.tableCell "Ageing period" ; Mui.tableCell state.AgeingPeriod ] ]
                                          ] ]
                                        ]
                                      ]
                        Mui.card [ card.square true; card.raised true ; card.classes.root "ThirdCard";
                          card.children [
                                        Mui.table [
                                          table.children [
                                          Mui.tableRow [ tableRow.children [ Mui.tableCell "Rotation"; Mui.tableCell state.Rotation] ]
                                          Mui.tableRow [ tableRow.children [ Mui.tableCell "GY %" ; Mui.tableCell state.GYPercentage ] ]
                                          Mui.tableRow [ tableRow.children [ Mui.tableCell "GP %" ; Mui.tableCell  state.GPPercentage] ]
                                          Mui.tableRow [ tableRow.children [ Mui.tableCell "Other supplier" ; Mui.tableCell state.OtherSupplier ] ]
                                          ] ]
                                        ]
                                      ]
                        Mui.card [ card.square true; card.raised true ; card.classes.root "ThirdCard";
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
                        Mui.card [ card.square true; card.raised true ; card.classes.root "HalfCard";
                          card.children [
                                        Strings.subtitle "Last 5 invoices for this customer"
                                        Mui.table [table.size.small ;table.children (tableRows state.InvoiceList) ]
                                        ]
                                        ]
                        Mui.card [ card.square true; card.raised true ; card.classes.root "HalfCard";
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
                        if not state.SalesGraphLoaded then Strings.body1 "Could not locate data"
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
                  expansionPanel.defaultExpanded true
                  expansionPanel.children [
                    Mui.expansionPanelSummary [
                      expansionPanelSummary.expandIcon (MaterialDesignIcons.arrowExpandDownIcon "")
                      expansionPanelSummary.children (Strings.header5 "Specials and purchases") ]
                    Mui.expansionPanelDetails [
                      expansionPanelDetails.classes.root "wrappingContainer"
                      expansionPanelDetails.children [
                        Mui.card [ card.square true; card.raised true ; card.classes.root "HalfCard";
                          card.children [ "Show which products where purchased and their average over last 8 weeks"] ]
                        Mui.card [ card.square true; card.raised true ; card.classes.root "HalfCard";
                          card.children [
                                        Mui.table [
                                          table.children [
                                          Mui.tableRow [ tableRow.children [ Mui.tableCell "Expiry date"; Mui.tableCell "2020-01-01"] ]
                                          Mui.tableRow [ tableRow.children [ Mui.tableCell "Effective from" ; Mui.tableCell "2020-02-02" ] ]
                                          Mui.tableRow [ tableRow.children [ Mui.tableCell "Product" ; Mui.tableCell  "X30"] ]
                                          Mui.tableRow [ tableRow.children [ Mui.tableCell "Price (dozen)" ; Mui.tableCell "R 12.30" ] ]
                                          ] ]
                                        ]
                                      ]
                          ] ] ] ]

                Mui.expansionPanel [
                  expansionPanel.variant.outlined
                  expansionPanel.children [
                    Mui.expansionPanelSummary [
                      expansionPanelSummary.expandIcon (MaterialDesignIcons.arrowExpandDownIcon "")
                      expansionPanelSummary.children (Strings.header5 "Record visit to customer") ]
                    Mui.expansionPanelDetails [
                      expansionPanelDetails.children [
                        Mui.grid [
                          grid.container true
                          grid.children [
                        Mui.card [ card.square true; card.raised true ; card.classes.root "FullCard";
                          card.children [ CustomerVisit.view state.VisitForm (CustomerVisitMessage >> dispatch) ]
                                        ]
                          ] ] ] ]
                  ] ]
              ]
        | None -> Mui.card [
            card.children [
                  Mui.container [container.children [ if state.IsLoading then Mui.linearProgress[] else Mui.hidden [hidden.xsUp true] ] ]
                  FormFields.textInput "Customer code" state.CustomerCode (fun x -> dispatch (CustomerChanged x))
                  Buttons.primaryButtonLarge "Load customer" (fun _ -> dispatch LookupCustomerId)
        ]
      ]

