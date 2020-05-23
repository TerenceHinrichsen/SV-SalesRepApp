namespace Components

open Feliz
open Feliz.MaterialUI

module Buttons =
  let primaryButton (id: int) (text: string) onClick =
    Mui.button [
      button.type'.button
      button.fullWidth true
      button.color.primary
      prop.id id
      prop.onClick onClick
      button.children [ text ]
    ]

  let secondaryButton (id: int) (text: string) onClick =
    Mui.button [
      button.type'.button
      button.fullWidth true
      button.color.secondary
      prop.onClick onClick
      prop.id id
      button.children [ text ]
    ]

  let primaryButtonLarge (text: string) onClick =
    Mui.button [
      button.type'.button
      button.fullWidth true
      button.color.primary
      button.size.large
      button.variant.contained
      prop.onClick onClick
      button.children [ text ]
    ]
  let secondaryButtonLarge (text: string) onClick =
    Mui.button [
      button.type'.button
      button.fullWidth true
      button.color.secondary
      button.size.large
      button.variant.contained
      prop.onClick onClick
      button.children [ text ]
    ]
