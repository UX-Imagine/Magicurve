USE [MasterDB]
GO

/****** Object:  Table [dbo].[Image]   ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER TABLE [dbo].[SecurityUser] ADD [Provider] [int] NULL
Go

UPDATE [dbo].[SecurityUser] SET [Provider] = 0 WHERE [Provider] IS NULL
GO

ALTER TABLE [dbo].[SecurityUser] ALTER COLUMN [Provider] [int] NOT NULL
GO