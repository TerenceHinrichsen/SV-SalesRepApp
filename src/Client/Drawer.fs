namespace Components

open Elmish
open Feliz
open Feliz.MaterialUI
open Fable.MaterialUI.MaterialDesignIcons

module Drawer =
  type MenuItem =
    | Home
    | CustomerListings
    | CustomerEdit
    | CustomerView
    | CustomerCreate
    | ChangeHistory

  type State = { drawerIsOpen : bool
                 CurrentMenu: MenuItem}

  type Message =
    | ToggleDrawerState
    | ChangeMenu of MenuItem

  let init() = {drawerIsOpen = false; CurrentMenu = Home}, Cmd.none
  
  let update message state =
    match message with
    | ToggleDrawerState -> {state with drawerIsOpen = (not state.drawerIsOpen)}, Cmd.none
    | ChangeMenu newMenu -> {state with CurrentMenu = newMenu; drawerIsOpen = false}, Cmd.none
  
  let menuItems dispatch =
    ["Home", Home;"Listings", CustomerListings; "Customer management", CustomerEdit; "Account application", CustomerCreate; "Change history", ChangeHistory]
    |> List.map (fun x ->
          Mui.listItem [ listItem.button true
                         prop.onClick (fun _ -> dispatch (x |> snd))
                         listItem.children [
                         Mui.listItemText [ listItemText.primary (x |> fst) ] ] ] )

  let view (state:State) dispatch =
    //TODO: add "clickAwayListener" to close drawer when user clicks out of drawer
    Mui.drawer [
      drawer.open' state.drawerIsOpen
      drawer.variant.persistent
      drawer.anchor.left
      drawer.children [
        Mui.iconButton [
          iconButton.edge.end'
          iconButton.disableRipple true
          iconButton.disableFocusRipple true
          prop.onClick (fun _ -> dispatch ToggleDrawerState)
          iconButton.children [
            chevronLeftIcon[]
          ] ]
        Mui.list [
          list.children (menuItems (fun x -> dispatch (ChangeMenu x)))
        ] ] ] 