using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSunset.Models
{
    public class Reservas
    {
        public int Id { get; set; } 
        public DateTime DataCheckin { get; set; } 
        public DateTime DataCheckout { get; set; } 
        public string Status { get; set; } 
        public decimal? ValorTotal { get; set; } 
        public int? NumeroHospedes { get; set; } 
        public string Observacoes { get; set; } 

        public int IdHospede { get; set; } 
        public int IdQuarto { get; set; }
        public int IdTipoPagamento { get; set; } 


        public Hospedes Hospede { get; set; }
        public Quartos Quarto { get; set; }
        public TipoPagamento TipoPagamento { get; set; } 


        public Reservas()
        {

        }

        public Reservas(int id, int hospedeId, int quartoId, DateTime dataCheckin, DateTime dataCheckout, string status, decimal valorTotal, int numeroHospedes, string observacoes)
        {
            Id = id;
            IdHospede = hospedeId;
            IdQuarto = quartoId;
            DataCheckin = dataCheckin;
            DataCheckout = dataCheckout;
            Status = status;
            ValorTotal = valorTotal;
            NumeroHospedes = numeroHospedes;
            Observacoes = observacoes;
        }
    }

}
