CREATE TABLE [dbo].[Cliente] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Nome]      VARCHAR (50)   NOT NULL,
    [Sobrenome] VARCHAR (50)   NOT NULL,
    [Email]     NVARCHAR (100) NOT NULL,
    [Senha]     VARCHAR (50)   NOT NULL,
    CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED ([Id] ASC)
);



