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
    /// Lógica interna para ServicosListar.xaml
    /// </summary>
    public partial class ServicosListar : Window
    {
        private int servicoSelecionadoId;

        public ServicosListar()
        {
            InitializeComponent();
            Carregar();
        }

        private void Carregar()
        {
            var dao = new ServicosDAO();
            try
            {
                ServicosDataGrid.ItemsSource = dao.List();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao carregar serviços", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ServicosDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ServicosDataGrid.SelectedItem != null)
            {
                Servicos servicoSelecionado = (Servicos)ServicosDataGrid.SelectedItem;
                servicoSelecionadoId = servicoSelecionado.Id;
            }
        }

        private void btEditar_Click(object sender, RoutedEventArgs e)
        {
            Servicos servicoSelecionado = (Servicos)ServicosDataGrid.SelectedItem;
            ServicosConsultar servicoConsultar = new ServicosConsultar(servicoSelecionado.Id);
            servicoConsultar.Show();
            this.Close();
        }

        private void btExcluir_Click(object sender, RoutedEventArgs e)
        {
            Servicos servicoSelecionado = (Servicos)ServicosDataGrid.SelectedItem;

            var result = MessageBox.Show($"Deseja excluir o serviço '{servicoSelecionado.Nome}'?", "Confirmação",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var dao = new ServicosDAO();
                    dao.Delete(servicoSelecionado);
                    MessageBox.Show("Serviço excluído com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    Carregar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro ao excluir serviço", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        
        
        }

        private void btVoltar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow menu = new MainWindow();
            menu.Show();
            this.Close();
        }

        private void btNovo_Click(object sender, RoutedEventArgs e)
        {
            ServicosCadastrar servicosCadastrar = new ServicosCadastrar();
            servicosCadastrar.Show();
            this.Close();
        }
    }
}
