
USE [master]
GO
SET NOCOUNT ON
GO

-- create database

IF NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'MasterDB')
BEGIN
	CREATE DATABASE [MasterDB] ON PRIMARY 
		( NAME = N'MasterDB', FILENAME = N'C:\Databases\SQLServer\2012\Ballarat\MasterDB.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
	LOG ON 
		( NAME = N'MasterDB_log', FILENAME = N'C:\Databases\SQLServer\2012\Ballarat\MasterDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
END

GO
