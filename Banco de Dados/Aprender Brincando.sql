CREATE DATABASE db_educacao;

USE db_educacao;

-- USUARIOS
CREATE TABLE usuario (
	[id] [int] IDENTITY(1,1) PRIMARY KEY,
	[nome] [varchar](15) NULL,
	[sobrenome] [varchar](15) NULL,
	[email] [varchar](30) NULL UNIQUE,
	[celular] [varchar](15) NULL,
	[perfil] [char] NOT NULL CHECK (perfil='A' OR perfil='E') default 'E', --'A'= Administrador; 'E'= Externo; 
	[senha] [varchar](65) NULL,
	[token] [varchar](65) NULL,
	[dataHoraToken] DATETIME NULL
);

-- VIDEOS
CREATE TABLE videos (
	[id] [int] IDENTITY PRIMARY KEY,
	[link] [varchar](50) NOT NULL,
	[descricao] [varchar](100) NOT NULL,
	[categoria] [varchar](30),
	[subcategoria] [varchar](30)
);

-- IMAGENS - FOTOS
CREATE TABLE imagens (
	[id] [int] IDENTITY PRIMARY KEY,
	[arquivo] [varbinary](MAX) NOT NULL,
	[content_type] [varchar](30) NOT NULL,
	[situacao] [char] NOT NULL CHECK (situacao='A' OR situacao='R' OR situacao='E') default 'E', --'A'= Aprovado; 'R'= Reprovado; 'E'= Em análise; 
	[data_inclusao] [datetime] NOT NULL default GETDATE(),
	[data_avaliacao] [datetime],
	[usuario] [int] NOT NULL FOREIGN KEY REFERENCES usuario (id),
	[avaliador] [int] FOREIGN KEY REFERENCES usuario (id)
);

-- ALTERACAO DE PERFIL DO USUARIO
select * from usuario;
update usuario set perfil = 'A' where id = 1;
