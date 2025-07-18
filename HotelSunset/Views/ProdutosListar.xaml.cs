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
    /// Lógica interna para ProdutosListar.xaml
    /// </summary>
    public partial class ProdutosListar : Window
    {
        private int produtoSelecionadoId;
        public ProdutosListar()
        {
            InitializeComponent();
            Carregar();
        }

        private void Carregar()
        {
            var dao = new ProdutosDAO();
            try
            {
                ProdutosDataGrid.ItemsSource = dao.List();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao carregar produtos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

      

      
    
        private void btNovo_Click_1(object sender, RoutedEventArgs e)
        {
            ProdutosCadastrar produtosCadastrar = new ProdutosCadastrar();
            produtosCadastrar.Show();
            this.Close();
        }

        private void btVoltar_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow menu = new MainWindow();
            menu.Show();
            this.Close();
        }

        private void ProdutosDataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (ProdutosDataGrid.SelectedItem != null)
            {
                Produtos produtoSelecionado = (Produtos)ProdutosDataGrid.SelectedItem;
                produtoSelecionadoId = produtoSelecionado.Id;
            }
        }

        private void btEditar_Click_1(object sender, RoutedEventArgs e)
        {
            Produtos produtoSelecionado = (Produtos)ProdutosDataGrid.SelectedItem;
            if (produtoSelecionado != null)
            {
                ProdutosConsultar produtosConsultar = new ProdutosConsultar(produtoSelecionado.Id);
                produtosConsultar.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Selecione um produto para editar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btExcluir_Click(object sender, RoutedEventArgs e)
        {
            Produtos produtoSelecionado = (Produtos)ProdutosDataGrid.SelectedItem;

            if (produtoSelecionado != null)
            {
                var result = MessageBox.Show($"Deseja excluir o produto '{produtoSelecionado.Nome}'?", "Confirmação",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        var dao = new ProdutosDAO();
                        dao.Delete(produtoSelecionado);
                        MessageBox.Show("Produto excluído com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                        Carregar();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Erro ao excluir produto", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione um produto para excluir.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}