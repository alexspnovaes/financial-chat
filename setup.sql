USE [master]
GO

PRINT -----------------------------------STARTING SCRIPT---------------------------------------
CREATE DATABASE [FinancialChatDB]
GO

--USE [master]
--GO

--/* For security reasons the login is created disabled and with a random password. */
--/****** Object:  Login [financialChatUser]    Script Date: 11/4/2021 2:52:58 PM ******/
--CREATE LOGIN [financialChatUser] WITH PASSWORD=N'3VRbsJ/dejmMGUW9KtV5e73u3dt2IcT/Md8XQsZVMMk=', DEFAULT_DATABASE=[FinancialChatDB], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
--GO

--ALTER LOGIN [financialChatUser] DISABLE
--GO

--ALTER SERVER ROLE [sysadmin] ADD MEMBER [financialChatUser]
--GO

--ALTER SERVER ROLE [securityadmin] ADD MEMBER [financialChatUser]
--GO

--ALTER SERVER ROLE [serveradmin] ADD MEMBER [financialChatUser]
--GO

--ALTER SERVER ROLE [setupadmin] ADD MEMBER [financialChatUser]
--GO

--ALTER SERVER ROLE [processadmin] ADD MEMBER [financialChatUser]
--GO

--ALTER SERVER ROLE [diskadmin] ADD MEMBER [financialChatUser]
--GO

--ALTER SERVER ROLE [dbcreator] ADD MEMBER [financialChatUser]
--GO



--/****** Object:  User [financialChatUser]    Script Date: 11/4/2021 2:52:31 PM ******/
--CREATE USER [financialChatUser] FOR LOGIN [financialChatUser] WITH DEFAULT_SCHEMA=[dbo]
--GO
--PRINT -----------------------------------ENDING SCRIPT---------------------------------------





