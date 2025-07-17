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
    internal class FuncionariosDAO
    {
        private Conexao conn;

        public FuncionariosDAO()
        {
            conn = new Conexao();
        }

        public Funcionarios GetById(int id)
        {
            MySqlDataReader reader = null;
            try
            {
                var query = conn.Query();
                query.CommandText = @"SELECT id_funcionario, nome_fun, cpf_fun, rg_fun, 
                                      data_nascimento_fun, salario_fun, email_fun, telefone_fun 
                                      FROM Funcionarios WHERE id_funcionario = @id";
                query.Parameters.AddWithValue("@id", id);

                reader = query.ExecuteReader();
                Funcionarios funcionario = null;

                if (reader.Read())
                {
                    funcionario = new Funcionarios
                    {
                        Id = reader.GetInt32("id_funcionario"),
                        Nome = reader.GetString("nome_fun"),
                        CPF = reader.GetString("cpf_fun"),
                        RG = reader.IsDBNull(reader.GetOrdinal("rg_fun")) ? string.Empty : reader.GetString("rg_fun"),
                        DataNascimento = reader.GetDateTime("data_nascimento_fun"),
                        Salario = reader.GetDecimal("salario_fun"),
                        Email = reader.IsDBNull(reader.GetOrdinal("email_fun")) ? string.Empty : reader.GetString("email_fun"),
                        Telefone = reader.IsDBNull(reader.GetOrdinal("telefone_fun")) ? string.Empty : reader.GetString("telefone_fun")
                    };
                }

                return funcionario;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DEBUG: Erro em FuncionariosDAO.GetById ID {id}: {ex.Message}");
                throw new Exception("Erro ao buscar funcionário por ID.", ex);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                conn.Close();
            }
        }

        public List<Funcionarios> List()
        {
            MySqlDataReader reader = null;
            try
            {
                var funcionarios = new List<Funcionarios>();

                var query = conn.Query();
                query.CommandText = @"SELECT id_funcionario, nome_fun, cpf_fun, rg_fun, 
                                      data_nascimento_fun, salario_fun, email_fun, telefone_fun 
                                      FROM Funcionarios";

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    funcionarios.Add(new Funcionarios
                    {
                        Id = reader.GetInt32("id_funcionario"),
                        Nome = reader.GetString("nome_fun"),
                        CPF = reader.GetString("cpf_fun"),
                        RG = reader.IsDBNull(reader.GetOrdinal("rg_fun")) ? string.Empty : reader.GetString("rg_fun"),
                        DataNascimento = reader.GetDateTime("data_nascimento_fun"),
                        Salario = reader.GetDecimal("salario_fun"),
                        Email = reader.IsDBNull(reader.GetOrdinal("email_fun")) ? string.Empty : reader.GetString("email_fun"),
                        Telefone = reader.IsDBNull(reader.GetOrdinal("telefone_fun")) ? string.Empty : reader.GetString("telefone_fun")
                    });
                }

                return funcionarios;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar funcionários: " + ex.Message, ex);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                conn.Close();
            }
        }

        public void Insert(Funcionarios funcionario)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = @"INSERT INTO Funcionarios 
                                     (nome_fun, cpf_fun, rg_fun, data_nascimento_fun, salario_fun, email_fun, telefone_fun) 
                                     VALUES (@nome, @cpf, @rg, @dataNascimento, @salario, @email, @telefone)";

                query.Parameters.AddWithValue("@nome", funcionario.Nome);
                query.Parameters.AddWithValue("@cpf", funcionario.CPF);
                query.Parameters.AddWithValue("@rg", funcionario.RG);
                query.Parameters.AddWithValue("@dataNascimento", funcionario.DataNascimento);
                query.Parameters.AddWithValue("@salario", funcionario.Salario);
                query.Parameters.AddWithValue("@email", funcionario.Email);
                query.Parameters.AddWithValue("@telefone", funcionario.Telefone);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("Funcionário não foi inserido.");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir funcionário: " + ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public void Update(Funcionarios funcionario)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = @"UPDATE Funcionarios SET 
                                      nome_fun = @nome, 
                                      cpf_fun = @cpf, 
                                      rg_fun = @rg, 
                                      data_nascimento_fun = @dataNascimento,
                                      salario_fun = @salario,
                                      email_fun = @email,
                                      telefone_fun = @telefone
                                      WHERE id_funcionario = @id";

                query.Parameters.AddWithValue("@nome", funcionario.Nome);
                query.Parameters.AddWithValue("@cpf", funcionario.CPF);
                query.Parameters.AddWithValue("@rg", funcionario.RG);
                query.Parameters.AddWithValue("@dataNascimento", funcionario.DataNascimento);
                query.Parameters.AddWithValue("@salario", funcionario.Salario);
                query.Parameters.AddWithValue("@email", funcionario.Email);
                query.Parameters.AddWithValue("@telefone", funcionario.Telefone);
                query.Parameters.AddWithValue("@id", funcionario.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("Funcionário não foi atualizado.");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar funcionário: " + ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public void Delete(Funcionarios funcionario)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "DELETE FROM Funcionarios WHERE id_funcionario = @id";
                query.Parameters.AddWithValue("@id", funcionario.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("Funcionário não foi excluído.");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir funcionário: " + ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
