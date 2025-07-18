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
    /// Lógica interna para EstoqueListar.xaml
    /// </summary>
    public partial class EstoqueListar : Window
    {
        private int estoqueSelecionadoId;

        public EstoqueListar()
        {
            InitializeComponent();
            Carregar();
        }

        private void Carregar()
        {
            var dao = new EstoqueDAO();
            try
            {
                EstoqueDataGrid.ItemsSource = dao.List();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao carregar estoque", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    
        private void btVoltar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow menu = new MainWindow();
            menu.Show();
            this.Close();
        }

        private void EstoqueDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EstoqueDataGrid.SelectedItem != null)
            {
                Estoque estoqueSelecionado = (Estoque)EstoqueDataGrid.SelectedItem;
                estoqueSelecionadoId = estoqueSelecionado.Id;
            }
        }

        private void btEditar_Click(object sender, RoutedEventArgs e)
        {
            Estoque estoqueSelecionado = (Estoque)EstoqueDataGrid.SelectedItem;
            EstoqueConsultar estoqueConsultar = new EstoqueConsultar(estoqueSelecionado.Id);
            estoqueConsultar.Show();
            this.Close();
        }

        private void btExcluir_Click(object sender, RoutedEventArgs e)
        {
            if (EstoqueDataGrid.SelectedItem != null)
            {
                Estoque estoqueSelecionado = (Estoque)EstoqueDataGrid.SelectedItem;

                var result = MessageBox.Show($"Deseja excluir o estoque do produto '{estoqueSelecionado.Produto.Nome}'?", "Confirmação",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        var dao = new EstoqueDAO();
                        dao.Delete(estoqueSelecionado);
                        MessageBox.Show("Item de estoque excluído com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                        Carregar();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Erro ao excluir item de estoque", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione um item de estoque para excluir.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
    

