namespace Components

open Elmish
open Elmish.Toastr

module Toast =

  let private basicToast message timeout =
    Toastr.message message
    |> Toastr.withProgressBar
    |> Toastr.timeout timeout
    |> Toastr.position BottomFullWidth

  let errorMessage timeout (exn: System.Exception) : Cmd<'Message> =
    basicToast exn.Message timeout |> Toastr.error

  let successMessage timeout message  : Cmd<'Message> =
    basicToast message timeout |> Toastr.success

  let warningMessage timeout message  : Cmd<'Message> =
    basicToast message timeout |> Toastr.warning

  let infoMessage timeout message  : Cmd<'Message> =
    basicToast message timeout |> Toastr.info
