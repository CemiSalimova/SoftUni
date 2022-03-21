IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Customers] (
    [CustomerId] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NULL,
    [Email] nvarchar(80) NULL,
    [CreditCardNumber] nvarchar(20) NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([CustomerId])
);

GO

CREATE TABLE [Products] (
    [ProductId] int NOT NULL IDENTITY,
    [Name] nvarchar(50) NULL,
    [Quantity] float NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([ProductId])
);

GO

CREATE TABLE [Stores] (
    [StoreId] int NOT NULL IDENTITY,
    [Name] nvarchar(80) NULL,
    CONSTRAINT [PK_Stores] PRIMARY KEY ([StoreId])
);

GO

CREATE TABLE [Sales] (
    [SaleId] int NOT NULL IDENTITY,
    [Date] datetime2 NOT NULL,
    [ProductId] int NOT NULL,
    [CustomerId] int NOT NULL,
    [StoreId] int NOT NULL,
    CONSTRAINT [PK_Sales] PRIMARY KEY ([SaleId]),
    CONSTRAINT [FK_Sales_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([CustomerId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Sales_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([ProductId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Sales_Stores_StoreId] FOREIGN KEY ([StoreId]) REFERENCES [Stores] ([StoreId]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Sales_CustomerId] ON [Sales] ([CustomerId]);

GO

CREATE INDEX [IX_Sales_ProductId] ON [Sales] ([ProductId]);

GO

CREATE INDEX [IX_Sales_StoreId] ON [Sales] ([StoreId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211109134407_InitialMigration', N'3.1.3');

GO

ALTER TABLE [Products] ADD [Description] nvarchar(250) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211109134455_ProductsAddColumnDescription', N'3.1.3');

GO

