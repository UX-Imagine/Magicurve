USE [MasterDB]
GO

ALTER TABLE [dbo].[SecurityUser] DROP CONSTRAINT [UNQ_SecurityUser_UserName] WITH (ONLINE = OFF)
GO

ALTER TABLE [dbo].[SecurityUser] ADD CONSTRAINT [UNQ_SecurityUser_UserName_Provider] UNIQUE(UserName, Provider)
GO