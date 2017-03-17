CREATE TABLE [dbo].[Application] (
    [OpportunityId]   INT             NOT NULL,
    [UserId]          INT             NOT NULL,
    [IsSubmitted]     BIT             NOT NULL,
    [ApplicationDate] DATETIME        NULL,
    [ApplicationText] NVARCHAR (1024) NULL,
    [MeetingTime]     DATETIME        NULL,
    [MeetingDuration] INT             NULL,
    [IsSuccessful]    BIT             NULL,
    [OwnerComments]   NVARCHAR (400)  NULL,
    [UpdatedAt]       DATETIME2 (7)   NOT NULL,
    [UpdatedBy]       NVARCHAR (32)   NOT NULL,
    PRIMARY KEY CLUSTERED ([OpportunityId] ASC, [UserId] ASC),
    CONSTRAINT [FK_OpportunityApplication_Applicant] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_OpportunityApplication_Opportunity] FOREIGN KEY ([OpportunityId]) REFERENCES [dbo].[Opportunity] ([Id])
);

