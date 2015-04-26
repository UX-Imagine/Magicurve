USE [MasterDB]
GO

SET NOCOUNT ON
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Country]    Script Date: 6/26/2014 9:38:21 AM ******/
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Country]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
  CREATE TABLE [dbo].[Country](
    [CountryID] [uniqueidentifier] ROWGUIDCOL  NOT NULL
		CONSTRAINT [DF_Country_CountryID] DEFAULT newsequentialid(),
    [AlternateSequence] [bigint] IDENTITY(1,1) NOT NULL,
    [Name] [nvarchar](100) NOT NULL,
    [sys_DateCreated] [datetime] NOT NULL
		CONSTRAINT [DF_Country_sys_DateCreated] DEFAULT getdate(),
    [sys_DateLastModified] [datetime] NOT NULL
		CONSTRAINT [DF_Country_sys_DateLastModified] DEFAULT getdate(),
    [sys_Version] [bigint] NOT NULL,
	CONSTRAINT
		[PK_Country] PRIMARY KEY NONCLUSTERED ([CountryID] ASC)
	WITH
		(PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]
	
	CREATE UNIQUE CLUSTERED INDEX IX_Country ON dbo.Country(AlternateSequence)
END
GO

GRANT INSERT ON [dbo].[Country] TO BallaratAppUserRole
GO
GRANT UPDATE ON [dbo].[Country] TO BallaratAppUserRole
GO
GRANT DELETE ON [dbo].[Country] TO BallaratAppUserRole
GO
GRANT SELECT ON [dbo].[Country] TO BallaratAppUserRole
GO


/****** Object:  Table [dbo].[Language]    Script Date: 6/26/2014 9:39:25 AM ******/

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Language]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
  CREATE TABLE [dbo].[Language](
    [LanguageID] [uniqueidentifier] ROWGUIDCOL  NOT NULL
		CONSTRAINT [DF_Language_LanguageID] DEFAULT newsequentialid(),
    [AlternateSequence] [bigint] IDENTITY(1,1) NOT NULL,
    [Name] [nvarchar](100) NOT NULL,
    [Code] [nvarchar](10) NOT NULL,
    [sys_DateCreated] [datetime] NOT NULL
		CONSTRAINT [DF_Language_sys_DateCreated] DEFAULT getdate(),
    [sys_DateLastModified] [datetime] NOT NULL
		CONSTRAINT [DF_Language_sys_DateLastModified] DEFAULT getdate(),
    [sys_Version] [bigint] NOT NULL,
	CONSTRAINT
		[PK_Language] PRIMARY KEY NONCLUSTERED ([LanguageID] ASC)
	WITH
		(PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]

	CREATE UNIQUE CLUSTERED INDEX IX_Language ON dbo.Language(AlternateSequence)
END

GO

GRANT INSERT ON [dbo].[Language] TO BallaratAppUserRole
GO
GRANT UPDATE ON [dbo].[Language] TO BallaratAppUserRole
GO
GRANT DELETE ON [dbo].[Language] TO BallaratAppUserRole
GO
GRANT SELECT ON [dbo].[Language] TO BallaratAppUserRole
GO

/****** Object:  Table [dbo].[Theme]    Script Date: 6/26/2014 9:42:32 AM ******/

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Theme]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
  CREATE TABLE [dbo].[Theme](
    [ThemeID] [uniqueidentifier] ROWGUIDCOL  NOT NULL
		CONSTRAINT [DF_Theme_ThemeID] DEFAULT newsequentialid(),
    [AlternateSequence] [bigint] IDENTITY(1,1) NOT NULL,
    [Name] [nvarchar](100) NOT NULL,
    [sys_DateCreated] [datetime] NOT NULL
		CONSTRAINT [DF_Theme_sys_DateCreated] DEFAULT getdate(),
    [sys_DateLastModified] [datetime] NOT NULL
		CONSTRAINT [DF_Theme_sys_DateLastModified] DEFAULT getdate(),
    [sys_Version] [bigint] NOT NULL,   
	CONSTRAINT
		[PK_Theme] PRIMARY KEY NONCLUSTERED ([ThemeID] ASC)
	WITH
		(PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]

	CREATE UNIQUE CLUSTERED INDEX IX_Theme ON dbo.Theme(AlternateSequence)
END
GO

GRANT INSERT ON [dbo].[Theme] TO BallaratAppUserRole
GO
GRANT UPDATE ON [dbo].[Theme] TO BallaratAppUserRole
GO
GRANT DELETE ON [dbo].[Theme] TO BallaratAppUserRole
GO
GRANT SELECT ON [dbo].[Theme] TO BallaratAppUserRole
GO


/****** Object:  Table [dbo].[Person]    Script Date: 6/26/2014 9:41:41 AM ******/
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Person]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
  CREATE TABLE [dbo].[Person](
    [PersonID] [uniqueidentifier] ROWGUIDCOL  NOT NULL
		CONSTRAINT [DF_Person_PersonID] DEFAULT newsequentialid(),
    [AlternateSequence] [bigint] IDENTITY(1,1) NOT NULL,
    [GenderID] [bigint] NULL,
    [AddressBusinessCountryID] [uniqueidentifier] NULL
		CONSTRAINT FK_Person_Country_AddressBusinessCountryID 
		FOREIGN KEY (AddressBusinessCountryID)
		REFERENCES Country(CountryID),
    [AddressHomeCountryID] [uniqueidentifier] NULL
		CONSTRAINT FK_Person_Country_AddressHomeCountryID 
		FOREIGN KEY (AddressHomeCountryID)
		REFERENCES Country(CountryID),
    [FirstName] [nvarchar](50) NOT NULL,
    [LastName] [nvarchar](50) NOT NULL,
    [Title] [nvarchar](10) NOT NULL,
    [Nickname] [nvarchar](20) NULL,
    [EmailAddressBusiness] [nvarchar](200) NOT NULL,
    [EmailAddressPersonal1] [nvarchar](200) NULL,
    [EmailAddressPersonal2] [nvarchar](200) NULL,
    [DateOfBirth] [datetime] NULL,
    [AddressBusinessRoad] [nvarchar](100) NULL,
    [AddressBusinessCity] [nvarchar](25) NULL,
    [AddressBusinessState] [nvarchar](40) NULL,
    [AddressBusinessZipCode] [nvarchar](10) NULL,
    [AddressBusinessTimeZone] [nvarchar](50) NULL,
    [AddressBusinessLocationLongitude] [nvarchar](20) NULL,
    [AddressBusinessLocationLatitude] [nvarchar](20) NULL,
    [AddressHomeRoad] [nvarchar](100) NULL,
    [AddressHomeCity] [nvarchar](25) NULL,
    [AddressHomeState] [nvarchar](40) NULL,
    [AddressHomeZipCode] [nvarchar](10) NULL,
    [AddressHomeTimeZone] [nvarchar](50) NULL,
    [AddressHomeLocationLongitude] [nvarchar](20) NULL,
    [AddressHomeLocationLatitude] [nvarchar](20) NULL,
    [HomePageURL] [nvarchar](1500) NULL,
    [BlogURL] [nvarchar](1500) NULL,
    [IMYahooID] [nvarchar](200) NULL,
    [IMLiveID] [nvarchar](200) NULL,
    [IMGoogleTalkID] [nvarchar](200) NULL,
    [IMSkypeID] [nvarchar](200) NULL,
    [IMAIMID] [nvarchar](200) NULL,
    [IMICQID] [nvarchar](200) NULL,
    [IMLCSID] [nvarchar](200) NULL,
    [IMQQID] [nvarchar](200) NULL,
    [IMIRCID] [nvarchar](200) NULL,
    [IMLotusSametimeID] [nvarchar](200) NULL,
    [IMCustomJabberID] [nvarchar](200) NULL,
    [PhoneBusinessFixed1] [nvarchar](20) NULL,
    [PhoneBusinessFixed2] [nvarchar](20) NULL,
    [PhoneBusinessMobile] [nvarchar](20) NULL,
    [PhoneBusinessFax] [nvarchar](20) NULL,
    [PhoneBusinessPager] [nvarchar](20) NULL,
    [PhoneHomeFixed1] [nvarchar](20) NULL,
    [PhoneHomeFixed2] [nvarchar](20) NULL,
    [PhoneHomeMobile] [nvarchar](20) NULL,
    [PhoneHomeFax] [nvarchar](20) NULL,
    [PhoneHomePager] [nvarchar](20) NULL,
    [Description] [nvarchar](1000) NULL,
    [Qualifications] [nvarchar](1000) NULL,
    [sys_DateCreated] [datetime] NOT NULL
		CONSTRAINT [DF_Person_sys_DateCreated] DEFAULT getdate(),
    [sys_DateLastModified] [datetime] NOT NULL
		CONSTRAINT [DF_Person_sys_DateLastModified] DEFAULT getdate(),
    [sys_Version] [bigint] NOT NULL,
   CONSTRAINT
		[PK_Person] PRIMARY KEY NONCLUSTERED ([PersonID] ASC)
	WITH
		(PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]

	CREATE UNIQUE CLUSTERED INDEX IX_Person ON dbo.Person(AlternateSequence)
END
GO

GRANT INSERT ON [dbo].[Person] TO BallaratAppUserRole
GO
GRANT UPDATE ON [dbo].[Person] TO BallaratAppUserRole
GO
GRANT DELETE ON [dbo].[Person] TO BallaratAppUserRole
GO
GRANT SELECT ON [dbo].[Person] TO BallaratAppUserRole
GO

/****** Object:  Table [dbo].[SecurityUser]    Script Date: 6/26/2014 9:42:06 AM ******/

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[SecurityUser]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
  CREATE TABLE [dbo].[SecurityUser](
    [SecurityUserID] [uniqueidentifier] ROWGUIDCOL  NOT NULL
		CONSTRAINT [DF_SecurityUser_SecurityUserID] DEFAULT newsequentialid(),
    [AlternateSequence] [bigint] IDENTITY(1,1) NOT NULL,
    [UserName] [nvarchar](100) NOT NULL
		CONSTRAINT UNQ_SecurityUser_UserName UNIQUE,
    [Secret] [nvarchar](50) NOT NULL,
    [Salt] [nvarchar](50) NOT NULL,
    [HasNeverSignedIn] [bit] NOT NULL,
    [HasAcceptedLicense] [bit] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [TemporaryPassword] [nvarchar](50) NULL,
    [TemporaryPasswordExpired] [datetime] NULL,
    [Iteration] [int] NULL,
    [sys_DateCreated] [datetime] NOT NULL
		CONSTRAINT [DF_SecurityUser_sys_DateCreated] DEFAULT getdate(),
    [sys_DateLastModified] [datetime] NOT NULL
		CONSTRAINT [DF_SecurityUser_sys_DateLastModified] DEFAULT getdate(),
    [sys_Version] [bigint] NOT NULL,
    [RememberMeToken] [nvarchar](50) NULL,
  	CONSTRAINT
		[PK_SecurityUser] PRIMARY KEY NONCLUSTERED ([SecurityUserID] ASC)
	WITH
		(PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]

	CREATE UNIQUE CLUSTERED INDEX IX_SecurityUser ON dbo.SecurityUser(AlternateSequence)
END
GO

GRANT INSERT ON [dbo].[SecurityUser] TO BallaratAppUserRole
GO
GRANT UPDATE ON [dbo].[SecurityUser] TO BallaratAppUserRole
GO
GRANT DELETE ON [dbo].[SecurityUser] TO BallaratAppUserRole
GO
GRANT SELECT ON [dbo].[SecurityUser] TO BallaratAppUserRole
GO


/****** Object:  Table [dbo].[ApplicationUser]    Script Date: 6/26/2014 9:36:32 AM ******/

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[ApplicationUser]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
  CREATE TABLE [dbo].[ApplicationUser](
    [ApplicationUserID] [uniqueidentifier] ROWGUIDCOL  NOT NULL
		CONSTRAINT [DF_ApplicationUser_ApplicationUserID] DEFAULT newsequentialid(),
    [AlternateSequence] [bigint] IDENTITY(1,1) NOT NULL,
    [PersonID] [uniqueidentifier] NOT NULL
		CONSTRAINT UNQ_ApplicationUser_PersonID UNIQUE,
		CONSTRAINT FK_ApplicationUser_Person_PersonID FOREIGN KEY (PersonID)
		REFERENCES Person(PersonID),
    [SecurityUserID] [uniqueidentifier] NOT NULL
		CONSTRAINT UNQ_ApplicationUser_SecurityUserID UNIQUE,
		CONSTRAINT FK_ApplicationUser_SecurityUser_SecurityUserID 
		FOREIGN KEY (SecurityUserID)
		REFERENCES SecurityUser(SecurityUserID),
    [LanguageID] [uniqueidentifier] NULL
		CONSTRAINT FK_ApplicationUser_Language_LanguageID 
		FOREIGN KEY (LanguageID)
		REFERENCES [Language](LanguageID),
	[ThemeID] [uniqueidentifier]
		CONSTRAINT FK_ApplicationUser_Theme_ThemeID 
		FOREIGN KEY (ThemeID)
		REFERENCES Theme(ThemeID),
	[sys_DateCreated] [datetime] NOT NULL
		CONSTRAINT [DF_ApplicationUser_sys_DateCreated] DEFAULT getdate(),
	[sys_DateLastModified] [datetime] NOT NULL
		CONSTRAINT [DF_ApplicationUser_sys_DateLastModified] DEFAULT getdate(),
	[sys_Version] [bigint] NOT NULL,
  CONSTRAINT
		[PK_ApplicationUser] PRIMARY KEY NONCLUSTERED ([ApplicationUserID] ASC)
	WITH
		(PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]

  CREATE UNIQUE CLUSTERED INDEX IX_ApplicationUser ON dbo.ApplicationUser(AlternateSequence)

END

GO

GRANT INSERT ON [dbo].[ApplicationUser] TO BallaratAppUserRole
GO
GRANT UPDATE ON [dbo].[ApplicationUser] TO BallaratAppUserRole
GO
GRANT DELETE ON [dbo].[ApplicationUser] TO BallaratAppUserRole
GO
GRANT SELECT ON [dbo].[ApplicationUser] TO BallaratAppUserRole
GO
