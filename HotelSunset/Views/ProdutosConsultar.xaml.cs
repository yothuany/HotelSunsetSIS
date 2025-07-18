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

    public partial class ProdutosConsultar : Window
    {
        int identificadorProduto;
        bool Editando = false;
        public ProdutosConsultar( int produtoId)
        {
            InitializeComponent();
            var dao = new ProdutosDAO();
            Produtos produtoSelecionado = null;

            try
            {
                produtoSelecionado = dao.GetById(produtoId);
                identificadorProduto = produtoId;

                if (produtoSelecionado != null)
                {
                    MessageBox.Show(
                        $"ID: {produtoSelecionado.Id}\n" +
                        $"Nome: {produtoSelecionado.Nome}\n" +
                        $"Descrição: {produtoSelecionado.Descricao}\n" +
                        $"Preço: {produtoSelecionado.Preco:C2}",
                        "Dados do Produto",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );

                    txtNome.Text = produtoSelecionado.Nome;
                    txtDescricao.Text = produtoSelecionado.Descricao;
                    txtPreco.Text = produtoSelecionado.Preco.ToString("F2");

                    SetFormEnabledState(false);
                    btEditar.Content = "Editar";
                    btEditar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3498DB"));
                }
                else
                {
                    MessageBox.Show("Produto não encontrado.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar produto: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        private void SetFormEnabledState(bool isEnabled)
        {
            txtNome.IsEnabled = isEnabled;
            txtDescricao.IsEnabled = isEnabled;
            txtPreco.IsEnabled = isEnabled;
        }

        private void btEditar_Click(object sender, RoutedEventArgs e)
        {
            if (btEditar.Content.ToString() == "Editar")
            {
                Editando = true;
                SetFormEnabledState(true);
                btEditar.Content = "Salvar";
                btEditar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#27AE60"));
            }
            else if (btEditar.Content.ToString() == "Salvar")
            {
                Produtos produto = new Produtos
                {
                    Id = identificadorProduto,
                    Nome = txtNome.Text.Trim(),
                    Descricao = txtDescricao.Text.Trim()
                };

                if (decimal.TryParse(txtPreco.Text, out decimal preco))
                {
                    produto.Preco = preco;
                }
                else
                {
                    MessageBox.Show("Preço inválido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(produto.Nome))
                {
                    MessageBox.Show("Nome é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                try
                {
                    var dao = new ProdutosDAO();
                    dao.Update(produto);

                    MessageBox.Show("Produto atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                    Editando = false;
                    btEditar.Content = "Editar";
                    btEditar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3498DB"));
                    SetFormEnabledState(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao atualizar produto: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btVoltar_Click(object sender, RoutedEventArgs e)
        {
            ProdutosListar listar = new ProdutosListar();
            listar.Show();
            this.Close();
        }

        private void btLimpar_Click(object sender, RoutedEventArgs e)
        {
            txtNome.Clear();
            txtDescricao.Clear();
            txtPreco.Clear();
        }
    }
}
