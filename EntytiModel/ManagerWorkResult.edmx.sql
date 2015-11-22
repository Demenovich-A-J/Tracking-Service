
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/22/2015 18:04:30
-- Generated from EDMX file: D:\SourceTreeProjects\Tracking-Service\EntytiModel\ManagerWorkResult.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ManagerWorkResult];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ManagerSet'
CREATE TABLE [dbo].[ManagerSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'WorkResultsSet'
CREATE TABLE [dbo].[WorkResultsSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Customer] nvarchar(max)  NOT NULL,
    [Product] nvarchar(max)  NOT NULL,
    [Summ] int  NOT NULL,
    [ManagerId] nvarchar(max)  NOT NULL,
    [Manager_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ManagerSet'
ALTER TABLE [dbo].[ManagerSet]
ADD CONSTRAINT [PK_ManagerSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WorkResultsSet'
ALTER TABLE [dbo].[WorkResultsSet]
ADD CONSTRAINT [PK_WorkResultsSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Manager_Id] in table 'WorkResultsSet'
ALTER TABLE [dbo].[WorkResultsSet]
ADD CONSTRAINT [FK_ManagerWorkResults]
    FOREIGN KEY ([Manager_Id])
    REFERENCES [dbo].[ManagerSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ManagerWorkResults'
CREATE INDEX [IX_FK_ManagerWorkResults]
ON [dbo].[WorkResultsSet]
    ([Manager_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------