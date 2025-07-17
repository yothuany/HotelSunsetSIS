using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSunset.Models
{
    public class Despesas
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataDespesa { get; set; }
        public string TipoDespesa { get; set; }
        public string Status {  get; set; }
        public string Parcelas { get; set; }

        public Despesas()
        {

        }

        public Despesas(int id, string descricao, decimal valor, DateTime dataDespesa, string tipoDespesa, string status , string parcelas)
        {
            Id = id;
            Descricao = descricao;
            Valor = valor;
            DataDespesa = dataDespesa;
            TipoDespesa = tipoDespesa;
            Status = status;
            Parcelas = parcelas;
        }
    }
}
