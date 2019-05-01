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