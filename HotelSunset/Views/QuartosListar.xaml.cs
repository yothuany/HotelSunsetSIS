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
    /// Lógica interna para QuartosListar.xaml
    /// </summary>
    public partial class QuartosListar : Window
    {
        private int quartoSelecionadoId;

        public QuartosListar()
        {
            InitializeComponent();
            Carregar();
        }

      
        private void Carregar()
        {
            var dao = new QuartosDAO();
            try
            {
                QuartosDataGrid.ItemsSource = dao.List();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao carregar quartos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btNovo_Click(object sender, RoutedEventArgs e)
        {
            QuartosCadastrar quartosCadastrar = new QuartosCadastrar();
            quartosCadastrar.Show();
            this.Close();
        }

        private void btVoltar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow menu = new MainWindow();
            menu.Show();
            this.Close();
        }

        private void QuartosDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (QuartosDataGrid.SelectedItem != null)
            {
                Quartos quartoSelecionado = (Quartos)QuartosDataGrid.SelectedItem;
                quartoSelecionadoId = quartoSelecionado.Id;
            }
        }

        private void btEditar_Click(object sender, RoutedEventArgs e)
        {
            Quartos quartoSelecionado = (Quartos)QuartosDataGrid.SelectedItem;
            QuartosConsultar quartosConsultar = new QuartosConsultar(quartoSelecionado.Id);
            quartosConsultar.Show();
            this.Close();
        }

        private void btExcluir_Click(object sender, RoutedEventArgs e)
        {
            if (QuartosDataGrid.SelectedItem != null)
            {
                Quartos quartoSelecionado = (Quartos)QuartosDataGrid.SelectedItem;

                var result = MessageBox.Show($"Deseja excluir o quarto '{quartoSelecionado.Numero}'?", "Confirmação",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        var dao = new QuartosDAO();
                        dao.Delete(quartoSelecionado);
                        MessageBox.Show("Quarto excluído com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                        Carregar();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Erro ao excluir quarto", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
