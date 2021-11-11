CREATE TABLE [dbo].[TaskyJTable] (
    [ID]          INT            NOT NULL,
    [Name]        VARCHAR (50)   NOT NULL,
    [Description] varchar(1000)  NOT NULL,
    [CreationDate] datetime  NOT NULL,
    [FinishDate] datetime  NULL,
    [Completed] tinyint  NOT NULL,
    [Deleted] bit  NOT NULL
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

