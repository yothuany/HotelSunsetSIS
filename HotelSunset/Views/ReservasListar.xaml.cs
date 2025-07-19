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
  
    public partial class ReservasListar : Window
    {
        private int ReservaselecionadaId;
        public ReservasListar()
        {
            InitializeComponent();
            CarregarReservas();
        }

        private void CarregarReservas()
        {
            var dao = new ReservasDAO();
            try
            {
                ReservasDataGrid.ItemsSource = dao.List();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar Reservas: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ReservasDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ReservasDataGrid.SelectedItem != null)
            {
                Reservas Reservaselecionada = (Reservas)ReservasDataGrid.SelectedItem;
                ReservaselecionadaId = Reservaselecionada.Id;
            }
        }
    
        private void btNovos_Click(object sender, RoutedEventArgs e)
        {
            ReservasCadastrar ReservasCadastrar = new ReservasCadastrar();
            ReservasCadastrar.Show();
            this.Close();
        }

        private void btVoltar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow menu = new MainWindow(); 
            menu.Show();
            this.Close();
        }

        private void btEditar_Click_1(object sender, RoutedEventArgs e)
        {
            if (ReservasDataGrid.SelectedItem != null)
            {
                Reservas Reservaselecionada = (Reservas)ReservasDataGrid.SelectedItem;
                ReservasConsultar ReservasConsultar = new ReservasConsultar(Reservaselecionada.Id);
                ReservasConsultar.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Selecione uma Reservas para editar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btExcluir_Click_1(object sender, RoutedEventArgs e)
        {
            if (ReservasDataGrid.SelectedItem != null)
            {
                Reservas Reservaselecionada = (Reservas)ReservasDataGrid.SelectedItem;

                var result = MessageBox.Show($"Deseja excluir a Reservas do hóspede '{Reservaselecionada.Hospede.Nome}' para o quarto '{Reservaselecionada.Quarto.Numero}'?", "Confirmação",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        var dao = new ReservasDAO();
                        dao.Delete(Reservaselecionada);
                        MessageBox.Show("Reservas excluída com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                        CarregarReservas(); 
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao excluir Reservas: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione uma Reservas para excluir.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}

