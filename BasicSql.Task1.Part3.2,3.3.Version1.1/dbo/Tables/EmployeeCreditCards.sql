CREATE TABLE [dbo].[EmployeeCreditCards]
(
	[EmployeeCreditCardID] INT NOT NULL, 
    [EmployeeID] INT NOT NULL, 
    [CreditCardNumber] INT NOT NULL, 
    [ExpirationDate] DATETIME NOT NULL, 
    [CardHolder] NVARCHAR(255) NULL, 
    CONSTRAINT [PK_EmployeeCreditCards] PRIMARY KEY ([EmployeeCreditCardID]), 
    CONSTRAINT [FK_EmployeeCreditCards_Employees] FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[Employees]([EmployeeID]) 
)
