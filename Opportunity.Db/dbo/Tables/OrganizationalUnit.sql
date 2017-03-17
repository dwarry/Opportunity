CREATE TABLE [dbo].[OrganizationalUnit] (
    [Id]       INT           NOT NULL,
    [Name]     NVARCHAR (80) NOT NULL,
    [Colour]   NVARCHAR (20) NOT NULL,
    [ParentId] INT           NULL,
    [Icon]     NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OrganizationalUnit_Hierarchy] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[OrganizationalUnit] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_OrgUnit_Name]
    ON [dbo].[OrganizationalUnit]([Name] ASC);

