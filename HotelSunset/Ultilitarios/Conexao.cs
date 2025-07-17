using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace HotelSunset.Ultilitarios
{
    internal class Conexao
    {
        static MySqlConnection conexao;

        public static MySqlConnection Conectar()
        {
            try
            {
                string strconecao = "server=localhost;port=3360;uid=root;pwd=root;database=BD_HotelSunset";
                conexao = new MySqlConnection(strconecao);
                conexao.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na conexão do banco de dados: " + ex.Message);
            }

            return conexao;
        }

        public static void FecharConexao()
        {
            conexao.Close();
        }
    }
}
