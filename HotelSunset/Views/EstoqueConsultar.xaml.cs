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
    /// Lógica interna para EstoqueConsultar.xaml
    /// </summary>
    public partial class EstoqueConsultar : Window
    {
        private int identificadorEstoque;
        private bool Editando = false;

        public EstoqueConsultar(int estoqueId)
        {
            InitializeComponent();
            PreencherProdutos();

            cbProdutos.IsEnabled = false; // Produto desabilitado

            var dao = new EstoqueDAO();
            Estoque estoqueSelected = null;

            try
            {
                estoqueSelected = dao.GetById(estoqueId);
                identificadorEstoque = estoqueId;

                if (estoqueSelected != null)
                {
                    MessageBox.Show(
                        $"ID: {estoqueSelected.Id}\n" +
                        $"Quantidade: {estoqueSelected.Quantidade}\n" +
                        $"Data de Validade: {estoqueSelected.DataValidade?.ToString("dd/MM/yyyy") ?? "N/A"}\n" +
                        $"Lote: {estoqueSelected.Lote}\n" +
                        $"Produto ID: {estoqueSelected.IdProduto}",
                        "Dados do Estoque",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );

                    txtQuantidade.Text = estoqueSelected.Quantidade.ToString();
                    dtpValidade.SelectedDate = estoqueSelected.DataValidade;
                    txtLote.Text = estoqueSelected.Lote;
                 
                    cbProdutos.IsEnabled = true;
                    cbProdutos.SelectedValue = estoqueSelected.IdProduto;
                    cbProdutos.IsEnabled = false;
                    SetFormEnabledState(false);
                    btEditar.Content = "Editar";
                    btEditar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3498DB"));
                }
                else
                {
                    MessageBox.Show("Estoque não encontrado. Verifique o ID.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar os dados do estoque: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        private void PreencherProdutos()
        {
            var dao = new ProdutosDAO();
            cbProdutos.ItemsSource = dao.List();
            cbProdutos.DisplayMemberPath = "Nome";
            cbProdutos.SelectedValuePath = "Id";
        }

        private void SetFormEnabledState(bool isEnabled)
        {
            txtQuantidade.IsEnabled = isEnabled;
            dtpValidade.IsEnabled = isEnabled;
            txtLote.IsEnabled = isEnabled;

            cbProdutos.IsEnabled = false;
        }
        private void btVoltar_Click(object sender, RoutedEventArgs e)
        {
            EstoqueListar listar = new EstoqueListar();
            listar.Show();
            this.Close();
        }

        private void btLimpar_Click(object sender, RoutedEventArgs e)
        {
            txtQuantidade.Clear();
            txtLote.Clear();
            dtpValidade.SelectedDate = null;
            cbProdutos.SelectedIndex = -1;
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
                if (!int.TryParse(txtQuantidade.Text, out int quantidade))
                {
                    MessageBox.Show("Informe uma quantidade válida.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Produto já está fixo, mas só para garantir:
                if (cbProdutos.SelectedValue == null)
                {
                    MessageBox.Show("Produto inválido. Contate o administrador.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Estoque estoque = new Estoque
                {
                    Id = identificadorEstoque,
                    Quantidade = quantidade,
                    DataValidade = dtpValidade.SelectedDate,
                    Lote = txtLote.Text.Trim(),
                    IdProduto = (int)cbProdutos.SelectedValue
                };

                try
                {
                    var dao = new EstoqueDAO();
                    dao.Update(estoque);

                    MessageBox.Show("Estoque atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                    btEditar.Content = "Editar";
                    btEditar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3498DB"));
                    SetFormEnabledState(false);
                    Editando = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao atualizar estoque: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}



