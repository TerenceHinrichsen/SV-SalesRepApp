namespace DatabaseInterface
open Configuration
open Dapper
open System
open System.Data.SqlClient
open Helpers

module Customer =
  type Customer = {
    CustomerId: int
    CustomerAccountNumber : string
    CustomerAccountName: string option
    CustomerAccountDescription : string option
    AreaId: int option
    AreaCode : string option
    AreaDescription : string option
    GroupId: int option
    GroupCode : string option
    GroupDescription : string option
    PriceListId: int option
    PriceListName : string option
    PriceListDescription : string option
    CustomerIsOnHold: bool
    Physical : string option
    Physical2 : string option
    Suburb : string option
    Physical4 : string option
    GPS : string option
    PhysicalPC : string option
    Telephone : string option
    Cellphone : string option
    Fax : string option
    MainAccLink: int option
    udARLastVisit : System.DateTime option
    IsChildAccount : string
    MasterAccountNumber : string option
    MasterAccountName : string option
    AgeingTermCode: string option
    AgeingTermDescription: string option
    RepVisitFrequency: string option
    DeliveryContact: string option
    ContactPerson: string option
    SalesRepId: int option
    SalesRepCode: string option
    SalesRepName: string option
    Email: string option
    DeliveryEmail: string option
    MarketSegment : string option
    AccountStatus: string option
    }

  type CustomerDisplayDetail = {
    Id: int
    Name: string option
    Group: string option
    Area: string option
    AgeingPeriod: string option
    Rotation: string option
    GYPercentage: int option
    GPPercentage: string option
    OtherSuppliers: string option
    LastRepVisit: string option
    LastInvoiceDate: string option
    GeneralComments: string option
    Telephone : string option
    }

    type TransactionDetail = {
      Number: string option
      TransactionDate: string option
      Total: string
    }

    type CustomerSpecialDb = {
      CustomerId : int option
      ItemCode : string option
      ItemDescription : string option
      DozenPrice : double option
      UnitPrice : double option
      EffectiveDate : DateTime option
      ExpiryDate : DateTime option
      ContractType:  string option
    }

    type SalesHistoryPoint = {
      Period: string
      Boxes: System.Double
      Value : System.Double
      QuarterTrend : System.Double
    }

    type CustomerProductMixDataPoint = {
      ProductCode : string
      TotalBoxes:  Double
    }

    type CustomerVisitData = {
        CustomerId : int64
        VisitDate: DateTime
        Rotation: string
        GYPercentage: int
        GPPercentage: string
        OtherSuppliers : string
        Comments: string
    }

  let FetchAllFromDatabase =
    registerOptionTypes()
    let sql = SqlScripts.CustomerListSelect
    let connection = new SqlConnection(sqlConnectionString)
    do connection.Open()
    let transaction = connection.BeginTransaction("ReadAllCustomers")
    connection.Query<Customer>(sql, transaction = transaction) |> List.ofSeq

  let FetchProductMix customerId =
    registerOptionTypes()
    let sql = SqlScripts.CustomerProductMix
    let connection = new SqlConnection(sqlConnectionString)
    do connection.Open()
    let transaction = connection.BeginTransaction("LookupCustomerId")
    let parameters = dict ["CustomerId" => customerId ]
    connection.Query<CustomerProductMixDataPoint>(sql, parameters ,transaction = transaction) |> List.ofSeq


  let customerVisitHistory customerId =
    registerOptionTypes()
    let sql = SqlScripts.CustomerVisitHistory
    let connection = new SqlConnection(sqlConnectionString)
    do connection.Open()
    let transaction = connection.BeginTransaction("LookupCustomerVisitHistory")
    let parameters = dict ["CustomerId" => customerId ]
    connection.Query<CustomerVisitData>(sql, parameters ,transaction = transaction) |> List.ofSeq


  let LookupCustomerId customerCode =
    registerOptionTypes()
    let sql = SqlScripts.CustomerIdLookup
    let connection = new SqlConnection(sqlConnectionString)
    do connection.Open()
    let transaction = connection.BeginTransaction("LookupCustomerId")
    let parameters = dict ["CustomerCode" => customerCode ]
    connection.Query<int>(sql, parameters ,transaction = transaction) |> List.ofSeq

  let FetchDetailFromDatabase customerId =
    registerOptionTypes()
    let sql = SqlScripts.customerDetailSelect
    let connection = new SqlConnection(sqlConnectionString)
    do connection.Open()
    let transaction = connection.BeginTransaction("ReadOneCustomers")
    let parameters = dict ["CustomerId" => customerId]
    connection.Query<Customer>(sql, parameters ,transaction = transaction) |> List.ofSeq

  let FetchListBasedOnSearch (search: string) =
    registerOptionTypes()
    let sql = SqlScripts.customerSearchString
    let connection = new SqlConnection(sqlConnectionString)
    do connection.Open()
    let transaction = connection.BeginTransaction("ReadOneCustomers")
    let wildcardSearch = sprintf "%%%s%%" (search.Trim())
    let parameters = dict ["Search" => wildcardSearch]
    printfn "%s" wildcardSearch
    connection.Query<Customer>(sql, parameters, transaction = transaction) |> List.ofSeq

  let FetchListBasedOnSalesRep salesRepId =
    registerOptionTypes()
    let sql = SqlScripts.CustomerListRep
    let connection = new SqlConnection(sqlConnectionString)
    do connection.Open()
    let transaction = connection.BeginTransaction("ReadOneCustomers")
    let parameters = dict ["Rep" => salesRepId]
    connection.Query<Customer>(sql, parameters, transaction = transaction) |> List.ofSeq


  let FetchListBasedOnGroup groupId =
    registerOptionTypes()
    let sql = SqlScripts.CustomerListGroup
    let connection = new SqlConnection(sqlConnectionString)
    do connection.Open()
    let transaction = connection.BeginTransaction("ReadOneCustomers")
    let parameters = dict ["Group" => groupId]
    connection.Query<Customer>(sql, parameters, transaction = transaction) |> List.ofSeq

  let FetchListBasedOnArea areaId =
    registerOptionTypes()
    let sql = SqlScripts.CustomerListArea
    let connection = new SqlConnection(sqlConnectionString)
    do connection.Open()
    let transaction = connection.BeginTransaction("ReadOneCustomers")
    let parameters = dict ["Area" => areaId]
    connection.Query<Customer>(sql, parameters, transaction = transaction) |> List.ofSeq

  let FetchListBasedOnAreaGroupRep areaId groupId repId =
    registerOptionTypes()
    let sql = SqlScripts.CustomerListAreaGroupRep
    let connection = new SqlConnection(sqlConnectionString)
    do connection.Open()
    let transaction = connection.BeginTransaction("ReadOneCustomers")
    let parameters = dict [ "Area" => areaId
                            "Group" => groupId
                            "Rep" => repId ]
    connection.Query<Customer>(sql, parameters, transaction = transaction) |> List.ofSeq

  let FetchListBasedOnAreaRep areaId repId =
    registerOptionTypes()
    let sql = SqlScripts.CustomerListAreaRep
    let connection = new SqlConnection(sqlConnectionString)
    do connection.Open()
    let transaction = connection.BeginTransaction("ReadOneCustomers")
    let parameters = dict ["Area" => areaId
                           "Rep" => repId ]
    connection.Query<Customer>(sql, parameters, transaction = transaction) |> List.ofSeq

  let FetchListBasedOnAreaGroup areaId groupId =
    registerOptionTypes()
    let sql = SqlScripts.CustomerListAreaGroup
    let connection = new SqlConnection(sqlConnectionString)
    do connection.Open()
    let transaction = connection.BeginTransaction("ReadOneCustomers")
    let parameters = dict ["Area"  => areaId
                           "Group" => groupId]
    connection.Query<Customer>(sql, parameters, transaction = transaction) |> List.ofSeq

  let FetchListBasedOnRepGroup repId groupId =
    registerOptionTypes()
    let sql = SqlScripts.CustomerListRepGroup
    let connection = new SqlConnection(sqlConnectionString)
    do connection.Open()
    let transaction = connection.BeginTransaction("ReadOneCustomers")
    let parameters = dict ["Rep" => repId
                           "Group" => groupId ]
    connection.Query<Customer>(sql, parameters, transaction = transaction) |> List.ofSeq

  let FetchListBasedOn_EITHER_AreaGroupRep areaId repId groupId search =
    match areaId, repId, groupId, search with
    | Some area, Some rep, Some group, _ ->
      printfn "All three, area & rep & group"
      FetchListBasedOnAreaGroupRep area rep group
    | Some area, Some rep, None, _ ->
      printfn "Only Area and Rep"
      FetchListBasedOnAreaRep area rep
    | Some area, None, Some group, _ ->
      printfn "Area and group"
      FetchListBasedOnAreaGroup area group
    | Some area, None, None, _ ->
      printfn "Only area"
      FetchListBasedOnArea area
    | None, Some rep, Some group, _ ->
      printfn "Rep and Group"
      FetchListBasedOnRepGroup rep group
    | None, Some rep, None, _ ->
      printfn "Only rep"
      FetchListBasedOnSalesRep rep
    | None, None, Some group, _ ->
      printfn "Only group"
      FetchListBasedOnGroup group
    | None, None, None, Some search ->
      printf "Search string"
      FetchListBasedOnSearch search
    | None, None, None, _ ->
      printfn "No parameters to query database!"
      failwith "Must provide at least one parameter"

  let UpdateCustomerDetails customerId (customerName : string) (groupId: int ) (repId: int ) (pricelistId: int )
    (contact1: string ) (contact2: string ) (telephone: string ) (cell: string ) (email: string ) (physical1: string) (physical2: string)
    (physical3: string) (deliveryEmail: string) (marketSegment: string)  (repVisitFreq: string) =
    registerOptionTypes()
    let sql = SqlScripts.UpdateCustomerMaster
    let connection = new SqlConnection(sqlConnectionString)
    do connection.Open()
    let transaction = connection.BeginTransaction("ReadOneCustomers")
    let changeText = sprintf "Updated to: CustomerId: %A CustomerName: %A GroupId: %A RepId: %A PricelistId: %A Contact1: %A
                              Contact2: %A Telepone: %A Cell:%A Email: %A Physical: %A Physical2: %A Physical3: %A DeliveryEmail: %A MarketSegment: %A RepVisitFrequency: %A "
                              customerId  customerName  groupId  repId  pricelistId  contact1  contact2  telephone
                              cell email physical1 physical2  physical3 deliveryEmail   marketSegment repVisitFreq
    let parameters = dict [ "CustomerId" => customerId
                            "ChangeText" =>  changeText
                            "CustomerName" => customerName
                            "GroupId"      => groupId
                            "RepId"        => repId
                            "PricelistId"  => pricelistId
                            "Contact1"     => contact1
                            "Contact2"     => contact2
                            "Telephone"    => telephone
                            "Cell"         => cell
                            "Email"        => email
                            "Physical1"    => physical1
                            "Physical2"    => physical2
                            "Physical3"    => physical3
                            "DeliveryEmail"=> deliveryEmail
                            "MarketSegment"=> marketSegment
                            "RepVisitFreq" => repVisitFreq  ]
    try
      try
        connection.Query<unit>(sql, parameters, transaction = transaction) |> ignore
        transaction.Commit()
        "Success"
      with
        | exn -> sprintf "Could not insert into datase %A" exn
    finally
      connection.Close()

  let fetchCustomerDisplayDetails (customerId: int) =
    registerOptionTypes()
    let sql = SqlScripts.CustomerViewData
    let connection = new SqlConnection(sqlConnectionString)
    do connection.Open()
    let transaction = connection.BeginTransaction("ReadOneCustomers")
    let parameters = dict ["CustomerId" => customerId ]
    try connection.Query<CustomerDisplayDetail> (sql, parameters, transaction) |> Seq.toList
    finally connection.Close()

  let fetchLast5Invoices (customerId : int) =
    registerOptionTypes()
    let sql = SqlScripts.Last5Invoices
    let connection = new SqlConnection(sqlConnectionString)
    do connection.Open()
    let transaction = connection.BeginTransaction("ReadOneCustomers")
    let parameters = dict ["CustomerId" => customerId ]
    try connection.Query<TransactionDetail> (sql, parameters, transaction) |> Seq.toList
    finally connection.Close()

  let fetchLast5Credits (customerId : int) =
    registerOptionTypes()
    let sql = SqlScripts.Last5Credits
    let connection = new SqlConnection(sqlConnectionString)
    do connection.Open()
    let transaction = connection.BeginTransaction("ReadOneCustomers")
    let parameters = dict ["CustomerId" => customerId ]
    try connection.Query<TransactionDetail> (sql, parameters, transaction) |> Seq.toList
    finally connection.Close()

  let fetchSpecials (customerId : int) =
    registerOptionTypes()
    let sql = SqlScripts.customerCurrentSpecials
    let connection = new SqlConnection(sqlConnectionString)
    do connection.Open()
    let transaction = connection.BeginTransaction("CustomerSpecials")
    let parameters = dict ["CustomerId" => customerId ]
    try connection.Query<CustomerSpecialDb> (sql, parameters, transaction) |> Seq.toList
    finally connection.Close()

  let fetchSalesHistory (customerId : int) =
    registerOptionTypes()
    let sql = SqlScripts.TwoYearSalesHistory
    let connection = new SqlConnection(sqlConnectionString)
    do connection.Open()
    let transaction = connection.BeginTransaction("ReadOneCustomers")
    let parameters = dict ["CustomerId" => customerId ]
    try connection.Query<SalesHistoryPoint> (sql, parameters, transaction) |> Seq.toList
    finally connection.Close()

  let markForDeletion (customerId : int, reason: string) =
    registerOptionTypes()
    let sql = SqlScripts.markCustomerForDeletion
    let connection = new SqlConnection(sqlConnectionString)
    do connection.Open()
    let transaction = connection.BeginTransaction("ReadOneCustomers")
    let parameters = dict ["CustomerId" => customerId
                           "Reason" => reason ]
    try connection.Query<unit> (sql, parameters, transaction) |> ignore
        transaction.Commit()
    finally connection.Close()

  let markForArchive (customerId : int, reason: string) =
    registerOptionTypes()
    let sql = SqlScripts.markCustomerForArchive
    let connection = new SqlConnection(sqlConnectionString)
    do connection.Open()
    let transaction = connection.BeginTransaction("ReadOneCustomers")
    let parameters = dict ["CustomerId" => customerId
                           "Reason" => reason ]
    try connection.Query<unit> (sql, parameters, transaction) |> ignore
        transaction.Commit()
    finally connection.Close()

  let newAccountApplication (customerName : string) (customerDescription : string) (groupId: int ) (repId: int ) (pricelistId: int )
    (contact1: string ) (contact2: string ) (telephone: string ) (cell: string ) (email: string ) (physical1: string) (physical2: string)
    (physical3: string) (deliveryEmail: string) (marketSegment: string)  (repVisitFreq: string) (areaId: int)
    (gps: string) (postCode: string) (fax: string) (taxNumber: string) (registrationNumber: string)
        =
    registerOptionTypes()
    let sql = SqlScripts.CreateCustomer
    let connection = new SqlConnection(sqlConnectionString)
    do connection.Open()
    let transaction = connection.BeginTransaction("CreateNew")
    let changeText = sprintf "New account for: CustomerName: %A GroupId: %A RepId: %A PricelistId: %A Contact1: %A
                              Contact2: %A Telepone: %A Cell:%A Email: %A Physical: %A Physical2: %A Physical3: %A DeliveryEmail: %A MarketSegment: %A RepVisitFrequency: %A "
                              customerName  groupId  repId  pricelistId  contact1  contact2  telephone
                              cell email physical1 physical2  physical3 deliveryEmail   marketSegment repVisitFreq
    let parameters = dict [ "ChangeText"          => changeText
                            "AccountName"        => customerName
                            "AccountDescription" => customerDescription
                            "AreaId"              => areaId
                            "GroupId"             => groupId
                            "PriceListId"         => pricelistId
                            "Physical1"           => physical1
                            "Physical2"           => physical2
                            "Suburb"              => physical3
                            "GPS"                 => gps
                            "PostCode"            => postCode
                            "Telephone"           => telephone
                            "Cellphone"           => cell
                            "Fax"                 => fax
                            "RepVisitFreq"        => repVisitFreq
                            "Contact_Person"      => contact1
                            "DeliverTo"           => contact2
                            "DeliveryEmail"       => deliveryEmail
                            "MarketSegment"       => marketSegment
                            "Email"               => email
                            "RepId"               => repId
                            "TaxNumber"           => taxNumber
                            "RegistrationNumber"  => registrationNumber ]
    try
      try
        connection.Query<unit>(sql, parameters, transaction = transaction) |> ignore
        transaction.Commit()
        "Success"
      with
        | exn -> sprintf "Could not insert into database %A" exn
    finally
      connection.Close()

  let recordVisit (visitData: CustomerVisitData) =
    registerOptionTypes()
    let sql = SqlScripts.recordCustomerVisit
    let connection = new SqlConnection(sqlConnectionString)
    do connection.Open()
    let transaction = connection.BeginTransaction("ReadOneCustomers")
    let parameters = dict ["CustomerId" => visitData.CustomerId
                           "VisitDate" => visitData.VisitDate
                           "Rotation" => visitData.Rotation
                           "GYPercentage" => visitData.GYPercentage
                           "GPPercentage" => visitData.GPPercentage
                           "OtherSuppliers" => visitData.OtherSuppliers
                           "GeneralComments" => visitData.Comments ]
    try connection.Query<unit> (sql, parameters, transaction) |> ignore
        transaction.Commit()
    finally connection.Close()