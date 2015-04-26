USE [MasterDB]
GO

/****** Object:  View [dbo].[UserDetails]    Script Date: 6/30/2014 11:25:30 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[UserDetails]
AS
SELECT        dbo.SecurityUser.SecurityUserID, dbo.SecurityUser.UserName, dbo.SecurityUser.Secret, dbo.SecurityUser.Salt, dbo.SecurityUser.HasNeverSignedIn, 
                         dbo.SecurityUser.HasAcceptedLicense, dbo.SecurityUser.IsDeleted, dbo.SecurityUser.TemporaryPassword, dbo.SecurityUser.TemporaryPasswordExpired, 
                         dbo.SecurityUser.Iteration, dbo.SecurityUser.sys_DateCreated, dbo.SecurityUser.sys_DateLastModified, dbo.SecurityUser.sys_Version, 
                         dbo.SecurityUser.RememberMeToken, dbo.ApplicationUser.ApplicationUserID, dbo.ApplicationUser.LanguageID, dbo.ApplicationUser.ThemeID, 
                         dbo.ApplicationUser.sys_DateCreated AS ApplicationUserCreated, dbo.ApplicationUser.sys_DateLastModified AS LastModified, 
                         dbo.ApplicationUser.sys_Version AS ApplicationUserVersion, dbo.Person.PersonID, dbo.Person.GenderID, dbo.Person.AddressBusinessCountryID, 
                         dbo.Person.AddressHomeCountryID, dbo.Person.FirstName, dbo.Person.LastName, dbo.Person.Title, dbo.Person.Nickname, 
                         dbo.Person.EmailAddressBusiness, dbo.Person.EmailAddressPersonal1, dbo.Person.EmailAddressPersonal2, dbo.Person.DateOfBirth, 
                         dbo.Person.AddressBusinessRoad, dbo.Person.AddressBusinessCity, dbo.Person.AddressBusinessState, dbo.Person.AddressBusinessZipCode, 
                         dbo.Person.AddressBusinessTimeZone, dbo.Person.AddressBusinessLocationLongitude, dbo.Person.AddressBusinessLocationLatitude, 
                         dbo.Person.AddressHomeRoad, dbo.Person.AddressHomeCity, dbo.Person.AddressHomeState, dbo.Person.AddressHomeZipCode, 
                         dbo.Person.AddressHomeTimeZone, dbo.Person.AddressHomeLocationLongitude, dbo.Person.AddressHomeLocationLatitude, dbo.Person.HomePageURL, 
                         dbo.Person.BlogURL, dbo.Person.IMYahooID, dbo.Person.IMLiveID, dbo.Person.IMGoogleTalkID, dbo.Person.IMSkypeID, dbo.Person.IMAIMID, 
                         dbo.Person.IMICQID, dbo.Person.IMLCSID, dbo.Person.IMQQID, dbo.Person.IMIRCID, dbo.Person.IMLotusSametimeID, dbo.Person.IMCustomJabberID, 
                         dbo.Person.PhoneBusinessFixed1, dbo.Person.PhoneBusinessFixed2, dbo.Person.PhoneBusinessMobile, dbo.Person.PhoneBusinessFax, 
                         dbo.Person.PhoneBusinessPager, dbo.Person.PhoneHomeFixed1, dbo.Person.PhoneHomeFixed2, dbo.Person.PhoneHomeMobile, 
                         dbo.Person.PhoneHomeFax, dbo.Person.PhoneHomePager, dbo.Person.Description, dbo.Person.Qualifications, 
                         dbo.Person.sys_DateCreated AS PersonCreated, dbo.Person.sys_DateLastModified AS PersonModified, dbo.Person.sys_Version AS PersonVersion, dbo.SecurityUser.Provider
FROM            dbo.ApplicationUser INNER JOIN
                         dbo.Person ON dbo.ApplicationUser.PersonID = dbo.Person.PersonID INNER JOIN
                         dbo.SecurityUser ON dbo.ApplicationUser.SecurityUserID = dbo.SecurityUser.SecurityUserID

GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[5] 4[37] 2[18] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = -192
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ApplicationUser"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 241
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "Person"
            Begin Extent = 
               Top = 0
               Left = 305
               Bottom = 130
               Right = 581
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "SecurityUser"
            Begin Extent = 
               Top = 143
               Left = 435
               Bottom = 273
               Right = 670
            End
            DisplayFlags = 280
            TopColumn = 11
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 80
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UserDetails'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UserDetails'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UserDetails'
GO


