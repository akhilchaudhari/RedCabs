IF NOT EXISTS (Select * from sys.tables where type='U' and OBJECT_ID=OBJECT_ID('dbo.CarType'))
BEGIN
create table CarType
(
ID INT PRIMARY KEY IDENTITY (1,1),
Type varchar(15) NOT NULL
)
END

IF NOT EXISTS (Select * from sys.tables where type='U' and OBJECT_ID=OBJECT_ID('dbo.Car'))
BEGIN
create table Car
(
ID INT PRIMARY KEY IDENTITY(1,1),
Model varchar(30) NOT NULL,
Color varchar(15) NOT NULL,
CarType_Id INT FOREIGN KEY REFERENCES Car(ID) NOT NULL
)
END




IF NOT EXISTS (Select * from sys.tables where type='U' and OBJECT_ID=OBJECT_ID('dbo.Driver'))
BEGIN
create table Driver
(
ID INT PRIMARY KEY IDENTITY(1,1),
FirstName varchar(20) NOT NULL,
LastName varchar(20),
Email varchar(50),
ContactNo varchar(15) NOT NULL,
Address varchar(300) NOT NULL,
Car_Id INT NOT NULL,
IsActive bit NOT NULL,
IsAvailable int NOT NULL,
LastLocation varchar(50)
)
END

IF NOT EXISTS (Select * from sys.tables where type='U' and OBJECT_ID=OBJECT_ID('dbo.DriverLocationDetails'))
BEGIN
create table DriverLocationDetails
(
ID INT Primary KEY IDENTITY (1,1),
Driver_Id INT FOREIGN KEY REFERENCES Driver(Id) NOT NULL,
UpdatedOn DateTime NOT NULL DEFAULT(GETUTCDATE()),
Location varchar(50) NOT NULL
)
END

IF NOT EXISTS (Select * from sys.objects where [type]='TR' and [name] = 'trgAfterInsert')
BEGIN

CREATE TRIGGER trgAfterInsert ON dbo.DriverLocationDetails
FOR INSERT
AS

DECLARE @DriverId INT = (Select dld.Driver_Id from inserted dld);
DECLARE @LastLocation varchar(50) = (Select Location from DriverLocationDetails where UpdatedOn = (Select MAX(UpdatedOn) from DriverLocationDetails where Driver_Id = @DriverId));
Update Driver
set
LastLocation = @LastLocation
END

IF NOT EXISTS (Select * from sys.tables where type='U' and OBJECT_ID=OBJECT_ID('dbo.User'))
BEGIN
create table [User]
(
ID INT PRIMARY KEY IDENTITY(1,1),
FirstName varchar(20) NOT NULL,
LastName varchar(20),
Email varchar(50),
ContactNo varchar(15) NOT NULL,
IsActive bit NOT NULL,
LastLocation varchar(50)
)
END

IF NOT EXISTS (Select * from sys.tables where type='U' and OBJECT_ID=OBJECT_ID('dbo.UserLocationDetails'))
BEGIN
create table UserLocationDetails
(
ID INT Primary KEY IDENTITY (1,1),
[User_Id] INT FOREIGN KEY REFERENCES [User](Id) NOT NULL,
UpdatedOn DateTime NOT NULL DEFAULT(GETUTCDATE()),
Location varchar(50) NOT NULL
)
END

IF NOT EXISTS (Select * from sys.objects where [type]='TR' and [name] = 'trgAfterUserInsert')
BEGIN

CREATE TRIGGER dbo.[trgAfterUserInsert] ON dbo.[UserLocationDetails]
FOR INSERT
AS

DECLARE @UserId INT = (Select dld.User_Id from inserted dld);
DECLARE @LastLocation varchar(50) = (Select Location from UserLocationDetails where UpdatedOn = (Select MAX(UpdatedOn) from UserLocationDetails where [User_Id] = @UserId));
Update [User]
set
LastLocation = @LastLocation
END




IF NOT EXISTS (Select * from sys.tables where type='U' and OBJECT_ID=OBJECT_ID('dbo.UserAddress'))
BEGIN
CREATE TABLE UserAddress
(
ID INT PRIMARY KEY IDENTITY(1,1),
[User_Id] INT FOREIGN KEY REFERENCES [User](Id) NOT NULL,
[Address] VARCHAR(300) NOT NULL,
[AddressType] VARCHAR(50) NULL
)
END

IF NOT EXISTS (Select * from sys.tables where type='U' and OBJECT_ID=OBJECT_ID('dbo.RideStatus'))
BEGIN
CREATE TABLE RideStatus
(
ID INT PRIMARY KEY IDENTITY(1,1),
[Type] VARCHAR(50) NOT NULL
)
END

IF NOT EXISTS (Select * from sys.tables where type='U' and OBJECT_ID=OBJECT_ID('dbo.RideDetails'))
BEGIN
CREATE TABLE RideDetails
(
ID INT PRIMARY KEY IDENTITY(1,1),
[User_Id] INT FOREIGN KEY REFERENCES [User](Id) NOT NULL,
[Driver_Id] INT FOREIGN KEY REFERENCES [Driver](Id) NOT NULL,
UpdatedOn DateTime NOT NULL DEFAULT(GETUTCDATE()),
RideStatus_Id INT FOREIGN KEY REFERENCES RideStatus(Id) NOT NULL
)
END

IF NOT EXISTS (Select * from sys.tables where type='U' and OBJECT_ID=OBJECT_ID('dbo.CouponValueType'))
BEGIN

CREATE TABLE CouponValueType
(
ID INT PRIMARY KEY IDENTITY(1,1),
Value VARCHAR(15) NOT NULL
)
END

IF NOT EXISTS (Select * from sys.tables where type='U' and OBJECT_ID=OBJECT_ID('dbo.Coupon'))
BEGIN

CREATE TABLE Coupon
(
ID INT PRIMARY KEY IDENTITY(1,1),
CouponCode VARCHAR(30) NOT NULL,
ExpiryDate DateTime,
CouponValueType_Id INT FOREIGN KEY REFERENCES CouponValueType(Id) NOT NULL

)

END

IF NOT EXISTS (Select * from sys.tables where type='U' and OBJECT_ID=OBJECT_ID('dbo.UserCoupons'))
BEGIN

CREATE TABLE UserCoupons
(
ID INT PRIMARY KEY IDENTITY(1,1),
[User_Id]  INT FOREIGN KEY REFERENCES [User](Id) NOT NULL,
[Coupon_Id]  INT FOREIGN KEY REFERENCES [Coupon](Id) NOT NULL
)
END

