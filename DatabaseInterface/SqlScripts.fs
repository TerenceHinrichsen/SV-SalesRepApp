module SqlScripts

[<Literal>]
let LoginUser = """DECLARE	@return_value int
EXEC	@return_value = [dbo].[LoginUser]
		@Username = @Username,
		@Password = @Password
SELECT	'LoginSuccess' = @return_value"""

[<Literal>]
let SalesRepSelect = """
SELECT SalesRepId Id
	 , SalesRepCode Code
	 , SalesRepDescription Description
FROM   dbo.SalesRep;
"""
[<Literal>]
let AreaSelect = """
SELECT idArea AreaId
	 , AreaCode
	 , AreaDescription
FROM   dbo.Area;
"""
[<Literal>]
let CustomerGroupSelect = """
SELECT GroupId GroupId
	 , GroupCode
	 , GroupDescription
FROM   dbo.CustomerGroup;
"""
[<Literal>]
let PriceListSelect = """
SELECT PriceListId Id
	 , PriceListName Name
	 , PriceListDescription Description
FROM   dbo.Pricelists;
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
let customerSearchString = """
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
WHERE CustomerAccountName LIKE @Search
OR CustomerAccountNumber LIKE @Search 
OR Physical LIKE @Search
OR Suburb LIKE @Search
OR Delivered_To LIKE @Search
OR Contact_Person LIKE @Search
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
    SELECT TOP (1) [MarketSegment] cLookupOptions
        FROM [dbo].[MarketSegments]
  """

[<Literal>]
let RepVisitFrequency =
  """
    SELECT TOP (1) [RepVisitFrequency] cLookupOptions
      FROM [dbo].[RepVisitFrequencies]
  """

let UpdateCustomerMaster =
  """
INSERT INTO dbo.ChangeLog
( CustomerId , ChangeText )
VALUES (@CustomerId, @ChangeText)

INSERT INTO dbo.CustomerAlter
(CustomerId, CustomerAccountName, GroupId, RepId, PriceListId, Contact_Person, Delivered_To, Telephone, Cellphone, Email, Physical, Physical2, Suburb, ucARDeliveryEmail, ulARMarketSegment, RepVisitFrequency)
SELECT   
	 @CustomerId
	, CustomerAccountName = @CustomerName
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

  """

let CreateCustomer =
  """
INSERT INTO dbo.ChangeLog
( CustomerId , ChangeText )
VALUES ('0', @ChangeText)

INSERT INTO dbo.NewAccountApplication
(
	AccountName   , AccountDescription  , AreaId  , GroupId  , PriceListId  , Physical  , Physical2  , Suburb
  , GPS  , PostalCode  , Telephone  , Cellphone  , Fax  , RepVisitFreq  , Contact_Person  , Delivered_To  , DeliveryEmail
  , MarketSegment  , Email  , RepId  , Processed  , ReceivedOnDateTime)
VALUES
(	@AccountName		  , @AccountDescription	  , @AreaId  , @GroupId  , @PriceListId
  , @Physical1  , @Physical2  , @Suburb  , @GPS  , @PostCode  , @Telephone
  , @Cellphone  , @Fax  , @RepVisitFreq  , @Contact_Person  , @DeliverTo  , @DeliveryEmail  , @MarketSegment
  , @Email  , @RepId  , 0 , SYSDATETIME()
)
  """

let CustomerViewData =
  """
 WITH LastInvoiceByCustomer
  AS (SELECT	 par.CustomerId
		   , MAX(par.TransactionDate) LastTransactionDate
	FROM	 dbo.CustomerTransactions par
  WHERE par.CustomerId = @customerId
	GROUP BY par.CustomerId)
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
LEFT JOIN LastInvoiceByCustomer li ON c.CustomerId = li.CustomerId
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
			  , SUM(ISNULL(csh.Boxes, 0)) Boxes
			  , SUM(ISNULL(csh.ActualValue, 0))	 Value
  FROM		  PeriodList			   pl
	  LEFT JOIN dbo.CustomerSalesHistory csh ON csh.Period = pl.Period
  WHERE		  csh.DCLink = @customerId
  GROUP BY	  pl.Period )

  SELECT sv.Period
	   , sv.Boxes
	   , sv.Value
	   , AVG(ISNULL(sv.Value,0)) OVER (ORDER BY sv.Period ROWS BETWEEN 4 PRECEDING AND CURRENT ROW) AS QuarterTrend
  FROM SalesValues sv
"""