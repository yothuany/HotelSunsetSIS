using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSunset.Models
{
    public class Hospedes
    {
        private int Id { get; set; }
        private string NomeCompleto { get; set; }
        private string CPF { get; set; }
        private DateTime DataNascimento { get; set; }
        private string Endereco { get; set; }

        public Hospedes()
        {

        }

        public Hospedes(int id, string nomeCompleto, string cpf, DateTime dataNascimento, string endereco)
        {
            Id = id;
            NomeCompleto = nomeCompleto;
            CPF = cpf;
            DataNascimento = dataNascimento;
            Endereco = endereco;
        }
    }
}
