CREATE TABLE [dbo].[InitiativeFollower] (
    [FollowerId]   INT      NOT NULL,
    [InitiativeId] INT      NOT NULL,
    [CreatedAt]    DATETIME NOT NULL,
    PRIMARY KEY CLUSTERED ([FollowerId] ASC, [InitiativeId] ASC),
    CONSTRAINT [FK_IF_Follower] FOREIGN KEY ([FollowerId]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_IF_Initiative] FOREIGN KEY ([InitiativeId]) REFERENCES [dbo].[Initiative] ([Id])
);



