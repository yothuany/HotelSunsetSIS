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

namespace HotelSunset.Views
{
    /// <summary>
    /// Lógica interna para ServicosCadastrar.xaml
    /// </summary>
    public partial class ServicosCadastrar : Window
    {
        public ServicosCadastrar()
        {
            InitializeComponent();
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            Servicos servico = new Servicos();

            if (!string.IsNullOrWhiteSpace(txtNome.Text))
            {
                servico.Nome = txtNome.Text;
            }
            else
            {
                MessageBox.Show("O campo Nome é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            servico.Descricao = txtDescricao.Text ?? "";

            if (decimal.TryParse(txtPreco.Text, out decimal preco))
            {
                servico.Preco = preco;
            }
            else
            {
                MessageBox.Show("Preço inválido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var dao = new ServicosDAO();
            dao.Insert(servico);

            MessageBox.Show("Serviço cadastrado com sucesso!", "Confirmação", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btLimpar_Click(object sender, RoutedEventArgs e)
        {
            txtNome.Clear();
            txtDescricao.Clear();
            txtPreco.Clear();
        }

        private void btVoltar_Click(object sender, RoutedEventArgs e)
        {
            ServicosListar servicosListar = new ServicosListar();
            servicosListar.Show();
            this.Close();
        }
    }
}
