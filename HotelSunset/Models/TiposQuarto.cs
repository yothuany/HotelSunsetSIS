using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSunset.Models
{
    public class TiposQuarto
    {
        public int Id { get; set; }
        public string Nome{ get; set; }
        public string Descricao { get; set; }

        public TiposQuarto()
        {

        }

        public TiposQuarto(int id, string nomeTipo, string descricaoTipo)
        {
            Id = id;
            Nome = nomeTipo;
            Descricao = descricaoTipo;
        }
    }
}
