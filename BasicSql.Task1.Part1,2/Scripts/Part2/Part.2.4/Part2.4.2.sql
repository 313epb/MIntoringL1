/*
Выдать всех продавцов, которые имеют более 150 заказов. Использовать вложенный SELECT.
*/

SELECT e.[LastName] + ' ' + e.[FirstName] AS "Seller"
FROM [dbo].[Employees] AS e
WHERE e.[EmployeeID] IN
(
SELECT o.[EmployeeID]
FROM [dbo].[Orders] AS o
GROUP BY o.[EmployeeID]
HAVING COUNT(o.[EmployeeID]) > 150
)