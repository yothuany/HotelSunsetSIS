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
    internal class HospedesDAO
    {
        private Conexao conn;

        public HospedesDAO()
        {
            conn = new Conexao();
        }

        public Hospedes GetById(int id)
        {
            MySqlDataReader reader = null;
            try
            {
                var query = conn.Query();
                query.CommandText = "SELECT id_hospede, nome_hos, cpf_hos, data_nascimento_hos, email_hos, telefone_hos FROM Hospedes WHERE id_hospede = @id";
                query.Parameters.AddWithValue("@id", id);

                reader = query.ExecuteReader();
                Hospedes hospede = null;

                if (reader.Read())
                {
                    hospede = new Hospedes();
                    hospede.Id = reader.GetInt32("id_hospede");
                    hospede.Nome = reader.GetString("nome_hos");
                    hospede.Cpf= reader.GetString("cpf_hos");
                    hospede.DataNascimento= reader.GetDateTime("data_nascimento_hos");
                    hospede.Email = reader.IsDBNull(reader.GetOrdinal("email_hos")) ? string.Empty : reader.GetString("email_hos");
                    hospede.Telefone= reader.IsDBNull(reader.GetOrdinal("telefone_hos")) ? string.Empty : reader.GetString("telefone_hos");
                }
                return hospede;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DEBUG: Erro no HospedesDAO.GetById para ID {id}: {ex.Message}");
                throw new Exception($"Erro ao buscar hóspede por ID: {ex.Message}", ex);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                conn.Close();
            }
        }


        public List<Hospedes> List()
        {
            MySqlDataReader reader = null;
            try
            {
                List<Hospedes> list = new List<Hospedes>();

                var query = conn.Query();
                query.CommandText = "SELECT id_hospede, nome_hos, cpf_hos, data_nascimento_hos, email_hos, telefone_hos FROM Hospedes";

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Hospedes()
                    {
                        Id = reader.GetInt32("id_hospede"),
                        Nome = reader.GetString("nome_hos"),
                        Cpf = reader.GetString("cpf_hos"),
                        DataNascimento = reader.GetDateTime("data_nascimento_hos"),
                        Email = reader.IsDBNull(reader.GetOrdinal("email_hos")) ? string.Empty : reader.GetString("email_hos"),
                        Telefone = reader.IsDBNull(reader.GetOrdinal("telefone_hos")) ? string.Empty : reader.GetString("telefone_hos")
                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar hóspedes: {ex.Message}", ex);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                conn.Close();
            }
        }

        public void Insert(Hospedes hospede)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "INSERT INTO Hospedes (nome_hos, cpf_hos, data_nascimento_hos, email_hos, telefone_hos) VALUES (@nome, @cpf, @dataNascimento, @email, @telefone)";

                query.Parameters.AddWithValue("@nome", hospede.Nome);
                query.Parameters.AddWithValue("@cpf", hospede.Cpf);
                query.Parameters.AddWithValue("@dataNascimento", hospede.DataNascimento);
                query.Parameters.AddWithValue("@email", hospede.Email);
                query.Parameters.AddWithValue("@telefone", hospede.Telefone);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("O registro do hóspede não foi inserido. Verifique e tente novamente.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir hóspede: {ex.Message}", ex);
            }
            finally
            {
                conn.Close();
            }
        }
        public void Update(Hospedes hospede)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "UPDATE Hospedes SET nome_hos = @nome, cpf_hos = @cpf, data_nascimento_hos = @dataNascimento, email_hos = @email, telefone_hos = @telefone WHERE id_hospede = @id";

                query.Parameters.AddWithValue("@nome", hospede.Nome);
                query.Parameters.AddWithValue("@cpf", hospede.Cpf);
                query.Parameters.AddWithValue("@dataNascimento", hospede.DataNascimento);
                query.Parameters.AddWithValue("@email", hospede.Email);
                query.Parameters.AddWithValue("@telefone", hospede.Telefone);
                query.Parameters.AddWithValue("@id", hospede.Id);

                var resultado = query.ExecuteNonQuery();

                if (resultado == 0)
                {
                    throw new Exception("Registro do hóspede não atualizado.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar hóspede: {ex.Message}", ex);
            }
            finally
            {
                conn.Close();
            }
        }
        public void Delete(Hospedes hospede)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "DELETE FROM Hospedes WHERE id_hospede = @id";

                query.Parameters.AddWithValue("@id", hospede.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("O registro do hóspede não foi excluído. Verifique e tente novamente.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir hóspede: {ex.Message}", ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}