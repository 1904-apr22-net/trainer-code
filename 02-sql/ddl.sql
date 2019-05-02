-- DDL
-- Data Definition Language

CREATE DATABASE MovieDb;

GO
CREATE SCHEMA Movie;
GO

-- CREATE TABLE
CREATE TABLE Movie.Movie (
	-- list of columns, constraints, etc.
	-- each column: name, and then type, and then contraints
	MovieId INT NOT NULL
);

SELECT * FROM Movie.Movie;

-- we can use ALTER TABLE to add/delete columns, modify things
ALTER TABLE Movie.Movie ADD
	Title NVARCHAR(200) NOT NULL;

-- DROP TABLE to delete tables entirely
DROP TABLE Movie.Movie;

-- constraints

-- NOT NULL: NULL not allowed in the column.
-- NULL: explicitly allowing NULL for documentation
--   (already there by default) (maybe not really a constraint)
-- PRIMARY KEY
--   (enforces uniqueness and NOT NULL, sets clustered index)
-- UNIQUE: the column cannot have any duplicate values
--   (can be set on sets of columns, as well as just one)
-- DEFAULT: provide a default value for this column.
--   (either this or NULL is necessary when adding a new column
--   to a table that already has data)
-- FOREIGN KEY
--   (can set "cascade" behavior... ON DELETE CASCADE, ON DELETE SET NULL, ON UPDATE ...)
-- CHECK:
--   catch all, any boolean expression you want to write
--   to validate the values in a row. is checked every time a row
--   is updated or inserted.
-- IDENTITY(start=1, increment=1):
--   useful for integer primary keys.
--   prevents inserts or updates on the column, and gives automatic
--   incrementing ID.
--   (it is possible to switch on IDENTITY_INSERT)

--DROP TABLE Movie.Movie;
CREATE TABLE Movie.Movie (
	MovieId INT NOT NULL IDENTITY,
	Title NVARCHAR(200) NOT NULL,
	ReleaseDate DATE NOT NULL,
	DateModified DATETIME2 NOT NULL DEFAULT(GETDATE()),
	Active BIT NOT NULL DEFAULT(1),
	CONSTRAINT PK_MovieId PRIMARY KEY (MovieId),
	CONSTRAINT U_Title_ReleaseDate UNIQUE (Title, ReleaseDate),
	CONSTRAINT CK_DateNotTooEarly CHECK (ReleaseDate > '1900')
);

INSERT INTO Movie.Movie (Title, ReleaseDate) VALUES
	('Avengers: Endgame', '2019');

INSERT INTO Movie.Movie (MovieId, Title, ReleaseDate) VALUES
	(8, 'asdf', '1019');

CREATE TABLE Movie.Genre (
	GenreId INT NOT NULL IDENTITY,
	Name NVARCHAR(100) NOT NULL UNIQUE,
	DateModified DATETIME2 NOT NULL DEFAULT(GETDATE()),
	Active BIT NOT NULL DEFAULT(1)
);

ALTER TABLE Movie.Genre ADD
	CONSTRAINT PK_GenreId PRIMARY KEY (GenreId);

INSERT INTO Movie.Genre (Name) VALUES
	('Action'); -- 1

ALTER TABLE Movie.Movie ADD
	GenreId INT NOT NULL DEFAULT(1),
	CONSTRAINT FK_Movie_Genre FOREIGN KEY (GenreId)
		REFERENCES Movie.Genre (GenreId)
		ON DELETE CASCADE;

ALTER TABLE Movie.Movie DROP
	CONSTRAINT DF__Movie__GenreId__5535A963;