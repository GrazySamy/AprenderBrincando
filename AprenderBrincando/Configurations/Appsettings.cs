namespace AprenderBrincando.Configurations
{
    public class Appsettings
    {
        public static string getKeyConnectionString() //Esse método foi criado para dizer qual chave deve ser consultada no arquivo appsettings.json que está localizado logo abaixo da pasta views. facilitar a alteração da string de conexão sem precisar recompilar o código.
        {
            return "DefaultConnection";
        }

        public static string getKeyNomeRemetente() //Esse método foi criado para dizer qual chave deve ser consultada no arquivo appsettings.json que está localizado logo abaixo da pasta views. facilitar a alteração da string de conexão sem precisar recompilar o código.
        {
            return "Coravel:Mail:Name";
        }

        public static string getKeyEmailRemetente() //Esse método foi criado para dizer qual chave deve ser consultada no arquivo appsettings.json que está localizado logo abaixo da pasta views. facilitar a alteração da string de conexão sem precisar recompilar o código.
        {
            return "Coravel:Mail:Username";
        }

    }
}
