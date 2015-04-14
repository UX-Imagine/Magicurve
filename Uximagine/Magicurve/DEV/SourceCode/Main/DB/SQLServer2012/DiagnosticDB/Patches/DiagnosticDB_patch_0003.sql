USE [MagicurveDiagnosticDB]
GO
SET NOCOUNT ON
GO

-- create role

IF NOT EXISTS (SELECT 1 FROM dbo.sysusers WHERE (name = N'UxAppUserRole'))
	CREATE ROLE UxAppUserRole AUTHORIZATION dbo
GO


-- create user

IF NOT EXISTS (SELECT 1 FROM dbo.sysusers WHERE name = N'uxAppUser')
	CREATE USER UxAppUser FOR LOGIN UxAppUser WITH DEFAULT_SCHEMA = dbo
GO

-- add user to role

EXEC sp_addrolemember N'UxAppUserRole', N'UxAppUser'

GO
