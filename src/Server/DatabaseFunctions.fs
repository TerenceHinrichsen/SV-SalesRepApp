namespace Database
module DbFunctions =

  open DatabaseInterface
  open Shared

  let getAreaList =
    Area.FetchAllFromDatabase |> List.map (fun x -> {
      Area.Id = x.AreaId
      Area.Code = x.AreaCode
      Area.Description = x.AreaDescription
    })

  let getGroupList =
    Group.FetchAllFromDatabase |> List.map(fun x -> {
      Group.Id = x.GroupId
      Group.Code = x.GroupCode
      Group.Description = x.GroupDescription
    })

  let getPriceLists =
    PriceList.FetchAllFromDatabase |> List.map(fun x -> {
      Shared.PriceList.Id = x.Id
      Name = x.Name
      Description = x.Description
    })

  let getSalesRepList =
    SalesRep.FetchAllFromDatabase |> List.map(fun x -> {
      Shared.SalesRep.Id = x.Id
      Code = x.Code
      Description = x.Description
    })

  let getMarketSegments =
    UserDefinedFields.MarketSegments |> Array.toList

  let loginUser (username: string, password: string) =
    User.login username password

  let getRepVisitFreq =
    UserDefinedFields.RepVisitFrequencies |> Array.toList

  let fromDatabase (customer : Customer.Customer ) : Shared.Customer = {
      Shared.Customer.CustomerId  = customer.CustomerId
      CustomerAccountNumber       = customer.CustomerAccountNumber
      CustomerAccountName         = customer.CustomerAccountName
      CustomerAccountDescription  = customer.CustomerAccountDescription
      AreaId                      = customer.AreaId
      AreaCode                    = customer.AreaCode
      AreaDescription             = customer.AreaDescription
      GroupId                     = customer.GroupId
      GroupCode                   = customer.GroupCode
      GroupDescription            = customer.GroupDescription
      PriceListId                 = customer.PriceListId
      PriceListName               = customer.PriceListName
      PriceListDescription        = customer.PriceListDescription
      CustomerIsOnHold            = customer.CustomerIsOnHold
      Physical                    = customer.Physical
      Physical2                   = customer.Physical2
      Suburb                      = customer.Suburb
      Physical4                   = customer.Physical4
      GPS                         = customer.GPS
      PhysicalPC                  = customer.PhysicalPC
      Telephone                   = customer.Telephone
      Cellphone                   = customer.Cellphone
      Fax                         = customer.Fax
      MainAccLink                 = customer.MainAccLink
      udARLastVisit               = customer.udARLastVisit
      IsChildAccount              = customer.IsChildAccount
      MasterAccountNumber         = customer.MasterAccountNumber
      MasterAccountName           = customer.MasterAccountName
      RepVisitFrequency           = customer.RepVisitFrequency
      ContactPerson               = customer.ContactPerson
      DeliveryContact             = customer.DeliveryContact
      AgeingTermCode              = customer.AgeingTermCode
      AgeingTermDescription       = customer.AgeingTermDescription
      DeliveryEmail               = customer.DeliveryEmail
      SalesRepId                  = customer.SalesRepId
      MarketSegment               = customer.MarketSegment
      Email                       = customer.Email
      AccountStatus               = customer.AccountStatus }

  let getCustomerById id =
    let response = Customer.FetchDetailFromDatabase id |> List.map(fun x -> fromDatabase x)
    if response.Length <> 1 then failwith "Not found!" else response.Head

  let getCustomerByAreaId areaId =
    Customer.FetchListBasedOnArea areaId |> List.map(fun x -> fromDatabase x)

  let getCustomerByGroupId groupId =
    Customer.FetchListBasedOnGroup groupId |> List.map(fun x -> fromDatabase x)

  let getCustomerByRepId repId =
    Customer.FetchListBasedOnSalesRep repId |> List.map(fun x -> fromDatabase x)

  let getCustomerBySearch searchString =
    Customer.FetchListBasedOnSearch searchString |> List.map(fun x -> fromDatabase x)

  let getCustomerByCriteria area rep group search =
    printfn "Calling database function with Area: %A Rep: %A Group: %A" area rep group
    Customer.FetchListBasedOn_EITHER_AreaGroupRep area rep group search |> List.map(fun x -> fromDatabase x)

  let getCustomerIdForCode customerCode =
    let answer = Customer.LookupCustomerId customerCode
    if answer.Length = 0
    then 0
    else answer.Head

  let updateCustomerData (updateData : UpdateData) =
    Customer.UpdateCustomerDetails
      updateData.CustomerId
      updateData.CustomerName
      updateData.GroupId
      updateData.RepId
      updateData.PricelistId
      updateData.Contact1
      updateData.Contact2
      updateData.Telephone
      updateData.Cell
      updateData.Email
      updateData.Physical1
      updateData.Physical2
      updateData.Physical3
      updateData.DeliveryEmail
      updateData.MarketSegment
      updateData.RepVisitFreq

  let addTodo (todo: Todo) =
    Todo.CreateNew todo.CustomerId todo.Assignee todo.Message todo.PromisedDate

  let fetchLast5Invoices customerId =
    Customer.fetchLast5Invoices customerId
    |> List.map ( fun x ->
    { Shared.TransDetail.Date = x.TransactionDate
      Number = x.Number
      Total = x.Total })

  let fetchLast5CreditNotes customerId =
    Customer.fetchLast5Credits customerId
    |> List.map ( fun x ->
    { Shared.TransDetail.Date = x.TransactionDate
      Number = x.Number
      Total = x.Total })

  let fetchDisplayDetail customerId =
    let invoices = fetchLast5Invoices customerId
    let credits = fetchLast5CreditNotes customerId
    Customer.fetchCustomerDisplayDetails customerId
    |> List.map (fun x ->  {
      Shared.CustomerDisplayDetail.Id = x.Id
      Name = x.Name
      Group = x.Group
      Area = x.Area
      AgeingPeriod = x.AgeingPeriod
      Rotation = x.Rotation
      GYPercentage = x.GYPercentage |> Option.map string
      GPPercentage = x.GPPercentage
      OtherSuppliers = x.OtherSuppliers
      LastRepVisit = x.LastRepVisit
      LastInvoiceDate = x.LastInvoiceDate
      GeneralComments = x.GeneralComments
      Last5Invoices = invoices
      Last5Credits = credits }) |> List.head

  let fetchSalesHistory customerId =
    Customer.fetchSalesHistory customerId
    |> List.map (fun x -> {
      SalesGraphPoint.Period = x.Period
      SalesGraphPoint.Boxes = x.Boxes
      SalesGraphPoint.Value = x.Value
      SalesGraphPoint.QtrTrend = x.QuarterTrend
    })

  let fetchProductMix customerId =
    Customer.FetchProductMix customerId
    |> List.map (fun x ->  {
        ProductCode = x.ProductCode
        TotalBoxes = x.TotalBoxes
    })

  let newCustomerApplication newAccDetail =
    Customer.newAccountApplication
      newAccDetail.CustomerName
      newAccDetail.CustomerDescription
      newAccDetail.GroupId
      newAccDetail.RepId
      newAccDetail.PricelistId
      newAccDetail.Contact1
      newAccDetail.DeliveryContact
      newAccDetail.Telephone
      newAccDetail.Cell
      newAccDetail.Email
      newAccDetail.Physical1
      newAccDetail.Physical2
      newAccDetail.Suburb
      newAccDetail.DeliveryEmail
      newAccDetail.MarketSegment
      newAccDetail.RepVisitFreq
      newAccDetail.AreaId
      newAccDetail.GPS
      newAccDetail.PostCode
      newAccDetail.Fax

  let fetchCustomerSpecials customerId =
    Customer.fetchSpecials customerId
    |> List.map (fun x -> {
          CustomerId = x.CustomerId |> Option.defaultValue 0
          ExpiryDate = x.ExpiryDate |> Option.defaultValue System.DateTime.Now
          EffectiveDate = x.EffectiveDate |> Option.defaultValue System.DateTime.Now
          ItemCode = x.ItemCode |> Option.defaultValue "Error loading data"
          ItemDescription = x.ItemDescription |> Option.defaultValue "Error loading data"
          UnitPrice = x.UnitPrice |> Option.defaultValue 0.00 |> decimal
          DozenPrice = x.DozenPrice |> Option.defaultValue 0.00 |> decimal
          ContractType = x.ContractType |> Option.defaultValue "Error loading data"
    })

  let markCustomerForDeletion customerId reason =
    Customer.markForDeletion (customerId, reason)
    |> ignore

  let markCustomerForArchive customerId reason =
    Customer.markForArchive (customerId, reason)
    |> ignore

  let recordCustomerVisit (visitData : Shared.CustomerVisitData) =
    let requestData = {
        Customer.CustomerVisitData.CustomerId = visitData.CustomerId
        Customer.CustomerVisitData.VisitDate = visitData.VisitDate
        Customer.CustomerVisitData.Rotation = visitData.Rotation
        Customer.CustomerVisitData.GYPercentage = visitData.GYPercentage
        Customer.CustomerVisitData.GPPercentage = visitData.GPPercentage
        Customer.CustomerVisitData.OtherSupplier = visitData.OtherSupplier
        Customer.CustomerVisitData.Comments = visitData.Comments
           }
    Customer.recordVisit requestData