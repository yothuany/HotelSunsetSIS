using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSunset.Models
{
    public class Estoque
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public DateTime? DataValidade { get; set; } 
        public string Lote { get; set; }
        public int IdProduto { get; set; }
        public Produtos Produto { get; set; }
        public Estoque()
        {

        }

        public Estoque(int id, int produtoId, int quantidade, DateTime? dataValidade, string lote, int idProduto)
        {
            Id = id;
            ProdutoId = produtoId;
            Quantidade = quantidade;
            DataValidade = dataValidade;
            Lote = lote;
            IdProduto = idProduto;
        }
    }

}
