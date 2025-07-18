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
    internal class ServicosDAO
    {
        private Conexao conn;

        public ServicosDAO()
        {
            conn = new Conexao();
        }

        public Servicos GetById(int id)
        {
            MySqlDataReader reader = null;
            try
            {
                var query = conn.Query();
                query.CommandText = "SELECT id_servico, nome_ser, descricao_ser, preco_ser FROM Servicos WHERE id_servico = @id";
                query.Parameters.AddWithValue("@id", id);

                reader = query.ExecuteReader();
                Servicos servico = null;

                if (reader.Read())
                {
                    servico = new Servicos()
                    {
                        Id = reader.GetInt32("id_servico"),
                        Nome = reader.GetString("nome_ser"),
                        Descricao = reader.IsDBNull(reader.GetOrdinal("descricao_ser")) ? "" : reader.GetString("descricao_ser"),
                        Preco = reader.GetDecimal("preco_ser")
                    };
                }

                return servico;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar serviço por ID: {ex.Message}", ex);
            }
            finally
            {
                reader?.Close();
                conn.Close();
            }
        }

        public List<Servicos> List()
        {
            MySqlDataReader reader = null;
            try
            {
                var lista = new List<Servicos>();
                var query = conn.Query();
                query.CommandText = "SELECT id_servico, nome_ser, descricao_ser, preco_ser FROM Servicos";

                reader = query.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Servicos()
                    {
                        Id = reader.GetInt32("id_servico"),
                        Nome = reader.GetString("nome_ser"),
                        Descricao = reader.IsDBNull(reader.GetOrdinal("descricao_ser")) ? "" : reader.GetString("descricao_ser"),
                        Preco = reader.GetDecimal("preco_ser")
                    });
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar serviços: {ex.Message}", ex);
            }
            finally
            {
                reader?.Close();
                conn.Close();
            }
        }

        public void Insert(Servicos servico)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = @"INSERT INTO Servicos (nome_ser, descricao_ser, preco_ser) 
                                      VALUES (@nome, @descricao, @preco)";

                query.Parameters.AddWithValue("@nome", servico.Nome);
                query.Parameters.AddWithValue("@descricao", servico.Descricao);
                query.Parameters.AddWithValue("@preco", servico.Preco);

                int result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("O registro do serviço não foi inserido.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir serviço: {ex.Message}", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public void Update(Servicos servico)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = @"UPDATE Servicos 
                                      SET nome_ser = @nome, descricao_ser = @descricao, preco_ser = @preco 
                                      WHERE id_servico = @id";

                query.Parameters.AddWithValue("@nome", servico.Nome);
                query.Parameters.AddWithValue("@descricao", servico.Descricao);
                query.Parameters.AddWithValue("@preco", servico.Preco);
                query.Parameters.AddWithValue("@id", servico.Id);

                int result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("O registro do serviço não foi atualizado.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar serviço: {ex.Message}", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public void Delete(Servicos servico)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "DELETE FROM Servicos WHERE id_servico = @id";
                query.Parameters.AddWithValue("@id", servico.Id);

                int result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("O registro do serviço não foi excluído.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir serviço: {ex.Message}", ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}