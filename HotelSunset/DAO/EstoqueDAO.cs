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
                query.CommandText = @"SELECT id_estoque, quantidade_est, data_validade_est, lote_est, id_produto_fk
                                      FROM Estoque
                                      WHERE id_estoque = @id";
                query.Parameters.AddWithValue("@id", id);

                reader = query.ExecuteReader();
                Estoque estoque = null;

                if (reader.Read())
                {
                    estoque = new Estoque()
                    {
                        Id = reader.GetInt32("id_estoque"),
                        Quantidade = reader.GetInt32("quantidade_est"),
                        DataValidade = reader.IsDBNull(reader.GetOrdinal("data_validade_est")) ? (DateTime?)null : reader.GetDateTime("data_validade_est"),
                        Lote = reader.GetString("lote_est"),
                        IdProduto = reader.GetInt32("id_produto_fk")
                    };
                }

                return estoque;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar item de estoque por ID: {ex.Message}", ex);
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
                    SELECT e.id_estoque, e.quantidade_est, e.data_validade_est, e.lote_est, e.id_produto_fk,
                           p.nome_pro -- <<-- CORRIGIDO AQUI!
                    FROM Estoque e
                    JOIN Produtos p ON p.id_produto = e.id_produto_fk";

                reader = query.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Estoque()
                    {
                        Id = reader.GetInt32("id_estoque"),
                        Quantidade = reader.GetInt32("quantidade_est"),
                        DataValidade = reader.IsDBNull(reader.GetOrdinal("data_validade_est")) ? (DateTime?)null : reader.GetDateTime("data_validade_est"),
                        Lote = reader.GetString("lote_est"),
                        IdProduto = reader.GetInt32("id_produto_fk"),
                        Produto = new Models.Produtos 
                        {
                            Nome = reader.GetString("nome_pro") 
                        }
                    });
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar itens de estoque: {ex.Message}", ex);
            }
            finally
            {
                reader?.Close();
                conn.Close();
            }
        }

        public void Insert(Estoque estoque)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = @"INSERT INTO Estoque (quantidade_est, data_validade_est, lote_est, id_produto_fk)
                                      VALUES (@quantidade, @dataValidade, @lote, @idProdutoFk)";

                query.Parameters.AddWithValue("@quantidade", estoque.Quantidade);
                query.Parameters.AddWithValue("@dataValidade", estoque.DataValidade.HasValue ? (object)estoque.DataValidade.Value : DBNull.Value);
                query.Parameters.AddWithValue("@lote", estoque.Lote);
                query.Parameters.AddWithValue("@idProdutoFk", estoque.IdProduto);

                int result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("O item de estoque não foi inserido.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir item de estoque: {ex.Message}", ex);
            }
            finally
            {
                conn.Close();
            }
        }
        public void Update(Estoque estoque)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = @"UPDATE Estoque
                                      SET quantidade_est = @quantidade,
                                          data_validade_est = @dataValidade,
                                          lote_est = @lote,
                                          id_produto_fk = @idProdutoFk
                                      WHERE id_estoque = @id";

                query.Parameters.AddWithValue("@quantidade", estoque.Quantidade);
                query.Parameters.AddWithValue("@dataValidade", estoque.DataValidade.HasValue ? (object)estoque.DataValidade.Value : DBNull.Value);
                query.Parameters.AddWithValue("@lote", estoque.Lote);
                query.Parameters.AddWithValue("@idProdutoFk", estoque.IdProduto);
                query.Parameters.AddWithValue("@id", estoque.Id);

                int result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("O item de estoque não foi atualizado.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar item de estoque: {ex.Message}", ex);
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
                    throw new Exception("O item de estoque não foi excluído.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir item de estoque: {ex.Message}", ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
