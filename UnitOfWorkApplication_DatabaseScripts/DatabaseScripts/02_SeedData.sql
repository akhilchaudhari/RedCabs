IF NOT EXISTS (Select * from CarType)
BEGIN

INSERT INTO CarType values ('Mini')
INSERT INTO CarType values ('Prime')
INSERT INTO CarType values ('Sedan')
END
GO

IF NOT EXISTS (Select * from Car)
BEGIN

INSERT INTO Car Values ('Tata Indica','White',1)
INSERT INTO Car Values ('Toyota Innova','White',2)
INSERT INTO Car Values ('Hyundai Xcent','White',3)

END
GO

IF NOT EXISTS (Select * from Driver)
BEGIN

INSERT INTO Driver Values('Rahul','Lal','rahullal27@gmail.com','9406075683','Sector 4 Bhilai',1,1,1,NULL)

END
GO