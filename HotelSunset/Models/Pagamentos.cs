using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSunset.Models
{
    public class Pagamentos
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataPagamento { get; set; }
        public string Status { get; set; }
        public int TipoPagamentoId { get; set; }

        public Pagamentos()
        {

        }

        public Pagamentos(int id, decimal valor, DateTime dataPagamento, string status, int tipoPagamentoId)
        {
            Id = id;
            Valor = valor;
            DataPagamento = dataPagamento;
            Status = status;
            TipoPagamentoId = tipoPagamentoId;
        }
    }
}