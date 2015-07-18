USE [MasterDB]
GO

SET NOCOUNT ON
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/****** Object:  Table [dbo].[Feedback]    Script Date: 6/26/2014 9:39:00 AM ******/
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Feedback]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
	CREATE TABLE [dbo].[Feedback](
		[FeedbackID] [uniqueidentifier] NOT NULL
			CONSTRAINT [DF_Feedback_FeedbackID] DEFAULT newsequentialid(),
		[ApplicationUserID] [uniqueidentifier] NOT NULL			
			CONSTRAINT FK_Feedback_ApplicationUser_ApplicationUserID FOREIGN KEY (ApplicationUserID)
			REFERENCES ApplicationUser(ApplicationUserID),
		[Subject] [nvarchar](500) NOT NULL,
		[Comment] [nvarchar](max) NOT NULL,
		[sys_DateCreated] [datetime] NOT NULL
			CONSTRAINT [DF_Feedback_sys_DateCreated] DEFAULT getdate(),
		[sys_DateLastModified] [datetime] NOT NULL
			CONSTRAINT [DF_Feedback_sys_DateLastModified] DEFAULT getdate(),
		[sys_Version] [bigint] NOT NULL,
	 CONSTRAINT [PK_Feedback] PRIMARY KEY CLUSTERED 
	(
		[FeedbackID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
GRANT INSERT ON [dbo].[Feedback] TO BallaratAppUserRole
GO
GRANT UPDATE ON [dbo].[Feedback] TO BallaratAppUserRole
GO
GRANT DELETE ON [dbo].[Feedback] TO BallaratAppUserRole
GO
GRANT SELECT ON [dbo].[Feedback] TO BallaratAppUserRole
GO
