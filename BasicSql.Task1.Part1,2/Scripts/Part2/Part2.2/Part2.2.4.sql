/*
Найти покупателей и продавцов, которые живут в одном городе. 
Если в городе живут только один или несколько продавцов, или только один или несколько покупателей, 
то информация о таких покупателя и продавцах не должна попадать в результирующий набор. 
Не использовать конструкцию JOIN.
*/

SELECT e.[FirstName] + ' ' + e.[LastName] AS "Name", 'Seller' AS "Person Type", e.[City] 
FROM [dbo].[Employees] AS e
WHERE EXISTS 
(
SELECT c.[City]
FROM [dbo].[Customers] AS c
WHERE c.[City] = e.[City]
)
UNION ALL
SELECT c.[ContactName] AS "Name", 'Customer' AS "Person Type", c.[City]
FROM [dbo].[Customers] AS c
WHERE EXISTS
(
SELECT e.[City]
FROM [dbo].[Employees] AS e
WHERE e.[City] = c.[City]
)