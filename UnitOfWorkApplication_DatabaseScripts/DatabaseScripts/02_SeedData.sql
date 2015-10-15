IF NOT EXISTS (Select * from Country)
BEGIN
Insert Into Country values ('India')
Insert Into Country values ('Canada')
Insert Into Country values ('Belgium')
Insert Into Country values ('United States')
Insert Into Country values ('United Kingdom')
Insert Into Country values ('Australia')
Insert Into Country values ('Argentina')
END

IF NOT EXISTS (Select * from Person)
BEGIN

Insert Into Person values ('Person1','1234','India','CG',1)
Insert Into Person values ('Person2','2234','Belgium','CG',3)
Insert Into Person values ('Person3','3234','Australia','CG',6)
Insert Into Person values ('Person4','4234','Argentina','CG',7)

END

