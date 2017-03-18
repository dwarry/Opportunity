CREATE TABLE [dbo].[User] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [AccountName]  NVARCHAR (32) NOT NULL,
    [FirstName]    NVARCHAR (32) NOT NULL,
    [FamilyName]   NVARCHAR (32) NOT NULL,
    [EmailAddress] NVARCHAR (50) NOT NULL,
    [ProfileUrl]   NVARCHAR (80) NOT NULL,
    [UpdatedAt]    DATETIME2 (7) NOT NULL,
    [UpdatedBy]    NVARCHAR (32) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);






GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_User_AccountName]
    ON [dbo].[User]([AccountName] ASC);

