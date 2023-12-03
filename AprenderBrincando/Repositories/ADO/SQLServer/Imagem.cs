using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using AprenderBrincando.Models;
using static System.Net.Mime.MediaTypeNames;

namespace AprenderBrincando.Repositories.ADO.SQLServer
{
    public class Imagem
    {
        private readonly string connectionString; //Declarado para toda a classe. Possível alterar somente no construtor.
        public Imagem(string connectionString) //Quem invocar o construtor do repositório deve enviar a string de conexão.
        {
            this.connectionString = connectionString; //atualização do atributo por meio do valor que veio no parâmetro do construtor..
        }

        public void add(byte[] imagem, string contentType, int usuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "insert into imagens (arquivo, content_type, usuario) values (@arquivo, @content_type, @usuario); select convert(int,@@identity) as id;;";

                    command.Parameters.Add(new SqlParameter("@arquivo", System.Data.SqlDbType.Binary)).Value = imagem;
                    command.Parameters.Add(new SqlParameter("@content_type", System.Data.SqlDbType.VarChar)).Value = contentType;
                    command.Parameters.Add(new SqlParameter("@usuario", System.Data.SqlDbType.Int)).Value = usuario;

                    command.ExecuteScalar();
                }
            }
        }

        public List<Models.Imagem> getAll()
        {
            List<Models.Imagem> imagens = new List<Models.Imagem>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select i.id, i.arquivo, i.content_type, i.situacao, i.data_inclusao, i.data_avaliacao, (u.nome + ' ' + u.sobrenome) as avaliador from imagens as i left join usuario as u on i.avaliador = u.id;";

                    SqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        Models.Imagem imagem = new Models.Imagem();
                        imagem.Id = (int)dr["id"];
                        imagem.Arquivo = Convert.ToBase64String((byte[])dr["arquivo"]);
                        imagem.ContentType = (string)dr["content_type"];
                        imagem.Situacao = ((string)dr["situacao"] == "A") ? "Aprovado" : ((string)dr["situacao"] == "R") ? "Reprovado" : "Em análise";
                        imagem.DataInclusao = (DateTime)dr["data_inclusao"];
                        imagem.DataAvaliacao = (dr["data_avaliacao"] == DBNull.Value) ? null : (DateTime)dr["data_avaliacao"];
                        imagem.Avaliador = (dr["avaliador"] == DBNull.Value) ? "" : (string)dr["avaliador"];

                        imagens.Add(imagem);
                    }
                }
            }

            return imagens;
        }


        public List<Models.Imagem> getAllByUser(int usuario)
        {
            List<Models.Imagem> imagens = new List<Models.Imagem>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select id, arquivo, content_type, situacao, data_inclusao from imagens where usuario=@usuario;";
                    command.Parameters.Add(new SqlParameter("@usuario", System.Data.SqlDbType.Int)).Value = usuario;

                    SqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        Models.Imagem imagem = new Models.Imagem();
                        imagem.Id = (int)dr["id"];
                        imagem.Arquivo = Convert.ToBase64String((byte[])dr["arquivo"]);
                        imagem.ContentType = (string)dr["content_type"];
                        imagem.Situacao = (string)dr["situacao"];
                        imagem.DataInclusao = (DateTime)dr["data_inclusao"];

                        imagens.Add(imagem);
                    }
                }
            }

            return imagens;
        }

        public void delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "delete from imagens where id = @id;";
                    command.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = id;

                    command.ExecuteNonQuery();
                }
            }
        }

        public Models.Imagem getById(int id) 
        {
            Models.Imagem imagem = new Models.Imagem();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select id, arquivo, content_type, situacao, data_inclusao, usuario, avaliador from imagens where id=@id;";
                    command.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = id;

                    SqlDataReader dr = command.ExecuteReader();

                    if (dr.Read())
                    {
                        imagem.Id = (int)dr["id"];
                        imagem.Arquivo = Convert.ToBase64String((byte[])dr["arquivo"]);
                        imagem.ContentType = (string)dr["content_type"];
                        imagem.Situacao = (string)dr["situacao"];
                        imagem.DataInclusao = (DateTime)dr["data_inclusao"];
                        imagem.Usuario = (string)dr["usuario"];
                        imagem.Avaliador = (string)dr["avaliador"];
                    }
                }
            }

            return imagem;
        }

        public void update(int id, char situacao, int avaliador)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "update imagens set situacao = @situacao, avaliador = @avaliador, data_avaliacao=GETDATE() where id=@id;";

                    command.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = id;
                    command.Parameters.Add(new SqlParameter("@situacao", System.Data.SqlDbType.Char)).Value = situacao;
                    command.Parameters.Add(new SqlParameter("@avaliador", System.Data.SqlDbType.Int)).Value = avaliador;

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
