/*
Выбрать в таблице Orders заказы, которые были доставлены после 6 мая 1998 года (ShippedDate) не включая эту дату 
или которые еще не доставлены. 
В запросе должны возвращаться только колонки OrderID (переименовать в Order Number) и ShippedDate (переименовать в Shipped Date). 
В результатах запроса возвращать для колонки ShippedDate вместо значений NULL строку ‘Not Shipped’, 
для остальных значений возвращать дату в формате по умолчанию.
*/

SELECT [OrderID] as "Order Number", 
CASE
WHEN [ShippedDate] IS NULL THEN 'Not Shipped'
ELSE FORMAT([ShippedDate], 'yyyy-mm-dd hh:mm:ss.mmmm')
END AS "Shipped Date"
FROM [dbo].[Orders]
WHERE [ShippedDate] > CONVERT(datetime, '1998-05-06')
OR [ShippedDate] IS NULL