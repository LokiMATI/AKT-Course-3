sp_adduser 'login1', 'user1'
EXEC sp_adduser 'login2', 'user2'


-- 1
CREATE USER [user3] FOR LOGIN [login3]

CREATE USER [user4] FOR LOGIN [login4]

CREATE LOGIN [ispp3113] 
WITH PASSWORD=N'lvT63l7CiV93mXm3D+qWE3m9XBrrh8+S/oP5viJ6Opk=', -- √енерируетс€ случайно
DEFAULT_DATABASE=[ispp3113], 
DEFAULT_LANGUAGE=[русский], 
CHECK_EXPIRATION=OFF, -- “ребовать ли через определЄнный промежуток вресмени пароль
CHECK_POLICY=OFF -- ƒолжны ли политики паролей Windows сервера, на котором запущен SQL Server, примен€тьс€ к логину

EXEC sp_addlogin 'isppLogin132', 'Password!', 'ispp3113', 'русский'

EXEC sp_addsrvrolemember 'isppLogin132', 'securityadmin'


-- 2
EXEC sp_addrolemember 'db_owner', 'user1'

EXEC sp_addrolemember 'db_datareader', 'user2'
EXEC sp_addrolemember 'db_datawriter', 'user2'

EXEC sp_droprolemember 'db_datawriter', 'user2';


-- 3
GRANT INSERT, DELETE ON Ticket TO [user3];

GRANT SELECT, UPDATE ([Name], Email) ON Visitor TO [user4];

DENY SELECT ON Visitor TO [user2];

DENY UPDATE ([Name]) ON Visitor TO [user4];

-- 4
DECLARE @count TINYINT;

SET @count = 1;

WHILE @count < 5
	BEGIN
		EXEC('CREATE USER [reader' + @count  + '] FOR LOGIN [reader' + @count + ']');
		EXEC( 'sp_addrolemember ''db_datareader'', ''reader' + @count +'''');

		SET @count = @count + 1; 
	END;

-- 5
DECLARE @password NVARCHAR(255);
SET @password = 'Password';

INSERT INTO PW4Users([Login], [Password], EncryptedPassword)
VALUES ('Test', @password, HASHBYTES('SHA2_256', @password))

SELECT [Login]
FROM PW4Users
WHERE [