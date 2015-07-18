
USE [MasterDB]
GO
SET NOCOUNT ON
GO

-- create role

IF NOT EXISTS (SELECT 1 FROM dbo.sysusers WHERE (name = N'BallaratAppUserRole'))
	CREATE ROLE BallaratAppUserRole AUTHORIZATION dbo
GO


-- create user

IF NOT EXISTS (SELECT 1 FROM dbo.sysusers WHERE name = N'BallaratAppUser')
	CREATE USER BallaratAppUser FOR LOGIN BallaratAppUser WITH DEFAULT_SCHEMA = dbo
GO

-- add user to role

EXEC sp_addrolemember N'BallaratAppUserRole', N'BallaratAppUser'

GO
