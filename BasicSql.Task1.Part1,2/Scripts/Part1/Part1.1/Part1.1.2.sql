/*
Написать запрос, который выводит только недоставленные заказы из таблицы Orders. 
В результатах запроса возвращать для колонки ShippedDate вместо значений NULL строку ‘Not Shipped’ (использовать системную функцию CASЕ). 
Запрос должен возвращать только колонки OrderID и ShippedDate.
*/

SELECT [OrderID], [ShippedDate] = 
CASE
WHEN [ShippedDate] IS NULL THEN 'Not Shipped'
END
FROM [dbo].[Orders]
WHERE [ShippedDate] IS NULL