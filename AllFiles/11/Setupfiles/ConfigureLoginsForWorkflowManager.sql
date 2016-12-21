USE [master]
GO

EXEC master..sp_addsrvrolemember @loginame = N'CONTOSO\Administrator', @rolename=N'securityadmin'
GO

EXEC master..sp_addsrvrolemember @loginame = N'CONTOSO\Administrator', @rolename=N'dbcreator'
GO
