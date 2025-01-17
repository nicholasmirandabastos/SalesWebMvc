USe SalesWebMvc

select *
from SalesRecord

select *
from seller

select *
from Department


INSERT INTO seller (Name, Email, BirthDate, BaseSalary, DepartmentId)
VALUES ('Nicholas', 'nicholasmirandabastos@gmail.com', '1996-10-01', 3000, 1);


INSERT INTO SalesRecord (Date, Amount, Status, SellerId)
VALUES (GETDATE(), 1, 1, 14);
