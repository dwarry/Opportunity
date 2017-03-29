CREATE TABLE [dbo].[Opportunity] (
    [Id]                   INT             IDENTITY (1, 1) NOT NULL,
    [OwnerId]              INT             NOT NULL,
    [OrganizationalUnitId] INT             NOT NULL,
    [InitiativeId]         INT             NULL,
    [Title]                NVARCHAR (50)   NOT NULL,
    [Description]          NVARCHAR (1024) NOT NULL,
    [EstimatedWorkload]    NVARCHAR (50)   NOT NULL,
    [Outcomes]             NVARCHAR (200)  NOT NULL,
    [StartDate]            DATE            NOT NULL,
    [EndDate]              DATE            NOT NULL,
    [VacancyCount]         INT             NOT NULL,
    [FurtherDetailsUrl]    NVARCHAR (100)  NULL,
    [CategoryId]           INT             NOT NULL,
    [UpdatedAt]            DATETIME2 (7)   NOT NULL,
    [UpdatedBy]            NVARCHAR (32)   NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_Opportunity_EndDate_After_StartDate] CHECK ([EndDate]>[StartDate]),
    CONSTRAINT [CK_Opportunity_VacancyCount_Positive] CHECK ([VacancyCount]>(0)),
    CONSTRAINT [FK_Opportunity_Category] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([Id]),
    CONSTRAINT [FK_Opportunity_Initiative] FOREIGN KEY ([InitiativeId]) REFERENCES [dbo].[Initiative] ([Id]),
    CONSTRAINT [FK_Opportunity_OrgUnit] FOREIGN KEY ([OrganizationalUnitId]) REFERENCES [dbo].[OrganizationalUnit] ([Id]),
    CONSTRAINT [FK_Opportunity_Owner] FOREIGN KEY ([OwnerId]) REFERENCES [dbo].[User] ([Id])
);



