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

    public partial class FuncionariosCadastrar : Window
    {
        public FuncionariosCadastrar()
        {
            InitializeComponent();
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            Funcionarios funcionario = new Funcionarios();

            if (!string.IsNullOrWhiteSpace(txtNome.Text))
            {
                funcionario.Nome = txtNome.Text;
            }
            else
            {
                MessageBox.Show("O campo Nome é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtCpf.Text))
            {
                funcionario.CPF = txtCpf.Text;
            }
            else
            {
                MessageBox.Show("O campo CPF é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtTelefone.Text))
            {
                funcionario.Telefone = txtTelefone.Text;
            }
            else
            {
                MessageBox.Show("O campo Telefone é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (dtpDataNasc.SelectedDate.HasValue)
            {
                funcionario.DataNascimento = dtpDataNasc.SelectedDate.Value;
            }

            funcionario.RG = txtRg.Text ?? string.Empty;

            if (decimal.TryParse(txtSalario.Text, out decimal salario))
            {
                funcionario.Salario = salario;
            }
            else
            {
                funcionario.Salario = 0.00m; 
            }

            funcionario.Email = txtEmail.Text ?? string.Empty;

             var dao = new FuncionariosDAO();
             dao.Insert(funcionario);

            MessageBox.Show("Funcionário cadastrado com sucesso!", "Confirmação", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void btLimpar_Click(object sender, RoutedEventArgs e)
        {
            txtNome.Clear();
            txtCpf.Clear();
            dtpDataNasc.SelectedDate = null;
            txtRg.Clear();
            txtSalario.Clear();
            txtEmail.Clear();
            txtTelefone.Clear();
        }

        private void btVoltar_Click(object sender, RoutedEventArgs e)
        {
            FuncionariosListar funcionariosListar = new FuncionariosListar();
            funcionariosListar.Show();
            this.Close();
        }
    }
}
