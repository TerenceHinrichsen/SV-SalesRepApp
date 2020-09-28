open System
open System.IO
open System.Threading.Tasks

open Microsoft.AspNetCore
open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.DependencyInjection

open FSharp.Control.Tasks.V2
open Giraffe
open Shared

open Fable.Remoting.Server
open Fable.Remoting.Giraffe

open Database

let tryGetEnv = System.Environment.GetEnvironmentVariable >> function null | "" -> None | x -> Some x

let publicPath =
  tryGetEnv "public_path"
  |> Option.defaultValue "../Client/public"
  |> Path.GetFullPath

let port =
    "SERVER_PORT"
    |> tryGetEnv |> Option.map uint16 |> Option.defaultValue 8085us

let successApi = {
  getCustomerById                    = fun (value)   -> async { return DbFunctions.getCustomerById value }
  getCustomerIdFromCode              = fun (code)    -> async { return DbFunctions.getCustomerIdForCode code}
  getAreaList                        = fun ()        -> async { return DbFunctions.getAreaList }
  getPricelist                       = fun ()        -> async { return DbFunctions.getPriceLists }
  getGroupList                       = fun ()        -> async { return DbFunctions.getGroupList }
  getSalesRepList                    = fun ()        -> async { return DbFunctions.getSalesRepList }
  getCustomerListBySearch            = fun search    -> async { return DbFunctions.getCustomerBySearch search}
  getCustomerListByArea              = fun (areaId)  -> async { return DbFunctions.getCustomerByAreaId areaId}
  getCustomerListBySalesRep          = fun (repId)   -> async { return DbFunctions.getCustomerByRepId repId }
  getCustomerListByGroup             = fun (groupId) -> async { return DbFunctions.getCustomerByRepId groupId }
  getCustomerListByOneOfRepGroupArea = fun (area, rep, group, search) -> async { return DbFunctions.getCustomerByCriteria area rep group search }
  getMarketSegments                  = fun ()       -> async { return DbFunctions.getMarketSegments }
  getRepVisitFrequencies             = fun ()       -> async { return DbFunctions.getRepVisitFreq }
  updateCustomerMaster               = fun (customerUpdateData) -> async {return DbFunctions.updateCustomerData customerUpdateData }
  getCustomerViewDetail              = fun (customerId) -> async { return DbFunctions.fetchDisplayDetail customerId }
  getLast5Invoices                   = fun customerId   -> async { return DbFunctions.fetchLast5Invoices customerId }
  getLast5Credits                    = fun customerId   -> async { return DbFunctions.fetchLast5CreditNotes customerId }
  getSalesGraphData                  = fun customerId   -> async { return DbFunctions.fetchSalesHistory customerId }
  createNewCustomerAccount           = fun customerApplicationData -> async {return DbFunctions.newCustomerApplication customerApplicationData }
  loginUser                          = fun (username, password) -> async {return DbFunctions.loginUser (username, password)}
  addNewTodo                         = fun (todo) -> async { return DbFunctions.addTodo todo}
  getCustomerSpecials                = fun customerId -> async { return DbFunctions.fetchCustomerSpecials customerId }
  getProductMix                      = fun customerId -> async { return DbFunctions.fetchProductMix customerId }
  markCustomerForDeletion            = fun (customerId, reason) -> async {return DbFunctions.markCustomerForDeletion customerId reason }
  markCustomerForArchive             = fun (customerId, reason) -> async {return DbFunctions.markCustomerForArchive customerId reason }
  recordCustomerVisit                = fun (customerVisitData) -> async {return DbFunctions.recordCustomerVisit customerVisitData }
}

let errorHandler (ex: Exception) (routeInfo: RouteInfo<HttpContext>) =
  printfn "Exception occurred at: %s while executing %s" routeInfo.path routeInfo.methodName
  printfn "%A" ex
  Propagate (sprintf "%A" ex.Message)

let webApp =
    Remoting.createApi()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.fromValue successApi
    |> Remoting.withErrorHandler errorHandler
    |> Remoting.buildHttpHandler


let configureApp (app : IApplicationBuilder) =
    app.UseDefaultFiles()
       .UseStaticFiles()
       .UseGiraffe webApp

let configureServices (services : IServiceCollection) =
    services.AddGiraffe() |> ignore

WebHost
    .CreateDefaultBuilder()
    .UseWebRoot(publicPath)
    .UseContentRoot(publicPath)
    .Configure(Action<IApplicationBuilder> configureApp)
    .ConfigureServices(configureServices)
    .UseIISIntegration()
    .UseUrls("http://0.0.0.0:" + port.ToString() + "/")
    .Build()
    .Run()
