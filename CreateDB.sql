CREATE DATABASE Warehouse
--DROP DATABASE Warehouse
--alter table Products alter column ExpirationDate int


-- Ñîçäàíèå òàáëèö
USE Warehouse
CREATE TABLE [Products] (
  [ProductId] int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
  [ProductName] nvarchar(25) NOT NULL,
  [Storage] nvarchar(40) NOT NULL,
  [Packaging] nvarchar(30) NOT NULL,
  [ExpirationDate] int NOT NULL,
  [TypeId] int NOT NULL
)
GO
CREATE TABLE [ProductTypes] (
  [TypeId] int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
  [TypeName] nvarchar(30) NOT NULL,
  [Description] nvarchar(100) NOT NULL,
  [Features] nvarchar(100) NOT NULL
)
GO
CREATE TABLE [Dialers] (
  [DialerId] int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
  [DialerName] nvarchar(40) NOT NULL,
  [DialerAddress] nvarchar(40) NOT NULL,
  [TelNumber] int NOT NULL
)
GO
CREATE TABLE [Orders] (
  [OrderId] int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
  [OrderDate] date NOT NULL,
  [DispatchDate] date NOT NULL,
  [delivery] nvarchar(50) NOT NULL,
  [volume] int NOT NULL,
  [cost] money NOT NULL,
  [employee] nvarchar(50) NOT NULL,
  [ProductId] int NOT NULL,
  [CustomerId] int NOT NULL
)
GO
CREATE TABLE [Customers] (
  [CustomerId] int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
  [CustomerName] nvarchar(40) NOT NULL,
  [CustomerAddress] nvarchar(50) NOT NULL,
  [TelNumber] int NOT NULL
)
GO

CREATE TABLE [Storages] (
  [StorageId] int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
  [ReceiptDate] date NOT NULL,
  [volume] int NOT NULL,
  [cost] money NOT NULL,
  [employee] nvarchar(50) NOT NULL,
  [ProductId] int NOT NULL,
  [DialerId] int NOT NULL
)
GO

--Äîáàâëåíèå âíåøíèõ êëþ÷åé
ALTER TABLE [Products] ADD FOREIGN KEY ([TypeId]) REFERENCES [ProductTypes] ([TypeId]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [Orders] ADD FOREIGN KEY ([ProductId]) REFERENCES [Products] ([ProductId]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [Orders] ADD FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([CustomerId]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [Storages] ADD FOREIGN KEY ([ProductId]) REFERENCES [Products] ([ProductId]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [Storages] ADD FOREIGN KEY ([DialerId]) REFERENCES [Dialers] ([DialerId]) ON DELETE CASCADE ON UPDATE CASCADE
GO

--Çàïîëíåíèå òàáëèöû Òèïû ïðîäóêòîâ
USE Warehouse
INSERT INTO ProductTypes (TypeName, Description, Features)
VALUES ('Çåðíîìó÷íûå', 'Îñíîâíîå ñûðüå äëÿ ýòèõ ïðîäóêòîâ - çåðíî', 'Âûñîêîå ñîäåðæàíèå óãëåâîäîâ'),
       ('Ïëîäîîâîùíûå', 'Èñòî÷íèê êëåò÷àòêè', 'Áîëüøîå ñîäåðæàíèå âèòàìèíîâ è ïèùåâûõ âîëîêîí'),
	   ('Âêóñîâûå', 'Îñíîâíûå êîìïîíåíòû - âåùåñòâà, îêàçûâàþùèå âîçäåéñòâèå íà íåðâíóþ ñèñòåìó', 'Ñîäåðæàò âåùåñòâà, äåéñòâóþùèå íà íåðâíóþ ñèñòåìó'),
	   ('Êîíäèòåðñêèå', 'Â îñíîâíîì ñîñòîÿò èç ñàõàðà', 'Õîðîøàÿ óñâîÿåìîñòü, íî íèçêàÿ áèîëîãè÷åñêàÿ öåííîñòü'),
	   ('Ìîëî÷íûå', 'Èñòî÷íèê ôîñôîðà è êàëüöèÿ', 'Ñîäåðæàò ëåãêî óñâîÿåìûå âåùåñòâà'),
	   ('Ìÿñíûå', 'Âûñîêîå ñîäåðæàíèå áåëêîâ', 'Èñòî÷íèê ïîëíîöåííûõ áåëêîâ'),
	   ('Ðûáíûå', 'Âûñîêîå ñîäåðæàíèå áåëêîâ', 'Èñòî÷íèê ïîëíîöåííûõ áåëêîâ')


--Îòêëþ÷åíèå âûâîäà ñîîáùåíèÿ î êîëè÷åñòâå îáðàáîòàííûõ çàïèñåé
SET NOCOUNT ON

--Îáúÿâëåíèå ïåðåìåííûõ
USE Warehouse
DECLARE @Symbol CHAR(52)= 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz',
		@Position int,

		@i int,
		@NameLimit int,

		@DialerName nvarchar(50),
		@DialerAddress nvarchar(100), 

		@CustomerName nvarchar(50),
		@CustomerAddress nvarchar(100),

		@MaxStorageName int,
		@MaxPackingName int,
		@StgLimit int,
		@PackingLimit int,
		@ProductName nvarchar(25),
		@Storage nvarchar(40),
		@Packing nvarchar(30),
		@date int,
		@TypeId int,

		@employee nvarchar(100),
		@cost money,
		@volume int,
		@RecDate date,
		@ProductId int,
		@DialerId int,

		@delivery nvarchar(100),
		@CustomerId int,
		@orderDate date,
		@dispatchDate date,

		@RowCount INT,
		@NumberProducts int,
		@NumberCustomers int,
		@NumberDialers int,
		@NumberOrders int,
		@NumberStorages int,
		@MinNumberSymbols int,
		@MaxNumberSymbols int,
		@NumberTypes int

SET @NumberProducts = 500
SET @NumberCustomers = 500
SET @NumberDialers = 500
SET @NumberOrders = 20000
SET @NumberStorages = 20000
SET @NumberTypes = 7

BEGIN TRAN

SELECT @i=0 FROM dbo.Dialers WITH (TABLOCKX) WHERE 1=0

-- Çàïîëíåíèå ïîñòàâùèêîâ
	SET @RowCount=1
	SET @MinNumberSymbols=5
	SET @MaxNumberSymbols=40	
	WHILE @RowCount<=@NumberDialers
	BEGIN		

		SET @NameLimit=@MinNumberSymbols+RAND()*(@MaxNumberSymbols-@MinNumberSymbols) -- èìÿ îò 5 äî 40 ñèìâîëîâ
		SET @i=1
        SET @DialerName=''
		SET @DialerAddress=''
		WHILE @i<=@NameLimit
		BEGIN
			SET @Position=RAND()*52
			SET @DialerName = @DialerName + SUBSTRING(@Symbol, @Position, 1)
			SET @DialerAddress = @DialerAddress + SUBSTRING(@Symbol, @Position, 1) 
			SET @i=@i+1
		END
		INSERT INTO dbo.Dialers (DialerName, DialerAddress, TelNumber) SELECT @DialerName, @DialerAddress, FLOOR(RAND()*(999999-111111)+111111)
		

		SET @RowCount +=1
	END

SELECT @i=0 FROM dbo.Customers WITH (TABLOCKX) WHERE 1=0

-- Çàïîëíåíèå çàêàç÷èêîâ
	SET @RowCount=1
	SET @MinNumberSymbols=5
	SET @MaxNumberSymbols=40	
	WHILE @RowCount<=@NumberCustomers
	BEGIN		

		SET @NameLimit=@MinNumberSymbols+RAND()*(@MaxNumberSymbols-@MinNumberSymbols) -- èìÿ îò 5 äî 40 ñèìâîëîâ
		SET @i=1
        SET @CustomerName=''
		SET @CustomerAddress=''
		WHILE @i<=@NameLimit
		BEGIN
			SET @Position=RAND()*52
			SET @CustomerName = @CustomerName + SUBSTRING(@Symbol, @Position, 1)
			SET @CustomerAddress = @CustomerAddress + SUBSTRING(@Symbol, @Position, 1)
			SET @i=@i+1
		END
		INSERT INTO dbo.Customers(CustomerName, CustomerAddress, TelNumber) SELECT @CustomerName, @CustomerAddress, FLOOR(RAND()*(999999-111111)+111111)
		SET @RowCount +=1
	END


SELECT @i=0 FROM dbo.Products WITH (TABLOCKX) WHERE 1=0

-- Çàïîëíåíèå ïðîäóêòîâ
	SET @RowCount=1
	SET @MinNumberSymbols=5
	SET @MaxNumberSymbols=25
	SET @MaxStorageName = 40
	SET @MaxPackingName = 30
	WHILE @RowCount<=@NumberProducts
	BEGIN		

		SET @NameLimit=@MinNumberSymbols+RAND()*(@MaxNumberSymbols-@MinNumberSymbols) -- èìÿ îò 5 äî 40 ñèìâîëîâ
		SET @StgLimit =@MinNumberSymbols+RAND()*(@MaxStorageName-@MinNumberSymbols)
		SET @PackingLimit =@MinNumberSymbols+RAND()*(@MaxPackingName-@MinNumberSymbols)
		SET @i=1
        SET @ProductName=''
		SET @Storage=''
		SET @Packing=''
		WHILE @i<=@NameLimit
		BEGIN
			SET @Position=RAND()*52
			SET @ProductName = @ProductName + SUBSTRING(@Symbol, @Position, 1)
			SET @i=@i+1
		END
		WHILE @i<=@StgLimit
		BEGIN
			SET @Position=RAND()*52
			SET @Storage = @Storage + SUBSTRING(@Symbol, @Position, 1)
			SET @i=@i+1
		END
		WHILE @i<=@PackingLimit
		BEGIN
			SET @Position=RAND()*52
			SET @Packing = @Packing + SUBSTRING(@Symbol, @Position, 1)
			SET @i=@i+1
		END
		SET @date=FLOOR(RAND()*(70-5)+5)
		SET @TypeId = CAST((1 + RAND() * (@NumberTypes)) as int)
		INSERT INTO dbo.Products(ProductName, Storage, Packaging, ExpirationDate, TypeId) SELECT @ProductName, @Storage, @Packing, @date, @TypeId
		SET @RowCount +=1
	END


SELECT @i=0 FROM dbo.Storages WITH (TABLOCKX) WHERE 1=0

-- Çàïîëíåíèå òàáëèöû ïîñòàâîê
	SET @RowCount=1
	SET @MinNumberSymbols=5
	SET @MaxNumberSymbols=50
	WHILE @RowCount<=@NumberStorages
	BEGIN		

		SET @NameLimit=@MinNumberSymbols+RAND()*(@MaxNumberSymbols-@MinNumberSymbols) -- èìÿ îò 5 äî 100 ñèìâîëîâ
		SET @i=1
        SET @employee=''
		WHILE @i<=@NameLimit
		BEGIN
			SET @Position=RAND()*52
			SET @employee = @employee + SUBSTRING(@Symbol, @Position, 1)
			SET @i=@i+1
		END
		SET @cost = CAST(RAND()*(500-10)+10 AS DECIMAL)
		SET @volume = FLOOR(RAND()*(100-10)+10)
		SET @RecDate=dateadd(day,-RAND()*365,GETDATE())
		SET @ProductId = CAST((1 + RAND() * (@NumberProducts)) as int)
		SET @DialerId = CAST((1 + RAND() * (@NumberDialers)) as int)
		INSERT INTO dbo.Storages(ReceiptDate, Volume, Cost, employee, ProductId, DialerId) SELECT @RecDate, @volume, @cost, @employee, @ProductId, @DialerId
		SET @RowCount +=1
	END


SELECT @i=0 FROM dbo.Orders WITH (TABLOCKX) WHERE 1=0
	--Çàïîëíåíèå òàáëèöû çàêàçîâ
	SET @RowCount=1
	SET @MinNumberSymbols=5
	SET @MaxNumberSymbols=50
	WHILE @RowCount<=@NumberStorages
	BEGIN		

		SET @NameLimit=@MinNumberSymbols+RAND()*(@MaxNumberSymbols-@MinNumberSymbols) -- èìÿ îò 5 äî 100 ñèìâîëîâ
		SET @i=1
        SET @employee=''
		SET @delivery=''
		WHILE @i<=@NameLimit
		BEGIN
			SET @Position=RAND()*52
			SET @employee = @employee + SUBSTRING(@Symbol, @Position, 1)
			SET @delivery = @delivery + SUBSTRING(@Symbol, @Position, 1)
			SET @i=@i+1
		END
		SET @OrderDate=dateadd(day,-RAND()*365,GETDATE())
		SET @DispatchDate=dateadd(day,-RAND()*365,GETDATE())
		SET @cost = CAST(RAND()*(500-10)+10 AS DECIMAL)
		SET @volume = FLOOR(RAND()*(100-10)+10)
		SET @ProductId = CAST((1 + RAND() * (@NumberProducts)) as int)
		SET @CustomerId = CAST((1 + RAND() * (@NumberCustomers)) as int)
		INSERT INTO dbo.Orders(OrderDate, DispatchDate, delivery, volume, cost, employee, ProductId, CustomerId) SELECT @OrderDate, @DispatchDate, @delivery, @volume, @cost, @employee, @ProductId, @CustomerId
		SET @RowCount +=1
	END

COMMIT TRAN


