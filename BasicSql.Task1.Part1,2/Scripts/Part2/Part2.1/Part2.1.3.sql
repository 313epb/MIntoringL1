/*
По таблице Orders найти количество различных покупателей (CustomerID), сделавших заказы. 
Использовать функцию COUNT и не использовать предложения WHERE и GROUP.
*/

SELECT COUNT(DISTINCT [CustomerID]) AS "Distinct Customers"
FROM [dbo].[Orders]