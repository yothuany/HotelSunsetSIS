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

    public partial class QuartosConsultar : Window
    {
        private int identificadorQuarto;
        private bool Editando = false;
        public QuartosConsultar(int quartoId)
        {
            InitializeComponent();
            PreencherTiposQuarto();
            cbTiposQuarto.SelectedIndex = -1;

            var dao = new QuartosDAO();
            Quartos quartoSelected = null;

            try
            {
                quartoSelected = dao.GetById(quartoId);
                identificadorQuarto = quartoId;

                if (quartoSelected != null)
                {
                    MessageBox.Show(
                        $"ID: {quartoSelected.Id}\n" +
                        $"Número: {quartoSelected.Numero}\n" +
                        $"Andar: {quartoSelected.Andar}\n" +
                        $"Capacidade: {quartoSelected.Capacidade}\n" +
                        $"Status: {quartoSelected.Status}\n" +
                        $"Tipo Quarto ID: {quartoSelected.IdTipoQuarto}",
                        "Dados do Quarto",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );

                    txtNumero.Text = quartoSelected.Numero;
                    txtAndar.Text = quartoSelected.Andar.ToString();
                    txtCapacidade.Text = quartoSelected.Capacidade.ToString();
                    foreach (ComboBoxItem item in cbStatus.Items)
                    {
                        if (item.Content.ToString() == quartoSelected.Status)
                        {
                            cbStatus.SelectedItem = item;
                            break;
                        }
                    }
                    cbTiposQuarto.SelectedValue = quartoSelected.IdTipoQuarto;

                    SetFormEnabledState(false);
                    btEditar.Content = "Editar";
                    btEditar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3498DB"));
                }
                else
                {
                    MessageBox.Show("Quarto não encontrado. Verifique o ID.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar os dados do quarto: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        private void PreencherTiposQuarto()
        {
            var dao = new TipoQuartoDAO();
            cbTiposQuarto.ItemsSource = dao.List();
            cbTiposQuarto.DisplayMemberPath = "Nome";
            cbTiposQuarto.SelectedValuePath = "Id";
        }

        private void SetFormEnabledState(bool isEnabled)
        {
            txtNumero.IsEnabled = isEnabled;
            txtAndar.IsEnabled = isEnabled;
            txtCapacidade.IsEnabled = isEnabled;
            cbStatus.IsEnabled = isEnabled;
            cbTiposQuarto.IsEnabled = isEnabled;
        }
        private void btVoltar_Click(object sender, RoutedEventArgs e)
        {
            QuartosListar listar = new QuartosListar();
            listar.Show();
            this.Close();
        }

        private void btLimpar_Click(object sender, RoutedEventArgs e)
        {
            txtNumero.Clear();
            txtAndar.Clear();
            txtCapacidade.Clear();
            cbStatus.SelectedIndex = -1;
            cbTiposQuarto.SelectedIndex = -1;
        }

        private void btEditar_Click(object sender, RoutedEventArgs e)
        {
            if (btEditar.Content.ToString() == "Editar")
            {
                btEditar.Content = "Salvar";
                btEditar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#27AE60"));
                SetFormEnabledState(true);
                Editando = true;
            }
            else if (btEditar.Content.ToString() == "Salvar")
            {
                if (!int.TryParse(txtAndar.Text, out int andar))
                {
                    MessageBox.Show("Informe um número válido para o andar.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!int.TryParse(txtCapacidade.Text, out int capacidade))
                {
                    MessageBox.Show("Informe um número válido para a capacidade.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
              
                if (cbTiposQuarto.SelectedValue == null)
                {
                    MessageBox.Show("Selecione um tipo de quarto válido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
              
                Quartos quarto = new Quartos
                {
                    Id = identificadorQuarto,
                    Numero = txtNumero.Text.Trim(),
                    Andar = andar,
                    Capacidade = capacidade,
                    Status = (cbStatus.SelectedItem as ComboBoxItem)?.Content.ToString(),
                    IdTipoQuarto = (int)cbTiposQuarto.SelectedValue

                };

                if (string.IsNullOrWhiteSpace(quarto.Numero))
                {
                    MessageBox.Show("Número do quarto é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (cbStatus.SelectedItem == null || (cbStatus.SelectedItem as ComboBoxItem)?.Content.ToString() == string.Empty)
                {
                    MessageBox.Show("O campo Status é obrigatório. Selecione um status para o quarto.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                try
                {
                    var dao = new QuartosDAO();
                    dao.Update(quarto);

                    MessageBox.Show("Quarto atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                    btEditar.Content = "Editar";
                    btEditar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3498DB"));
                    SetFormEnabledState(false);
                    Editando = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao atualizar quarto: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
