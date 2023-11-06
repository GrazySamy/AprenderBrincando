﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using AprenderBrincando.Models;

namespace AprenderBrincando.Repositories.ADO.SQLServer
{
    public class Usuario
    {
        private readonly string connectionString; //Declarado para toda a classe. Possível alterar somente no construtor.
        public Usuario(string connectionString) //Quem invocar o construtor do repositório deve enviar a string de conexão.
        {
            this.connectionString = connectionString; //atualização do atributo por meio do valor que veio no parâmetro do construtor..
        }

        public void add(Models.Usuario usuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "insert into usuario (nome, sobrenome, email, celular, senha) values (@nome,@sobrenome,@email,@celular,@senha); select convert(int,@@identity) as id;;";

                    command.Parameters.Add(new SqlParameter("@nome", System.Data.SqlDbType.VarChar)).Value = usuario.Nome;
                    command.Parameters.Add(new SqlParameter("@sobrenome", System.Data.SqlDbType.VarChar)).Value = usuario.Sobrenome;
                    command.Parameters.Add(new SqlParameter("@email", System.Data.SqlDbType.VarChar)).Value = usuario.Email;
                    command.Parameters.Add(new SqlParameter("@celular", System.Data.SqlDbType.VarChar)).Value = usuario.Celular;
                    command.Parameters.Add(new SqlParameter("@senha", System.Data.SqlDbType.VarChar
                        )).Value = usuario.Senha;

                    usuario.Id = (int)command.ExecuteScalar(); // o homem do saco leva os dados até o sgbd e volta com o valor do id => ExecuteScalar retorna um único valor. Observe que o CommandText foi alterado com mais uma instrução. Então, as duas instruções são executadas e temos como retorno o valor do id que foi gerado pelo sgbd na tabela carro. Assim, conseguimos atualizar o valor do id do objeto carro que antes da inserção era 0.
                }
            }
        }

        public List<Models.Usuario> get()
        {
            List<Models.Usuario> usuarios = new List<Models.Usuario>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select id, nome, sobrenome, email, celular, senha from usuario;";

                    SqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        Models.Usuario usuario = new Models.Usuario();
                        usuario.Id = (int)dr["id"];
                        usuario.Nome = (string)dr["nome"];
                        usuario.Sobrenome = (string)dr["sobrenome"];
                        usuario.Email = (string)dr["email"];
                        usuario.Celular = (string)dr["celular"];
                        usuario.Senha = (string)dr["senha"];
                        usuarios.Add(usuario);
                    }
                }
            }

            return usuarios;
        }

        public void delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "delete from usuario where id = @id;";
                    command.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = id;

                    command.ExecuteNonQuery();
                }
            }
        }

        public Models.Usuario getById(int id) 
        {
            Models.Usuario usuario = new Models.Usuario();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select id, nome, sobrenome, email, celular, senha from usuario where id=@id;";
                    command.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = id;

                    SqlDataReader dr = command.ExecuteReader();

                    if (dr.Read())
                    {
                        usuario.Id = (int)dr["id"];
                        usuario.Nome = (string)dr["nome"];
                        usuario.Sobrenome = (string)dr["sobrenome"];
                        usuario.Email = (string)dr["email"];
                        usuario.Celular = (string)dr["celular"];
                        usuario.Senha = (string)dr["senha"];
                    }
                }
            }

            return usuario;
        }

        public Models.Usuario getByLogin(string email) 
        {
            Models.Usuario usuario = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select id, nome, sobrenome, email, celular, senha from usuario where email=@email;";
                    command.Parameters.Add(new SqlParameter("@email", System.Data.SqlDbType.VarChar)).Value = email;

                    SqlDataReader dr = command.ExecuteReader();

                    if (dr.Read())
                    {
                        usuario = new Models.Usuario();
                        usuario.Id = (int)dr["id"];
                        usuario.Nome = (string)dr["nome"];
                        usuario.Sobrenome = (string)dr["sobrenome"];
                        usuario.Email = (string)dr["email"];
                        usuario.Celular = (string)dr["celular"];
                        usuario.Senha = (string)dr["senha"];
                    }
                }
            }

            return usuario;
        }

        public void update(int id, Models.Usuario usuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "update carro set nome = @nome, sobrenome = @sobrenome, email = @email, celular = @celular, senha = @senha where id=@id;";

                    command.Parameters.Add(new SqlParameter("@nome", System.Data.SqlDbType.VarChar)).Value = usuario.Nome;
                    command.Parameters.Add(new SqlParameter("@sobrenome", System.Data.SqlDbType.VarChar)).Value = usuario.Sobrenome;
                    command.Parameters.Add(new SqlParameter("@email", System.Data.SqlDbType.VarChar)).Value = usuario.Email;
                    command.Parameters.Add(new SqlParameter("@celular", System.Data.SqlDbType.VarChar)).Value = usuario.Celular;
                    command.Parameters.Add(new SqlParameter("@senha", System.Data.SqlDbType.VarChar)).Value = usuario.Senha;
                    command.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = id;

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
