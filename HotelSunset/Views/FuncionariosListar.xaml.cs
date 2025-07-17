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


    public partial class FuncionariosListar : Window
    {
        private int funcionarioSelecionadoId;

        public FuncionariosListar()
        {
            InitializeComponent();
            Carregar();
        }

        private void Carregar()
        {
            var dao = new FuncionariosDAO();
            try
            {
                FuncionariosDataGrid.ItemsSource = dao.List();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao carregar funcionários", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btNovo_Click(object sender, RoutedEventArgs e)
        {
            FuncionariosCadastrar funcionariosCadastrar = new FuncionariosCadastrar();
            funcionariosCadastrar.Show();
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
            Funcionarios funcionarioSelecionado = (Funcionarios)FuncionariosDataGrid.SelectedItem;
            FuncionarioConsultar funcionariosConsultar = new FuncionarioConsultar(funcionarioSelecionado.Id);
            funcionariosConsultar.Show();
            this.Close();
        }

        private void FuncionariosDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FuncionariosDataGrid.SelectedItem != null)
            {
                Funcionarios funcionarioSelecionado = (Funcionarios)FuncionariosDataGrid.SelectedItem;
                funcionarioSelecionadoId = funcionarioSelecionado.Id;
            }
        }

        private void btExcluir_Click(object sender, RoutedEventArgs e)
        {
            Funcionarios funcionarioSelecionado = (Funcionarios)FuncionariosDataGrid.SelectedItem;

            var result = MessageBox.Show($"Deseja excluir o funcionário '{funcionarioSelecionado.Nome}'?", "Confirmação",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var dao = new FuncionariosDAO();
                    dao.Delete(funcionarioSelecionado);
                    MessageBox.Show("Funcionário excluído com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    Carregar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro ao excluir funcionário", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
