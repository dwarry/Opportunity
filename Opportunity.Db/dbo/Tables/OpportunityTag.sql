CREATE TABLE [dbo].[OpportunityTag] (
    [OpportunityId] INT NOT NULL,
    [TagId]         INT NOT NULL,
    PRIMARY KEY CLUSTERED ([OpportunityId] ASC, [TagId] ASC),
    CONSTRAINT [fk_opportunitytag_opportunity] FOREIGN KEY ([OpportunityId]) REFERENCES [dbo].[Opportunity] ([Id]),
    CONSTRAINT [fk_opportunitytag_tag] FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tag] ([Id])
);

