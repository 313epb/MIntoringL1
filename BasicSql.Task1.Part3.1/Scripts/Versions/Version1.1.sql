/*
Update script to version 1.1.
*/

IF OBJECT_ID(N'[dbo].[EmployeeCreditCards]', N'U') IS NULL
BEGIN
	CREATE TABLE "EmployeeCreditCards" (
	"EmployeeCreditCardID" "int" IDENTITY (1, 1) NOT NULL,
	"EmployeeID" "int" NOT NULL,
	"CreditCardNumber" "int" NOT NULL,
	"ExpirationDate" "datetime" NOT NULL,
	"CardHolder" nvarchar(255) NULL,
	CONSTRAINT "PK_EmployeeCreditCards" PRIMARY KEY CLUSTERED 
	(
		"EmployeeCreditCardID"
	),
	CONSTRAINT "FK_EmployeeCreditCards_Employees" FOREIGN KEY 
	(
		"EmployeeID"
	) REFERENCES "dbo"."Employees" (
	"EmployeeID"
	)
	)	
END