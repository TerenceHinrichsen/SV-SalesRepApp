namespace Pages

open Components
open System
open Elmish
open API


module Login =
  open Feliz
  open Feliz.MaterialUI

  type State = {
    Username: string
    Password: string
    IsLoading : bool 
    LoginSuccess: bool option
    StatusMessage: string  }

  type Message =
    | UsernameChanged of string
    | Passwordchanged of string
    | LoginRequested
    | LoginResponse of bool

  let init = {
    Username = String.Empty
    Password = String.Empty
    IsLoading = false
    LoginSuccess = None
    StatusMessage = "" }

  let update (msg : Message) (state : State) : State * Cmd<Message> =
    match msg with
    | UsernameChanged x -> {state with Username = x}, Cmd.none
    | Passwordchanged x -> {state with Password = x}, Cmd.none
    | LoginRequested -> {state with IsLoading = true}, (Cmd.OfAsync.perform Server.api.loginUser (state.Username, state.Password)) LoginResponse
    | LoginResponse success ->
        let response = if not success then "Could not log you in!" else ""
        {state with IsLoading = false; StatusMessage = response; LoginSuccess = Some success }, Cmd.none

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
                  textField.disabled state.IsLoading
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
                  textField.disabled state.IsLoading
                  textField.onChange (fun x -> dispatch <| Passwordchanged x)
                  textField.variant.outlined
                  textField.type' "password"
                  textField.label "Password"
                  textField.error (if state.StatusMessage <> "" then true else false)
                  textField.required true ] ] ]
            Mui.formControl [
              formControl.fullWidth true
              formControl.margin.normal
              formControl.children [
                Mui.button [
                  button.type'.submit
                  button.disabled state.IsLoading
                  button.variant.contained
                  button.color.primary
                  prop.onClick (fun _ -> dispatch  <| LoginRequested)
                  button.fullWidth true
                  button.children "LOG IN"
                ]
              ] ]
            if state.IsLoading then Mui.linearProgress[]
            if state.LoginSuccess |> Option.isSome then 
                if not state.LoginSuccess.Value then Mui.alert[ alert.severity.error; alert.children[state.StatusMessage]]
            Strings.body1 "Please note that all information on this site is confidential and should not be shared with any individual."
          ]
        ]
      ]]