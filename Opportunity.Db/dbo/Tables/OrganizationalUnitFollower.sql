CREATE TABLE [dbo].[OrganizationalUnitFollower] (
    [FollowerId]           INT           NOT NULL,
    [OrganizationalUnitId] INT           NOT NULL,
    [UpdatedAt]            DATETIME2 (7) NOT NULL,
    [UpdatedBy]            NVARCHAR (32) NOT NULL,
    PRIMARY KEY CLUSTERED ([FollowerId] ASC, [OrganizationalUnitId] ASC),
    CONSTRAINT [FK_OUF_Follower] FOREIGN KEY ([FollowerId]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_OUF_OrgUnit] FOREIGN KEY ([OrganizationalUnitId]) REFERENCES [dbo].[OrganizationalUnit] ([Id])
);

