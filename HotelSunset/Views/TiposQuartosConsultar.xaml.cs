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
    /// Lógica interna para TiposQuartosConsultar.xaml
    /// </summary>
    public partial class TiposQuartosConsultar : Window
    {
        private int identificadorTipo;
        private bool editando = false;
        public TiposQuartosConsultar(int tipoId)
        {
            InitializeComponent();
            var dao = new TipoQuartoDAO();
            TiposQuarto tipoSelecionado = null;

            try
            {
                tipoSelecionado = dao.GetById(tipoId);
                identificadorTipo = tipoId;

                if (tipoSelecionado != null)
                {
                    MessageBox.Show(
                        $"ID: {tipoSelecionado.Id}\n" +
                        $"Nome: {tipoSelecionado.Nome}\n" +
                        $"Descrição: {tipoSelecionado.Descricao}",
                        "Dados do Tipo de Quarto",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );

                    txtNome.Text = tipoSelecionado.Nome;
                    txtDescricao.Text = tipoSelecionado.Descricao;

                    SetFormEnabledState(false);
                    btEditar.Content = "Editar";
                    btEditar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3498DB"));
                }
                else
                {
                    MessageBox.Show("Tipo de quarto não encontrado.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar tipo de quarto: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        private void SetFormEnabledState(bool isEnabled)
        {
            txtNome.IsEnabled = isEnabled;
            txtDescricao.IsEnabled = isEnabled;
        }

        private void btEditar_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btEditar.Content == "Editar")
            {
                editando = true;
                SetFormEnabledState(true);
                btEditar.Content = "Salvar";
                btEditar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#27AE60"));
            }
            else if ((string)btEditar.Content == "Salvar")
            {
                TiposQuarto tipo = new TiposQuarto
                {
                    Id = identificadorTipo,
                    Nome = txtNome.Text.Trim(),
                    Descricao = txtDescricao.Text.Trim()
                };

                if (string.IsNullOrWhiteSpace(tipo.Nome))
                {
                    MessageBox.Show("Nome é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                try
                {
                    var dao = new TipoQuartoDAO();
                    dao.Update(tipo);

                    MessageBox.Show("Tipo de quarto atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                    editando = false;
                    btEditar.Content = "Editar";
                    btEditar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3498DB"));
                    SetFormEnabledState(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao atualizar tipo de quarto: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btLimpar_Click(object sender, RoutedEventArgs e)
        {
            txtNome.Clear();
            txtDescricao.Clear();
        }

        private void btVoltar_Click(object sender, RoutedEventArgs e)
        {
            TiposQuartoListar listar = new TiposQuartoListar();
            listar.Show();
            this.Close();
        }
    }
}
