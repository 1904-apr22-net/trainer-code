-- every track, with his genre
SELECT Track.Name, Genre.Name
FROM Track
	LEFT OUTER JOIN Genre ON Track.GenreId = Genre.GenreId;

-- condition is always true = cross join
SELECT g1.Name, g2.Name
FROM Genre AS g1
	INNER JOIN Genre AS g2 ON 1 = 1;

-- all rock songs in the database, showing the name like this:
--     (artist name) - (song name)
SELECT COALESCE(ar.Name, 'Unknown Artist') + ' - ' + t.Name
FROM Track AS t
	LEFT JOIN Genre AS g ON t.GenreId = g.GenreId
	LEFT JOIN Album AS al ON t.AlbumId = al.AlbumId
	LEFT JOIN Artist AS ar ON al.ArtistId = ar.ArtistId
WHERE g.Name = 'Rock';

-- set operators --
-- these operators are going to suppress duplicates
-- and implement "set union" "set intersection" and "set difference"

-- UNION
-- gives all results from two queries combined.

-- all first names of customers or employees
SELECT FirstName FROM Employee
UNION
SELECT FirstName FROM Customer;

-- we can switch off the removing of duplicates.

-- every set operator in some other versions of SQL has a regular ("DISTINCT") version,
--    and an ALL version.
-- in SQL Server, only UNION has an ALL version.

-- UNION ALL is faster than UNION by itself


SELECT FirstName FROM Employee
UNION ALL
SELECT FirstName FROM Customer;

-- that is "set union" - like a big OR

-- every customer first name that is also a employee first name
SELECT FirstName FROM Employee
INTERSECT
SELECT FirstName FROM Customer;

-- can do the same with join in this case
SELECT DISTINCT e.FirstName
FROM Employee AS e
	INNER JOIN Customer AS c ON e.FirstName = c.FirstName;

-- set intersection is like a big AND

-- "set difference" implemented by EXCEPT (in some other variants of SQL, MINUS)

-- every employee first name that is NOT a customer first name.
SELECT FirstName FROM Employee
EXCEPT
SELECT FirstName FROM Customer;