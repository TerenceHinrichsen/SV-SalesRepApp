namespace API

module Server =
  open System
  open Fable.Core
  open Shared
  open Fable.Remoting.Client

  /// A proxy you can use to talk to server directly
  let api : ISuccessApi =
    Remoting.createApi()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.buildProxy<ISuccessApi>

