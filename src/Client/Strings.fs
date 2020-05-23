namespace Components

open Feliz
open Feliz.MaterialUI

module Strings =
  let header1 (string: string) =
    Mui.typography [
      typography.variant.h1
      typography.children string ]

  let header2 (string: string) =
    Mui.typography [
      typography.variant.h2
      typography.children string ]

  let header3 (string: string) =
    Mui.typography [
      typography.variant.h3
      typography.align.center
      typography.children string ]

  let header4 (string: string) =
    Mui.typography [
      typography.variant.h4
      typography.children string ]

  let header5 (string: string) =
    Mui.typography [
      typography.variant.h5
      typography.children string ]

  let subtitle (string: string) =
    Mui.typography [
      typography.variant.subtitle1
      typography.children string ]

  let body1 (string: string) =
    Mui.typography [
      typography.variant.body1
      typography.align.center
      typography.children string ]
