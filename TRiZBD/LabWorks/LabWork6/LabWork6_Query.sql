-- 1
ALTER TRIGGER TrChangeEmail
    ON Visitor
    AFTER UPDATE
    AS
		IF UPDATE (Email)
			INSERT INTO ChangedEmail (VisitorId, Email)
			SELECT VisitorId, Email FROM deleted;

-- 2
CREATE TRIGGER TrDeleteFilm
    ON Film
    INSTEAD OF DELETE
    AS
		UPDATE Film
		SET IsDeleted = 1
		WHERE FilmId IN (SELECT FilmId FROM deleted);

-- 3
CREATE TRIGGER TrDeleteVisitor
    ON Visitor
    AFTER DELETE
    AS
		INSERT INTO DeletedVisitor
           (VisitorId
           ,Phone
           ,[Name]
           ,Birthday
           ,Email)
     SELECT * FROM deleted;

-- 4
CREATE TRIGGER TrInsertWithCheckPrice
    ON [Session]
		INSTEAD OF INSERT
		AS
			INSERT INTO [Session]
			   (FilmId
			   ,HallId
			   ,Price
			   ,SessionTime
			   ,Is3d)
		 SELECT FilmId, HallId, CASE WHEN Price < 100 THEN 100 ELSE Price END Price, SessionTime, Is3d
		 FROM inserted;

-- 5
ALTER TRIGGER TrInsertWithCheckSeatAndRow
    ON Ticket
    INSTEAD OF INSERT
    AS
    BEGIN
		DECLARE @seatsQuantityInHall TINYINT;
		DECLARE @rowsQuantityInHall TINYINT;

		DECLARE @seatInTicket TINYINT;
		DECLARE @rowInTicket TINYINT;

		SELECT @seatsQuantityInHall = h.SeatsQuantity, @rowsQuantityInHall = h.RowsQuantity FROM Hall h
			JOIN [Session] s ON s.HallId = h.HallId
			JOIN inserted i ON i.SessionId = s.SessionId;

		If (@seatInTicket > @seatsQuantityInHall OR @rowInTicket > @rowsQuantityInHall)
			THROW 50000, 'Неправильно указано место.', 1;
		ELSE
			INSERT INTO [dbo].[Ticket]
					([VisitorId]
					,[SessionId]
					,[Row]
					,[Seat])
			SELECT VisitorId, SessionId, [Row], Seat FROM inserted;
    END
