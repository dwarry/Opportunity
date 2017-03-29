CREATE TABLE [dbo].[OpportunityOwnerFollower] (
    [FollowerId]         INT      NOT NULL,
    [OpportunityOwnerId] INT      NOT NULL,
    [CreatedAt]          DATETIME NOT NULL,
    PRIMARY KEY CLUSTERED ([FollowerId] ASC, [OpportunityOwnerId] ASC),
    CONSTRAINT [CK_OOF_cannot_follow_self] CHECK ([FollowerId]<>[OpportunityOwnerId]),
    CONSTRAINT [FK_OOF_Follower] FOREIGN KEY ([FollowerId]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_OOF_Owner] FOREIGN KEY ([OpportunityOwnerId]) REFERENCES [dbo].[User] ([Id])
);



