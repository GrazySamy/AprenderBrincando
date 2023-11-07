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