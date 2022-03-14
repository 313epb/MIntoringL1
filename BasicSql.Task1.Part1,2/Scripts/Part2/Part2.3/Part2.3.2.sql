/*
Выдать в результатах запроса имена всех заказчиков из таблицы Customers и суммарное количество их заказов из таблицы Orders. 
Принять во внимание, что у некоторых заказчиков нет заказов, но они также должны быть выведены в результатах запроса. 
Упорядочить результаты запроса по возрастанию количества заказов.
*/

SELECT c.[ContactName] AS "Customer", COUNT(o.[OrderID]) AS "Order Count"
FROM [dbo].[Customers] AS c
LEFT OUTER JOIN [dbo].[Orders] AS o
ON c.[CustomerID] = o.[CustomerID]
GROUP BY c.[CustomerID], c.[ContactName]
ORDER BY COUNT(o.[OrderID])