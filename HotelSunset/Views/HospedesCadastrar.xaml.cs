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
using HotelSunset.Models;

namespace HotelSunset.Views
{
    /// <summary>
    /// Lógica interna para HospedesCadastrar.xaml
    /// </summary>
    public partial class HospedesCadastrar : Window
    {
        public HospedesCadastrar()
        {
            InitializeComponent();
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            Hospedes hospede = new Hospedes();

            if (!string.IsNullOrWhiteSpace(txtNome.Text))
            {
                hospede.NomeCompleto = txtNome.Text;
            }
            else
            {
                MessageBox.Show("O campo Nome Completo é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtCpf.Text))
            {
                hospede.CPF = txtCpf.Text;
            }
            else
            {
                MessageBox.Show("O campo CPF é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtTelefone.Text))
            {
                hospede.Telefone = txtTelefone.Text;
            }
            else
            {
                MessageBox.Show("O campo Telefone é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (dtpDataNasc.SelectedDate.HasValue)
            {
                hospede.DataNascimento = dtpDataNasc.SelectedDate.Value;
            }
            else
            {
                
            }

           // var dao = new HospedeDAO();
            //dao.Insert(hospede);

            MessageBox.Show("Hóspede cadastrado com sucesso!", "Confirmação", MessageBoxButton.OK, MessageBoxImage.Information);

            this.Close();
        }

        private void btLimpar_Click(object sender, RoutedEventArgs e)
        {
            txtNome.Clear();
            txtCpf.Clear();
            dtpDataNasc.SelectedDate = null;
            txtEmail.Clear();
            txtTelefone.Clear();
        }
    }
}
