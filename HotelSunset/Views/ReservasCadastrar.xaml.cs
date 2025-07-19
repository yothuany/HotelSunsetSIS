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

    public partial class ReservasCadastrar : Window
    {
        public ReservasCadastrar()
        {
            InitializeComponent();
            PreencherHospedes();
            PreencherQuartos();
            PreencherTiposPagamento();
            cbHospedes.SelectedIndex = -1;
            cbQuartos.SelectedIndex = -1;
        }

 
        private void PreencherHospedes()
        {
            var dao = new HospedesDAO(); 
            try
            {
                cbHospedes.ItemsSource = dao.List();
                cbHospedes.DisplayMemberPath = "Nome"; 
                cbHospedes.SelectedValuePath = "Id";   
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar hóspedes: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PreencherQuartos()
        {
            var dao = new QuartosDAO();
            try
            {
                cbQuartos.ItemsSource = dao.List();
                cbQuartos.DisplayMemberPath = "Numero"; 
                cbQuartos.SelectedValuePath = "Id";  
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar quartos: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void PreencherTiposPagamento()
        {
            var dao = new TiposPagamentoDAO();
            try
            {
                cbTiposPagamento.ItemsSource = dao.List();
                cbTiposPagamento.DisplayMemberPath = "Nome"; 
                cbTiposPagamento.SelectedValuePath = "Id";   
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar tipos de pagamento: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btVoltar_Click_1(object sender, RoutedEventArgs e)
        {
            ReservasListar reservasListar = new ReservasListar();
            reservasListar.Show();
            this.Close();
        }

        private void btLimpar_Click(object sender, RoutedEventArgs e)
        {
            cbHospedes.SelectedIndex = -1;
            cbQuartos.SelectedIndex = -1;
            dtpCheckIn.SelectedDate = null;
            dtpCheckOut.SelectedDate = null;
            cbStatus.SelectedIndex = -1;
            txtValorTotal.Clear();
            txtNumeroH.Clear();
            cbTiposPagamento.SelectedIndex = -1;
            txtObservacoes.Clear();
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            Reservas reserva = new Reservas();

            if (cbHospedes.SelectedValue != null && int.TryParse(cbHospedes.SelectedValue.ToString(), out int hospedeId))
            {
                reserva.IdHospede = hospedeId;
            }
            else
            {
                MessageBox.Show("Selecione um hóspede válido.", "Erro de Validação", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (cbQuartos.SelectedValue != null && int.TryParse(cbQuartos.SelectedValue.ToString(), out int quartoId))
            {
                reserva.IdQuarto = quartoId;
            }
            else
            {
                MessageBox.Show("Selecione um quarto válido.", "Erro de Validação", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

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
                if (decimal.TryParse(txtValorTotal.Text, out decimal valorTotal) && valorTotal >= 0)
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

            if (!string.IsNullOrWhiteSpace(txtNumeroH.Text))
            {
                if (int.TryParse(txtNumeroH.Text, out int numeroHospedes) && numeroHospedes > 0)
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

            if (cbTiposPagamento.SelectedValue != null && int.TryParse(cbTiposPagamento.SelectedValue.ToString(), out int tipoPagamentoId))
            {
                reserva.IdTipoPagamento = tipoPagamentoId;
            }
            else
            {
                MessageBox.Show("Selecione um tipo de pagamento válido.", "Erro de Validação", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            reserva.Observacoes = string.IsNullOrWhiteSpace(txtObservacoes.Text) ? null : txtObservacoes.Text.Trim();

            try
            {
                var dao = new ReservasDAO();
                dao.Insert(reserva);

                MessageBox.Show("Reserva cadastrada com sucesso!", "Confirmação", MessageBoxButton.OK, MessageBoxImage.Information);
                btLimpar_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao cadastrar reserva: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

   