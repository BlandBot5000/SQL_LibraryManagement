CREATE DATABASE dbLibrary
GO

	CREATE TABLE tblBooks (
		bookID INT PRIMARY KEY NOT NULL IDENTITY (1,1),
		title VARCHAR(100) NOT NULL,
		author VARCHAR(100) NOT NULL,
		publisherName VARCHAR(100) NOT NULL,
	);

	INSERT INTO tblBooks
		(title, author, publisherName)
		VALUES
		('Convenience Store Woman', 'Sayaka Murata', 'Grove Press'),
		('The Three-Body Problem', 'Liu Cixin', 'Tor Books'),
		('The Master and Margarita', 'Mikhail Bulgakov', 'Penguin Classics');

GO

	CREATE PROC BooksCreateOrUpdate
	@bookID int,
	@title varchar(100),
	@author varchar(100),
	@publisherName varchar(100)
	AS
	BEGIN
	IF(@bookID=0)
		BEGIN
		INSERT INTO tblBooks(title, author, publisherName)
		VALUES (@title, @author, @publisherName)
		END
	ELSE
		BEGIN
		UPDATE tblBooks
		SET
			title = @title,
			author = @author,
			publisherName = @publisherName
		WHERE bookID = @bookID
		END
	END
GO

	CREATE PROC BooksViewAll
	AS
		BEGIN
		SELECT * FROM tblBooks
		END
GO

	CREATE PROC BooksDeleteByID
	@bookID int
	AS
		BEGIN
		DELETE FROM tblBooks
		WHERE bookID = @bookID
		END
GO
	CREATE PROC BooksViewByID
	@bookID int
	AS
		BEGIN
		SELECT * FROM tblBooks
		WHERE bookID = @bookID
		END