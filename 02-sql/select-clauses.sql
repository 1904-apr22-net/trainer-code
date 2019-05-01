-- SELECT clauses
	-- SELECT
	-- FROM
	-- WHERE

--SELECT (some set of columns)
--FROM (a table)
--WHERE (the column values meet some condition)

-- get the full names of everyone whose name is John
SELECT FirstName, LastName
FROM SalesLT.Customer
WHERE FirstName = 'John';