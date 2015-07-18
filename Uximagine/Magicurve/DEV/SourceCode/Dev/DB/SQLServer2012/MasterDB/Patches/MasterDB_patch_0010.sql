USE [MasterDB]
GO

/****** Object:  Table [dbo].[Image]   ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER TABLE [dbo].[Person] ADD [ProfileImageID] [uniqueidentifier] NULL, [AddressHomeCountry] [nvarchar](50) NULL
Go

ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_Image_ProfileImage] FOREIGN KEY([ProfileImageID])
REFERENCES [dbo].[Image] ([ImageID])
GO

ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_Person_Image_ProfileImage]
GO