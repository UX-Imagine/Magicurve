USE [MasterDB]
GO

/* Default languages. */
INSERT INTO [dbo].[Language]([Name],[Code],[sys_Version]) VALUES ('English','en',0)
GO
INSERT INTO [dbo].[Language]([Name],[Code],[sys_Version]) VALUES ('Italian','it',0)
GO
INSERT INTO [dbo].[Language]([Name],[Code],[sys_Version]) VALUES ('Japanese','ja',0)
GO
INSERT INTO [dbo].[Language]([Name],[Code],[sys_Version]) VALUES ('Arabic','ar',0)
GO

/* Default themes. */
INSERT INTO [dbo].[Theme]([Name],[sys_Version]) VALUES ('DefaultTheme',0)
GO
INSERT INTO [dbo].[Theme]([Name],[sys_Version]) VALUES ('Spacelab',0)
GO
INSERT INTO [dbo].[Theme]([Name],[sys_Version]) VALUES ('Cerulean',0)
GO
INSERT INTO [dbo].[Theme]([Name],[sys_Version]) VALUES ('InnoMetriX',0)
GO
INSERT INTO [dbo].[Theme]([Name],[sys_Version]) VALUES ('Untitled',0)
GO
