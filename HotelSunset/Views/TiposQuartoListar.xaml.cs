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
    /// Lógica interna para TiposQuartoListar.xaml
    /// </summary>
    public partial class TiposQuartoListar : Window
    {
        public TiposQuartoListar()
        {
            InitializeComponent();
            Carregar();
        }

        private void Carregar()
        {
            var dao = new TipoQuartoDAO();
            try
            {
                TiposQuartoDataGrid.ItemsSource = dao.List();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao carregar tipos de quarto", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btNovo_Click(object sender, RoutedEventArgs e)
        {
            TiposQuartoCadastrar cadastrar = new TiposQuartoCadastrar();
            cadastrar.Show();
            this.Close();
        }

        private void btVoltar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow menu = new MainWindow();
            menu.Show();
            this.Close();
        }

        private void btExcluir_Click(object sender, RoutedEventArgs e)
        {
            var tipo = (TiposQuarto)TiposQuartoDataGrid.SelectedItem;

            if (tipo != null)
            {
                var result = MessageBox.Show($"Deseja excluir o tipo '{tipo.Nome}'?", "Confirmação",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        var dao = new TipoQuartoDAO();
                        dao.Delete(tipo);
                        MessageBox.Show("Tipo de quarto excluído com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                        Carregar();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Erro ao excluir tipo de quarto", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void btEditar_Click(object sender, RoutedEventArgs e)
        {
            var tipo = (TiposQuarto)TiposQuartoDataGrid.SelectedItem;
            if (tipo != null)
            {
                TiposQuartosConsultar consultar = new TiposQuartosConsultar(tipo.Id);
                consultar.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Selecione um tipo de quarto para editar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
    }


}
