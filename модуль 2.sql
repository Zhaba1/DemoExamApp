-- Скрипт для создания базы данных DemoExamDB на основе задания В2
-- 3-я нормальная форма, ссылочная целостность через внешние ключи

SET NAMES utf8mb4;
SET CHARACTER SET utf8mb4;
USE DemoExamDB;

-- Удаляем старые таблицы (кроме Users)
SET FOREIGN_KEY_CHECKS = 0;
DROP TABLE IF EXISTS OrderItems;
DROP TABLE IF EXISTS Production;
DROP TABLE IF EXISTS ProductMaterials;
DROP TABLE IF EXISTS Orders;
DROP TABLE IF EXISTS Companies;
DROP TABLE IF EXISTS Products;
DROP TABLE IF EXISTS Materials;
SET FOREIGN_KEY_CHECKS = 1;

-- Таблица организаций (продавцы и покупатели в одной таблице)
CREATE TABLE Companies (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    ExternalId VARCHAR(20) UNIQUE,
    Name VARCHAR(100) NOT NULL,
    INN VARCHAR(20),
    Address VARCHAR(255),
    Phone VARCHAR(50),
    IsSalesman BOOLEAN DEFAULT FALSE,
    IsBuyer BOOLEAN DEFAULT FALSE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Таблица продуктов (готовой продукции)
CREATE TABLE Products (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    Unit VARCHAR(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Таблица материалов
CREATE TABLE Materials (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    Unit VARCHAR(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Таблица спецификаций: состав продуктов из материалов
CREATE TABLE ProductMaterials (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    ProductId INT NOT NULL,
    MaterialId INT NOT NULL,
    Quantity DECIMAL(10,3) NOT NULL,
    FOREIGN KEY (ProductId) REFERENCES Products(Id) ON DELETE CASCADE,
    FOREIGN KEY (MaterialId) REFERENCES Materials(Id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Таблица заказов
CREATE TABLE Orders (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    OrderNumber VARCHAR(50) NOT NULL,
    OrderDate DATE NOT NULL,
    BuyerId INT NOT NULL,
    ShopId INT NOT NULL,
    Total DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (BuyerId) REFERENCES Companies(Id) ON DELETE CASCADE,
    FOREIGN KEY (ShopId) REFERENCES Companies(Id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Таблица строк заказа
CREATE TABLE OrderItems (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    OrderId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    Sum DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES Orders(Id) ON DELETE CASCADE,
    FOREIGN KEY (ProductId) REFERENCES Products(Id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Таблица производства
CREATE TABLE Production (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    ProductionNumber VARCHAR(50) NOT NULL,
    ProductionDate DATE NOT NULL,
    ProductId INT NOT NULL,
    CompanyId INT NOT NULL,
    Quantity INT NOT NULL,
    FOREIGN KEY (ProductId) REFERENCES Products(Id) ON DELETE CASCADE,
    FOREIGN KEY (CompanyId) REFERENCES Companies(Id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Наполнение данными

-- Организации из файла companies.json
INSERT INTO Companies (ExternalId, Name, INN, Address, Phone, IsSalesman, IsBuyer) VALUES
('000000001', 'ООО "Поставка"', '', 'г.Пятигорск', '+79198634592', TRUE, TRUE),
('000000002', 'ООО "Кинотеатр Квант"', '26320045123', 'г. Железноводск, ул. Мира, 123', '+79884581555', TRUE, FALSE),
('000000008', 'ООО "Новый JDTO"', '26320045111', 'г. Железноводсу', '+79884581555', TRUE, FALSE),
('000000003', 'ООО "Ромашка"', '4140784214', 'г. Омск, ул. Строителей, 294', '+79882584546', FALSE, TRUE),
('000000009', 'ООО "Ипподром"', '5874045632', 'г. Уфа, ул. Набережная,  37', '+79627486389', TRUE, TRUE),
('000000010', 'ООО "Ассоль"', '2629011278', 'г. Калуга, ул. Пушкина, 94', '+79184572398', FALSE, TRUE);

INSERT INTO Products (Name, Price, Unit) VALUES
('Батон нарезной', 45.00, 'шт'),
('Булочка с изюмом', 35.00, 'шт'),
('Булочка с корицей', 35.00, 'шт'),
('Хлеб белый 1 кг.', 42.00, 'шт'),
('Хлеб Кронштадтский 1 кг.', 120.00, 'шт'),
('Хлеб ржаной 800г.', 47.00, 'шт');

INSERT INTO Materials (Name, Price, Unit) VALUES
('Закваска сметанная', 45.00, 'шт'),
('Изюм', 150.00, 'кг'),
('Масло сливочное', 124.00, 'кг'),
('Молоко нормализованное', 34.00, 'кг'),
('Мука', 220.00, 'кг'),
('Сода', 60.00, 'шт'),
('Яйца', 80.00, 'шт');

-- Спецификация: Булочка с изюмом
INSERT INTO ProductMaterials (ProductId, MaterialId, Quantity)
SELECT p.Id, m.Id, q.Quantity
FROM (
    SELECT 'Булочка с изюмом' AS ProductName, 'Изюм' AS MaterialName, 0.020 AS Quantity
    UNION ALL SELECT 'Булочка с изюмом', 'Масло сливочное', 0.020
    UNION ALL SELECT 'Булочка с изюмом', 'Молоко нормализованное', 0.150
    UNION ALL SELECT 'Булочка с изюмом', 'Яйца', 0.250
    UNION ALL SELECT 'Булочка с изюмом', 'Мука', 0.100
    UNION ALL SELECT 'Булочка с изюмом', 'Сода', 0.005
) q
JOIN Products p ON p.Name = q.ProductName
JOIN Materials m ON m.Name = q.MaterialName;

-- Заказ покупателя № 3 от 7 июня 2025 г.
-- Покупатель: ООО "Ромашка" (Id = 4), Исполнитель: ООО "Поставка" (Id = 1)
INSERT INTO Orders (OrderNumber, OrderDate, BuyerId, ShopId, Total)
VALUES ('Заказ № 3', '2025-06-07', 4, 1, 689.00);

INSERT INTO OrderItems (OrderId, ProductId, Quantity, Price, Sum)
SELECT 1, p.Id, q.Quantity, q.Price, q.Sum
FROM (
    SELECT 'Хлеб белый 1 кг.' AS ProductName, 8 AS Quantity, 45.00 AS Price, 360.00 AS Sum
    UNION ALL SELECT 'Хлеб ржаной 800г.', 7, 47.00, 329.00
) q
JOIN Products p ON p.Name = q.ProductName;

-- Производство № 1 от 9 июня 2025 г.
-- Изготовитель: ООО "Поставка" (Id = 1), Продукт: Булочка с изюмом
INSERT INTO Production (ProductionNumber, ProductionDate, ProductId, CompanyId, Quantity)
VALUES ('Производство № 1', '2025-06-09', 2, 1, 1);

-- Проверка
SELECT 'Companies' AS TableName, COUNT(*) AS Count FROM Companies
UNION ALL SELECT 'Products', COUNT(*) FROM Products
UNION ALL SELECT 'Materials', COUNT(*) FROM Materials
UNION ALL SELECT 'ProductMaterials', COUNT(*) FROM ProductMaterials
UNION ALL SELECT 'Orders', COUNT(*) FROM Orders
UNION ALL SELECT 'OrderItems', COUNT(*) FROM OrderItems
UNION ALL SELECT 'Production', COUNT(*) FROM Production;
