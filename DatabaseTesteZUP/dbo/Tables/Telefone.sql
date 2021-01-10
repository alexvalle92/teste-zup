CREATE TABLE [dbo].[Telefone] (
    [Id]             INT          IDENTITY (1, 1) NOT NULL,
    [ClienteId]      INT          NOT NULL,
    [NumeroTelefone] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Telefone_1] PRIMARY KEY CLUSTERED ([Id] ASC, [ClienteId] ASC),
    CONSTRAINT [FK_Telefone_Cliente] FOREIGN KEY ([Id]) REFERENCES [dbo].[Cliente] ([Id])
);

