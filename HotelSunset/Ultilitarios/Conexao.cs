using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace HotelSunset.Ultilitarios
{
    public class Conexao
    {
        private static string host = "localhost";
        private static string porta = "3306"; 
        private static string usuario = "root";
        private static string senha = "root";
        private static string nomebd = "BD_HotelSunset"; 

        private static MySqlConnection connection;
        private static MySqlCommand command;

        public Conexao()
        {
            try
            {
                if (connection == null || connection.State == ConnectionState.Closed)
                {
                    connection = new MySqlConnection($"server={host};database={nomebd};port={porta};user={usuario};password={senha}");
                    connection.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao conectar ao banco de dados: {ex.Message}", "Erro de Conexão", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        public MySqlCommand Query()
        {
            try
            {
                command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                return command;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao criar comando SQL: {ex.Message}", "Erro de Comando", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        public void Close()
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
