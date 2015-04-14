
USE [master]
GO
SET NOCOUNT ON
GO

-- create database

IF NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'MagicurveDiagnosticDB')
BEGIN
	CREATE DATABASE [MagicurveDiagnosticDB] ON PRIMARY 
		( NAME = N'MagicurveDiagnosticDB', FILENAME = N'D:\Databases\SQLServer\2012\Magicurve\MagicurveDiagnosticDB.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
	LOG ON 
		( NAME = N'MagicurveDiagnosticDB_log', FILENAME = N'D:\Databases\SQLServer\2012\Magicurve\MagicurveDiagnosticDB.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
END

GO
