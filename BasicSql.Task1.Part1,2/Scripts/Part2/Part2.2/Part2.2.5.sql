/*
Найти всех покупателей, которые живут в одном городе.
*/

SELECT COUNT([CustomerID]) AS "Employees", [City]
FROM [dbo].[Customers]
GROUP BY [City]