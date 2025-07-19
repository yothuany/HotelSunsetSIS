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
    internal class ReservasDAO
    {
        private Conexao conn;
        public ReservasDAO()
        {
            conn = new Conexao();
        }


        public Reservas GetById(int id)
        {
            MySqlDataReader reader = null;
            try
            {
                var query = conn.Query();
                query.CommandText = @"SELECT id_reserva, data_checkin_res, data_checkout_res, status_res,
                                             valor_total_res, numero_hospedes_res, observacoes_res,
                                             id_hospede_fk, id_quarto_fk, id_tipo_pagamento_fk 
                                      FROM Reservas
                                      WHERE id_reserva = @id";
                query.Parameters.AddWithValue("@id", id);

                reader = query.ExecuteReader();
                Reservas reserva = null;

                if (reader.Read())
                {
                    reserva = new Reservas()
                    {
                        Id = reader.GetInt32("id_reserva"),
                        DataCheckin = reader.GetDateTime("data_checkin_res"),
                        DataCheckout = reader.GetDateTime("data_checkout_res"),
                        Status = reader.GetString("status_res"),
                        ValorTotal = reader.IsDBNull(reader.GetOrdinal("valor_total_res")) ? (decimal?)null : reader.GetDecimal("valor_total_res"),
                        NumeroHospedes = reader.IsDBNull(reader.GetOrdinal("numero_hospedes_res")) ? (int?)null : reader.GetInt32("numero_hospedes_res"),
                        Observacoes = reader.IsDBNull(reader.GetOrdinal("observacoes_res")) ? null : reader.GetString("observacoes_res"),
                        IdHospede = reader.GetInt32("id_hospede_fk"),
                        IdQuarto = reader.GetInt32("id_quarto_fk"),
                        IdTipoPagamento = reader.GetInt32("id_tipo_pagamento_fk")
                    };
                }

                return reserva;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar reserva por ID: {ex.Message}", ex);
            }
            finally
            {
                reader?.Close();
                conn.Close();
            }
        }

        public List<Reservas> List()
        {
            MySqlDataReader reader = null;

            try
            {
                var lista = new List<Reservas>();
                var query = conn.Query();

                query.CommandText = @"
                    SELECT r.id_reserva, r.data_checkin_res, r.data_checkout_res, r.status_res,
                           r.valor_total_res, r.numero_hospedes_res, r.observacoes_res,
                           r.id_hospede_fk, r.id_quarto_fk, r.id_tipo_pagamento_fk, 
                           h.nome_hos,   
                           q.numero_qua, 
                           tp.nome_tpg
                    FROM Reservas r
                    JOIN Hospedes h ON h.id_hospede = r.id_hospede_fk
                    JOIN Quartos q ON q.id_quarto = r.id_quarto_fk
                    JOIN TiposPagamento tp ON tp.id_tipo_pagamento = r.id_tipo_pagamento_fk";

                reader = query.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Reservas()
                    {
                        Id = reader.GetInt32("id_reserva"),
                        DataCheckin = reader.GetDateTime("data_checkin_res"),
                        DataCheckout = reader.GetDateTime("data_checkout_res"),
                        Status = reader.GetString("status_res"),
                        ValorTotal = reader.IsDBNull(reader.GetOrdinal("valor_total_res")) ? (decimal?)null : reader.GetDecimal("valor_total_res"),
                        NumeroHospedes = reader.IsDBNull(reader.GetOrdinal("numero_hospedes_res")) ? (int?)null : reader.GetInt32("numero_hospedes_res"),
                        Observacoes = reader.IsDBNull(reader.GetOrdinal("observacoes_res")) ? null : reader.GetString("observacoes_res"),
                        IdHospede = reader.GetInt32("id_hospede_fk"),
                        IdQuarto = reader.GetInt32("id_quarto_fk"),
                        IdTipoPagamento = reader.GetInt32("id_tipo_pagamento_fk"),
                        Hospede = new Models.Hospedes
                        {
                            Nome = reader.GetString("nome_hos")
                        },
                        Quarto = new Models.Quartos
                        {
                            Numero = reader.GetString("numero_qua")
                        },
                        TipoPagamento = new Models.TipoPagamento
                        {
                            Nome = reader.GetString("nome_tpg") 
                        }
                    });
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar reservas: {ex.Message}", ex);
            }
            finally
            {
                reader?.Close();
                conn.Close();
            }
        }

        public void Insert(Reservas reserva)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = @"INSERT INTO Reservas (data_checkin_res, data_checkout_res, status_res,
                                                          valor_total_res, numero_hospedes_res, observacoes_res,
                                                          id_hospede_fk, id_quarto_fk, id_tipo_pagamento_fk) -- NOVO CAMPO
                                      VALUES (@dataCheckin, @dataCheckout, @status, @valorTotal,
                                              @numeroHospedes, @observacoes, @idHospedeFk, @idQuartoFk, @idTipoPagamentoFk)";

                query.Parameters.AddWithValue("@dataCheckin", reserva.DataCheckin);
                query.Parameters.AddWithValue("@dataCheckout", reserva.DataCheckout);
                query.Parameters.AddWithValue("@status", reserva.Status ?? "Pendente");
                query.Parameters.AddWithValue("@valorTotal", reserva.ValorTotal.HasValue ? (object)reserva.ValorTotal.Value : DBNull.Value);
                query.Parameters.AddWithValue("@numeroHospedes", reserva.NumeroHospedes.HasValue ? (object)reserva.NumeroHospedes.Value : DBNull.Value);
                query.Parameters.AddWithValue("@observacoes", string.IsNullOrEmpty(reserva.Observacoes) ? DBNull.Value : (object)reserva.Observacoes);
                query.Parameters.AddWithValue("@idHospedeFk", reserva.IdHospede);
                query.Parameters.AddWithValue("@idQuartoFk", reserva.IdQuarto);
                query.Parameters.AddWithValue("@idTipoPagamentoFk", reserva.IdTipoPagamento);

                int result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("A reserva não foi inserida.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir reserva: {ex.Message}", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Updates an existing reservation in the database.
        /// </summary>
        /// <param name="reserva">The Reserva object with updated information.</param>
        public void Update(Reservas reserva)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = @"UPDATE Reservas
                                      SET data_checkin_res = @dataCheckin,
                                          data_checkout_res = @dataCheckout,
                                          status_res = @status,
                                          valor_total_res = @valorTotal,
                                          numero_hospedes_res = @numeroHospedes,
                                          observacoes_res = @observacoes,
                                          id_hospede_fk = @idHospedeFk,
                                          id_quarto_fk = @idQuartoFk,
                                          id_tipo_pagamento_fk = @idTipoPagamentoFk -- NOVO CAMPO
                                      WHERE id_reserva = @id";

                query.Parameters.AddWithValue("@dataCheckin", reserva.DataCheckin);
                query.Parameters.AddWithValue("@dataCheckout", reserva.DataCheckout);
                query.Parameters.AddWithValue("@status", reserva.Status);
                query.Parameters.AddWithValue("@valorTotal", reserva.ValorTotal.HasValue ? (object)reserva.ValorTotal.Value : DBNull.Value);
                query.Parameters.AddWithValue("@numeroHospedes", reserva.NumeroHospedes.HasValue ? (object)reserva.NumeroHospedes.Value : DBNull.Value);
                query.Parameters.AddWithValue("@observacoes", string.IsNullOrEmpty(reserva.Observacoes) ? DBNull.Value : (object)reserva.Observacoes);
                query.Parameters.AddWithValue("@idHospedeFk", reserva.IdHospede);
                query.Parameters.AddWithValue("@idQuartoFk", reserva.IdQuarto);
                query.Parameters.AddWithValue("@idTipoPagamentoFk", reserva.IdTipoPagamento); // NOVO CAMPO
                query.Parameters.AddWithValue("@id", reserva.Id);

                int result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("A reserva não foi atualizada.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar reserva: {ex.Message}", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public void Delete(Reservas reserva)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "DELETE FROM Reservas WHERE id_reserva = @id";
                query.Parameters.AddWithValue("@id", reserva.Id);

                int result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("A reserva não foi excluída.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir reserva: {ex.Message}", ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
