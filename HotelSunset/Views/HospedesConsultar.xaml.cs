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
    public partial class HospedesConsultar : Window
    {
        int identificadorHospede;
        bool Editando = false;
        public HospedesConsultar(int hospedeId)
        {
            InitializeComponent();

            var dao = new HospedesDAO();
            Hospedes hospedeSelected = null;

            try
            {
                hospedeSelected = dao.GetById(hospedeId);
                identificadorHospede = hospedeId;

                if (hospedeSelected != null)
                {
                    MessageBox.Show(
                        $"ID: {hospedeSelected.Id}\n" +
                        $"Nome: {hospedeSelected.Nome}\n" +
                        $"CPF: {hospedeSelected.Cpf}\n" +
                        $"Nascimento: {hospedeSelected.DataNascimento.ToShortDateString()}\n" +
                        $"Email: {hospedeSelected.Email}\n" +
                        $"Telefone: {hospedeSelected.Telefone}",
                        "Dados do Hóspede",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );

                    txtNome.Text = hospedeSelected.Nome;
                    txtCPF.Text = hospedeSelected.Cpf;
                    dtpNascimento.SelectedDate = hospedeSelected.DataNascimento;
                    txtEmail.Text = hospedeSelected.Email;
                    txtTelefone.Text = hospedeSelected.Telefone;

                    SetFormEnabledState(false);
                    btEditar.Content = "Editar";
                    btEditar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3498DB"));
                }
                else
                {
                    MessageBox.Show("Hóspede não encontrado. Verifique o ID.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar os dados do hóspede: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }
      
        private void SetFormEnabledState(bool isEnabled)
        {
            txtNome.IsEnabled = isEnabled;
            txtCPF.IsEnabled = isEnabled;
            dtpNascimento.IsEnabled = isEnabled;
            txtEmail.IsEnabled = isEnabled;
            txtTelefone.IsEnabled = isEnabled;
        }
       
        private void btVoltar_Click_1(object sender, RoutedEventArgs e)
        {
            HospedesListar listar = new HospedesListar();
            listar.Show();
            this.Close();
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
                Hospedes hospede = new Hospedes
                {
                    Id = identificadorHospede,
                    Nome = txtNome.Text.Trim(),
                    Cpf = txtCPF.Text.Trim(),
                    DataNascimento = (DateTime)dtpNascimento.SelectedDate,
                    Email = txtEmail.Text?.Trim(),
                    Telefone = txtTelefone.Text?.Trim()
                };

                if (string.IsNullOrWhiteSpace(hospede.Nome))
                {
                    MessageBox.Show("Nome é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(hospede.Cpf))
                {
                    MessageBox.Show("CPF é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                try
                {
                    var dao = new HospedesDAO();
                    dao.Update(hospede);

                    MessageBox.Show("Hóspede atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                    btEditar.Content = "Editar";
                    btEditar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3498DB"));
                    SetFormEnabledState(false);
                    Editando = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao atualizar hóspede: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btLimpar_Click(object sender, RoutedEventArgs e)
        {

            txtNome.Clear();
            txtCPF.Clear();
            txtTelefone.Clear();
            txtEmail.Clear();
            dtpNascimento.SelectedDate = null; 
        }
    }
}

