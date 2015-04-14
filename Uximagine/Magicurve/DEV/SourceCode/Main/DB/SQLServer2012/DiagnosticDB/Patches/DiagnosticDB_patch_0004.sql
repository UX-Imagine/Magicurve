USE [MagicurveDiagnosticDB]
GO
SET NOCOUNT ON
GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Log]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
	CREATE TABLE [dbo].[Log](
		[LogID] [uniqueidentifier] ROWGUIDCOL NOT NULL
			CONSTRAINT [DF_Log_LogID] DEFAULT newsequentialid(),
		[AlternateSequence] [bigint] IDENTITY(1,1) NOT NULL,
		[LogTimestamp] [datetime] NOT NULL,
		[Thread] [varchar] (255) NOT NULL,
		[Level] [varchar] (20) NOT NULL,
		[Logger] [varchar] (255) NOT NULL,
		[Message] [nvarchar] (4000) NOT NULL,
		[Exception] [nvarchar] (4000) NOT NULL,
		[SourceApplication] [varchar] (50) NOT NULL,
		[SourceApplicationVersion] [varchar] (50) NOT NULL,
		[LoggingApplication] [varchar] (50) NOT NULL,
		[LoggingApplicationVersion] [varchar] (50) NOT NULL,
		[ClientIPAddress] [varchar] (50) NOT NULL,
		[ServerIPAddress] [varchar] (50) NOT NULL,
		[sys_DateCreated] [datetime] NOT NULL
			CONSTRAINT [DF_Log_sys_DateCreated] DEFAULT getdate(),
		[sys_DateLastModified] [datetime] NOT NULL
			CONSTRAINT [DF_Log_sys_DateLastModified] DEFAULT getdate(),
		[sys_Version] [bigint] NOT NULL DEFAULT 1,
	 CONSTRAINT
		[PK_LogID] PRIMARY KEY NONCLUSTERED ([LogID] ASC)
	 WITH
		(PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]

	CREATE UNIQUE CLUSTERED INDEX [IX_Log] ON [dbo].[Log](AlternateSequence)
END

GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[TRG_Log_IncrementVersion]') AND OBJECTPROPERTY(id, N'IsTrigger') = 1)
BEGIN
	DROP TRIGGER [TRG_Log_IncrementVersion]
END

GO 

CREATE TRIGGER [TRG_Log_IncrementVersion] ON [dbo].[Log] AFTER UPDATE
AS 
	UPDATE
		Log
	SET
		sys_Version = sys_Version + 1
	WHERE
		LogID IN (
			SELECT
				LogID
			FROM
				inserted
		)
GO

GRANT INSERT ON [dbo].[Log] TO UxAppUserRole
GO
GRANT SELECT ON [dbo].[Log] TO UxAppUserRole
GO

