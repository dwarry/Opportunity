CREATE TABLE [dbo].[Recommendation] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [OpportunityId]    INT            NOT NULL,
    [RecommendeeId]    INT            NOT NULL,
    [RecommendedBy]    INT            NOT NULL,
    [CreatedTimestamp] DATETIME       NOT NULL,
    [Message]          NVARCHAR (400) NULL,
    [UpdatedAt]        DATETIME2 (7)  NOT NULL,
    [UpdatedBy]        NVARCHAR (32)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_Recommendation_not_self] CHECK ([RecommendeeId]<>[RecommendedBy]),
    CONSTRAINT [FK_Recommendation_Opportunity] FOREIGN KEY ([OpportunityId]) REFERENCES [dbo].[Opportunity] ([Id]),
    CONSTRAINT [FK_Recommendation_RecommendedBy] FOREIGN KEY ([RecommendedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_Recommendation_Recommendee] FOREIGN KEY ([RecommendeeId]) REFERENCES [dbo].[User] ([Id])
);

