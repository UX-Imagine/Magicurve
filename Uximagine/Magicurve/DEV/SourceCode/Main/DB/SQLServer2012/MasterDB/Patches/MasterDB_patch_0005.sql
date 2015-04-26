USE [MasterDB]
GO

SET NOCOUNT ON
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/****** Object:  Table [dbo].[OrganizationReference]    Script Date: 6/26/2014 9:41:14 AM ******/
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[OrganizationReference]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
	CREATE TABLE [dbo].[OrganizationReference](
		[OrganizationReferenceID] [uniqueidentifier] ROWGUIDCOL  NOT NULL
			CONSTRAINT [DF_OrganizationReference_OrganizationReferenceID] DEFAULT newsequentialid(),
		[AlternateSequence] [bigint] IDENTITY(1,1) NOT NULL,
		[Code] [nvarchar](20) NOT NULL,
		[Name] [nvarchar](100) NOT NULL,
		[sys_DateCreated] [datetime] NOT NULL
			CONSTRAINT [DF_OrganizationReference_sys_DateCreated] DEFAULT getdate(),
		[sys_DateLastModified] [datetime] NOT NULL
			CONSTRAINT [DF_OrganizationReference_sys_DateLastModified] DEFAULT getdate(),
		[sys_Version] [bigint] NOT NULL,
	 CONSTRAINT
		[PK_OrganizationReference] PRIMARY KEY NONCLUSTERED ([OrganizationReferenceID] ASC)
	WITH
		(PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]

	CREATE UNIQUE CLUSTERED INDEX IX_OrganizationReference ON dbo.OrganizationReference(AlternateSequence)
END
GO

GRANT INSERT ON [dbo].[OrganizationReference] TO BallaratAppUserRole
GO
GRANT UPDATE ON [dbo].[OrganizationReference] TO BallaratAppUserRole
GO
GRANT DELETE ON [dbo].[OrganizationReference] TO BallaratAppUserRole
GO
GRANT SELECT ON [dbo].[OrganizationReference] TO BallaratAppUserRole
GO


/****** Object:  Table [dbo].[OrganizationDBConnection]    Script Date: 6/26/2014 9:40:40 AM ******/

SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[OrganizationDBConnection]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
	CREATE TABLE [dbo].[OrganizationDBConnection](
		[OrganizationDBConnectionGuid] [uniqueidentifier] ROWGUIDCOL  NOT NULL
			CONSTRAINT [DF_OrganizationDBConnection_OrganizationDBConnectionGuid] DEFAULT newsequentialid(),
		[AlternateSequence] [bigint] IDENTITY(1,1) NOT NULL,
		[OrganizationID] [uniqueidentifier] NOT NULL
			CONSTRAINT FK_OrganizationDBConnection_OrganizationID
			FOREIGN KEY (OrganizationID)
			REFERENCES OrganizationReference(OrganizationReferenceID),
		[ProviderName] [varchar](200) NULL,
		[ConnectionString] [varchar](1000) NOT NULL,
		[IsEnabled] [bit] NOT NULL,
		[sys_DateCreated] [datetime] NOT NULL
			CONSTRAINT [DF_OrganizationDBConnection_sys_DateCreated] DEFAULT getdate(),
		[sys_DateLastModified] [datetime] NOT NULL
			CONSTRAINT [DF_OrganizationDBConnection_sys_DateLastModified] DEFAULT getdate(),
		[sys_Version] [bigint] NOT NULL,
	CONSTRAINT
		[PK_OrganizationDBConnection] PRIMARY KEY NONCLUSTERED ([OrganizationDBConnectionGuid] ASC)
	WITH
		(PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]

	CREATE UNIQUE CLUSTERED INDEX IX_OrganizationDBConnection ON dbo.OrganizationDBConnection(AlternateSequence)
END
GO

GRANT INSERT ON [dbo].[OrganizationDBConnection] TO BallaratAppUserRole
GO
GRANT UPDATE ON [dbo].[OrganizationDBConnection] TO BallaratAppUserRole
GO
GRANT DELETE ON [dbo].[OrganizationDBConnection] TO BallaratAppUserRole
GO
GRANT SELECT ON [dbo].[OrganizationDBConnection] TO BallaratAppUserRole
GO


/****** Object:  Table [dbo].[Organization]    Script Date: 6/26/2014 9:39:48 AM ******/
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Organization]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
	CREATE TABLE [dbo].[Organization](
		[OrganizationID] [uniqueidentifier] ROWGUIDCOL  NOT NULL
			CONSTRAINT [DF_Organization_OrganizationID] DEFAULT newsequentialid(),
		[AlternateSequence] [bigint] IDENTITY(1,1) NOT NULL,
		[Code] [nvarchar](20) NOT NULL,
		[Name] [nvarchar](100) NOT NULL,
		[Description] [nvarchar](1000) NOT NULL,
		[DateFounded] [datetime] NULL,
		[DateClosed] [datetime] NULL,
		[sys_DateCreated] [datetime] NOT NULL
			CONSTRAINT [DF_Organization_sys_DateCreated] DEFAULT getDate(),
		[sys_DateLastModified] [datetime] NOT NULL
			CONSTRAINT [DF_Organization_sys_DateLastModified] DEFAULT getDate(),
		[sys_Version] [bigint] NOT NULL,
	CONSTRAINT
		[PK_Organization] PRIMARY KEY NONCLUSTERED ([OrganizationID] ASC)
	WITH
		(PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]

	CREATE UNIQUE CLUSTERED INDEX IX_Organization ON dbo.Organization(AlternateSequence)
END
GO

GRANT INSERT ON [dbo].[Organization] TO BallaratAppUserRole
GO
GRANT UPDATE ON [dbo].[Organization] TO BallaratAppUserRole
GO
GRANT DELETE ON [dbo].[Organization] TO BallaratAppUserRole
GO
GRANT SELECT ON [dbo].[Organization] TO BallaratAppUserRole
GO

/****** Object:  Table [dbo].[ConfigurationData]    Script Date: 6/26/2014 9:37:04 AM ******/
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[ConfigurationData]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
	CREATE TABLE [dbo].[ConfigurationData](
		[ConfigurationDataID] [uniqueidentifier] ROWGUIDCOL  NOT NULL
			CONSTRAINT [DF_ConfigurationData_ConfigurationDataID] DEFAULT newsequentialid(),
		[AlternateSequence] [bigint] IDENTITY(1,1) NOT NULL,
		[OrganizationID] [uniqueidentifier] NULL
			CONSTRAINT FK_ConfigurationData_OrganizationID
			FOREIGN KEY (OrganizationID)
			REFERENCES OrganizationReference(OrganizationReferenceID),
		[ConfigKey] [varchar](50) NOT NULL,
		[Value] [varchar](max) NOT NULL,
		[Created] [datetime] NOT NULL
			CONSTRAINT [DF_ConfigurationData_Created] DEFAULT getdate(),
		[LastModified] [datetime] NOT NULL
			CONSTRAINT [DF_ConfigurationData_LastModified] DEFAULT getdate(),
		[Version] [bigint] NOT NULL,
		CONSTRAINT
				[PK_ConfigurationDataID] PRIMARY KEY NONCLUSTERED ([ConfigurationDataID] ASC),
		CONSTRAINT
				[UNQ_ConfigurationData_ConfigKey] UNIQUE NONCLUSTERED (OrganizationID, [ConfigKey])
		WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
		
		CREATE UNIQUE CLUSTERED INDEX IX_ConfigurationData ON dbo.ConfigurationData(AlternateSequence)
END
GO

GRANT INSERT ON [dbo].[ConfigurationData] TO BallaratAppUserRole
GO
GRANT UPDATE ON [dbo].[ConfigurationData] TO BallaratAppUserRole
GO
GRANT DELETE ON [dbo].[ConfigurationData] TO BallaratAppUserRole
GO
GRANT SELECT ON [dbo].[ConfigurationData] TO BallaratAppUserRole
GO