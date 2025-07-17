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
    /// Lógica interna para HospedesListar.xaml
    /// </summary>
    public partial class HospedesListar : Window
    {
        private int hospedeSelecionadoId;
        public HospedesListar()
        {
            InitializeComponent();
            Carregar();
        }

        private void Carregar()
        {
            var dao = new HospedesDAO();
            try
            {
                HospedesDataGrid.ItemsSource = dao.List();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao carregar hóspedes", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btNovo_Click(object sender, RoutedEventArgs e)
        {
            HospedesCadastrar hospedeCadastrar = new HospedesCadastrar();
            hospedeCadastrar.Show();
            this.Close();
        }

        private void btVoltar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow menu = new MainWindow();
            menu.Show();
            this.Close();
        }

        private void btEditar_Click(object sender, RoutedEventArgs e)
        {
            Hospedes hospedeSelecionado = (Hospedes)HospedesDataGrid.SelectedItem;
            HospedesConsultar hospedeConsultar = new HospedesConsultar(hospedeSelecionado.Id);
            hospedeConsultar.Show();
            this.Close();
        }

        private void HospedesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HospedesDataGrid.SelectedItem != null)
            {
                Hospedes hospedeSelecionado = (Hospedes)HospedesDataGrid.SelectedItem;
                hospedeSelecionadoId = hospedeSelecionado.Id;
            }
        }

        private void btExcluir_Click(object sender, RoutedEventArgs e)
        {
            Hospedes hospedeSelecionado = (Hospedes)HospedesDataGrid.SelectedItem;

            var result = MessageBox.Show($"Deseja excluir o hóspede '{hospedeSelecionado.Nome}'?", "Confirmação",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var dao = new HospedesDAO();
                    dao.Delete(hospedeSelecionado);
                    MessageBox.Show("Hóspede excluído com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    Carregar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro ao excluir hóspede", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
