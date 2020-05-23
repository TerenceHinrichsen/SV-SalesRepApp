module SqlScripts

[<Literal>]
let SalesRepSelect = """
SELECT SalesRepId Id
	 , SalesRepCode Code
	 , SalesRepDescription Description
FROM   SalesRep.dbo.SalesRep;
"""
[<Literal>]
let AreaSelect = """
SELECT idArea AreaId
	 , AreaCode
	 , AreaDescription
FROM   SalesRep.dbo.Area;
"""
[<Literal>]
let CustomerGroupSelect = """
SELECT GroupId GroupId
	 , GroupCode
	 , GroupDescription
FROM   SalesRep.dbo.CustomerGroup;
"""
[<Literal>]
let PriceListSelect = """
SELECT PriceListId Id
	 , PriceListName Name
	 , PriceListDescription Description
FROM   SalesRep.dbo.Pricelists;
"""

[<Literal>]
let CustomerListSelect = """
SELECT c.CustomerId
	 , c.CustomerAccountNumber
	 , c.CustomerAccountName
	 , c.CustomerAccountDescription
	 , c.AreaId
	 , c.AreaCode
	 , c.AreaDescription
	 , c.GroupId
	 , c.GroupCode
	 , c.GroupDescription
	 , c.PriceListId
	 , c.PriceListName
	 , c.PriceListDescription
	 , c.CustomerIsOnHold
	 , c.Physical
	 , c.Physical2
	 , c.Suburb
	 , c.Physical4
	 , c.GPS
	 , c.PhysicalPC
	 , c.Telephone
	 , c.Cellphone
	 , c.Fax
	 , c.MainAccLink
	 , c.udARLastVisit
	 , c.IsChildAccount
	 , c.MasterAccountNumber
	 , c.MasterAccountName
	 , c.AgeingTermCode
	 , c.AgeingTermDescription
	 , c.RepVisitFrequency
	 , c.Delivered_To DeliveryContact
	 , c.Contact_Person ContactPerson
	 , c.SalesRepId
	 , c.SalesRepCode
	 , c.SalesRepName
	 , c.EMail
	 , c.ucARDeliveryEmail DeliveryEmail
	 , c.ulARMarketSegment MarketSegment
FROM   dbo.Customer c
ORDER BY CustomerAccountNumber ;
"""

[<Literal>]
let customerDetailSelect = """
SELECT c.CustomerId
	 , c.CustomerAccountNumber
	 , c.CustomerAccountName
	 , c.CustomerAccountDescription
	 , c.AreaId
	 , c.AreaCode
	 , c.AreaDescription
	 , c.GroupId
	 , c.GroupCode
	 , c.GroupDescription
	 , c.PriceListId
	 , c.PriceListName
	 , c.PriceListDescription
	 , c.CustomerIsOnHold
	 , c.Physical
	 , c.Physical2
	 , c.Suburb
	 , c.Physical4
	 , c.GPS
	 , c.PhysicalPC
	 , c.Telephone
	 , c.Cellphone
	 , c.Fax
	 , c.MainAccLink
	 , c.udARLastVisit
	 , c.IsChildAccount
	 , c.MasterAccountNumber
	 , c.MasterAccountName
	 , c.AgeingTermCode
	 , c.AgeingTermDescription
	 , c.RepVisitFrequency
	 , c.Delivered_To DeliveryContact
	 , c.Contact_Person ContactPerson
	 , c.SalesRepId
	 , c.SalesRepCode
	 , c.SalesRepName
	 , c.EMail
	 , c.ucARDeliveryEmail DeliveryEmail
	 , c.ulARMarketSegment MarketSegment
FROM   dbo.Customer c
WHERE c.CustomerId = @CustomerId
ORDER BY CustomerAccountNumber ;

"""

[<Literal>]
let CustomerListArea = """
SELECT c.CustomerId
	 , c.CustomerAccountNumber
	 , c.CustomerAccountName
	 , c.CustomerAccountDescription
	 , c.AreaId
	 , c.AreaCode
	 , c.AreaDescription
	 , c.GroupId
	 , c.GroupCode
	 , c.GroupDescription
	 , c.PriceListId
	 , c.PriceListName
	 , c.PriceListDescription
	 , c.CustomerIsOnHold
	 , c.Physical
	 , c.Physical2
	 , c.Suburb
	 , c.Physical4
	 , c.GPS
	 , c.PhysicalPC
	 , c.Telephone
	 , c.Cellphone
	 , c.Fax
	 , c.MainAccLink
	 , c.udARLastVisit
	 , c.IsChildAccount
	 , c.MasterAccountNumber
	 , c.MasterAccountName
	 , c.AgeingTermCode
	 , c.AgeingTermDescription
	 , c.RepVisitFrequency
	 , c.Delivered_To DeliveryContact
	 , c.Contact_Person ContactPerson
	 , c.SalesRepId
	 , c.SalesRepCode
	 , c.SalesRepName
	 , c.EMail
	 , c.ucARDeliveryEmail DeliveryEmail
	 , c.ulARMarketSegment MarketSegment
FROM   dbo.Customer c
WHERE c.AreaId = @Area
ORDER BY CustomerAccountNumber ;
"""

[<Literal>]
let CustomerListRep = """
SELECT c.CustomerId
	 , c.CustomerAccountNumber
	 , c.CustomerAccountName
	 , c.CustomerAccountDescription
	 , c.AreaId
	 , c.AreaCode
	 , c.AreaDescription
	 , c.GroupId
	 , c.GroupCode
	 , c.GroupDescription
	 , c.PriceListId
	 , c.PriceListName
	 , c.PriceListDescription
	 , c.CustomerIsOnHold
	 , c.Physical
	 , c.Physical2
	 , c.Suburb
	 , c.Physical4
	 , c.GPS
	 , c.PhysicalPC
	 , c.Telephone
	 , c.Cellphone
	 , c.Fax
	 , c.MainAccLink
	 , c.udARLastVisit
	 , c.IsChildAccount
	 , c.MasterAccountNumber
	 , c.MasterAccountName
	 , c.AgeingTermCode
	 , c.AgeingTermDescription
	 , c.RepVisitFrequency
	 , c.Delivered_To DeliveryContact
	 , c.Contact_Person ContactPerson
	 , c.SalesRepId
	 , c.SalesRepCode
	 , c.SalesRepName
	 , c.EMail
	 , c.ucARDeliveryEmail DeliveryEmail
	 , c.ulARMarketSegment MarketSegment
FROM   dbo.Customer c
WHERE SalesRepId = @Rep
ORDER BY CustomerAccountNumber ;
"""

[<Literal>]
let CustomerListGroup = """
SELECT c.CustomerId
	 , c.CustomerAccountNumber
	 , c.CustomerAccountName
	 , c.CustomerAccountDescription
	 , c.AreaId
	 , c.AreaCode
	 , c.AreaDescription
	 , c.GroupId
	 , c.GroupCode
	 , c.GroupDescription
	 , c.PriceListId
	 , c.PriceListName
	 , c.PriceListDescription
	 , c.CustomerIsOnHold
	 , c.Physical
	 , c.Physical2
	 , c.Suburb
	 , c.Physical4
	 , c.GPS
	 , c.PhysicalPC
	 , c.Telephone
	 , c.Cellphone
	 , c.Fax
	 , c.MainAccLink
	 , c.udARLastVisit
	 , c.IsChildAccount
	 , c.MasterAccountNumber
	 , c.MasterAccountName
	 , c.AgeingTermCode
	 , c.AgeingTermDescription
	 , c.RepVisitFrequency
	 , c.Delivered_To DeliveryContact
	 , c.Contact_Person ContactPerson
	 , c.SalesRepId
	 , c.SalesRepCode
	 , c.SalesRepName
	 , c.EMail
	 , c.ucARDeliveryEmail DeliveryEmail
	 , c.ulARMarketSegment MarketSegment
FROM   dbo.Customer c
WHERE c.GroupId = @Group
ORDER BY CustomerAccountNumber ;
"""

[<Literal>]
let CustomerListAreaGroupRep = """
SELECT c.CustomerId
	 , c.CustomerAccountNumber
	 , c.CustomerAccountName
	 , c.CustomerAccountDescription
	 , c.AreaId
	 , c.AreaCode
	 , c.AreaDescription
	 , c.GroupId
	 , c.GroupCode
	 , c.GroupDescription
	 , c.PriceListId
	 , c.PriceListName
	 , c.PriceListDescription
	 , c.CustomerIsOnHold
	 , c.Physical
	 , c.Physical2
	 , c.Suburb
	 , c.Physical4
	 , c.GPS
	 , c.PhysicalPC
	 , c.Telephone
	 , c.Cellphone
	 , c.Fax
	 , c.MainAccLink
	 , c.udARLastVisit
	 , c.IsChildAccount
	 , c.MasterAccountNumber
	 , c.MasterAccountName
	 , c.AgeingTermCode
	 , c.AgeingTermDescription
	 , c.RepVisitFrequency
	 , c.Delivered_To DeliveryContact
	 , c.Contact_Person ContactPerson
	 , c.SalesRepId
	 , c.SalesRepCode
	 , c.SalesRepName
	 , c.EMail
	 , c.ucARDeliveryEmail DeliveryEmail
	 , c.ulARMarketSegment MarketSegment
FROM   dbo.Customer c
WHERE c.AreaId = @Area
AND c.GroupId = @Group
AND c.SalesRepId = @Rep
ORDER BY CustomerAccountNumber ;
"""

[<Literal>]
let CustomerListAreaRep = """
SELECT c.CustomerId
	 , c.CustomerAccountNumber
	 , c.CustomerAccountName
	 , c.CustomerAccountDescription
	 , c.AreaId
	 , c.AreaCode
	 , c.AreaDescription
	 , c.GroupId
	 , c.GroupCode
	 , c.GroupDescription
	 , c.PriceListId
	 , c.PriceListName
	 , c.PriceListDescription
	 , c.CustomerIsOnHold
	 , c.Physical
	 , c.Physical2
	 , c.Suburb
	 , c.Physical4
	 , c.GPS
	 , c.PhysicalPC
	 , c.Telephone
	 , c.Cellphone
	 , c.Fax
	 , c.MainAccLink
	 , c.udARLastVisit
	 , c.IsChildAccount
	 , c.MasterAccountNumber
	 , c.MasterAccountName
	 , c.AgeingTermCode
	 , c.AgeingTermDescription
	 , c.RepVisitFrequency
	 , c.Delivered_To DeliveryContact
	 , c.Contact_Person ContactPerson
	 , c.SalesRepId
	 , c.SalesRepCode
	 , c.SalesRepName
	 , c.EMail
	 , c.ucARDeliveryEmail DeliveryEmail
	 , c.ulARMarketSegment MarketSegment
FROM   dbo.Customer c
WHERE c.AreaId = @Area
AND c.SalesRepId = @Rep
ORDER BY CustomerAccountNumber ;
"""

[<Literal>]
let CustomerListAreaGroup = """
SELECT c.CustomerId
	 , c.CustomerAccountNumber
	 , c.CustomerAccountName
	 , c.CustomerAccountDescription
	 , c.AreaId
	 , c.AreaCode
	 , c.AreaDescription
	 , c.GroupId
	 , c.GroupCode
	 , c.GroupDescription
	 , c.PriceListId
	 , c.PriceListName
	 , c.PriceListDescription
	 , c.CustomerIsOnHold
	 , c.Physical
	 , c.Physical2
	 , c.Suburb
	 , c.Physical4
	 , c.GPS
	 , c.PhysicalPC
	 , c.Telephone
	 , c.Cellphone
	 , c.Fax
	 , c.MainAccLink
	 , c.udARLastVisit
	 , c.IsChildAccount
	 , c.MasterAccountNumber
	 , c.MasterAccountName
	 , c.AgeingTermCode
	 , c.AgeingTermDescription
	 , c.RepVisitFrequency
	 , c.Delivered_To DeliveryContact
	 , c.Contact_Person ContactPerson
	 , c.SalesRepId
	 , c.SalesRepCode
	 , c.SalesRepName
	 , c.EMail
	 , c.ucARDeliveryEmail DeliveryEmail
	 , c.ulARMarketSegment MarketSegment
FROM   dbo.Customer c
WHERE c.AreaId = @Area
AND c.GroupId = @Group
ORDER BY CustomerAccountNumber ;
"""

[<Literal>]
let CustomerListRepGroup = """
SELECT c.CustomerId
	 , c.CustomerAccountNumber
	 , c.CustomerAccountName
	 , c.CustomerAccountDescription
	 , c.AreaId
	 , c.AreaCode
	 , c.AreaDescription
	 , c.GroupId
	 , c.GroupCode
	 , c.GroupDescription
	 , c.PriceListId
	 , c.PriceListName
	 , c.PriceListDescription
	 , c.CustomerIsOnHold
	 , c.Physical
	 , c.Physical2
	 , c.Suburb
	 , c.Physical4
	 , c.GPS
	 , c.PhysicalPC
	 , c.Telephone
	 , c.Cellphone
	 , c.Fax
	 , c.MainAccLink
	 , c.udARLastVisit
	 , c.IsChildAccount
	 , c.MasterAccountNumber
	 , c.MasterAccountName
	 , c.AgeingTermCode
	 , c.AgeingTermDescription
	 , c.RepVisitFrequency
	 , c.Delivered_To DeliveryContact
	 , c.Contact_Person ContactPerson
	 , c.SalesRepId
	 , c.SalesRepCode
	 , c.SalesRepName
	 , c.EMail
	 , c.ucARDeliveryEmail DeliveryEmail
	 , c.ulARMarketSegment MarketSegment
FROM   dbo.Customer c
WHERE c.GroupId = @Group
AND c.SalesRepId = @Rep
ORDER BY CustomerAccountNumber ;
"""

[<Literal>]
let CustomerIdLookup = """
DECLARE @CustomerId AS INT;
SELECT TOP (1)
	   @CustomerId = c.CustomerId
FROM   dbo.Customer c
WHERE  c.CustomerAccountNumber = @CustomerCode;
SELECT @CustomerId
"""

[<Literal>]
let MarketSegments =
  """
    SELECT rud.cLookupOptions
    FROM   SDK_Practise.dbo._rtblUserDict rud
    WHERE  rud.cFieldName = 'ulARMarketSegment';
  """

[<Literal>]
let RepVisitFrequency =
  """
    SELECT rud.cLookupOptions
    FROM   SDK_Practise.dbo._rtblUserDict rud
    WHERE  rud.cFieldName = 'ulARREPVISITFREQ';
  """

let UpdateCustomerMaster =
  """
INSERT INTO dbo.ChangeLog
( CustomerId , ChangeText )
VALUES (@CustomerId, @ChangeText)

UPDATE dbo.CustomerAlter
SET   CustomerAccountName = @CustomerName
	, GroupId = @GroupId
	, RepId = @RepId
	, PriceListId = @PricelistId
	, Contact_Person = @Contact1
	, Delivered_To = @Contact2
	, Telephone = @Telephone
	, Cellphone = @Cell
	, EMail = @Email
	, Physical = @Physical1
	, Physical2 = @Physical2
	, Suburb = @Physical3
	, ucARDeliveryEmail = @DeliveryEmail
	, ulARMarketSegment = @MarketSegment
	, RepVisitFrequency = @RepVisitFreq
WHERE CustomerId = @CustomerId

  """

let CustomerViewData =
  """
  WITH LastInvoiceByCustomer
  AS (SELECT	 par.AccountLink
		   , MAX(par.TxDate) LastTransactionDate
	FROM	 SDK_Practise.dbo.PostAR par
  WHERE par.AccountLink = @customerId
	GROUP BY par.AccountLink)
SELECT Id			   = c.CustomerId
	 , [Name]		   = c.CustomerAccountName
	 , [Group]		   = c.GroupCode
	 , Area			   = c.AreaCode
	 , AgeingPeriod	   = c.AgeingTermCode
	 , Rotation		   = c.Rotation
	 , GYPercentage	   = c.GYPercentage
	 , GPPercentage	   = c.GPPercentage
	 , OtherSuppliers  = c.OtherSuppliers
	 , LastRepVisit	   = FORMAT(c.udARLastVisit, 'yyyy-MM-dd')
	 , LastInvoiceDate = FORMAT(li.LastTransactionDate,'yyyy-MM-dd')
	 , GeneralComments = c.GeneralComments
FROM   Customer c
LEFT JOIN LastInvoiceByCustomer li ON c.CustomerId = li.AccountLink
WHERE c.CustomerId = @customerId
  """

let Last5Invoices = """
SELECT TOP 5
		 ct.Reference Number
	   , FORMAT(ct.TransactionDate, 'yyyy-MM-dd') TransactionDate
	   , FORMAT(ct.Debit,'0 000.00') Total
FROM	 dbo.CustomerTransactions ct
WHERE	 ct.CustomerId = @customerId
		 AND ct.Debit > 0
		 AND ct.TransactionType <> 'ARTx'
ORDER BY ct.Id DESC;
"""

let Last5Credits = """
SELECT TOP 5
		 ct.Reference Number
	   , FORMAT(ct.TransactionDate, 'yyyy-MM-dd') TransactionDate
	   , FORMAT(ct.Credit,'0 000.00') Total
FROM	 dbo.CustomerTransactions ct
WHERE	 ct.CustomerId = @customerId
		 AND ct.Credit > 0
		 AND ct.TransactionType = 'Crn'
ORDER BY ct.Id DESC;
"""

let TwoYearSalesHistory = """
  ;WITH PeriodList
  AS (SELECT	 DISTINCT TOP (15)
			   csh.Period
	  FROM	 dbo.CustomerSalesHistory csh
	  ORDER BY csh.Period DESC)
  , SalesValues AS 
  (
  SELECT		  pl.Period
			  , SUM(ISNULL(csh.ActualQuantity, 0)) Dozens
			  , SUM(ISNULL(csh.ActualValue, 0))	 Value
  FROM		  PeriodList			   pl
	  LEFT JOIN dbo.CustomerSalesHistory csh ON csh.Period = pl.Period
  WHERE		  csh.DCLink = @customerId
  GROUP BY	  pl.Period )

  SELECT sv.Period
	   , sv.Dozens
	   , sv.Value
	   , AVG(ISNULL(sv.Value,0)) OVER (ORDER BY sv.Period ROWS BETWEEN 4 PRECEDING AND CURRENT ROW) AS QuarterTrend
  FROM SalesValues sv
"""