using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelSunset.Models;
using HotelSunset.Ultilitarios;
using MySql.Data.MySqlClient;

namespace HotelSunset.DAO
{
    internal class EstoqueDAO
    {
        private Conexao conn;

        public EstoqueDAO()
        {
            conn = new Conexao();
        }

        public Estoque GetById(int id)
        {
            MySqlDataReader reader = null;

            try
            {
                var query = conn.Query();
                query.CommandText = @"
                    SELECT e.id_estoque, e.quantidade_est, e.data_validade_est, e.lote_est,
                           e.id_produto_fk, p.nome_pro
                    FROM Estoque e
                    INNER JOIN Produtos p ON e.id_produto_fk = p.id_produto
                    WHERE e.id_estoque = @id";

                query.Parameters.AddWithValue("@id", id);

                reader = query.ExecuteReader();
                Estoque estoque = null;

                if (reader.Read())
                {
                    estoque = new Estoque()
                    {
                        Id = reader.GetInt32("id_estoque"),
                        Quantidade = reader.GetInt32("quantidade_est"),
                        DataValidade = reader.IsDBNull(reader.GetOrdinal("data_validade_est"))
                            ? (DateTime?)null
                            : reader.GetDateTime("data_validade_est"),
                        Lote = reader.GetString("lote_est"),
                        ProdutoId = reader.GetInt32("id_produto_fk"),
                        Produto = new Produtos
                        {
                            Nome = reader.GetString("nome_pro")
                        }
                    };
                }

                return estoque;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar estoque por ID: {ex.Message}", ex);
            }
            finally
            {
                reader?.Close();
                conn.Close();
            }
        }

        public List<Estoque> List()
        {
            MySqlDataReader reader = null;

            try
            {
                var lista = new List<Estoque>();
                var query = conn.Query();

                query.CommandText = @"
                    SELECT e.id_estoque, e.quantidade_est, e.data_validade_est, e.lote_est,
                           e.id_produto_fk, p.nome_pro
                    FROM Estoque e
                    INNER JOIN Produtos p ON e.id_produto_fk = p.id_produto";

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Estoque()
                    {
                        Id = reader.GetInt32("id_estoque"),
                        Quantidade = reader.GetInt32("quantidade_est"),
                        DataValidade = reader.IsDBNull(reader.GetOrdinal("data_validade_est"))
                            ? (DateTime?)null
                            : reader.GetDateTime("data_validade_est"),
                        Lote = reader.GetString("lote_est"),
                        ProdutoId = reader.GetInt32("id_produto_fk"),
                        Produto = new Produtos
                        {
                            Nome = reader.GetString("nome_pro")
                        }
                    });
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar estoques: {ex.Message}", ex);
            }
            finally
            {
                reader?.Close();
                conn.Close();
            }
        }

        public void Update(Estoque estoque)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = @"
                    UPDATE Estoque 
                    SET quantidade_est = @quantidade,
                        data_validade_est = @validade,
                        lote_est = @lote,
                        id_produto_fk = @produto
                    WHERE id_estoque = @id";

                query.Parameters.AddWithValue("@quantidade", estoque.Quantidade);
                query.Parameters.AddWithValue("@validade", estoque.DataValidade?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@lote", estoque.Lote);
                query.Parameters.AddWithValue("@produto", estoque.ProdutoId);
                query.Parameters.AddWithValue("@id", estoque.Id);

                int result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("O estoque não foi atualizado.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar estoque: {ex.Message}", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public void Delete(Estoque estoque)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "DELETE FROM Estoque WHERE id_estoque = @id";
                query.Parameters.AddWithValue("@id", estoque.Id);

                int result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("O estoque não foi excluído.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir estoque: {ex.Message}", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        // (Opcional) método para atualizar quantidade sem criar novo
        public void AtualizarQuantidadeSeExistir(int produtoId, int novaQuantidade)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = @"
                    UPDATE Estoque 
                    SET quantidade_est = @quantidade 
                    WHERE id_produto_fk = @produto";

                query.Parameters.AddWithValue("@quantidade", novaQuantidade);
                query.Parameters.AddWithValue("@produto", produtoId);

                int result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("Produto não encontrado no estoque para atualizar.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar a quantidade: {ex.Message}", ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
