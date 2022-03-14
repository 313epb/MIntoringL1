/*
Выдать всех заказчиков (таблица Customers), которые не имеют ни одного заказа (подзапрос по таблице Orders).
Использовать оператор EXISTS.
*/

SELECT c.[ContactName]
FROM [dbo].[Customers] AS c
WHERE NOT EXISTS
(
SELECT o.[OrderID] 
FROM [dbo].[Orders] AS o
WHERE o.[CustomerID] = c.[CustomerID]
)