using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using AprenderBrincando.Models;

namespace AprenderBrincando.Repositories.ADO.SQLServer
{
    public class Video
    {
        private readonly string connectionString; //Declarado para toda a classe. Possível alterar somente no construtor.
        public Video(string connectionString) //Quem invocar o construtor do repositório deve enviar a string de conexão.
        {
            this.connectionString = connectionString; //atualização do atributo por meio do valor que veio no parâmetro do construtor..
        }

        public void add(Models.Video video)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "insert into videos (link, descricao, categoria, subcategoria) values (@link,@descricao,@categoria,@subcategoria); select convert(int,@@identity) as id;;";

                    command.Parameters.Add(new SqlParameter("@link", System.Data.SqlDbType.VarChar)).Value = video.Link;
                    command.Parameters.Add(new SqlParameter("@descricao", System.Data.SqlDbType.VarChar)).Value = video.Descricao;
                    command.Parameters.Add(new SqlParameter("@categoria", System.Data.SqlDbType.VarChar)).Value = video.Categoria;
                    command.Parameters.Add(new SqlParameter("@subcategoria", System.Data.SqlDbType.VarChar)).Value = video.Subcategoria;

                    video.Id = (int)command.ExecuteScalar();
                }
            }
        }

        public List<Models.Video> getAll()
        {
            List<Models.Video> videos = new List<Models.Video>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select id, link, descricao, subcategoria, categoria from videos;";

                    SqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        Models.Video video = new Models.Video();
                        video.Id = (int)dr["id"];
                        video.Link = (string)dr["link"];
                        video.Descricao = (string)dr["descricao"];
                        video.Subcategoria = (string)dr["subcategoria"];
                        video.Categoria = (string)dr["categoria"];

                        videos.Add(video);
                    }
                }
            }

            return videos;
        }

        public void delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "delete from videos where id = @id;";
                    command.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = id;

                    command.ExecuteNonQuery();
                }
            }
        }

        public Models.Video getById(int id) 
        {
            Models.Video video = new Models.Video();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select id, link, descricao, subcategoria, categoria from videos where id=@id;";
                    command.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = id;

                    SqlDataReader dr = command.ExecuteReader();

                    if (dr.Read())
                    {
                        video.Id = (int)dr["id"];
                        video.Link = (string)dr["link"];
                        video.Descricao = (string)dr["descricao"];
                        video.Subcategoria = (string)dr["subcategoria"];
                        video.Categoria = (string)dr["categoria"];
                    }
                }
            }

            return video;
        }

        public void update(int id, Models.Video video)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "update videos set link = @link, descricao = @descricao, categoria = @categoria, subcategoria = @subcategoria where id=@id;";

                    command.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = id;
                    command.Parameters.Add(new SqlParameter("@link", System.Data.SqlDbType.VarChar)).Value = video.Link;
                    command.Parameters.Add(new SqlParameter("@descricao", System.Data.SqlDbType.VarChar)).Value = video.Descricao;
                    command.Parameters.Add(new SqlParameter("@categoria", System.Data.SqlDbType.VarChar)).Value = video.Categoria;
                    command.Parameters.Add(new SqlParameter("@subcategoria", System.Data.SqlDbType.VarChar)).Value = video.Subcategoria;

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
