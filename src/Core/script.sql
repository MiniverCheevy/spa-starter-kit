-- using the connections string in for  in 'C:\dev\spa-starter-kit\src\Core\..\React\appsettings.json', if this is incorrect edit the file DesignTimeDbContextFactory in Core
IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF SCHEMA_ID(N'scratch') IS NULL EXEC(N'CREATE SCHEMA [scratch];');

GO

CREATE TABLE [ApplicationSettings] (
    [Id] int NOT NULL IDENTITY,
    [Name] varchar(128) NULL,
    [Value] varchar(128) NULL,
    CONSTRAINT [PK_ApplicationSettings] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Exceptions] (
    [Id] bigint NOT NULL IDENTITY,
    [CreationDate] datetimeoffset NOT NULL,
    [Detail] nvarchar(max) NULL,
    [ErrorHash] int NULL,
    [FullJson] nvarchar(max) NULL,
    [Host] nvarchar(200) NULL,
    [HttpMethod] nvarchar(200) NULL,
    [IpAddress] nvarchar(200) NULL,
    [MachineName] nvarchar(200) NULL,
    [Message] nvarchar(200) NULL,
    [RequestId] uniqueidentifier NULL,
    [Source] nvarchar(200) NULL,
    [StatusCode] int NULL,
    [Type] nvarchar(200) NULL,
    [Url] nvarchar(200) NULL,
    [User] varchar(128) NULL,
    CONSTRAINT [PK_Exceptions] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [TestClasses] (
    [Id] int NOT NULL IDENTITY,
    [Name] varchar(128) NULL,
    CONSTRAINT [PK_TestClasses] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] varchar(128) NULL,
    [LastAuthentication] datetime2 NULL,
    [LastName] varchar(128) NULL,
    [LastUserAgent] varchar(128) NULL,
    [LockoutEnabled] bit NOT NULL,
    [PasswordHash] varchar(128) NULL,
    [Salt] varchar(128) NULL,
    [UserName] varchar(128) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [scratch].[Members] (
    [Id] int NOT NULL IDENTITY,
    [ManagerId] int NULL,
    [Name] varchar(128) NULL,
    [OptionalDate] datetime2 NULL,
    [OptionalDateTimeOffset] datetimeoffset NULL,
    [OptionalDecimal] decimal(18, 2) NULL,
    [OptionalInt] int NULL,
    [RequiredDate] datetime2 NOT NULL,
    [RequiredDateTimeOffset] datetimeoffset NOT NULL,
    [RequiredDecimal] decimal(18, 2) NOT NULL,
    [RequiredInt] int NOT NULL,
    [Title] varchar(128) NULL,
    CONSTRAINT [PK_Members] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Members_Members_ManagerId] FOREIGN KEY ([ManagerId]) REFERENCES [scratch].[Members] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Roles] (
    [Id] int NOT NULL IDENTITY,
    [Name] varchar(128) NULL,
    [UserId] int NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Roles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [scratch].[BlobOfText] (
    [Id] int NOT NULL IDENTITY,
    [MemberId] int NOT NULL,
    [Text] varchar(128) NULL,
    CONSTRAINT [PK_BlobOfText] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BlobOfText_Members_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [scratch].[Members] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [scratch].[Teams] (
    [Id] int NOT NULL IDENTITY,
    [MemberId] int NULL,
    [Name] varchar(128) NULL,
    CONSTRAINT [PK_Teams] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Teams_Members_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [scratch].[Members] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [scratch].[Projects] (
    [Id] int NOT NULL IDENTITY,
    [Name] varchar(128) NULL,
    [TeamId] int NOT NULL,
    CONSTRAINT [PK_Projects] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Projects_Teams_TeamId] FOREIGN KEY ([TeamId]) REFERENCES [scratch].[Teams] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Roles_UserId] ON [Roles] ([UserId]);

GO

CREATE INDEX [IX_BlobOfText_MemberId] ON [scratch].[BlobOfText] ([MemberId]);

GO

CREATE INDEX [IX_Members_ManagerId] ON [scratch].[Members] ([ManagerId]);

GO

CREATE INDEX [IX_Projects_TeamId] ON [scratch].[Projects] ([TeamId]);

GO

CREATE INDEX [IX_Teams_MemberId] ON [scratch].[Teams] ([MemberId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20180313001300_Initial', N'2.0.1-rtm-125');

GO


