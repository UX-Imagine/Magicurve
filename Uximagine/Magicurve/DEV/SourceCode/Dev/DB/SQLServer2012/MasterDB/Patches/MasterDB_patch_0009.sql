USE [MasterDB]
GO

/****** Object:  Table [dbo].[Image]   ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Image](
	[ImageID] [uniqueidentifier] NOT NULL,
	[AlternateSequence] [bigint] IDENTITY(1,1) NOT NULL,
	[Caption] [nvarchar](50) NULL,
	[FileSystemPath] [nvarchar](500) NOT NULL,
	[Type] [nvarchar](10) NULL,
	[IsDeleted] [bit] NULL,
	[sys_DateCreated] [datetime] NOT NULL,
	[sys_DateLastModified] [datetime] NOT NULL,
	[sys_Version] [bigint] NOT NULL,
 CONSTRAINT [PK_Image] PRIMARY KEY NONCLUSTERED 
(
	[ImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
CREATE UNIQUE CLUSTERED INDEX IX_Image ON dbo.Image(AlternateSequence)

GO

GRANT INSERT ON [dbo].[Image] TO BallaratAppUserRole
GO
GRANT UPDATE ON [dbo].[Image] TO BallaratAppUserRole
GO
GRANT DELETE ON [dbo].[Image] TO BallaratAppUserRole
GO
GRANT SELECT ON [dbo].[Image] TO BallaratAppUserRole
GO
