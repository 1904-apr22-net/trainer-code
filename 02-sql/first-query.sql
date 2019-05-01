--comments with --

-- many statements in one file like this...
-- the "execute" button (F5) will run all the statements
-- and show all the outputs

-- when we don't want to run the whole file, we highlight what
-- we want to run and then hit F5

-- SQL has many commands/statements
-- the first category of them is called DML
--     Data Manipulation Language

-- the most complicated/important DML statement
-- is the SELECT statement. it's what we use to read from
-- database tables.

SELECT 'Hello world';

-- all rows and all columns
-- of a table
SELECT *
FROM SalesLT.Customer;

-- 4. list all customers (full names, customer ID, and country) who are not in the US
SELECT c.CustomerID, FirstName, MiddleName, LastName, CountryRegion
FROM SalesLT.Customer AS c
	INNER JOIN SalesLT.CustomerAddress AS ca ON c.CustomerID = ca.CustomerID
	INNER JOIN SalesLT.Address AS a ON ca.AddressID = a.AddressID
WHERE CountryRegion != 'United States';

-- 6. list all sales agents
SELECT DISTINCT SalesPerson
FROM SalesLT.Customer;

-- 11. show total sales per country, ordered by highest sales first.
SELECT COUNT(*), SUM(TotalDue), CountryRegion
FROM SalesLT.SalesOrderHeader AS soh
	INNER JOIN SalesLT.Address AS a ON soh.ShipToAddressID = a.AddressID
GROUP BY CountryRegion
ORDER BY SUM(TotalDue) DESC;
