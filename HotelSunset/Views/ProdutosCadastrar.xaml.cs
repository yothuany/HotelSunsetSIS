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
    public partial class ProdutosCadastrar : Window
    {
        public ProdutosCadastrar()
        {
            InitializeComponent();
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            Produtos produto = new Produtos();

            if (!string.IsNullOrWhiteSpace(txtNome.Text))
            {
                produto.Nome = txtNome.Text;
            }
            else
            {
                MessageBox.Show("O campo Nome é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            produto.Descricao = txtDescricao.Text ?? string.Empty;


            if (decimal.TryParse(txtPreco.Text, out decimal preco))
            {
                produto.Preco = preco;
            }
            else
            {
                MessageBox.Show("O campo Preço é obrigatório e deve ser numérico.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var dao = new ProdutosDAO();
            dao.Insert(produto);

            MessageBox.Show("Produto cadastrado com sucesso!", "Confirmação", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btVoltar_Click(object sender, RoutedEventArgs e)
        {
            ProdutosListar telaListar = new ProdutosListar();
            telaListar.Show();
            this.Hide();
        }

        private void btLimpar_Click(object sender, RoutedEventArgs e)
        {
            txtNome.Clear();
            txtDescricao.Clear();
            txtPreco.Clear(); 
        }
    }
}
