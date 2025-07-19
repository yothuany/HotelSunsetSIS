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
    internal class TiposPagamentoDAO
    {

        private Conexao conn;


        public TiposPagamentoDAO()
        {
            conn = new Conexao();
        }

        public TipoPagamento GetById(int id)
        {
            MySqlDataReader reader = null;
            try
            {
                var query = conn.Query();
                query.CommandText = @"SELECT id_tipo_pagamento, nome_tpg, descricao_tpg
                                      FROM TiposPagamento
                                      WHERE id_tipo_pagamento = @id";
                query.Parameters.AddWithValue("@id", id);

                reader = query.ExecuteReader();
                TipoPagamento tipoPagamento = null;

                if (reader.Read())
                {
                    tipoPagamento = new TipoPagamento()
                    {
                        Id = reader.GetInt32("id_tipo_pagamento"),
                        Nome = reader.GetString("nome_tpg"),
                        Descricao = reader.IsDBNull(reader.GetOrdinal("descricao_tpg")) ? null : reader.GetString("descricao_tpg")
                    };
                }

                return tipoPagamento;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar tipo de pagamento por ID: {ex.Message}", ex);
            }
            finally
            {
                reader?.Close();
                conn.Close();
            }
        }

        public List<TipoPagamento> List()
        {
            MySqlDataReader reader = null;

            try
            {
                var lista = new List<TipoPagamento>();
                var query = conn.Query();

                query.CommandText = @"SELECT id_tipo_pagamento, nome_tpg, descricao_tpg
                                      FROM TiposPagamento";

                reader = query.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new TipoPagamento()
                    {
                        Id = reader.GetInt32("id_tipo_pagamento"),
                        Nome = reader.GetString("nome_tpg"),
                        Descricao = reader.IsDBNull(reader.GetOrdinal("descricao_tpg")) ? null : reader.GetString("descricao_tpg")
                    });
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar tipos de pagamento: {ex.Message}", ex);
            }
            finally
            {
                reader?.Close();
                conn.Close();
            }
        }

        public void Insert(TipoPagamento tipoPagamento)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = @"INSERT INTO TiposPagamento (nome_tpg, descricao_tpg)
                                      VALUES (@nome, @descricao)";

                query.Parameters.AddWithValue("@nome", tipoPagamento.Nome);
                query.Parameters.AddWithValue("@descricao", string.IsNullOrEmpty(tipoPagamento.Descricao) ? DBNull.Value : (object)tipoPagamento.Descricao);

                int result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("O tipo de pagamento não foi inserido.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir tipo de pagamento: {ex.Message}", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public void Update(TipoPagamento tipoPagamento)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = @"UPDATE TiposPagamento
                                      SET nome_tpg = @nome,
                                          descricao_tpg = @descricao
                                      WHERE id_tipo_pagamento = @id";

                query.Parameters.AddWithValue("@nome", tipoPagamento.Nome);
                query.Parameters.AddWithValue("@descricao", string.IsNullOrEmpty(tipoPagamento.Descricao) ? DBNull.Value : (object)tipoPagamento.Descricao);
                query.Parameters.AddWithValue("@id", tipoPagamento.Id);

                int result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("O tipo de pagamento não foi atualizado.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar tipo de pagamento: {ex.Message}", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public void Delete(TipoPagamento tipoPagamento)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "DELETE FROM TiposPagamento WHERE id_tipo_pagamento = @id";
                query.Parameters.AddWithValue("@id", tipoPagamento.Id);

                int result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("O tipo de pagamento não foi excluído.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir tipo de pagamento: {ex.Message}", ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
    