namespace Pages

module CustomerVisit =
  open System
  open Elmish
  open Feliz.MaterialUI
  open Components

  type State = {
    Date: string
    Rotation: string
    GYPercentage: double
    GPPercentage: string
    OtherSupplier: string
    GeneralComments: string
    }

  let init () =
    { Date            = System.DateTime.Now.ToString("yyyy-MM-dd")
      Rotation        = "NVT"
      GYPercentage    = 0.00
      GPPercentage    = ""
      OtherSupplier   = ""
      GeneralComments = ""
    }, Cmd.none

  type Message =
    | DateUpdated of string
    | RotationUpdated of string
    | GYPercentageUpdated of double
    | GPPercentageUpdated of string
    | OtherSupplier of string
    | GeneralComments of string

  let update (msg: Message) (state: State) =
    match msg with
    | DateUpdated s ->
        let myDate = (System.DateTime.Parse s).ToString("yyyy-MM-dd")
        { state with Date = myDate }, Cmd.none
    | RotationUpdated s ->
        printf "%A" s
        { state with Rotation = s }, Cmd.none
    | GYPercentageUpdated s -> { state with GYPercentage = s }, Cmd.none
    | GPPercentageUpdated s -> { state with GPPercentage = s }, Cmd.none
    | OtherSupplier s -> { state with OtherSupplier = s }, Cmd.none
    | GeneralComments s -> { state with GeneralComments = s }, Cmd.none

  let RotationSelect dispatch =
    Mui.select [
      select.classes.root "inputPadding"
      select.variant.outlined
      select.label "Rotation"
      select.onChange (RotationUpdated >> dispatch)
      select.children [
        Mui.menuItem [ menuItem.children ["GOED" ]]
        Mui.menuItem [ menuItem.children ["GEMIDDELD"] ]
        Mui.menuItem [ menuItem.children ["SWAK"] ]
        Mui.menuItem [ menuItem.children ["NVT"] ]
      ] ]
  let view (state : State) (dispatch : Message -> unit) =
    Mui.paper [
        paper.classes.root "paper"
        paper.children [
    Mui.card [
      card.classes.root "FullCard"
      card.children [
          Mui.cardContent [
        Mui.formControl [
          formControl.children [
            FormFields.dateInput "Visit date" state.Date (DateUpdated >> dispatch)
            RotationSelect dispatch
            FormFields.numberInput "Golden Yolk %" state.GYPercentage (double >> GYPercentageUpdated >> dispatch)
            FormFields.textInput "GP Percentage" state.GPPercentage (GPPercentageUpdated >> dispatch)
            FormFields.textInput "Other Supplier" state.OtherSupplier (OtherSupplier >> dispatch)
            FormFields.MultiLineTextInput "General comments" state.GeneralComments (GeneralComments >> dispatch)
            Mui.typography "Please ensure the detail is captured is accurate"
          ] ] ] ] ] ] ]