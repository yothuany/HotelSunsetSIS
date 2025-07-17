using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSunset.Models
{
    public class Quartos
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public string Status { get; set; }
        public int Andar { get; set; }
        public int Capacidade { get; set; }
        public int TipoQuartoId { get; set; }

        public Quartos()
        {

        }

        public Quartos(int id, string numero, string status, int andar, int capacidade, int tipoQuartoId)
        {
            Id = id;
            Numero = numero;
            Status = status;
            Andar = andar;
            Capacidade = capacidade;
            TipoQuartoId = tipoQuartoId;
        }
    }
}