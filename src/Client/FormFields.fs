namespace Components

open Feliz
open Feliz.MaterialUI

module FormFields =

  let textInput (label : string) value (onChangeFunction : string -> unit) =
      Mui.textField [
        textField.classes.root "inputPadding"
        textField.label label
        textField.value value
        textField.fullWidth true
        textField.variant.outlined
        textField.required true
        textField.onChange onChangeFunction ]

  let numberInput (label : string) value (onChangeFunction : string -> unit) =
      Mui.textField [
        textField.classes.root "inputPadding"
        textField.label label
        textField.value value
        textField.fullWidth true
        textField.variant.outlined
        textField.required true
        textField.onChange onChangeFunction
        textField.type' "number" ]

  let emailInput (label : string) value (onChangeFunction : string -> unit) =
      Mui.textField [
        textField.classes.root "inputPadding"
        textField.label label
        textField.value value
        textField.fullWidth true
        textField.variant.outlined
        textField.required true
        textField.onChange onChangeFunction
        textField.type' "email" ]

  let dateInput (label : string) value (onChangeFunction : string -> unit) =
      Mui.textField [
        textField.label label
        textField.value value
        textField.fullWidth true
        textField.variant.outlined
        textField.required true
        textField.onChange onChangeFunction
        textField.type' "date" ]

  let MultiLineTextInput (label : string) value (onChangeFunction : string -> unit) =
      Mui.textField [
        textField.classes.root "inputPadding"
        textField.label label
        textField.value value
        textField.fullWidth true
        textField.variant.outlined
        textField.required true
        textField.multiline true
        textField.rows 5
        textField.onChange onChangeFunction ]

  let RotationSelect (label : string) value (onChangeFunction : string -> unit) =
    Mui.select [
      select.classes.root "inputPadding"
      select.variant.outlined
      select.label label
      select.value value
      select.onChange onChangeFunction
      select.children [
        Mui.menuItem [ menuItem.children ["GOED" ]]
        Mui.menuItem [ menuItem.children ["GEMIDDELD"] ]
        Mui.menuItem [ menuItem.children ["SWAK"] ]
        Mui.menuItem [ menuItem.children ["NVT"] ]

      ] ]