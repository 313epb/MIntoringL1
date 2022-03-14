/*
Выбрать в таблице Orders заказы, которые были доставлены после 6 мая 1998 года (колонка ShippedDate) включительно
и которые доставлены с ShipVia >= 2. Запрос должен возвращать только колонки OrderID, ShippedDate и ShipVia.
*/

SELECT [OrderID], [ShippedDate], [ShipVia]
FROM [dbo].[Orders]
WHERE [ShippedDate] >= CONVERT(datetime, '1998-05-06')
AND [ShipVia] >= 2