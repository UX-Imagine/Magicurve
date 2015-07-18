USE [master]
GO
SET NOCOUNT ON
GO

-- create login

IF NOT EXISTS(SELECT 1 FROM syslogins WHERE name ='UxAppUser')
BEGIN
	CREATE LOGIN UxAppUser WITH PASSWORD = 'Az3r0th', DEFAULT_DATABASE = [MagicurveDiagnosticDB]
END
	
GO

