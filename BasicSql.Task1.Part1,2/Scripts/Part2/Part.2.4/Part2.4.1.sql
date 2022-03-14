/*
Выдать всех поставщиков (колонка CompanyName в таблице Suppliers), 
у которых нет хотя бы одного продукта на складе (UnitsInStock в таблице Products равно 0). 
Использовать вложенный SELECT для этого запроса с использованием оператора IN.
*/

SELECT s.[CompanyName]
FROM [dbo].[Suppliers] AS s
WHERE s.[SupplierID] IN
(
SELECT p.[SupplierID]
FROM [dbo].[Products] AS p
WHERE p.[UnitsInStock] = 0
)