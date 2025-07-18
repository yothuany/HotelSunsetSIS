using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSunset.Models
{
    public class Vendas
    {
        public int Id { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal ValorTotal { get; set; }
        public int FuncionarioId { get; set; }
        public int TipoPagamentoId { get; set; }
        public int? ReservaId { get; set; } 

        public Vendas()
        {

        }

        public Vendas(int id, DateTime dataVenda, decimal valorTotal, int funcionarioId, int tipoPagamentoId, int? reservaId)
        {
            Id = id;
            DataVenda = dataVenda;
            ValorTotal = valorTotal;
            FuncionarioId = funcionarioId;
            TipoPagamentoId = tipoPagamentoId;
            ReservaId = reservaId;
        }
    }
}
