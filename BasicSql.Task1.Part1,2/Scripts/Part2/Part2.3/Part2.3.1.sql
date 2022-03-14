/*
Определить продавцов, которые обслуживают регион 'Western' (таблица Region).
*/

SELECT DISTINCT e.[LastName] + ' ' + e.[FirstName] AS "Employee", r.[RegionDescription]
FROM [dbo].[Employees] AS e
INNER JOIN [dbo].[EmployeeTerritories] AS et
ON e.[EmployeeID] = et.[EmployeeID]
INNER JOIN [dbo].[Territories] AS t
ON et.[TerritoryID] = t.[TerritoryID]
INNER JOIN [dbo].[Region] AS r
ON t.[RegionID] = r.[RegionID]
WHERE r.[RegionDescription] = 'Western'