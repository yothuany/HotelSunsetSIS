using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelSunset.Models;
using HotelSunset.Ultilitarios;
using MySql.Data.MySqlClient;

namespace HotelSunset.DAO
{
    internal class ProdutosDAO
    {
        private Conexao conn;

        public ProdutosDAO()
        {
            conn = new Conexao();
        }

        public Produtos GetById(int id)
        {
            MySqlDataReader reader = null;
            try
            {
                var query = conn.Query();
                query.CommandText = "SELECT id_produto, nome_pro, descricao_pro, preco_pro FROM Produtos WHERE id_produto = @id";
                query.Parameters.AddWithValue("@id", id);

                reader = query.ExecuteReader();
                Produtos produto = null;

                if (reader.Read())
                {
                    produto = new Produtos()
                    {
                        Id = reader.GetInt32("id_produto"),
                        Nome = reader.GetString("nome_pro"),
                        Descricao = reader.GetString("descricao_pro"),
                        Preco = reader.GetDecimal("preco_pro")
                    };
                }

                return produto;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar produto por ID: {ex.Message}", ex);
            }
            finally
            {
                reader?.Close();
                conn.Close();
            }
        }

        public List<Produtos> List()
        {
            MySqlDataReader reader = null;
            try
            {
                var lista = new List<Produtos>();
                var query = conn.Query();
                query.CommandText = "SELECT id_produto, nome_pro, descricao_pro, preco_pro FROM Produtos";

                reader = query.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Produtos()
                    {
                        Id = reader.GetInt32("id_produto"),
                        Nome = reader.GetString("nome_pro"),
                        Descricao = reader.GetString("descricao_pro"),
                        Preco = reader.GetDecimal("preco_pro")
                    });
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar produtos: {ex.Message}", ex);
            }
            finally
            {
                reader?.Close();
                conn.Close();
            }
        }

        public void Insert(Produtos produto)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = @"INSERT INTO Produtos (nome_pro, descricao_pro, preco_pro) 
                                      VALUES (@nome, @descricao, @preco)";

                query.Parameters.AddWithValue("@nome", produto.Nome);
                query.Parameters.AddWithValue("@descricao", produto.Descricao);
                query.Parameters.AddWithValue("@preco", produto.Preco);

                int result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("O registro do produto não foi inserido.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir produto: {ex.Message}", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public void Update(Produtos produto)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = @"UPDATE Produtos 
                                      SET nome_pro = @nome, descricao_pro = @descricao, preco_pro = @preco 
                                      WHERE id_produto = @id";

                query.Parameters.AddWithValue("@nome", produto.Nome);
                query.Parameters.AddWithValue("@descricao", produto.Descricao);
                query.Parameters.AddWithValue("@preco", produto.Preco);
                query.Parameters.AddWithValue("@id", produto.Id);

                int result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("O registro do produto não foi atualizado.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar produto: {ex.Message}", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public void Delete(Produtos produto)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "DELETE FROM Produtos WHERE id_produto  = @id";
                query.Parameters.AddWithValue("@id", produto.Id);

                int result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("O registro do produto não foi excluído.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir produto: {ex.Message}", ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}