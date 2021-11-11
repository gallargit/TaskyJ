
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/16/2018 15:55:32
-- Generated from EDMX file: C:\Users\IBM_ADMIN\Documents\Visual Studio 2015\Projects\TaskyJ\TaskyJ.43.DataRepo.SqlSrv.Windows\TaskyJ.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [TaskyJ];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[TaskyJTable]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TaskyJTable];
GO
IF OBJECT_ID(N'[dbo].[CategoryJTable]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CategoryJTable];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'TaskyJTable'
CREATE TABLE [dbo].[TaskyJTable] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [Description] varchar(1000)  NOT NULL,
    [CreationDate] datetime  NOT NULL,
    [Deadline] datetime  NULL,
    [FinishDate] datetime  NULL,
    [Priority] tinyint  NOT NULL,
    [Completed] tinyint  NOT NULL,
    [Deleted] bit  NOT NULL,
    [IDCategory] int  NOT NULL
);
GO

-- Creating table 'CategoryJTable'
CREATE TABLE [dbo].[CategoryJTable] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [IconBase64] varchar(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'TaskyJTable'
ALTER TABLE [dbo].[TaskyJTable]
ADD CONSTRAINT [PK_TaskyJTable]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'CategoryJTable'
ALTER TABLE [dbo].[CategoryJTable]
ADD CONSTRAINT [PK_CategoryJTable]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IDCategory] in table 'TaskyJTable'
ALTER TABLE [dbo].[TaskyJTable]
ADD CONSTRAINT [FK_CategoryJTableTaskyJTable]
    FOREIGN KEY ([IDCategory])
    REFERENCES [dbo].[CategoryJTable]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryJTableTaskyJTable'
CREATE INDEX [IX_FK_CategoryJTableTaskyJTable]
ON [dbo].[TaskyJTable]
    ([IDCategory]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------