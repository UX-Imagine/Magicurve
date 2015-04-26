
USE [master]
GO
SET NOCOUNT ON
GO

-- create login

IF NOT EXISTS(SELECT 1 FROM syslogins WHERE name ='BallaratAppUser')
BEGIN
	CREATE LOGIN BallaratAppUser WITH PASSWORD = 'Az3r0th', DEFAULT_DATABASE = [MasterDB]
END
	
GO

