CREATE TABLE [dbo].[Cliente] (
    [id]        INT          IDENTITY (1, 1) NOT NULL,
    [nome]      VARCHAR (50) NULL,
    [sobrenome] VARCHAR (50) NULL,
    CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED ([id] ASC)
);

