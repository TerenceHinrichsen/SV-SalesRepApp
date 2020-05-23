namespace API

module ServerPath =
  open System
  open Fable.Core

  /// when publishing to IIS, your application most likely runs inside a virtual path (i.e. localhost/SafeApp)
  /// every request made to the server will have to account for this virtual path
  /// so we get the virtual path from the location
  /// `virtualPath` of `http://localhost/SafeApp` -> `/SafeApp/`
  [<Emit("window.location.pathname")>]
  let virtualPath : string = jsNative

  /// takes path segments and combines them into a valid path
  let combine (paths: string list) =
      paths
      |> List.map (fun path -> List.ofArray (path.Split('/')))
      |> List.concat
      |> List.filter (fun segment -> not (segment.Contains(".")))
      |> List.filter (String.IsNullOrWhiteSpace >> not)
      |> String.concat "/"
      |> sprintf "/%s"

  /// Normalized the path taking into account the virtual path of the server
  let normalize (path: string) = combine [virtualPath; path]

module Server = 
  open Shared
  open Fable.Remoting.Client

  // normalize routes so that they work with IIS virtual path in production
  let normalizeRoutes typeName methodName =
      Route.builder typeName methodName
      |> ServerPath.normalize

  /// A proxy you can use to talk to server directly
  let api : ISuccessApi =
    Remoting.createApi()
    |> Remoting.withRouteBuilder normalizeRoutes
    |> Remoting.buildProxy<ISuccessApi>

