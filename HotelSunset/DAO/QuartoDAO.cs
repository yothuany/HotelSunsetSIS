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
    internal class QuartosDAO
    {
        private Conexao conn;

        public QuartosDAO()
        {
            conn = new Conexao();
        }

        public Quartos GetById(int id)
        {
            MySqlDataReader reader = null;
            try
            {
                var query = conn.Query();
                query.CommandText = @"SELECT id_quarto, numero_qua, status_qua, andar_qua, capacidade_qua, id_tipo_quarto_fk 
                                      FROM Quartos 
                                      WHERE id_quarto = @id";
                query.Parameters.AddWithValue("@id", id);

                reader = query.ExecuteReader();
                Quartos quarto = null;

                if (reader.Read())
                {
                    quarto = new Quartos()
                    {
                        Id = reader.GetInt32("id_quarto"),
                        Numero = reader.GetString("numero_qua"),
                        Status = reader.GetString("status_qua"),
                        Andar = reader.GetInt32("andar_qua"),
                        Capacidade = reader.GetInt32("capacidade_qua"),
                        IdTipoQuarto = reader.GetInt32("id_tipo_quarto_fk")
                    };
                }

                return quarto;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar quarto por ID: {ex.Message}", ex);
            }
            finally
            {
                reader?.Close();
                conn.Close();
            }
        }

        public List<Quartos> List()
        {
            MySqlDataReader reader = null;

            try
            {
                var lista = new List<Quartos>();
                var query = conn.Query();

                query.CommandText = @"
            SELECT q.id_quarto, q.numero_qua, q.status_qua, q.andar_qua, q.capacidade_qua,
                   q.id_tipo_quarto_fk,
                   t.nome_tip
            FROM Quartos q
            JOIN TiposQuarto t ON t.id_tipo_quarto = q.id_tipo_quarto_fk";

                reader = query.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Quartos()
                    {
                        Id = reader.GetInt32("id_quarto"),
                        Numero = reader.GetString("numero_qua"),
                        Status = reader.GetString("status_qua"),
                        Andar = reader.GetInt32("andar_qua"),
                        Capacidade = reader.GetInt32("capacidade_qua"),
                        IdTipoQuarto = reader.GetInt32("id_tipo_quarto_fk"),
                        TipoQuarto = new TiposQuarto
                        {
                            Nome = reader.GetString("nome_tip")
                        }
                    });
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar quartos: {ex.Message}", ex);
            }
            finally
            {
                reader?.Close();
                conn.Close();
            }
        }

        public void Insert(Quartos quarto)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = @"INSERT INTO Quartos (numero_qua, status_qua, andar_qua, capacidade_qua, id_tipo_quarto_fk)
                                      VALUES (@numero, @status, @andar, @capacidade, @tipo_fk)";

                query.Parameters.AddWithValue("@numero", quarto.Numero);
                query.Parameters.AddWithValue("@status", quarto.Status ?? "Disponível");
                query.Parameters.AddWithValue("@andar", quarto.Andar);
                query.Parameters.AddWithValue("@capacidade", quarto.Capacidade);
                query.Parameters.AddWithValue("@tipo_fk", quarto.IdTipoQuarto);

                int result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("O quarto não foi inserido.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir quarto: {ex.Message}", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public void Update(Quartos quarto)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = @"UPDATE Quartos 
                                      SET numero_qua = @numero,
                                          status_qua = @status,
                                          andar_qua = @andar,
                                          capacidade_qua = @capacidade,
                                          id_tipo_quarto_fk = @tipo_fk
                                      WHERE id_quarto = @id";

                query.Parameters.AddWithValue("@numero", quarto.Numero);
                query.Parameters.AddWithValue("@status", quarto.Status);
                query.Parameters.AddWithValue("@andar", quarto.Andar);
                query.Parameters.AddWithValue("@capacidade", quarto.Capacidade);
                query.Parameters.AddWithValue("@tipo_fk", quarto.IdTipoQuarto);
                query.Parameters.AddWithValue("@id", quarto.Id);

                int result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("O quarto não foi atualizado.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar quarto: {ex.Message}", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public void Delete(Quartos quarto)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "DELETE FROM Quartos WHERE id_quarto = @id";
                query.Parameters.AddWithValue("@id", quarto.Id);

                int result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("O quarto não foi excluído.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir quarto: {ex.Message}", ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}