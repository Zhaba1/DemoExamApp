-- Запрос для модуля 3:
-- Вычисление полной стоимости заказа покупателя с учётом:
-- 1) количества продукции в заказе;
-- 2) стоимости всех материалов, использованных для производства данной продукции
--    (с учётом нормы расхода из спецификации).

-- Вариант 1: Детальный расчёт по каждой строке заказа
SELECT 
    o.OrderNumber AS 'Номер заказа',
    o.OrderDate AS 'Дата заказа',
    b.Name AS 'Покупатель',
    p.Name AS 'Продукция',
    oi.Quantity AS 'Количество',
    oi.Price AS 'Цена продажи',
    oi.Sum AS 'Сумма продажи',
    COALESCE(SUM(m.Price * pm.Quantity * oi.Quantity), 0) AS 'Себестоимость материалов'
FROM Orders o
JOIN Companies b ON o.BuyerId = b.Id
JOIN OrderItems oi ON oi.OrderId = o.Id
JOIN Products p ON oi.ProductId = p.Id
LEFT JOIN ProductMaterials pm ON pm.ProductId = p.Id
LEFT JOIN Materials m ON pm.MaterialId = m.Id
GROUP BY o.OrderNumber, o.OrderDate, b.Name, p.Name, oi.Quantity, oi.Price, oi.Sum
ORDER BY o.OrderNumber, p.Name;

-- Вариант 2: Сводный расчёт по заказу (общая сумма продажи и общая себестоимость)
SELECT 
    o.OrderNumber AS 'Номер заказа',
    o.OrderDate AS 'Дата заказа',
    b.Name AS 'Покупатель',
    s.Name AS 'Исполнитель',
    o.Total AS 'Сумма заказа',
    COALESCE(SUM(m.Price * pm.Quantity * oi.Quantity), 0) AS 'Себестоимость материалов',
    o.Total - COALESCE(SUM(m.Price * pm.Quantity * oi.Quantity), 0) AS 'Разница (прибыль/убыток)'
FROM Orders o
JOIN Companies b ON o.BuyerId = b.Id
JOIN Companies s ON o.ShopId = s.Id
JOIN OrderItems oi ON oi.OrderId = o.Id
LEFT JOIN ProductMaterials pm ON pm.ProductId = oi.ProductId
LEFT JOIN Materials m ON pm.MaterialId = m.Id
GROUP BY o.OrderNumber, o.OrderDate, b.Name, s.Name, o.Total
ORDER BY o.OrderDate;

-- Вариант 3: Расчёт для конкретного заказа (например, Заказ № 3)
SELECT 
    o.OrderNumber AS 'Номер заказа',
    o.OrderDate AS 'Дата заказа',
    b.Name AS 'Покупатель',
    s.Name AS 'Исполнитель',
    p.Name AS 'Продукция',
    oi.Quantity AS 'Количество',
    oi.Sum AS 'Сумма продажи',
    m.Name AS 'Материал',
    pm.Quantity AS 'Норма расхода',
    m.Price AS 'Цена материала',
    (m.Price * pm.Quantity * oi.Quantity) AS 'Стоимость материалов в заказе'
FROM Orders o
JOIN Companies b ON o.BuyerId = b.Id
JOIN Companies s ON o.ShopId = s.Id
JOIN OrderItems oi ON oi.OrderId = o.Id
JOIN Products p ON oi.ProductId = p.Id
LEFT JOIN ProductMaterials pm ON pm.ProductId = p.Id
LEFT JOIN Materials m ON pm.MaterialId = m.Id
WHERE o.OrderNumber = 'Заказ № 3'
ORDER BY p.Name, m.Name;
