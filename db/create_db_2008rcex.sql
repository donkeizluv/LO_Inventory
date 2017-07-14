USE [InventoryDb]
GO
/****** Object:  UserDefinedFunction [dbo].[GetAcceptPermission]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Dant
-- Create date: <Create Date, ,>
-- Description:	0 = godlike; 1 = moveble & order; 2 = move; 3 = no go
-- =============================================
create FUNCTION [dbo].[GetAcceptPermission]
(
	@userId int,
	@cabinetTypeId int
)
RETURNS bit
AS
BEGIN
	IF EXISTS(SELECT *
	FROM TransactionPermission
	WHERE TransactionPermission.UserId = @userId AND 
	TransactionPermission.CabinetTypeId = @cabinetTypeId AND
	PermissionTypeName = 'Accept')
		BEGIN
			RETURN 1;
		END
	RETURN 0;
END

GO
/****** Object:  UserDefinedFunction [dbo].[GetAllReceiveOfItem]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[GetAllReceiveOfItem](@itemId int, @cabinetId int)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @result int;
	SELECT @result = SUM(Transactions.Quanlity)
	FROM Items
	INNER JOIN Transactions ON Items.ItemId = Transactions.ItemId
	WHERE Items.ItemId = @Itemid and ReceiverCabinetId = @cabinetId
	GROUP BY Items.ItemId
	-- Return the result of the function
	IF @result is NULL
		SET @result = 0

	RETURN @result

END

GO
/****** Object:  UserDefinedFunction [dbo].[GetAllSendOutOfItem]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[GetAllSendOutOfItem](@itemId int, @cabinetId int)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @result int;
	SELECT @result = SUM(Transactions.Quanlity)
	FROM Items
	INNER JOIN Transactions ON Items.ItemId = Transactions.ItemId
	WHERE Items.ItemId = @Itemid and ProviderCabinetId = @cabinetId
	GROUP BY Items.ItemId
	-- Return the result of the function
	IF @result is NULL
		SET @result = 0

	RETURN @result

END

GO
/****** Object:  UserDefinedFunction [dbo].[GetCabinetName]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[GetCabinetName](@cabinetId int) 
RETURNS nvarchar(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @result nvarchar(50)

	-- Add the T-SQL statements to compute the return value here
	SELECT @result = Cabinets.CabinetName FROM Cabinets WHERE Cabinets.CabinetId = @cabinetId

	-- Return the result of the function
	RETURN @result

END

GO
/****** Object:  UserDefinedFunction [dbo].[GetCurrentItemsCabinetHas]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[GetCurrentItemsCabinetHas](@cabinetId int)
RETURNS @itemsTable TABLE
(
	ItemId int NOT NULL,
	ItemCode varchar(50) NOT NULL,
	ItemName nvarchar(50) NOT NULL,
	OnHandQuanlity int NULL
) 
AS
BEGIN
	INSERT INTO @itemsTable(ItemId, ItemCode, ItemName, OnHandQuanlity)
	SELECT ItemId, ItemCode, ItemName, dbo.GetOnHandQuanlityOfItem(ItemId, @cabinetId) as 'OnHandQuanlity'
	FROM Items
	WHERE dbo.GetOnHandQuanlityOfItem(ItemId, @cabinetId) > 0
	RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetCurrentItemsInUse]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[GetCurrentItemsInUse]()
RETURNS @inUseTable TABLE
(
	ItemCode varchar(50) NOT NULL,
	ItemName nvarchar(50) NOT NULL,
	Quanlity int NULL
) 
AS
BEGIN
	INSERT INTO @inUseTable(ItemCode, ItemName, Quanlity)
	SELECT DISTINCT ItemCode, ItemName, sum(Orders.Quanlity) over (partition by(Orders.Itemid)) - ISNULL(SellAmount, 0) as 'Quanlity'
	FROM Items LEFT JOIN Orders on items.Itemid = Orders.ItemId 
	LEFT JOIN
	(SELECT ItemId, SUM(Quanlity) as 'SellAmount' 
	FROM Transactions
	WHERE ReceiverCabinetId = 1
	GROUP BY ItemId) 
	sell on sell.ItemId = items.ItemId
	RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetFilterdItemsList]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[GetFilterdItemsList]
(	
	@filter nvarchar(150)
)
RETURNS @filterTable TABLE
(
	ItemId int NOT NULL,
	ItemCode varchar(50) NOT NULL,
	ItemName nvarchar(50) NOT NULL,
	InUse int NULL
)
AS
BEGIN
	INSERT INTO @filterTable(ItemId,ItemCode , ItemName, InUse)
	SELECT itemid, ItemCode, ItemName, InUse FROM ItemList
	WHERE ItemCode LIKE CONCAT('%', @filter, '%') OR
	ItemName LIKE CONCAT('%', @filter, '%')
	RETURN
END

GO
/****** Object:  UserDefinedFunction [dbo].[GetGodlikePermission]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Dant
-- Create date: <Create Date, ,>
-- Description:	0 = godlike; 1 = moveble & order; 2 = move; 3 = no go
-- =============================================
create FUNCTION [dbo].[GetGodlikePermission]
(
	@userId int
)
RETURNS bit
AS
BEGIN
	IF EXISTS(SELECT *
	FROM TransactionPermission
	WHERE TransactionPermission.UserId = @userId AND
	PermissionTypeName = 'Godlike')
		BEGIN
			RETURN 1;
		END
	RETURN 0;
END

GO
/****** Object:  UserDefinedFunction [dbo].[GetItemTransactionsOfCabinet]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[GetItemTransactionsOfCabinet](@cabinetId int, @itemId int)
RETURNS @itemsTable TABLE
(
	ItemId int NOT NULL,
	ItemCode varchar(50) NOT NULL,
	ItemName nvarchar(50) NOT NULL,
	Quanlity int NOT NULL,
	ActionDate datetime NOT NULL,
	Username varchar(50) NOT NULL,
	ActionType varchar(3) NOT NULL
)

AS
BEGIN
INSERT INTO @itemsTable
SELECT ItemId, ItemCode, ItemName, Quanlity, ActionDate, Username, ActionType FROM
		((SELECT Items.ItemId, Items.ItemCode, Items.ItemName, Quanlity, Orders.OrderDate as 'ActionDate', UserId,'ActionType' = 'BUY'
		FROM Items INNER JOIN Orders ON Items.ItemId = Orders.ItemId 
		WHERE Items.ItemId = @itemId AND CabinetId = @cabinetId)
		UNION
		(SELECT Items.ItemId, Items.ItemCode, Items.ItemName, Quanlity, TransactionDate as 'ActionDate', UserId,'ActionType' = 'IN'
		FROM Items INNER JOIN Transactions ON Items.ItemId = Transactions.ItemId
		WHERE Items.ItemId = @itemId AND Transactions.ReceiverCabinetId = @cabinetId)
		UNION
		(SELECT Items.ItemId, Items.ItemCode, Items.ItemName, Quanlity, TransactionDate as 'ActionDate', UserId,'ActionType' = 'OUT'
		FROM Items INNER JOIN Transactions ON Items.ItemId = Transactions.ItemId
		WHERE Items.ItemId = @itemId AND Transactions.ProviderCabinetId = @cabinetId)) tempTable
		LEFT JOIN Users ON tempTable.UserId = Users.UserId
	RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetLoginLevel]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[GetLoginLevel]
(	
	@userName varchar(50)
)
RETURNS @loginTable TABLE
(
	login_level int NOT NULL
)
AS
BEGIN
	DECLARE @loginLevel int
	
	SELECT @loginLevel = RoleId FROM Users WHERE Username = @userName
	IF @loginLevel IS NULL
		SET @loginLevel = -1
	INSERT INTO @loginTable(login_level) VALUES(@loginLevel)
	RETURN
END

GO
/****** Object:  UserDefinedFunction [dbo].[GetOnHandQuanlityOfItem]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[GetOnHandQuanlityOfItem](@itemId int, @cabinetId int)
RETURNS int
AS
BEGIN
	DECLARE @result int;
	DECLARE @orderQuanlity int;

	SELECT @orderQuanlity =  SUM(Orders.Quanlity)
	FROM Items INNER JOIN Orders ON Items.ItemId = Orders.ItemId 
	WHERE Items.ItemId = @itemId and Orders.CabinetId = @cabinetId
	GROUP BY Items.ItemId

	IF @orderQuanlity IS Null
		SET @orderQuanlity = 0

	SELECT @result = dbo.GetAllReceiveOfItem(@itemId, @cabinetId) - dbo.GetAllSendOutOfItem(@itemId, @cabinetId) + @orderQuanlity

	RETURN @result

END

GO
/****** Object:  UserDefinedFunction [dbo].[GetOrderPermission]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Dant
-- Create date: <Create Date, ,>
-- Description:	0 = godlike; 1 = moveble & order; 2 = move; 3 = no go
-- =============================================
CREATE FUNCTION [dbo].[GetOrderPermission]
(
	@userId int,
	@cabinetTypeId int
)
RETURNS bit
AS
BEGIN
	IF EXISTS(
			SELECT *
			FROM TransactionPermission
			WHERE (TransactionPermission.UserId = @userId AND 
			TransactionPermission.CabinetTypeId = @cabinetTypeId AND
			PermissionTypeName = 'Order') OR (TransactionPermission.UserId = @userId AND
			PermissionTypeName = 'Godlike'))
		BEGIN
			RETURN 1;
		END
	RETURN 0;
END

GO
/****** Object:  UserDefinedFunction [dbo].[GetOrdersOfItem]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[GetOrdersOfItem](@itemId int)
RETURNS @itemsTable TABLE
(
	ItemId int NOT NULL,
	ItemCode varchar(50) NOT NULL,
	OrderDate datetime NULL,
	[Provider] nvarchar(50) NULL,
	CabinetName nvarchar(50) NULL,
	Quanlity int NULL,
	Price int NULL,
	Username varchar(50) NULL
) 
AS
BEGIN
	INSERT INTO @itemsTable(ItemId, ItemCode, OrderDate, [Provider], CabinetName, Quanlity, Price, Username)
	SELECT Items.ItemId, ItemCode, OrderDate, Providers.Name as 'Provider', CabinetName, Quanlity ,Price, Username
	FROM Items LEFT JOIN Orders ON Items.ItemId = Orders.ItemId 
	JOIN Users ON Orders.UserId = Users.UserId
	JOIN Cabinets ON Orders.CabinetId = Cabinets.CabinetId
	JOIN Providers ON orders.ProviderId = Providers.ProviderId
	WHERE Items.ItemId = @itemId
	RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetOrdersOfUser]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[GetOrdersOfUser]
(	
	@userName varchar(50)
)
RETURNS @returnTable TABLE 
(
	Username varchar(50) NOT NULL,
	RoleName varchar(50) NOT NULL,
	ItemCode nvarchar(50) NOT NULL,
	Quanlity int NOT NULL,
	OrderDate datetime NOT NULL
)
AS
BEGIN
	INSERT INTO @returnTable(Username, RoleName, ItemCode, Quanlity, OrderDate)
	SELECT Username, RoleName, ItemCode, Quanlity, OrderDate
	FROM Users INNER JOIN Roles ON Users.RoleId = Roles.RoleId
	INNER JOIN Orders ON Orders.UserId = users.UserId
	INNER JOIN Items ON Orders.ItemId = Items.ItemId
	WHERE Users.Username = @userName
	RETURN
END


GO
/****** Object:  UserDefinedFunction [dbo].[GetSalesOfItem]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[GetSalesOfItem](@itemId int)
RETURNS @itemsTable TABLE
(
	ItemId int NOT NULL,
	ItemCode varchar(50) NOT NULL,
	[From] nvarchar(50) NOT NULL,
	TransactionDate datetime NOT NULL,
	Quanlity int NULL,
	Price int NULL,
	Note nvarchar(150) NULL,
	Username varchar(50) NULL
) 
AS
BEGIN
	INSERT INTO @itemsTable(ItemId, ItemCode, [From], TransactionDate, Quanlity, Price, Note, Username)
	SELECT Items.ItemId, ItemCode, Cabinets.CabinetName as 'From', TransactionDate, Quanlity ,Price, Transactions.Note, Username
	FROM Items JOIN Transactions ON Items.ItemId = Transactions.ItemId
	JOIN Users ON Users.UserId = Transactions.UserId
	JOIN Cabinets ON Transactions.ProviderCabinetId = Cabinets.CabinetId
	WHERE Items.ItemId = @itemId AND Transactions.ReceiverCabinetId = 1
	RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetTotalBuyInOfItem]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[GetTotalBuyInOfItem](@Itemid int)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @result int;
	SELECT @result = SUM(Orders.Quanlity)
	FROM Items
	INNER JOIN Orders ON Items.ItemId = Orders.ItemId
	WHERE Items.ItemId = @Itemid
	GROUP BY Items.ItemId
	-- Return the result of the function
	RETURN @result

END

GO
/****** Object:  UserDefinedFunction [dbo].[GetTranferPermission]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Dant
-- Create date: <Create Date, ,>
-- Description:	0 = godlike; 1 = moveble & order; 2 = move; 3 = no go
-- =============================================
CREATE FUNCTION [dbo].[GetTranferPermission]
(
	@userId int,
	@cabinetTypeId int
)
RETURNS bit
AS
BEGIN
	IF EXISTS(SELECT *
	FROM TransactionPermission
	WHERE TransactionPermission.UserId = @userId AND 
	TransactionPermission.CabinetTypeId = @cabinetTypeId AND
	PermissionTypeName = 'Transfer')
		BEGIN
			RETURN 1;
		END
	RETURN 0;
END

GO
/****** Object:  UserDefinedFunction [dbo].[LoginUser]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[LoginUser](@userName varchar(50))
	returns int
AS
BEGIN
	--return role
	DECLARE @result int
	SELECT @result = roleid FROM Users WHERE Username = @userName
	IF(@result IS NULL) SET @result = -1
	RETURN @result
END

GO
/****** Object:  UserDefinedFunction [dbo].[StrTrim]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[StrTrim](@Str VARCHAR(MAX)) RETURNS VARCHAR(MAX) BEGIN
    DECLARE @NewStr VARCHAR(MAX) = NULL

    IF (@Str IS NOT NULL) BEGIN
        SET @NewStr = ''

        DECLARE @WhiteChars VARCHAR(4) =
              CHAR(13) + CHAR(10) -- ENTER
            + CHAR(9) -- TAB
            + ' ' -- SPACE

        IF (@Str LIKE ('%[' + @WhiteChars + ']%')) BEGIN

            ;WITH Split(Chr, Pos) AS (
                SELECT
                      SUBSTRING(@Str, 1, 1) AS Chr
                    , 1 AS Pos
                UNION ALL
                SELECT
                      SUBSTRING(@Str, Pos, 1) AS Chr
                    , Pos + 1 AS Pos
                FROM Split
                WHERE Pos <= LEN(@Str)
            )
            SELECT @NewStr = @NewStr + Chr
            FROM Split
            WHERE
                Pos >= (
                    SELECT MIN(Pos)
                    FROM Split
                    WHERE CHARINDEX(Chr, @WhiteChars) = 0
                )
                AND Pos <= (
                    SELECT MAX(Pos)
                    FROM Split
                    WHERE CHARINDEX(Chr, @WhiteChars) = 0
                )
        END
    END

    RETURN @NewStr
END
GO
/****** Object:  Table [dbo].[Cabinets]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cabinets](
	[CabinetId] [int] IDENTITY(1,1) NOT NULL,
	[CabinetName] [nvarchar](50) NOT NULL,
	[CabinetTypeId] [int] NOT NULL,
	[Address] [nvarchar](120) NULL,
	[Phone] [varchar](30) NULL,
 CONSTRAINT [PK_Cabinets] PRIMARY KEY CLUSTERED 
(
	[CabinetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Cabinets] UNIQUE NONCLUSTERED 
(
	[CabinetName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CabinetType]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CabinetType](
	[CabinetTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](120) NULL,
 CONSTRAINT [PK_CabinetType] PRIMARY KEY CLUSTERED 
(
	[CabinetTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_CabinetType] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Items]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Items](
	[ItemId] [int] IDENTITY(1,1) NOT NULL,
	[ItemCode] [varchar](50) NOT NULL,
	[ItemName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED 
(
	[ItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Items] UNIQUE NONCLUSTERED 
(
	[ItemCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Orders]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[ItemId] [int] NOT NULL,
	[CabinetId] [int] NOT NULL,
	[Quanlity] [int] NOT NULL,
	[ProviderId] [int] NOT NULL,
	[Price] [int] NULL,
	[InputDate] [date] NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[Note] [nvarchar](150) NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PermissionType]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermissionType](
	[PermissionTypeName] [varchar](15) NOT NULL,
	[Description] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_PermissionType] PRIMARY KEY CLUSTERED 
(
	[PermissionTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Providers]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Providers](
	[ProviderId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](120) NOT NULL,
	[Phone] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Provider] PRIMARY KEY CLUSTERED 
(
	[ProviderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Providers] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](50) NOT NULL,
	[Note] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Roles] UNIQUE NONCLUSTERED 
(
	[RoleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TransactionPermission]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionPermission](
	[PermissionId] [int] IDENTITY(1,1) NOT NULL,
	[PermissionTypeName] [varchar](15) NOT NULL,
	[UserId] [int] NOT NULL,
	[CabinetTypeId] [int] NOT NULL,
	[Note] [nvarchar](150) NULL,
 CONSTRAINT [PK_TransactionPermission] PRIMARY KEY CLUSTERED 
(
	[PermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[TransactionsId] [int] IDENTITY(1,1) NOT NULL,
	[ItemId] [int] NOT NULL,
	[Quanlity] [int] NOT NULL,
	[ProviderCabinetId] [int] NOT NULL,
	[ReceiverCabinetId] [int] NOT NULL,
	[InputDate] [date] NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[Price] [int] NULL,
	[Note] [nvarchar](150) NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[TransactionsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[RoleId] [int] NOT NULL,
	[Note] [nvarchar](150) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Users] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[CabinetList]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[CabinetList]
AS
SELECT        dbo.Cabinets.CabinetId, dbo.Cabinets.CabinetName, dbo.CabinetType.Name AS Type, dbo.Cabinets.Address, dbo.Cabinets.Phone
FROM            dbo.Cabinets INNER JOIN
                         dbo.CabinetType ON dbo.Cabinets.CabinetTypeId = dbo.CabinetType.CabinetTypeId

GO
/****** Object:  View [dbo].[CabinetTypeList]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[CabinetTypeList]
AS
SELECT        Description, Name, CabinetTypeId
FROM            dbo.CabinetType

GO
/****** Object:  View [dbo].[ItemList]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ItemList]
AS
SELECT        dbo.Items.ItemId, dbo.Items.ItemCode, dbo.Items.ItemName, inUseTable.Quanlity AS 'InUse'
FROM            dbo.Items INNER JOIN
                         dbo.GetCurrentItemsInUse() AS inUseTable ON dbo.Items.ItemCode = inUseTable.ItemCode

GO
/****** Object:  View [dbo].[OrderList]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[OrderList]
AS
SELECT        dbo.Items.ItemCode, dbo.Orders.Quanlity, dbo.Cabinets.CabinetName, dbo.Providers.Name AS Provider, dbo.Orders.Price, dbo.Orders.InputDate, dbo.Orders.OrderDate, dbo.Orders.Note, dbo.Users.Username, 
                         dbo.Orders.OrderId, dbo.Orders.ItemId, dbo.Orders.CabinetId, dbo.Orders.UserId, dbo.Orders.ProviderId
FROM            dbo.Orders INNER JOIN
                         dbo.Items ON dbo.Orders.ItemId = dbo.Items.ItemId INNER JOIN
                         dbo.Cabinets ON dbo.Orders.CabinetId = dbo.Cabinets.CabinetId INNER JOIN
                         dbo.Providers ON dbo.Orders.ProviderId = dbo.Providers.ProviderId INNER JOIN
                         dbo.Users ON dbo.Orders.UserId = dbo.Users.UserId

GO
/****** Object:  View [dbo].[ProviderList]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ProviderList]
AS
SELECT        Name, Address, Phone, ProviderId
FROM            dbo.Providers

GO
/****** Object:  View [dbo].[TransactionList]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TransactionList]
AS
SELECT        dbo.Transactions.TransactionsId, dbo.GetCabinetName(dbo.Transactions.ProviderCabinetId) AS [From], dbo.GetCabinetName(dbo.Transactions.ReceiverCabinetId) AS [To], dbo.Items.ItemCode, dbo.Transactions.Quanlity, 
                         dbo.Transactions.InputDate, dbo.Transactions.TransactionDate, dbo.Transactions.Price, dbo.Transactions.Note, dbo.Users.Username, dbo.Transactions.UserId, dbo.Transactions.ItemId
FROM            dbo.Transactions INNER JOIN
                         dbo.Items ON dbo.Transactions.ItemId = dbo.Items.ItemId INNER JOIN
                         dbo.Users ON dbo.Transactions.UserId = dbo.Users.UserId

GO
/****** Object:  View [dbo].[TransPermissionLists]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TransPermissionLists]
AS
SELECT        dbo.Users.Username, dbo.TransactionPermission.PermissionTypeName AS PermissionType, dbo.CabinetType.Name AS [Cabinet Type], dbo.TransactionPermission.Note, dbo.TransactionPermission.PermissionId, 
                         dbo.TransactionPermission.UserId, dbo.TransactionPermission.CabinetTypeId
FROM            dbo.TransactionPermission INNER JOIN
                         dbo.Users ON dbo.TransactionPermission.UserId = dbo.Users.UserId INNER JOIN
                         dbo.CabinetType ON dbo.TransactionPermission.CabinetTypeId = dbo.CabinetType.CabinetTypeId

GO
/****** Object:  View [dbo].[UserList]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[UserList]
AS
SELECT        dbo.Users.Username, dbo.Roles.RoleName, dbo.Users.Note, dbo.Users.UserId
FROM            dbo.Users INNER JOIN
                         dbo.Roles ON dbo.Users.RoleId = dbo.Roles.RoleId

GO
ALTER TABLE [dbo].[Transactions] ADD  CONSTRAINT [DF_Transactions_Price]  DEFAULT ((0)) FOR [Price]
GO
ALTER TABLE [dbo].[Cabinets]  WITH CHECK ADD  CONSTRAINT [FK_Cabinets_CabinetType] FOREIGN KEY([CabinetTypeId])
REFERENCES [dbo].[CabinetType] ([CabinetTypeId])
GO
ALTER TABLE [dbo].[Cabinets] CHECK CONSTRAINT [FK_Cabinets_CabinetType]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Cabinets] FOREIGN KEY([CabinetId])
REFERENCES [dbo].[Cabinets] ([CabinetId])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Cabinets]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Items] FOREIGN KEY([ItemId])
REFERENCES [dbo].[Items] ([ItemId])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Items]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Providers] FOREIGN KEY([ProviderId])
REFERENCES [dbo].[Providers] ([ProviderId])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Providers]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Users]
GO
ALTER TABLE [dbo].[TransactionPermission]  WITH CHECK ADD  CONSTRAINT [FK_TransactionPermission_CabinetType] FOREIGN KEY([CabinetTypeId])
REFERENCES [dbo].[CabinetType] ([CabinetTypeId])
GO
ALTER TABLE [dbo].[TransactionPermission] CHECK CONSTRAINT [FK_TransactionPermission_CabinetType]
GO
ALTER TABLE [dbo].[TransactionPermission]  WITH CHECK ADD  CONSTRAINT [FK_TransactionPermission_PermissionType] FOREIGN KEY([PermissionTypeName])
REFERENCES [dbo].[PermissionType] ([PermissionTypeName])
GO
ALTER TABLE [dbo].[TransactionPermission] CHECK CONSTRAINT [FK_TransactionPermission_PermissionType]
GO
ALTER TABLE [dbo].[TransactionPermission]  WITH CHECK ADD  CONSTRAINT [FK_TransactionPermission_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[TransactionPermission] CHECK CONSTRAINT [FK_TransactionPermission_Users]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Users]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_TransactionsItem_Items] FOREIGN KEY([ItemId])
REFERENCES [dbo].[Items] ([ItemId])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_TransactionsItem_Items]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_TransactionsProvider_Cabinet] FOREIGN KEY([ProviderCabinetId])
REFERENCES [dbo].[Cabinets] ([CabinetId])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_TransactionsProvider_Cabinet]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_TransactionsReceiverr_Cabinet] FOREIGN KEY([ReceiverCabinetId])
REFERENCES [dbo].[Cabinets] ([CabinetId])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_TransactionsReceiverr_Cabinet]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [CK_Orders] CHECK  (([Quanlity]>(0) AND [Price]>=(0)))
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [CK_Orders]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [CK_Transactions] CHECK  (([ProviderCabinetId]<>[ReceiverCabinetId] AND [Price]>=(0) AND [Quanlity]>(0)))
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [CK_Transactions]
GO
/****** Object:  Trigger [dbo].[InsertOrderCheck]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [dbo].[InsertOrderCheck]
   ON  [dbo].[Orders]
   INSTEAD OF INSERT
AS 
BEGIN
	DECLARE @itemid int;		
	DECLARE @cabinetId int;
	DECLARE @quanlity int;
	DECLARE @providerId int;
	DECLARE @price int;
	DECLARE @inputDate datetime;
	--DECLARE @orderDay datetime;
	DECLARE @note nvarchar(150);
	DECLARE @userid int;

	--set
	SELECT @itemid = d.ItemId from inserted d
	SELECT @cabinetId = d.CabinetId from inserted d
	SELECT @quanlity = d.Quanlity from inserted d
	SELECT @providerId = d.ProviderId from inserted d
	SELECT @price = d.Price from inserted d
	SELECT @inputDate = d.InputDate from inserted d
	SELECT @note = d.Note from inserted d
	SELECT @userid = d.UserId from inserted d


	DECLARE @orderPermission bit;
	DECLARE @cabinetType int;

	SELECT @cabinetType = CabinetTypeId FROM Cabinets WHERE CabinetId = @cabinetId
	SELECT @orderPermission = dbo.GetOrderPermission(@userid, @cabinetType)

	--only 0, 1 is allow to order
	IF(@orderPermission = 0)
			BEGIN
				RAISERROR('Do not have permission to order to this cabinet', 16, 1);
				RETURN;
			END

	--check quanlity
	IF(@quanlity < 1)
		BEGIN
			RAISERROR('Quanlity must > 0', 16 , 1);
			RETURN;
		END
	IF(@price < 0)
		BEGIN
			RAISERROR('Price must >= 0', 16 , 1);
			RETURN;
		END
	
	--insert record
	INSERT INTO Orders(ItemId, CabinetId, Quanlity, ProviderId, Price, InputDate, OrderDate,
	Note, UserId)
	VALUES(@itemid, @cabinetId, @quanlity, @providerId, @price, @inputDate, CURRENT_TIMESTAMP, @note, @userid)
	--PRINT 'Record Inserted -- Instead Of Insert Trigger.'
	--to deal with affted row bla bla of EF
	SELECT Orders.OrderId FROM Orders WHERE @@ROWCOUNT > 0 and Orders.OrderId = scope_identity();
END

GO
ALTER TABLE [dbo].[Orders] ENABLE TRIGGER [InsertOrderCheck]
GO
/****** Object:  Trigger [dbo].[TransPerTrigger]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[TransPerTrigger]
   ON  [dbo].[TransactionPermission]
   INSTEAD OF INSERT
AS 
	DECLARE @perType varchar(15);	
	DECLARE @userId int;
	DECLARE @cTypeId int;
	DECLARE @note nvarchar(150);

	SELECT @perType = d.PermissionTypeName FROM inserted d;
	SELECT @userId= d.UserId FROM inserted d;
	SELECT @cTypeId= d.CabinetTypeId FROM inserted d;
	SELECT @note = d.Note FROM inserted d;

	DECLARE @userName varchar(50);
	DECLARE @cabinetTypeName varchar(50);

	SELECT @userName = Username FROM Users WHERE UserId = @userId
	SELECT @cabinetTypeName = CabinetType.Name FROM CabinetType WHERE CabinetTypeId = @cTypeId


	--check type
	IF NOT EXISTS (SELECT * FROM PermissionType WHERE PermissionTypeName = @perType)
		BEGIN
			RAISERROR('PermissionType is not valid.', 16, 1);
			RETURN;
		END
	--check existing perR
	IF EXISTS (SELECT PermissionId FROM TransactionPermission 
				WHERE UserId = @userId 
				AND PermissionTypeName = @perType 
				AND CabinetTypeId = @cTypeId)
		BEGIN
			RAISERROR('Permission [%s] for [%s] of user: [%s] is already existed.', 16, 1, @perType, @cabinetTypeName, @userName);
			RETURN;
		END
	INSERT INTO TransactionPermission(PermissionTypeName, UserId, CabinetTypeId, Note)
	VALUES(@perType, @userId, @cTypeId, @note)
	SELECT PermissionId FROM TransactionPermission WHERE @@ROWCOUNT > 0 and PermissionId = scope_identity();

GO
ALTER TABLE [dbo].[TransactionPermission] ENABLE TRIGGER [TransPerTrigger]
GO
/****** Object:  Trigger [dbo].[InsertTransactionCheck]    Script Date: 10/07/2017 13:42:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [dbo].[InsertTransactionCheck] on [dbo].[Transactions]
INSTEAD OF INSERT
AS
	DECLARE @itemid int;		
	DECLARE @fromid int;
	DECLARE @quanlity int;
	DECLARE @toid int;
	DECLARE @inputdate date;
	DECLARE @trandate datetime;
	DECLARE @price int;
	DECLARE @note nvarchar(150);
	DECLARE @userid int;

	DECLARE @godlike bit;
	DECLARE @transferPermission bit;
	DECLARE @acceptPermission bit;
	DECLARE @fromCabinetType int;
	DECLARE @toCabinetType int;

	DECLARE @onHand int;

	SELECT @quanlity = d.Quanlity FROM inserted d;
	SELECT @itemid= d.ItemId FROM inserted d;
	SELECT @fromid= d.ProviderCabinetId FROM inserted d;
	SELECT @toid = d.ReceiverCabinetId FROM inserted d;
	SELECT @inputdate= d.InputDate FROM inserted d;
	--SELECT @trandate= d.TransactionDate FROM inserted d;
	SELECT @price = d.Price FROM inserted d;
	SELECT @note= d.Note FROM inserted d;
	SELECT @userid = d.UserId FROM inserted d;

	BEGIN
		--check valid quanlity, price
		IF(@fromid = @toid)
			BEGIN
				RAISERROR('Cannot transfer to itself.', 16, 1);
				RETURN;
			END
		IF(@price < 0)
			BEGIN
				RAISERROR('Price must >= 0', 16, 1);
				RETURN;
			END
		IF( @quanlity < 1)
			BEGIN
				RAISERROR('Quanlity must > 1', 16, 1);
				RETURN;
			END
		--check FROM permission
		PRINT 'Begin checking transaction permission'
		SELECT @godlike = dbo.GetGodlikePermission(@userid)
		IF(@godlike = 1) --godlike allows all
			BEGIN
				goto Godlike
			END
		--check normal permissions
		SELECT @fromCabinetType = CabinetTypeId FROM Cabinets WHERE CabinetId = @fromid
		SELECT @toCabinetType = CabinetTypeId FROM Cabinets WHERE CabinetId = @toid
		SELECT @transferPermission = dbo.GetTranferPermission(@userid, @fromCabinetType)
		SELECT @acceptPermission = dbo.GetAcceptPermission(@userid, @toCabinetType)
		IF(@transferPermission = 0)
			BEGIN
				RAISERROR('Do not have permission to move from this cabinet', 16, 1);
				RETURN;
			END
		IF(@acceptPermission = 0)
			BEGIN
				RAISERROR('Do not have permission to move to this cabinet', 16, 1);
				RETURN;
			END

		GodLike:
		--PRINT 'Begin checking amount - transaction trigger'
		SET @onHand = dbo.GetOnHandQuanlityOfItem(@itemid, @fromid)
		IF(@onHand < @quanlity)
			BEGIN
				RAISERROR(N'Not enough items to transfer! On hand: %d < %d', 16, 1, @onHand, @quanlity);
				RETURN;
				--ROLLBACK; no changes been make so...
			END
		--ok to insert
		INSERT INTO Transactions(ItemId, Quanlity, ProviderCabinetId, ReceiverCabinetId, InputDate, TransactionDate, Price,
				Note, UserId)
		VALUES(@itemid, @quanlity, @fromid, @toid, @inputdate, CURRENT_TIMESTAMP, @price, @note, @userid)
		--PRINT 'Record Inserted -- Instead Of Insert Trigger.'
		SELECT TransactionsId FROM Transactions WHERE @@ROWCOUNT > 0 and TransactionsId = scope_identity();
	END
GO
ALTER TABLE [dbo].[Transactions] ENABLE TRIGGER [InsertTransactionCheck]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'DB based restriction to obey this table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TransactionPermission'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'App layer obeys this table and its roles' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Cabinets"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CabinetType"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 119
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CabinetList'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CabinetList'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "CabinetType"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 119
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CabinetTypeList'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CabinetTypeList'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Items"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 119
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "inUseTable"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 119
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 2220
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ItemList'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ItemList'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Orders"
            Begin Extent = 
               Top = 9
               Left = 14
               Bottom = 250
               Right = 252
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Items"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 119
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Cabinets"
            Begin Extent = 
               Top = 6
               Left = 454
               Bottom = 136
               Right = 624
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Providers"
            Begin Extent = 
               Top = 6
               Left = 662
               Bottom = 136
               Right = 832
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Users"
            Begin Extent = 
               Top = 138
               Left = 314
               Bottom = 268
               Right = 484
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Tabl' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'OrderList'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'e = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'OrderList'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'OrderList'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Providers"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ProviderList'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ProviderList'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[25] 2[16] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Transactions"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 242
               Right = 501
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Items"
            Begin Extent = 
               Top = 0
               Left = 654
               Bottom = 113
               Right = 824
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Users"
            Begin Extent = 
               Top = 141
               Left = 597
               Bottom = 271
               Right = 767
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 12
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 4875
         Alias = 4170
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TransactionList'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TransactionList'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "TransactionPermission"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 243
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Users"
            Begin Extent = 
               Top = 6
               Left = 420
               Bottom = 136
               Right = 590
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CabinetType"
            Begin Extent = 
               Top = 6
               Left = 628
               Bottom = 119
               Right = 798
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1950
         Width = 1500
         Width = 5040
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TransPermissionLists'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TransPermissionLists'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Users"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Roles"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 119
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UserList'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UserList'
GO
