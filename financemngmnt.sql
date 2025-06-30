use FINANCEDB;

INSERT INTO Userer (username, password, email) VALUES 
('alice', 'pass123', 'alice@example.com'),
('bob', 'secure456', 'bob@example.com'),
('carol', 'admin789', 'carol@example.com'),
('dave', 'test111', 'dave@example.com'),
('emma', 'qwerty123', 'emma@example.com'),
('frank', 'letmein', 'frank@example.com'),
('grace', 'grace456', 'grace@example.com'),
('hannah', 'hannah789', 'hannah@example.com'),
('ian', 'ian321', 'ian@example.com'),
('jane', 'jane123', 'jane@example.com');
           
          
CREATE TABLE ExpenseCategories (
    category_id INT PRIMARY KEY IDENTITY(1,1),
    category_name VARCHAR(100)
);

INSERT INTO ExpenseCategories (category_name) VALUES 
('Food'),
('Travel'),
('Utilities'),
('Entertainment'),
('Healthcare'),
('Education'),
('Rent'),
('Shopping'),
('Insurance'),
('Subscriptions');


CREATE TABLE Expenses (
    expense_id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT FOREIGN KEY REFERENCES Users(user_id),
    amount DECIMAL(10,2),
    category_id INT FOREIGN KEY REFERENCES ExpenseCategories(category_id),
    date DATE,
    description VARCHAR(255)
);

INSERT INTO Expenses (user_id, amount, category_id, date, description) VALUES 
(1, 150.75, 1, '2025-06-01', 'Groceries from supermarket'),
(2, 60.00, 2, '2025-06-02', 'Train fare to work'),
(3, 1200.00, 3, '2025-06-03', 'Electricity and water bill'),
(4, 320.50, 4, '2025-06-04', 'Cinema and snacks'),
(5, 850.00, 5, '2025-06-05', 'Dentist visit'),
(6, 400.00, 6, '2025-06-06', 'Online certification course'),
(7, 7500.00, 7, '2025-06-07', 'Monthly house rent'),
(8, 2200.00, 8, '2025-06-08', 'Clothes and accessories'),
(9, 1250.00, 9, '2025-06-09', 'Life insurance premium'),
(10, 299.00, 10, '2025-06-10', 'Netflix and Spotify subscriptions');

SELECT * FROM ExpenseCategories;
select * from Expenses;

SELECT * FROM Userer;

use master;
drop table Users;
DROP DATABASE IF EXISTS master;
use FINANCEDB;

