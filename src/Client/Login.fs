namespace Pages

open Components
open System
open Elmish
open Elmish.React


module Login =
  open Feliz
  open Feliz.MaterialUI

  type State = {
    Username: string
    Password: string
  }

  type Message =
    | UsernameChanged of string
    | Passwordchanged of string
    | LoginRequested of string * string

  let init = {
    Username = String.Empty
    Password = String.Empty
  }

  let update (msg : Message) (state : State) : State * Cmd<Message> =
    match msg with
    | UsernameChanged x -> {state with Username = x}, Cmd.none
    | Passwordchanged x -> {state with Password = x}, Cmd.none
    | LoginRequested (username, password) -> state, Cmd.none

  let view (state : State) (dispatch : Message -> unit) =
    Mui.paper [
      paper.classes.root "login"
      paper.children [
        Strings.header3 "Only authorised users allowed"
        Mui.card [
          card.classes.root "loginCard"
          card.variant.outlined
          card.children [
            Mui.formControl [
              formControl.fullWidth true
              formControl.margin.normal
              formControl.children [
                Mui.textField [
                  textField.autoFocus true
                  textField.fullWidth true
                  textField.onChange (fun x -> dispatch <| UsernameChanged x)
                  textField.variant.outlined
                  textField.label "Username"
                  textField.required true ] ] ]
            Mui.formControl [
              formControl.fullWidth true
              formControl.margin.normal
              formControl.children [
                Mui.textField [
                  textField.fullWidth true
                  textField.onChange (fun x -> dispatch <| Passwordchanged x)
                  textField.variant.outlined
                  textField.type' "password"
                  textField.label "Password"
                  textField.required true ] ] ]
            Mui.formControl [
              formControl.fullWidth true
              formControl.margin.normal
              formControl.children [
                Mui.button [
                  button.type'.submit
                  button.variant.contained
                  button.color.primary
                  prop.onClick (fun _ -> dispatch  <| LoginRequested (state.Username, state.Password))
                  button.fullWidth true
                  button.children "LOG IN"
                ]
              ]
            ]
            Strings.body1 "Please note that all information on this site is confidential and should not be shared with any individual."
          ]
        ]
      ]]