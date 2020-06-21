namespace Components

open Feliz
open Feliz.MaterialUI
open Fable.MaterialUI.MaterialDesignIcons

module AppBar =
  let init () =
    Drawer.init ()

  let update message (state : Drawer.State) =
    match message with
    | Drawer.Message.ToggleDrawerState -> {state with drawerIsOpen = not state.drawerIsOpen}
    | Drawer.Message.ChangeMenu _ -> state

  let view (state: Drawer.State) dispatch =
    Mui.appBar[
      appBar.position.sticky
      appBar.elevation 5
      appBar.color.primary
      appBar.children [
        Mui.toolbar [
          toolbar.children [
            Mui.iconButton [
              prop.onClick (fun _ -> dispatch Drawer.ToggleDrawerState)
              iconButton.color.inherit'
              iconButton.children [
                menuIcon[]]]
            Mui.typography [
              typography.align.left
              typography.classes.root "AppTitle"
              typography.display.block
              typography.children ["THE foXcE" ] ] ] ] ] ]