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
using HotelSunset.Ultilitarios;
using MySql.Data.MySqlClient;

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
            PreencherTiposQuarto();
        }


        private void PreencherTiposQuarto()
        {
            Conexao conn = null; 
            MySqlDataReader reader = null; 
            try
            {
                cbTiposQuarto.Items.Clear();

                conn = new Conexao(); 
                var query = conn.Query();
                query.CommandText = "SELECT nome_tip FROM TiposQuarto";

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    cbTiposQuarto.Items.Add(reader.GetString("nome_tip"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao preencher tipos de quarto: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        private void btVoltar_Click(object sender, RoutedEventArgs e)
        {
            QuartosListar quartosListar = new QuartosListar();
            quartosListar.Show();
            this.Close();
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            Quartos quarto = new Quartos();

            if (!string.IsNullOrWhiteSpace(txtNumero.Text))
            {
                quarto.Numero = txtNumero.Text;
            }
            else
            {
                MessageBox.Show("O campo Número do Quarto é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (cbStatus.SelectedItem != null && cbStatus.SelectedItem is ComboBoxItem selectedStatusItem)
            {
                quarto.Status = selectedStatusItem.Content.ToString();
            }
            else
            {
                MessageBox.Show("O campo Status é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (int.TryParse(txtAndar.Text, out int andar))
            {
                quarto.Andar = andar;
            }
            else
            {
                MessageBox.Show("O campo Andar deve ser um número válido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (int.TryParse(txtCapacidade.Text, out int capacidade))
            {
                quarto.Capacidade = capacidade;
            }
            else
            {
                MessageBox.Show("O campo Capacidade deve ser um número válido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (cbTiposQuarto.SelectedValue != null)
            {
                quarto.TipoQuartoNome = (int)cbTiposQuarto.SelectedValue;
            }
            else
            {
                MessageBox.Show("O campo Tipo de Quarto é obrigatório.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

           // var dao = new QuartoDAO();
           // dao.Insert(quarto);

            MessageBox.Show("Quarto cadastrado com sucesso!", "Confirmação", MessageBoxButton.OK, MessageBoxImage.Information);

            this.Close();
        }

        private void btLimpar_Click(object sender, RoutedEventArgs e)
        {
            txtNumero.Clear();
            cbStatus.SelectedIndex = -1; 
            txtAndar.Clear();
            txtCapacidade.Clear();
            cbTiposQuarto.SelectedIndex = -1; 
        }
    }
}
