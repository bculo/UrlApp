IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [ShortUrls] (
    [Id] bigint NOT NULL IDENTITY,
    [PageUrl] nvarchar(max) NOT NULL,
    [UniqueShortUrl] nvarchar(15) NOT NULL,
    CONSTRAINT [PK_ShortUrls] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220524074902_Initial-migration', N'6.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ShortUrls]') AND [c].[name] = N'UniqueShortUrl');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [ShortUrls] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [ShortUrls] DROP COLUMN [UniqueShortUrl];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ShortUrls]') AND [c].[name] = N'PageUrl');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [ShortUrls] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [ShortUrls] ALTER COLUMN [PageUrl] nvarchar(2500) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220524082653_ShortUrlEntityModified', N'6.0.5');
GO

COMMIT;
GO

