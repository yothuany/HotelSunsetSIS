using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSunset.Models
{
    public class Hospedes
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        public Hospedes()
        {

        }

        public Hospedes(int id, string nomeCompleto, string cpf, DateTime dataNascimento, string telefone, string email)
        {
            Id = id;
            Nome = nomeCompleto;
            Cpf = cpf;
            DataNascimento = dataNascimento;
            Telefone = Telefone;
            Email = email;
        }
    }
}
