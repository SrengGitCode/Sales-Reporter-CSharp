CREATE TABLE PRODUCTSALES (
    SALEID INT PRIMARY KEY,
    PRODUCTCODE NVARCHAR(20),
    PRODUCTNAME NVARCHAR(100),
    QUANTITY INT,
    UNITPRICE DECIMAL(18,2),
    SALEDATE DATE
);

INSERT INTO PRODUCTSALES (SALEID, PRODUCTCODE, PRODUCTNAME, QUANTITY, UNITPRICE, SALEDATE)
VALUES
(1, 'P001', 'Pen', 10, 1.50, '2025-06-20'),
(2, 'P001', 'Pen', 5, 1.50, '2025-06-25'),
(3, 'P002', 'Notebook', 3, 3.20, '2025-06-21'),
(4, 'P003', 'Eraser', 15, 0.80, '2025-06-22');


/* this is for extra data

INSERT INTO PRODUCTSALES (SALEID, PRODUCTCODE, PRODUCTNAME, QUANTITY, UNITPRICE, SALEDATE)
VALUES
(5, 'P004', 'Highlighter', 8, 1.20, '2025-06-20'),
(6, 'P002', 'Notebook', 5, 3.20, '2025-06-22'),
(7, 'P005', 'Stapler', 2, 5.50, '2025-06-23'),
(8, 'P001', 'Pen', 20, 1.45, '2025-06-28'),
(9, 'P003', 'Eraser', 10, 0.85, '2025-07-01'),
(10, 'P006', 'Ruler', 12, 0.75, '2025-07-02'),
(11, 'P004', 'Highlighter', 6, 1.20, '2025-07-03'),
(12, 'P007', 'Whiteboard Marker', 5, 2.10, '2025-07-05'),
(13, 'P002', 'Notebook', 4, 3.25, '2025-07-05'),
(14, 'P001', 'Pen', 15, 1.50, '2025-07-08'),
(15, 'P006', 'Ruler', 10, 0.75, '2025-07-10'),
(16, 'P005', 'Stapler', 3, 5.40, '2025-07-11'),
(17, 'P003', 'Eraser', 25, 0.80, '2025-07-15'),
(18, 'P007', 'Whiteboard Marker', 8, 2.10, '2025-07-18'),
(19, 'P004', 'Highlighter', 12, 1.15, '2025-07-20'),
(20, 'P002', 'Notebook', 10, 3.20, '2025-07-22');

*/


