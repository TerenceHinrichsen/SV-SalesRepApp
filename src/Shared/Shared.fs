namespace Shared

module Route =
    /// Defines how routes are generated on server and mapped from client
    let builder typeName methodName =
        sprintf "/api/%s/%s" typeName methodName

type User = {
  UserId : int
  Username: string
  }

type Todo = {
  CustomerId : int
  Assignee : string
  Message : string
  PromisedDate: System.DateTime
  RequestedBy: string
}

type ProductMixDatapoint = {
  ProductCode : string
  TotalBoxes: double
}

type CustomerSpecial = {
    CustomerId : int
    ItemCode : string
    ItemDescription : string
    DozenPrice : decimal
    UnitPrice : decimal
    EffectiveDate : System.DateTime
    ExpiryDate : System.DateTime
    ContractType:  string
}

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
  RepVisitFrequency: string option
  ContactPerson: string option
  DeliveryContact: string option
  AgeingTermCode: string option
  AgeingTermDescription: string option
  Email: string option
  DeliveryEmail: string option
  SalesRepId: int option
  MarketSegment: string option
  AccountStatus: string option
}
type TransDetail = {
  Number: string option
  Date: string option
  Total: string
}

type CustomerDisplayDetail = {
  Id:    int
  Name:  string option
  Group: string option
  Area:  string option
  AgeingPeriod: string option
  Rotation:     string option
  GYPercentage: string option
  GPPercentage: string option
  OtherSuppliers: string option
  LastRepVisit: string option
  LastInvoiceDate: string option
  GeneralComments: string option
  Telephone: string option
  Last5Invoices : TransDetail list
  Last5Credits : TransDetail list
}

type SalesGraphPoint = {
  Period: string
  Boxes : double
  Value : double
  QtrTrend: double
}

type PriceList = {
  Id: int
  Name: string
  Description: string }

type Group = {
  Id: int
  Code: string
  Description: string }

type Area = {
  Id: int
  Code: string
  Description: string }

type SalesRep = {
  Id: int
  Code: string
  Description: string }

type UpdateData = {
  CustomerId : int
  CustomerName : string
  GroupId: int
  RepId: int
  PricelistId: int
  Contact1: string
  Contact2: string
  Telephone: string
  Cell: string
  Email: string
  Physical1: string
  Physical2: string
  Physical3: string
  DeliveryEmail: string
  MarketSegment: string
  RepVisitFreq: string
}

type NewAccountData = {
  CustomerName : string
  CustomerDescription : string
  AreaId: int
  GroupId: int
  PricelistId: int
  Physical1: string
  Physical2: string
  Suburb: string
  GPS: string
  PostCode: string
  Telephone: string
  Cell: string
  Fax: string
  RepVisitFreq: string
  Contact1: string
  DeliveryContact: string
  DeliveryEmail: string
  MarketSegment: string
  Email: string
  RepId: int
  TaxNumber : string
  RegistrationNumber : string
}

type CustomerVisitData = {
    CustomerId : int
    VisitDate: System.DateTime
    Rotation: string
    GYPercentage: int
    GPPercentage: string
    OtherSupplier : string
    Comments: string
}


type ISuccessApi =
    { getCustomerById : int -> Async<Customer>
      getCustomerIdFromCode : string -> Async<int>
      getAreaList : unit -> Async<Area list>
      getPricelist : unit -> Async<PriceList list>
      getGroupList : unit -> Async<Group list>
      getSalesRepList : unit -> Async<SalesRep list>
      getCustomerListBySearch : string -> Async<Customer list>
      getCustomerListByArea : int -> Async<Customer list>
      getCustomerListBySalesRep : int -> Async<Customer list>
      getCustomerListByGroup : int -> Async<Customer list>
      getCustomerListByOneOfRepGroupArea : (int option * int option * int option * string option) -> Async<Customer list>
      getMarketSegments : unit -> Async<string list>
      getRepVisitFrequencies : unit -> Async<string list>
      getCustomerViewDetail : int -> Async<CustomerDisplayDetail>
      updateCustomerMaster : UpdateData -> Async<string>
      createNewCustomerAccount : NewAccountData -> Async<string>
      getLast5Invoices : int -> Async<TransDetail list>
      getLast5Credits : int -> Async<TransDetail list>
      getSalesGraphData : int -> Async<SalesGraphPoint list>
      loginUser : (string * string) -> Async<bool>
      addNewTodo : Todo -> Async<string>
      getCustomerSpecials : int -> Async<CustomerSpecial list>
      getProductMix : int -> Async<ProductMixDatapoint list>
      markCustomerForDeletion : (int * string) -> Async<unit>
      markCustomerForArchive : (int * string) -> Async<unit>
      recordCustomerVisit : CustomerVisitData -> Async<unit>
      getCustomerVisitHistory : int -> Async<CustomerVisitData list>
    }