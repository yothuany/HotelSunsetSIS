using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSunset.Models
{
    public class Caixa
    {
        public int Id { get; set; }
        public decimal ValorInicial { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; } // Pode ser nulo se o caixa estiver aberto
        public decimal TotalEntradas { get; set; }
        public decimal TotalSaidas { get; set; }
        public int FuncionarioId { get; set; }

        public Caixa()
        {

        }

        public Caixa(int id, decimal valorInicial, DateTime dataAbertura, DateTime? dataFechamento, decimal totalEntradas, decimal totalSaidas, int funcionarioId)
        {
            Id = id;
            ValorInicial = valorInicial;
            DataAbertura = dataAbertura;
            DataFechamento = dataFechamento;
            TotalEntradas = totalEntradas;
            TotalSaidas = totalSaidas;
            FuncionarioId = funcionarioId;
        }
    }
}
