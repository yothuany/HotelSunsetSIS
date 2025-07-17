using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSunset.Models
{
    public class Funcionarios
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string RG { get; set; }
        public decimal Salario { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        public Funcionarios()
        {

        }

        public Funcionarios(int id, string nome, string cpf, DateTime dataNascimento, string rg, decimal salario, string email, string telefone)
        {
            Id = id;
            Nome = nome;
            CPF = cpf;
            DataNascimento = dataNascimento;
            RG = rg;
            Salario = salario;
            Email = email;
            Telefone = telefone;
        }
    }
}
