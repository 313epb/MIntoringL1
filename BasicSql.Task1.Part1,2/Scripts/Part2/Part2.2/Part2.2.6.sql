/*
По таблице Employees найти для каждого продавца его руководителя.
*/

SELECT e.[LastName] + ' ' + e.[FirstName] AS "Employee", m.[LastName] + ' ' + m.[FirstName] AS "Manager" 
FROM [dbo].[Employees] AS e
INNER JOIN [dbo].[Employees] AS m
ON e.[ReportsTo] = m.[EmployeeID]