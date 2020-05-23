namespace Pages

module CustomerVisit =
  open System
  open Elmish
  open Feliz.MaterialUI
  open Components

  type State = {
    Date: System.DateTime
    Rotation: string
    GYPercentage: double
    GPPercentage: string
    OtherSupplier: string
    GeneralComments: string
    }

  let init () =
    { Date = System.DateTime.Now
      Rotation = ""
      GYPercentage= 0.00
      GPPercentage = ""
      OtherSupplier = ""
      GeneralComments = ""
    }, Cmd.none

  type Message =
    | DateUpdated of DateTime
    | RotationUpdated of string
    | GYPercentageUpdated of double
    | GPPercentageUpdated of string
    | OtherSupplier of string
    | GeneralComments of string

  let update (msg: Message) (state: State) =
    match msg with
    | DateUpdated s -> { state with Date = s }, Cmd.none
    | RotationUpdated s ->
        printf "%A" s
        { state with Rotation = s }, Cmd.none
    | GYPercentageUpdated s -> { state with GYPercentage = s }, Cmd.none
    | GPPercentageUpdated s -> { state with GPPercentage = s }, Cmd.none
    | OtherSupplier s -> { state with OtherSupplier = s }, Cmd.none
    | GeneralComments s -> { state with GeneralComments = s }, Cmd.none

  let view (state : State) (dispatch : Message -> unit) =
    Mui.card [
      card.classes.root "FullCard"
      card.children [
        Mui.formControl [formControl.fullWidth true; formControl.margin.dense ;formControl.children (FormFields.dateInput "Visit date" state.Date (fun x -> DateUpdated (x |> DateTime.Parse) |> dispatch )) ]
        Mui.formControl [formControl.fullWidth true; formControl.margin.dense ;formControl.children (FormFields.RotationSelect "Rotation" state.Rotation (fun x -> RotationUpdated x |> dispatch )) ]
        Mui.formControl [formControl.fullWidth true; formControl.margin.dense ;formControl.children (FormFields.numberInput "Golden Yolk %" state.GYPercentage (fun x -> GYPercentageUpdated (x |> double) |> dispatch )) ]
        Mui.formControl [formControl.fullWidth true; formControl.margin.dense ;formControl.children (FormFields.textInput "GP Percentage" state.GPPercentage (fun x -> GPPercentageUpdated x |> dispatch )) ]
        Mui.formControl [formControl.fullWidth true; formControl.margin.dense ;formControl.children (FormFields.textInput "Other Supplier" state.OtherSupplier (fun x -> OtherSupplier x |> dispatch )) ]
        Mui.formControl [formControl.fullWidth true; formControl.margin.dense ;formControl.children (FormFields.MultiLineTextInput "General comments" state.GeneralComments (fun x -> GeneralComments x |> dispatch )) ]
        Mui.formControl [formControl.fullWidth true; formControl.margin.normal ;formControl.children (Mui.button[button.variant.contained;button.color.primary; button.children["SUBMIT"] ])]
        Mui.typography "Please ensure the detail is captured is accurate"
    ] ]
