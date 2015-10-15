IF NOT EXISTS (Select * from sys.tables where type='U' and OBJECT_ID=OBJECT_ID('dbo.Country'))
BEGIN
create table Country
(
Id int PRIMARY KEY IDENTITY (1,1),
Name NVARCHAR(100) NOT NULL
)
END

IF NOT EXISTS (Select * from sys.tables where type='U' and OBJECT_ID=OBJECT_ID('dbo.Person'))
BEGIN
create table Person
(
Id int PRIMARY KEY IDENTITY (1,1),
Name NVARCHAR(50) NOT NULL,
Phone NVARCHAR(20) NOT NULL,
Address NVARCHAR(100) NOT NULL,
State NVARCHAR(50) NOT NULL,
CountryId int FOREIGN KEY REFERENCES Country(Id)
)
END