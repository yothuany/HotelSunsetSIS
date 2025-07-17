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
using HotelSunset.Ultilitarios;
using MySql.Data.MySqlClient;

namespace HotelSunset.Views
{
    /// <summary>
    /// Lógica interna para DespesaConsultar.xaml
    /// </summary>
    public partial class DespesaConsultar : Window
    {
        int identificadorDespesa;
        bool Editou = false;
        public DespesaConsultar(int despesaId)
        {
            InitializeComponent();
            var dao = new DespesaDAO();
            Despesas despesaSelected = null; 

            try
            {
                despesaSelected = dao.GetById(despesaId);
                identificadorDespesa = despesaId;

                if (despesaSelected != null)
                {
                    MessageBox.Show(
                        $"ID: {despesaSelected.Id}\n" +
                        $"Data: {despesaSelected.DataDespesa.ToShortDateString()}\n" +
                        $"Valor: {despesaSelected.Valor:C2}\n" +
                        $"Tipo: {despesaSelected.TipoDespesa}\n" +
                        $"Status: {despesaSelected.Status}\n" +
                        $"Parcela: {despesaSelected.Parcelas}\n" +
                        $"Descrição: {despesaSelected.Descricao}",
                        "Dados da Despesa",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );
                }

                if (despesaSelected != null && despesaSelected.Id > 0)
                {
                    dtpData.SelectedDate = despesaSelected.DataDespesa;
                    txtValor.Text = despesaSelected.Valor.ToString();
                    txtTipoDespesa.Text = despesaSelected.TipoDespesa;

                    foreach (ComboBoxItem item in cbStatus.Items.OfType<ComboBoxItem>())
                    {
                        if (string.Equals(item.Content?.ToString(), despesaSelected.Status, StringComparison.OrdinalIgnoreCase))
                        {
                            cbStatus.SelectedItem = item;
                            break;
                        }
                    }


                    foreach (ComboBoxItem item in cbParcela.Items.OfType<ComboBoxItem>())
                    {
                        if (item.Content?.ToString() == despesaSelected.Parcelas)
                        {
                            cbParcela.SelectedItem = item;
                            break;
                        }
                    }

                    txtDescricao.Text = despesaSelected.Descricao;

                    SetFormEnabledState(false);
                    btEditar.Content = "Editar";
                    btEditar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3498DB")); 
                }
                else
                {
                    MessageBox.Show("Despesa não encontrada ou ID inválido. Por favor, verifique o ID.", "Erro ao Carregar Despesa", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Close(); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro ao carregar os dados da despesa: {ex.Message}", "Erro de Carregamento", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close(); 
            }
        }

        private void SetFormEnabledState(bool isEnabled)
        {
            dtpData.IsEnabled = isEnabled;
            txtValor.IsEnabled = isEnabled;
            txtTipoDespesa.IsEnabled = isEnabled;
            cbStatus.IsEnabled = isEnabled;
            cbParcela.IsEnabled = isEnabled;
            txtDescricao.IsEnabled = isEnabled;

        }

        private void btEditar_Click(object sender, RoutedEventArgs e)
        {
            if (btEditar.Content.ToString() == "Editar")
            {
                btEditar.Content = "Salvar";
                SolidColorBrush brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#27AE60")); // Cor verde para "Salvar"
                btEditar.Background = brush;

                SetFormEnabledState(true); // Habilita todos os campos para edição
                Editou = true;
            }
            else if (btEditar.Content.ToString() == "Salvar")
            {
                Despesas despesa = new Despesas();

                // Atribui o ID da despesa que está sendo editada
                despesa.Id = identificadorDespesa;

                // Validação e atribuição dos campos
                if (dtpData.SelectedDate.HasValue)
                {
                    despesa.DataDespesa = dtpData.SelectedDate.Value;
                }
                else
                {
                    MessageBox.Show("O campo Data da Despesa é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (decimal.TryParse(txtValor.Text, out decimal valor))
                {
                    despesa.Valor = valor;
                }
                else
                {
                    MessageBox.Show("O campo Valor deve ser um número válido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!string.IsNullOrWhiteSpace(txtTipoDespesa.Text))
                {
                    despesa.TipoDespesa = txtTipoDespesa.Text;
                }
                else
                {
                    MessageBox.Show("O campo Tipo de Despesa é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (cbStatus.SelectedItem != null && cbStatus.SelectedItem is ComboBoxItem selectedStatusItem)
                {
                    despesa.Status = selectedStatusItem.Content.ToString();
                }
                else
                {
                    MessageBox.Show("O campo Status é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }


                string parcelaNome = null;
                if (cbParcela.SelectedItem is ComboBoxItem selectedParcelaItem)
                {
                    parcelaNome = selectedParcelaItem.Content?.ToString();
                }

                if (!string.IsNullOrWhiteSpace(parcelaNome))
                {
                    despesa.Parcelas = parcelaNome;
                }
                else
                {
                    MessageBox.Show("O campo Parcela é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                despesa.Descricao = txtDescricao.Text ?? string.Empty;

                try
                {
                    var dao = new DespesaDAO();
                    dao.Update(despesa);

                    MessageBox.Show("Despesa editada com sucesso!", "Confirmação", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Retorna ao modo de visualização após salvar
                    SolidColorBrush brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3498DB")); // Cor padrão de "Editar"
                    btEditar.Background = brush;
                    btEditar.Content = "Editar";
                    SetFormEnabledState(false); // Desabilita os campos
                    Editou = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro ao Salvar", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btVoltar_Click(object sender, RoutedEventArgs e)
        {
            DespesaListar despesaListar = new DespesaListar();
            despesaListar.Show();
            this.Close();
        }
    }
}