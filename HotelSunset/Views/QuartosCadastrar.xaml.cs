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

namespace HotelSunset.Views
{
    /// <summary>
    /// Lógica interna para QuartosCadastrar.xaml
    /// </summary>
    public partial class QuartosCadastrar : Window
    {
        public QuartosCadastrar()
        {
            InitializeComponent();
        }

        private void btVoltar_Click(object sender, RoutedEventArgs e)
        {
            QuartosListar quartosListar = new QuartosListar();
            quartosListar.Show();
            this.Close();
        }
    }
}
