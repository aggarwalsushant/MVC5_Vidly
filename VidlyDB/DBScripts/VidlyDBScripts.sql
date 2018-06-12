IF OBJECT_ID('SUSHANT..Movie') IS NOT NULL DROP TABLE Movie
CREATE TABLE Movie(
	MovieId INT PRIMARY KEY
	,MovieName VARCHAR(100)
	,CategoryId INT
	,CustomerId INT
)

IF OBJECT_ID('SUSHANT..MovieCategory') IS NOT NULL DROP TABLE MovieCategory
CREATE TABLE MovieCategory(
	CategoryId INT PRIMARY KEY
	,CategoryName VARCHAR(50)
)

IF OBJECT_ID('SUSHANT..Customer') IS NOT NULL DROP TABLE Customer
CREATE TABLE Customer(
	CustomerId INT PRIMARY KEY
	,Name VARCHAR(100)
	,Address VARCHAR(100)
)

IF OBJECT_ID('SUSHANT..MembershipType') IS NOT NULL DROP TABLE MembershipType
CREATE TABLE MembershipType(
	Id TINYINT PRIMARY KEY
	,SignUpFee SMALLINT NOT NULL
	,DurationInMonths TINYINT NOT NULL
	,DiscountRate TINYINT NOT NULL
)

ALTER TABLE MembershipType ADD Name VARCHAR(30);
GO
UPDATE MembershipType SET Name='Pay as You Go' WHERE ID=1
UPDATE MembershipType SET Name='Monthly' WHERE ID=2
UPDATE MembershipType SET Name='Quarterly' WHERE ID=3
UPDATE MembershipType SET Name='Annually' WHERE ID=4

ALTER TABLE Customer ADD MembershipTypeId TINYINT;
CREATE INDEX Index_Customer_MemTypeId ON Customer (MembershipTypeId)

ALTER TABLE Customer ADD CONSTRAINT fk_MemType_Customer_MemTypeId FOREIGN KEY (MembershipTypeId) REFERENCES MembershipType (Id) ON DELETE CASCADE;


ALTER TABLE Movie
ADD CONSTRAINT FK_CategoryMovie
FOREIGN KEY (CategoryId) REFERENCES MovieCategory(CategoryId);


ALTER TABLE Movie
ADD CONSTRAINT FK_CustomerMovie
FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId);

INSERT INTO MembershipType VALUES (1,0,0,0)
INSERT INTO MembershipType VALUES (2,30,1,10)
INSERT INTO MembershipType VALUES (3,90,3,15)
INSERT INTO MembershipType VALUES (4,300,12,20)


INSERT INTO MovieCategory VALUES(1, 'Comedy')
INSERT INTO MovieCategory VALUES(2, 'Action')
INSERT INTO MovieCategory VALUES(3, 'Spiritual')
INSERT INTO MovieCategory VALUES(4, 'Drama')
INSERT INTO MovieCategory VALUES(5, 'Animation')

INSERT INTO Customer VALUES(1,'Sushant','Delhi',4)
INSERT INTO Customer VALUES(2,'Vijay','Delhi',2)
INSERT INTO Customer VALUES(3,'Ravi','Indore',3)
INSERT INTO Customer VALUES(4,'Harshit','Meerut',1)
INSERT INTO Customer VALUES(5,'Karunjit','Delhi',2)

INSERT INTO Movie VALUES(1,'Hangover',1,NULL)
INSERT INTO Movie VALUES(2,'Fast And Furious',2,1)
INSERT INTO Movie VALUES(3,'Mahabharat',3,NULL)
INSERT INTO Movie VALUES(4,'The Wall',4,2)
INSERT INTO Movie VALUES(5,'Zootopia',5,3)

SELECT * FROM Movie
SELECT * FROM MovieCategory
SELECT * FROM Customer
SELECT * FROM MembershipType

DROP PROC SP_GetMovieById
GO
CREATE PROC SP_GetMovieById
	@Id INT
AS
	SELECT * FROM Movie WHERE MovieId=@Id
GO

EXEC SP_GetMovieById @Id=2

DROP PROC SP_GetAllMovies
GO
CREATE PROC SP_GetAllMovies
AS
	SELECT * FROM Movie
GO

 EXEC SP_GetAllMovies