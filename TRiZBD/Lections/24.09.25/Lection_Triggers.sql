
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


--CREATE TRIGGER TrDeleteGame
--    ON Game
--	INSTEAD OF DELETE
--	AS
--		UPDATE Game
--		SET IsDeleted=1
--		WHERE GameId IN (SELECT GameId FROM deleted)


--CREATE TRIGGER TraddSale
--    ON Sale
--    AFTER INSERT
--    AS
--		UPDATE Game
--		SET KeysAmount -= inserted.KeysAmount
--		FROM Game
--			JOIN inserted ON Game.GameId = inserted.GameId;

--CREATE TRIGGER TrChangePrice
--ON Game
--AFTER UPDATE
--AS
--  IF UPDATE(Price)
--    IF EXISTS (SELECT * 
--	           FROM inserted i
--			     JOIN deleted d ON i.GameId=d.GameId
--			   WHERE i.Price < d.Price)
--		THROW 50000, 'Нельзя уменьшать цену', 1;

--disable trigger TraddSale on Sale;

--CREATE TRIGGER TrAddSaleWithCheck
--ON Sale
--AFTER INSERT
--AS
--BEGIN 
--	-- объявление переменных
--	DECLARE @gamesKeys SMALLINT;
--	DECLARE @salesKeys SMALLINT;
--	-- присваивание значений
--	SELECT @gamesKeys=g.KeysAmount, @salesKeys=i.KeysAmount
--	FROM Game g JOIN inserted i ON g.GameId=i.GameId;

--	IF (@gamesKeys < @salesKeys)
--		THROW 50001, 'Недостаточно ключей игры', 1;
--	ELSE
--		UPDATE Game
--		SET KeysAmount -= inserted.KeysAmount
--		FROM Game JOIN inserted ON Game.GameId = inserted.GameId;
--END;

CREATE TRIGGER TrAddSaleWithCheck2
ON Sale
INSTEAD OF INSERT
AS
BEGIN
    -- объявление переменных
    DECLARE @gamesKeys SMALLINT;
    DECLARE @salesKeys SMALLINT;
	-- присваивание значений
	SELECT @gamesKeys=g.KeysAmount, @salesKeys=i.KeysAmount
	FROM Game g JOIN inserted i ON g.GameId=i.GameId; 

	IF (@gamesKeys < @salesKeys)
	  THROW 50001, 'недостаточно ключей игры', 1
	ELSE 
	BEGIN
	  INSERT INTO Sale(GameId, KeysAmount)
	  SELECT GameId, KeysAmount
	  FROM inserted;

      UPDATE Game
      SET KeysAmount -= inserted.KeysAmount
	  FROM Game JOIN inserted ON Game.GameId=inserted.GameId;
	END
END