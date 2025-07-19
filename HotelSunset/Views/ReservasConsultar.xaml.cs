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
    /// Lógica interna para ReservasConsultar.xaml
    /// </summary>
    public partial class ReservasConsultar : Window
    {
        private int identificadorReserva;
        private bool Editando = false;

        public ReservasConsultar(int reservaId)
        {
            InitializeComponent();
            PreencherComboBoxes(); 

            var dao = new ReservasDAO();
            Reservas reservaSelected = null;

            try
            {
                reservaSelected = dao.GetById(reservaId);
                identificadorReserva = reservaId;

                if (reservaSelected != null)
                {
                    MessageBox.Show(
                       $"ID: {reservaSelected.Id}\n" +
                       $"Check-in: {reservaSelected.DataCheckin:dd/MM/yyyy}\n" +
                       $"Check-out: {reservaSelected.DataCheckout:dd/MM/yyyy}\n" +
                       $"Status: {reservaSelected.Status}\n" +
                       $"Valor Total: {reservaSelected.ValorTotal:C2}\n" +
                       $"Nº Hóspedes: {reservaSelected.NumeroHospedes}\n" +
                       $"Observações: {reservaSelected.Observacoes}\n" +
                       $"ID Hóspede: {reservaSelected.IdHospede}\n" +
                       $"ID Quarto: {reservaSelected.IdQuarto}\n" +
                       $"ID Tipo Pagamento: {reservaSelected.IdTipoPagamento}",
                       "Dados da Reserva Carregados",
                       MessageBoxButton.OK,
                       MessageBoxImage.Information
                   );

                    dtpCheckIn.SelectedDate = reservaSelected.DataCheckin;
                    dtpCheckOut.SelectedDate = reservaSelected.DataCheckout;
                    foreach (ComboBoxItem item in cbStatus.Items)
                    {
                        if (item.Content.ToString() == reservaSelected.Status)
                        {
                            cbStatus.SelectedItem = item;
                            break;
                        }
                    }
                    txtValorTotal.Text = reservaSelected.ValorTotal?.ToString("F2"); 
                    txtNumeroHospedes.Text = reservaSelected.NumeroHospedes?.ToString();
                    txtObservacoes.Text = reservaSelected.Observacoes;

                    cbHospedes.SelectedValue = reservaSelected.IdHospede;
                    cbQuartos.SelectedValue = reservaSelected.IdQuarto;
                    cbTiposPagamento.SelectedValue = reservaSelected.IdTipoPagamento;

                    SetFormEnabledState(false);
                    btEditar.Content = "Editar";
                    btEditar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3498DB"));
                }
                else
                {
                    MessageBox.Show("Reserva não encontrada. Verifique o ID.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Close(); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar os dados da reserva: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close(); 
            }
        }

        private void PreencherComboBoxes()
        {
            var hospedeDao = new HospedesDAO();
            try
            {
                cbHospedes.Items.Clear(); 
                cbHospedes.ItemsSource = hospedeDao.List();
                cbHospedes.DisplayMemberPath = "Nome";
                cbHospedes.SelectedValuePath = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar hóspedes: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            var quartoDao = new QuartosDAO();
            try
            {
                cbQuartos.Items.Clear(); 
                cbQuartos.ItemsSource = quartoDao.List();
                cbQuartos.DisplayMemberPath = "Numero";
                cbQuartos.SelectedValuePath = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar quartos: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            var tipoPagamentoDao = new TiposPagamentoDAO();
            try
            {
                cbTiposPagamento.Items.Clear(); 
                cbTiposPagamento.ItemsSource = tipoPagamentoDao.List();
                cbTiposPagamento.DisplayMemberPath = "Nome";
                cbTiposPagamento.SelectedValuePath = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar tipos de pagamento: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SetFormEnabledState(bool isEnabled)
        {
            dtpCheckIn.IsEnabled = isEnabled;
            dtpCheckOut.IsEnabled = isEnabled;
            cbStatus.IsEnabled = isEnabled;
            txtValorTotal.IsEnabled = isEnabled;
            txtNumeroHospedes.IsEnabled = isEnabled;
            txtObservacoes.IsEnabled = isEnabled;

            cbHospedes.IsEnabled = isEnabled; 
            cbQuartos.IsEnabled = isEnabled;  
            cbTiposPagamento.IsEnabled = isEnabled; 
        }

        private void btEditar_Click(object sender, RoutedEventArgs e)
        {
            if (btEditar.Content.ToString() == "Editar")
            {
                btEditar.Content = "Salvar";
                btEditar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#27AE60")); // Cor verde
                SetFormEnabledState(true);
                Editando = true;
            }
            else if (btEditar.Content.ToString() == "Salvar")
            {
                Reservas reserva = new Reservas();
                reserva.Id = identificadorReserva; 

                if (dtpCheckIn.SelectedDate.HasValue)
                {
                    reserva.DataCheckin = dtpCheckIn.SelectedDate.Value;
                }
                else
                {
                    MessageBox.Show("A Data de Check-in é obrigatória.", "Erro de Validação", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (dtpCheckOut.SelectedDate.HasValue)
                {
                    reserva.DataCheckout = dtpCheckOut.SelectedDate.Value;
                    if (reserva.DataCheckout < reserva.DataCheckin)
                    {
                        MessageBox.Show("A Data de Check-out não pode ser anterior à Data de Check-in.", "Erro de Validação", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("A Data de Check-out é obrigatória.", "Erro de Validação", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (cbStatus.SelectedItem != null && cbStatus.SelectedItem is ComboBoxItem selectedStatusItem)
                {
                    reserva.Status = selectedStatusItem.Content.ToString();
                }
                else
                {
                    MessageBox.Show("O Status da reserva é obrigatório.", "Erro de Validação", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!string.IsNullOrWhiteSpace(txtValorTotal.Text))
                {
                    if (decimal.TryParse(txtValorTotal.Text, System.Globalization.NumberStyles.Currency, System.Globalization.CultureInfo.CurrentCulture, out decimal valorTotal) && valorTotal >= 0)
                    {
                        reserva.ValorTotal = valorTotal;
                    }
                    else
                    {
                        MessageBox.Show("Informe um Valor Total válido (número decimal não negativo).", "Erro de Validação", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else
                {
                    reserva.ValorTotal = null;
                }

                if (!string.IsNullOrWhiteSpace(txtNumeroHospedes.Text))
                {
                    if (int.TryParse(txtNumeroHospedes.Text, out int numeroHospedes) && numeroHospedes > 0)
                    {
                        reserva.NumeroHospedes = numeroHospedes;
                    }
                    else
                    {
                        MessageBox.Show("Informe um número válido de hóspedes (número inteiro positivo).", "Erro de Validação", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else
                {
                    reserva.NumeroHospedes = null;
                }

            
                if (cbHospedes.SelectedValue != null)
                {
                    reserva.IdHospede = (int)cbHospedes.SelectedValue;
                }
                else
                {
                    MessageBox.Show("Hóspede não selecionado. Recarregue a tela ou selecione um hóspede.", "Erro Interno", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (cbQuartos.SelectedValue != null)
                {
                    reserva.IdQuarto = (int)cbQuartos.SelectedValue;
                }
                else
                {
                    MessageBox.Show("Quarto não selecionado. Recarregue a tela ou selecione um quarto.", "Erro Interno", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (cbTiposPagamento.SelectedValue != null)
                {
                    reserva.IdTipoPagamento = (int)cbTiposPagamento.SelectedValue;
                }
                else
                {
                    MessageBox.Show("Tipo de pagamento não selecionado. Recarregue a tela ou selecione um tipo de pagamento.", "Erro Interno", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                reserva.Observacoes = string.IsNullOrWhiteSpace(txtObservacoes.Text) ? null : txtObservacoes.Text.Trim();

                try
                {
                    var dao = new ReservasDAO();
                    dao.Update(reserva);

                    MessageBox.Show("Reserva atualizada com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                    btEditar.Content = "Editar";
                    btEditar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3498DB")); // Cor azul
                    SetFormEnabledState(false);
                    Editando = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao atualizar reserva: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btLimpar_Click(object sender, RoutedEventArgs e)
        {
            dtpCheckIn.SelectedDate = null;
            dtpCheckOut.SelectedDate = null;
            cbStatus.SelectedIndex = -1;
            txtValorTotal.Clear();
            txtNumeroHospedes.Clear();
            txtObservacoes.Clear();
        }

        private void btVoltar_Click(object sender, RoutedEventArgs e)
        {
            ReservasListar reservasListar = new ReservasListar();
            reservasListar.Show();
            this.Close();
        }
    }
}
    
