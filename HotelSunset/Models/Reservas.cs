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
        public int HospedeId { get; set; }
        public int QuartoId { get; set; }
        public DateTime DataCheckin { get; set; }
        public DateTime DataCheckout { get; set; }
        public string Status { get; set; }
        public decimal ValorTotal { get; set; }
        public int NumeroHospedes { get; set; }
        public string Observacoes { get; set; }

        public Reservas()
        {

        }

        public Reservas(int id, int hospedeId, int quartoId, DateTime dataCheckin, DateTime dataCheckout, string status, decimal valorTotal, int numeroHospedes, string observacoes)
        {
            Id = id;
            HospedeId = hospedeId;
            QuartoId = quartoId;
            DataCheckin = dataCheckin;
            DataCheckout = dataCheckout;
            Status = status;
            ValorTotal = valorTotal;
            NumeroHospedes = numeroHospedes;
            Observacoes = observacoes;
        }
    }

}
