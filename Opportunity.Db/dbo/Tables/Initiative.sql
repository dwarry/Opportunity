CREATE TABLE [dbo].[Initiative] (
    [Id]                   INT            IDENTITY (1, 1) NOT NULL,
    [Name]                 NVARCHAR (50)  NOT NULL,
    [Description]          NVARCHAR (200) NULL,
    [Link]                 NVARCHAR (100) NULL,
    [LogoUrl]              NVARCHAR (80)  NULL,
    [StartDate]            DATE           NOT NULL,
    [EndDate]              DATE           NOT NULL,
    [UpdatedAt]            DATETIME2 (7)  NOT NULL,
    [UpdatedBy]            NVARCHAR (32)  NOT NULL,
    [OrganizationalUnitId] INT            NOT NULL,
    [Version]              ROWVERSION     NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_Initiative_EndDate_After_StartDate] CHECK ([EndDate]>[StartDate]),
    CONSTRAINT [fk_initiative_orgunit] FOREIGN KEY ([OrganizationalUnitId]) REFERENCES [dbo].[OrganizationalUnit] ([Id])
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_Initiative_Name]
    ON [dbo].[Initiative]([Name] ASC);

