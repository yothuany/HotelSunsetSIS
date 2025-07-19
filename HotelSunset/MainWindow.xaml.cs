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
            ReservasListar listarReservasWindow = new ReservasListar();
            listarReservasWindow.Show();
            this.Close();
        }

        private void HospedesButton_Click(object sender, RoutedEventArgs e)
        {
            HospedesListar hospedes = new HospedesListar();
            hospedes.Show();
            this.Hide();
        }

        private void QuartosButton_Click(object sender, RoutedEventArgs e)
        {
            QuartosListar quartos = new QuartosListar();
            quartos.Show();
            this.Hide();
        }

        private void TiposQuartoButton_Click(object sender, RoutedEventArgs e)
        {
            TiposQuartoListar tipos = new TiposQuartoListar();
            tipos.Show();
            this.Hide();
        }

        private void ServicosButton_Click(object sender, RoutedEventArgs e)
        {
            ServicosListar servicos = new ServicosListar();
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
            ProdutosListar produtos = new ProdutosListar();
            produtos.Show();
            this.Hide();
        }

        private void EstoqueButton_Click(object sender, RoutedEventArgs e)
        {
            EstoqueListar estoque = new EstoqueListar();
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
            FuncionariosListar funcionarios = new FuncionariosListar();
            funcionarios.Show();
            this.Hide();
        }
    }
}
