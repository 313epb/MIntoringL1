/*
Выбрать всех заказчиков из таблицы Customers, у которых название страны начинается на буквы из диапазона b и g, 
не используя оператор BETWEEN.
*/

SELECT [CustomerId], [Country]
FROM [dbo].[Customers]
WHERE SUBSTRING([Country], 1, 1) >= 'b' 
AND  SUBSTRING([Country], 1, 1) <= 'g'
ORDER BY [Country]