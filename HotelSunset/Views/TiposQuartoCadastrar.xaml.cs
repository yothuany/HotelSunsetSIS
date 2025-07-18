using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HotelSunset.DAO;
using HotelSunset.Models;
using HotelSunset.Ultilitarios;
using MySql.Data.MySqlClient;

namespace HotelSunset.Views
{
    /// <summary>
    /// Lógica interna para TiposQuartoCadastrar.xaml
    /// </summary>
    public partial class TiposQuartoCadastrar : Window
    {
        public TiposQuartoCadastrar()
        {
            InitializeComponent();
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            TiposQuarto tipo = new TiposQuarto();

            if (!string.IsNullOrWhiteSpace(txtNome.Text))
            {
                tipo.Nome = txtNome.Text;
            }
            else
            {
                MessageBox.Show("O campo Nome é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            tipo.Descricao = txtDescricao.Text ?? string.Empty;

            var dao = new TipoQuartoDAO();

            try
            {
                dao.Insert(tipo);
                MessageBox.Show("Tipo de quarto cadastrado com sucesso!", "Confirmação", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btVoltar_Click(object sender, RoutedEventArgs e)
        {
            TiposQuartoListar listar = new TiposQuartoListar();
            listar.Show();
            this.Hide();
        }

        private void btLimpar_Click(object sender, RoutedEventArgs e)
        {
            txtNome.Clear();
            txtDescricao.Clear();
        }
    }
}
