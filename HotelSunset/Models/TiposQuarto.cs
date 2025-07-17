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
        public string NomeTipo { get; set; }
        public string DescricaoTipo { get; set; }

        public TiposQuarto()
        {

        }

        public TiposQuarto(int id, string nomeTipo, string descricaoTipo)
        {
            Id = id;
            NomeTipo = nomeTipo;
            DescricaoTipo = descricaoTipo;
        }
    }
}
