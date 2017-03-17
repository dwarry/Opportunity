CREATE TABLE [dbo].[User] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [AccountName]  NVARCHAR (32) NULL,
    [EmailAddress] NVARCHAR (50) NOT NULL,
    [MySiteUrl]    NVARCHAR (80) NULL,
    [UpdatedAt]    DATETIME2 (7) NOT NULL,
    [UpdatedBy]    NVARCHAR (32) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_User_AccountName]
    ON [dbo].[User]([AccountName] ASC);

