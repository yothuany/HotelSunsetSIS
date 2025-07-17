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
    /// Lógica interna para DespesaCadastrar.xaml
    /// </summary>
    public partial class DespesaCadastrar : Window
    {
        public DespesaCadastrar()
        {
            InitializeComponent();
        }


        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            Despesas despesa = new Despesas();

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

            if (!string.IsNullOrWhiteSpace(txtTipo.Text))
            {
                despesa.TipoDespesa = txtTipo.Text;
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

            var dao = new DespesaDAO();
            dao.Insert(despesa);

            MessageBox.Show("Despesa cadastrada com sucesso!", "Confirmação", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void btVoltar_Click(object sender, RoutedEventArgs e)
        {
            DespesaListar despesaListar = new DespesaListar();
            despesaListar.Show();
            this.Close();
        }

        private void btLimpar_Click(object sender, RoutedEventArgs e)
        {
            dtpData.SelectedDate = DateTime.Today;
            txtValor.Clear();
            txtTipo.Clear();
            cbStatus.SelectedIndex = -1;
            cbParcela.SelectedIndex = -1;
            txtDescricao.Clear();
        }
    }
}
