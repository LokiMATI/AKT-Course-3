
--CREATE TRIGGER TrGamesRowsCount
--    ON Game
--    AFTER DELETE, INSERT, UPDATE
--    AS
--		PRINT 'Количество изменённых строк: ' + CAST(@@ROWCOUNT AS VARCHAR(10));

--UPDATE Game
--SET Price+=1;


--CREATE TRIGGER TrSavePrice
--    ON Game
--	AFTER UPDATE
--    AS
--		IF UPDATE(Price)
--		INSERT INTO GamePrice 
--			(GameId, OldPrice)
--		SELECT GameId, Price
--		FROM deleted;

--UPDATE Game
--SET Price+=10
--WHERE GameId<5;

--SELECT * FROM GamePrice;


--CREATE TRIGGER TrSaveCategory
--    ON Category
--	AFTER DELETE
--	AS
--		INSERT INTO DeletedCategory(CategoryId, [Name])
--		SELECT CategoryId, [Name] FROM deleted;

--INSERT INTO Category([Name]) VALUES('рогалик'), ('jROG');


CREATE TRIGGER TrDeleteGame
    ON Game
	INSTEAD OF DELETE
	AS
		UPDATE Game
		SET IsDeleted=1
		WHERE GameId IN (SELECT GameId FROM deleted)


