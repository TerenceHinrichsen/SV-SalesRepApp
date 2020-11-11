open System

open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection

open Giraffe
open Shared

open Fable.Remoting.Server
open Fable.Remoting.Giraffe

open Database
open Saturn

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
  getCustomerVisitHistory            = fun customerId -> async {return DbFunctions.getCustomerVisitHistory customerId}
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


let app =
    application {
        url "http://0.0.0.0:8085"
        use_router webApp
        memory_cache
        use_static "public"
        use_gzip
        use_developer_exceptions
    }
run app
