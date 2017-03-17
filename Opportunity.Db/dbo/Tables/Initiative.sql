CREATE TABLE [dbo].[Initiative] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NCHAR (10)     NOT NULL,
    [Description] NVARCHAR (200) NULL,
    [Link]        NVARCHAR (100) NULL,
    [Icon]        NVARCHAR (80)  NULL,
    [StartDate]   DATE           NOT NULL,
    [EndDate]     DATE           NOT NULL,
    [UpdatedAt]   DATETIME2 (7)  NOT NULL,
    [UpdatedBy]   NVARCHAR (32)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_Initiative_EndDate_After_StartDate] CHECK ([EndDate]>[StartDate])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_Initiative_Name]
    ON [dbo].[Initiative]([Name] ASC);

