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
    /// Lógica interna para ServicosConsultar.xaml
    /// </summary>
    public partial class ServicosConsultar : Window
    {
        int identificadorServico;
        bool Editando = false;
        public ServicosConsultar(int servicoId)
        {
            InitializeComponent();
            var dao = new ServicosDAO();
            Servicos servicoSelecionado = null;

            try
            {
                servicoSelecionado = dao.GetById(servicoId);
                identificadorServico = servicoId;

                if (servicoSelecionado != null)
                {
                    MessageBox.Show(
                        $"ID: {servicoSelecionado.Id}\n" +
                        $"Nome: {servicoSelecionado.Nome}\n" +
                        $"Descrição: {servicoSelecionado.Descricao}\n" +
                        $"Preço: R$ {servicoSelecionado.Preco:F2}",
                        "Dados do Serviço",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );

                    txtNome.Text = servicoSelecionado.Nome;
                    txtDescricao.Text = servicoSelecionado.Descricao;
                    txtPreco.Text = servicoSelecionado.Preco.ToString("F2");

                    SetFormEnabledState(false);
                    btEditar.Content = "Editar";
                    btEditar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3498DB"));
                }
                else
                {
                    MessageBox.Show("Serviço não encontrado. Verifique o ID.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar os dados do serviço: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        private void SetFormEnabledState(bool isEnabled)
        {
            txtNome.IsEnabled = isEnabled;
            txtDescricao.IsEnabled = isEnabled;
            txtPreco.IsEnabled = isEnabled;
        }

        private void btVoltar_Click(object sender, RoutedEventArgs e)
        {
            ServicosListar listar = new ServicosListar();
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
                if (!decimal.TryParse(txtPreco.Text, out decimal preco))
                {
                    MessageBox.Show("Preço inválido. Informe um número válido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Servicos servico = new Servicos
                {
                    Id = identificadorServico,
                    Nome = txtNome.Text.Trim(),
                    Descricao = txtDescricao.Text?.Trim(),
                    Preco = preco
                };

                if (string.IsNullOrWhiteSpace(servico.Nome))
                {
                    MessageBox.Show("Nome é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                try
                {
                    var dao = new ServicosDAO();
                    dao.Update(servico);

                    MessageBox.Show("Serviço atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                    btEditar.Content = "Editar";
                    btEditar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3498DB"));
                    SetFormEnabledState(false);
                    Editando = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao atualizar serviço: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btLimpar_Click(object sender, RoutedEventArgs e)
        {
            txtNome.Clear();
            txtDescricao.Clear();
            txtPreco.Clear();
        }
    }

}
