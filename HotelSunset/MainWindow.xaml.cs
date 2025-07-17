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
using System.Windows.Navigation;
using System.Windows.Shapes;
using HotelSunset.Views;

namespace HotelSunset
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ReservaButton_Click(object sender, RoutedEventArgs e)
        {
            ReservasCadastrar reservasWindow = new ReservasCadastrar();

            reservasWindow.Show();

            this.Hide();
        }

        private void HospedesButton_Click(object sender, RoutedEventArgs e)
        {
            HospedesCadastrar hospedes = new HospedesCadastrar();
            hospedes.Show();
            this.Hide();
        }

        private void QuartosButton_Click(object sender, RoutedEventArgs e)
        {
            QuartosCadastrar quartos = new QuartosCadastrar();
            quartos.Show();
            this.Hide();
        }

        private void TiposQuartoButton_Click(object sender, RoutedEventArgs e)
        {
            TiposQuartoCadastrar tipos = new TiposQuartoCadastrar();
            tipos.Show();
            this.Hide();
        }

        private void ServicosButton_Click(object sender, RoutedEventArgs e)
        {
            ServicosCadastrar servicos = new ServicosCadastrar();
            servicos.Show();
            this.Hide();
        }

        private void VendasButton_Click(object sender, RoutedEventArgs e)
        {
            VendasCadastrar vendas = new VendasCadastrar();
            vendas.Show();
            this.Hide();
        }

        private void ProdutosButton_Click(object sender, RoutedEventArgs e)
        {
            ProdutosCadastrar produtos = new ProdutosCadastrar();
            produtos.Show();
            this.Hide();
        }

        private void EstoqueButton_Click(object sender, RoutedEventArgs e)
        {
            EstoqueCadastrar estoque = new EstoqueCadastrar();
            estoque.Show();
            this.Hide();
        }

        private void CaixaButton_Click(object sender, RoutedEventArgs e)
        {
            CaixaCadastrar caixa = new CaixaCadastrar();
            caixa.Show();
            this.Hide();
        }

        private void PagamentosButton_Click(object sender, RoutedEventArgs e)
        {
            PagamentosCadastrar pagamentos = new PagamentosCadastrar();
            pagamentos.Show();
            this.Hide();
        }

        private void DespesasButton_Click(object sender, RoutedEventArgs e)
        {
            DespesaListar despesas = new DespesaListar();
            despesas.Show();
            this.Hide();
        }

        private void FuncionariosButton_Click(object sender, RoutedEventArgs e)
        {
            FuncionariosCadastrar funcionarios = new FuncionariosCadastrar();
            funcionarios.Show();
            this.Hide();
        }
    }
}
