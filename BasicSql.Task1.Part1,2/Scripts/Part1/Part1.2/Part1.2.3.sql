/*
Выбрать из таблицы Customers все страны, в которых проживают заказчики. 
Страна должна быть упомянута только один раз и список отсортирован по убыванию. 
Не использовать предложение GROUP BY. 
Возвращать только одну колонку в результатах запроса.
*/

SELECT DISTINCT [Country]
FROM [dbo].[Customers]
ORDER BY [Country] DESC