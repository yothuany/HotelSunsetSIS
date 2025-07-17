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

    public partial class FuncionarioConsultar : Window
    {
        int identificadorFuncionario;
        bool Editando = false;
        public FuncionarioConsultar(int funcionarioId)
        {
            InitializeComponent();
            var dao = new FuncionariosDAO();
            Funcionarios funcionarioSelecionado = null;

            try
            {
                funcionarioSelecionado = dao.GetById(funcionarioId);
                identificadorFuncionario = funcionarioId;

                if (funcionarioSelecionado != null)
                {
                    MessageBox.Show(
                        $"ID: {funcionarioSelecionado.Id}\n" +
                        $"Nome: {funcionarioSelecionado.Nome}\n" +
                        $"CPF: {funcionarioSelecionado.CPF}\n" +
                        $"RG: {funcionarioSelecionado.RG}\n" +
                        $"Nascimento: {funcionarioSelecionado.DataNascimento.ToShortDateString()}\n" +
                        $"Email: {funcionarioSelecionado.Email}\n" +
                        $"Telefone: {funcionarioSelecionado.Telefone}\n" +
                        $"Salário: {funcionarioSelecionado.Salario:C2}",
                        "Dados do Funcionário",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );

                    txtNome.Text = funcionarioSelecionado.Nome;
                    txtCPF.Text = funcionarioSelecionado.CPF;
                    txtRG.Text = funcionarioSelecionado.RG;
                    dtpNascimento.SelectedDate = funcionarioSelecionado.DataNascimento;
                    txtEmail.Text = funcionarioSelecionado.Email;
                    txtTelefone.Text = funcionarioSelecionado.Telefone;
                    txtSalario.Text = funcionarioSelecionado.Salario.ToString("F2");

                    SetFormEnabledState(false);
                    btEditar.Content = "Editar";
                    btEditar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3498DB"));
                }
                else
                {
                    MessageBox.Show("Funcionário não encontrado. Verifique o ID.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar os dados do funcionário: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        private void SetFormEnabledState(bool isEnabled)
        {
            txtNome.IsEnabled = isEnabled;
            txtCPF.IsEnabled = isEnabled;
            txtRG.IsEnabled = isEnabled;
            dtpNascimento.IsEnabled = isEnabled;
            txtEmail.IsEnabled = isEnabled;
            txtTelefone.IsEnabled = isEnabled;
            txtSalario.IsEnabled = isEnabled;
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
                Funcionarios funcionario = new Funcionarios
                {
                    Id = identificadorFuncionario,
                    Nome = txtNome.Text.Trim(),
                    CPF = txtCPF.Text.Trim(),
                    RG = txtRG.Text.Trim(),
                    DataNascimento = dtpNascimento.SelectedDate ?? DateTime.MinValue,
                    Email = txtEmail.Text?.Trim(),
                    Telefone = txtTelefone.Text?.Trim()
                };

                if (decimal.TryParse(txtSalario.Text, out decimal salario))
                {
                    funcionario.Salario = salario;
                }
                else
                {
                    MessageBox.Show("Salário inválido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(funcionario.Nome))
                {
                    MessageBox.Show("Nome é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(funcionario.CPF))
                {
                    MessageBox.Show("CPF é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                try
                {
                    var dao = new FuncionariosDAO();
                    dao.Update(funcionario);

                    MessageBox.Show("Funcionário atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                    btEditar.Content = "Editar";
                    btEditar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3498DB"));
                    SetFormEnabledState(false);
                    Editando = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao atualizar funcionário: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btVoltar_Click(object sender, RoutedEventArgs e)
        {
            FuncionariosListar listar = new FuncionariosListar();
            listar.Show();
            this.Close();
        }

        private void btLimpar_Click_1(object sender, RoutedEventArgs e)
        {
            txtNome.Clear();
            txtCPF.Clear();
            txtRG.Clear();
            dtpNascimento.SelectedDate = null;
            txtEmail.Clear();
            txtTelefone.Clear();
            txtSalario.Clear();
        }
    }
}