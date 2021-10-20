﻿USE [master]
GO

IF db_id('PryVata') IS NOT NULL
BEGIN
	ALTER DATABASE [PryVata] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE [PryVata] 
END 
GO

CREATE DATABASE [PryVata] 
GO

USE [PryVata] 

GO

DROP TABLE IF EXISTS [Circumstance];
DROP TABLE IF EXISTS [Controls];
DROP TABLE IF EXISTS [DBRA];
DROP TABLE IF EXISTS [DispositionOfInformation];
DROP TABLE IF EXISTS [Exception];
DROP TABLE IF EXISTS [Facility];
DROP TABLE IF EXISTS [Incident];
DROP TABLE IF EXISTS [IncidentType];
DROP TABLE IF EXISTS [Information];
DROP TABLE IF EXISTS [MethodType];
DROP TABLE IF EXISTS [Notes];
DROP TABLE IF EXISTS [RecipientType];
DROP TABLE IF EXISTS [User];
DROP TABLE IF EXISTS [UserType];



---------------------------------------------------------------------------------------------------------



CREATE TABLE [User] (
  [Id] integer PRIMARY KEY IDENTITY,
  [FullName] nvarchar(30) NOT NULL,
  [Email] nvarchar(50) NOT NULL,
  [FirebaseUserId] nvarchar(50) NOT NULL,
  [UserTypeId] integer NOT NULL,
  [FacilityId] integer,
  [isActive] bit
)
GO

CREATE TABLE [Incident] (
  [Id] integer PRIMARY KEY IDENTITY,
  [CreatorUserId] integer NOT NULL,
  [Title] nvarchar(100) NOT NULL,
  [Description] nvarchar(1000) NOT NULL,
  [DateReported] datetime NOT NULL,
  [DateOccurred] datetime NOT NULL,
  [NotesId] integer NOT NULL,
  [FacilityId] integer NOT NULL,
  [PatientId] integer NOT NULL,
  [Confirmed] bit NOT NULL,
  [Reportable] bit NOT NULL,
  [DBRAId] integer
)
GO

CREATE TABLE [Facility] (
  [Id] integer PRIMARY KEY IDENTITY,
  [FacilityName] nvarchar(100) NOT NULL,
  [Address] nvarchar(100) NOT NULL,
  [City] nvarchar(50) NOT NULL,
  [State] nvarchar(20) NOT NULL,
  [ZipCode] integer NOT NULL,
  [isDeleted] bit
)
GO

CREATE TABLE [DBRA] (
  [Id] integer PRIMARY KEY IDENTITY,
  [IncidentTypeId] integer NOT NULL,
  [UserCompleteId] integer NOT NULL,
  [MethodOfDisclosureId] integer NOT NULL,
  [TypeOfRecipientId] integer NOT NULL,
  [CircumstanceOfDisclosureId] integer NOT NULL,
  [DispositionOfDisclosureId] integer NOT NULL,
  [InformationId] integer NOT NULL,
  [AdditionalControlsId] integer NOT NULL
)
GO

CREATE TABLE [MethodType] (
  [Id] integer PRIMARY KEY IDENTITY,
  [Method] nvarchar(255) NOT NULL,
  [MethodValue] integer NOT NULL
)
GO

CREATE TABLE [RecipientType] (
  [Id] integer PRIMARY KEY IDENTITY,
  [Recipient] nvarchar(255) NOT NULL,
  [RecipientValue] integer NOT NULL
)
GO

CREATE TABLE [Circumstance] (
  [Id] integer PRIMARY KEY IDENTITY,
  [Circumstance] nvarchar(255) NOT NULL,
  [CircumstanceValue] integer NOT NULL
)
GO

CREATE TABLE [DispositionOfInformation] (
  [Id] integer PRIMARY KEY IDENTITY,
  [Disposition] nvarchar(255) NOT NULL,
  [DispositionValue] integer NOT NULL
)
GO

CREATE TABLE [Controls] (
  [Id] integer PRIMARY KEY IDENTITY,
  [Controls] nvarchar(255) NOT NULL,
  [ControlsValue] integer NOT NULL
)
GO

CREATE TABLE [IncidentType] (
  [Id] integer PRIMARY KEY IDENTITY,
  [Type] nvarchar(255) NOT NULL,
  [IncidentValue] integer NOT NULL
)
GO

CREATE TABLE [UserType] (
  [Id] integer PRIMARY KEY IDENTITY,
  [Name] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [Notes] (
  [Id] integer PRIMARY KEY IDENTITY,
  [Description] nvarchar(255) NOT NULL,
  [ImageUrl] nvarchar(255)
)
GO

CREATE TABLE [Information] (
  [Id] integer PRIMARY KEY IDENTITY,
  [InformationType] nvarchar(255) NOT NULL,
  [InformationValue] integer NOT NULL
)
GO

CREATE TABLE [Exception] (
  [Id] integer PRIMARY KEY IDENTITY,
  [Exception] nvarchar(255) NOT NULL
)

ALTER TABLE [Incident] ADD FOREIGN KEY ([CreatorUserId]) REFERENCES [User] ([Id])
GO

ALTER TABLE [Incident] ADD FOREIGN KEY ([FacilityId]) REFERENCES [Facility] ([Id])
GO

ALTER TABLE [DBRA] ADD FOREIGN KEY ([IncidentTypeId]) REFERENCES [IncidentType] ([Id])
GO

ALTER TABLE [Incident] ADD FOREIGN KEY ([DBRAId]) REFERENCES [DBRA] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

ALTER TABLE [DBRA] ADD FOREIGN KEY ([UserCompleteId]) REFERENCES [User] ([Id])
GO

ALTER TABLE [DBRA] ADD FOREIGN KEY ([MethodOfDisclosureId]) REFERENCES [MethodType] ([Id])
GO

ALTER TABLE [DBRA] ADD FOREIGN KEY ([TypeOfRecipientId]) REFERENCES [RecipientType] ([Id])
GO

ALTER TABLE [DBRA] ADD FOREIGN KEY ([CircumstanceOfDisclosureId]) REFERENCES [Circumstance] ([Id])
GO

ALTER TABLE [DBRA] ADD FOREIGN KEY ([DispositionOfDisclosureId]) REFERENCES [DispositionOfInformation] ([Id])
GO

ALTER TABLE [DBRA] ADD FOREIGN KEY ([AdditionalControlsId]) REFERENCES [Controls] ([Id])
GO

ALTER TABLE [User] ADD FOREIGN KEY ([UserTypeId]) REFERENCES [UserType] ([Id])
GO

ALTER TABLE [User] ADD FOREIGN KEY ([FacilityId]) REFERENCES [Facility] ([Id])
GO

ALTER TABLE [Incident] ADD FOREIGN KEY ([NotesId]) REFERENCES [Notes] ([Id])
GO

ALTER TABLE [DBRA] ADD FOREIGN KEY ([InformationId]) REFERENCES [Information] ([Id])
GO