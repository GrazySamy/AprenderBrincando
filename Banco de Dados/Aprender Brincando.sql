CREATE DATABASE db_educacao;

USE db_educacao;

CREATE TABLE usuario (
	[id] [int] IDENTITY(1,1) PRIMARY KEY,
	[nome] [varchar](15) NULL,
	[sobrenome] [varchar](15) NULL,
	[email] [varchar](30) NULL UNIQUE,
	[celular] [varchar](15) NULL,
	[senha] [varchar](65) NULL,
	[token] [varchar](65) NULL,
	[dataHoraToken] DATETIME NULL
);

SELECT * FROM USUARIO;

-- VIDEOS
CREATE TABLE videos (
	[id] [int] IDENTITY PRIMARY KEY,
	[link] [varchar](50) NOT NULL,
	[descricao] [varchar](100) NOT NULL,
	[categoria] [varchar](30),
	[subcategoria] [varchar](30)
);
