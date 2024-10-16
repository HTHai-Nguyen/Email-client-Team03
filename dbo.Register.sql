CREATE TABLE [dbo].[Register] (
    [ID]       INT           IDENTITY (1, 1) NOT NULL,
    [username] VARCHAR (100) NOT NULL,
    [password] VARCHAR (100) NOT NULL,
    [email]    VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_Register] PRIMARY KEY CLUSTERED ([ID] ASC)
);

