/*
По таблице Orders найти количество заказов, сделанных каждым продавцом и для каждого покупателя. 
Необходимо определить это только для заказов, сделанных в 1998 году.
*/

SELECT "Seller" = 
(
SELECT e.[LastName] + ' ' + e.[FirstName]
FROM [dbo].[Employees] AS e
Where e.[EmployeeID] = o.[EmployeeID]
), 
o.[CustomerID], COUNT(o.[OrderID]) AS "Total Orders", YEAR(o.[OrderDate]) AS "Order Year"
FROM [dbo].[Orders] AS o
GROUP BY o.[EmployeeID], o.[CustomerID], YEAR(o.[OrderDate])
HAVING YEAR(o.[OrderDate]) = '1998'