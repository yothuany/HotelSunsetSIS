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
    /// Lógica interna para DespesaListar.xaml
    /// </summary>
    public partial class DespesaListar : Window
    {
        private int despesaSelecionadoId;

        public DespesaListar()
        {
            InitializeComponent();
            Carregar();
        }
        private void Carregar()
        {
            var dao = new DespesaDAO();

            try
            {
                DespesaDataGrid.ItemsSource = dao.List();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção ao Carregar Despesas", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DespesaDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DespesaDataGrid.SelectedItem != null)
            {
                Despesas despesaSelecionada = (Despesas)DespesaDataGrid.SelectedItem;
                despesaSelecionadoId = despesaSelecionada.Id;
            }
        }

        private void Voltar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow menu = new MainWindow();
            menu.Show();
            this.Close();
        }

        private void Novo_Click(object sender, RoutedEventArgs e)
        {
            DespesaCadastrar despesaCadastrar = new DespesaCadastrar();
            despesaCadastrar.Show();
            this.Close();
        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {
                Despesas despesaSelected = (Despesas)DespesaDataGrid.SelectedItem;
                // Abre a tela de consulta/edição passando o ID da despesa
                DespesaConsultar despesaConsultar = new DespesaConsultar(despesaSelected.Id);
                despesaConsultar.Show();
                this.Close();
        }

        private void Excluir_Click(object sender, RoutedEventArgs e)
        {
                Despesas despesaSelected = (Despesas)DespesaDataGrid.SelectedItem;

                var result = MessageBox.Show($"Deseja realmente remover a despesa de '{despesaSelected.TipoDespesa}' no valor de {despesaSelected.Valor:C2}?", "Confirmação de Exclusão",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);

                try
                {
                    if (result == MessageBoxResult.Yes)
                    {
                        var dao = new DespesaDAO();
                        dao.Delete(despesaSelected);
                        MessageBox.Show("Despesa removida com sucesso!", "Exclusão", MessageBoxButton.OK, MessageBoxImage.Information);
                        Carregar(); // Recarrega a lista após a exclusão
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro ao Excluir Despesa", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            
        }
    }
}
