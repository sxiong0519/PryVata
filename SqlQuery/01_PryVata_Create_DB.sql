USE [master]
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
DROP TABLE IF EXISTS [Disposition];
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
DROP TABLE IF EXISTS [PatientIncident];



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
  [AssignedUserId] integer NOT NULL,
  [Title] nvarchar(100) NOT NULL,
  [Description] nvarchar(1000) NOT NULL,
  [DateReported] datetime NOT NULL,
  [DateOccurred] datetime NOT NULL,
  [FacilityId] integer NOT NULL,
  [Confirmed] bit,
  [Reportable] bit,
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
  [MethodId] integer NOT NULL,
  [RecipientId] integer NOT NULL,
  [CircumstanceId] integer NOT NULL,
  [DispositionId] integer NOT NULL,
  [InformationId] integer NOT NULL,
  [ControlsId] integer NOT NULL,
  [IncidentId] integer NOT NULL,
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

CREATE TABLE [Disposition] (
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

CREATE TABLE [UserType] (
  [Id] integer PRIMARY KEY IDENTITY,
  [Name] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [Notes] (
  [Id] integer PRIMARY KEY IDENTITY,
  [Description] nvarchar(255) NOT NULL,
  [ImageUrl] nvarchar(255),
  [IncidentId] integer NOT NULL
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
GO

CREATE TABLE [Patient] (
  [Id] integer PRIMARY KEY IDENTITY,
  [PatientNumber] integer NOT NULL,
  [FirstName] nvarchar(255) NOT NULL,
  [LastName] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [PatientIncident] (
  [Id] integer PRIMARY KEY IDENTITY,
  [IncidentId] integer NOT NULL,
  [PatientId] integer NOT NULL
)
GO

ALTER TABLE [Incident] ADD FOREIGN KEY ([AssignedUserId]) REFERENCES [User] ([Id])
GO

ALTER TABLE [Incident] ADD FOREIGN KEY ([FacilityId]) REFERENCES [Facility] ([Id])
GO

ALTER TABLE [DBRA] ADD FOREIGN KEY ([IncidentId]) REFERENCES [Incident] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

ALTER TABLE [DBRA] ADD FOREIGN KEY ([UserCompleteId]) REFERENCES [User] ([Id])
GO

ALTER TABLE [DBRA] ADD FOREIGN KEY ([MethodId]) REFERENCES [MethodType] ([Id])
GO

ALTER TABLE [DBRA] ADD FOREIGN KEY ([RecipientId]) REFERENCES [RecipientType] ([Id])
GO

ALTER TABLE [DBRA] ADD FOREIGN KEY ([CircumstanceId]) REFERENCES [Circumstance] ([Id])
GO

ALTER TABLE [DBRA] ADD FOREIGN KEY ([DispositionId]) REFERENCES [Disposition] ([Id])
GO

ALTER TABLE [DBRA] ADD FOREIGN KEY ([ControlsId]) REFERENCES [Controls] ([Id])
GO

ALTER TABLE [User] ADD FOREIGN KEY ([UserTypeId]) REFERENCES [UserType] ([Id])
GO

ALTER TABLE [User] ADD FOREIGN KEY ([FacilityId]) REFERENCES [Facility] ([Id])
GO

ALTER TABLE [DBRA] ADD FOREIGN KEY ([InformationId]) REFERENCES [Information] ([Id]) 
GO

ALTER TABLE [PatientIncident] ADD FOREIGN KEY ([IncidentId]) REFERENCES [Incident] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

ALTER TABLE [PatientIncident] ADD FOREIGN KEY ([PatientId]) REFERENCES [Patient] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

ALTER TABLE [Notes] ADD FOREIGN KEY ([IncidentId]) REFERENCES [Incident] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO