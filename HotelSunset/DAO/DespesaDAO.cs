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
    internal class DespesaDAO
    {

        private Conexao conn; 

        public DespesaDAO()
        {
            conn = new Conexao();
        }

        public Despesas GetById(int id)
        {
            MySqlDataReader reader = null;
            try
            {
                var query = conn.Query();
                query.CommandText = "SELECT id_despesa, data_des, valor_des, tipo_des, status_des, descricao_des, parcela_des FROM Despesas WHERE id_despesa = @id";
                query.Parameters.AddWithValue("@id", id);

                reader = query.ExecuteReader();
                // CORREÇÃO: Inicializa a despesa apenas se houver um registro
                Despesas despesa = null;

                if (reader.Read())
                {
                    despesa = new Despesas(); // Cria a instância apenas se houver dados
                    despesa.Id = reader.GetInt32("id_despesa");
                    despesa.DataDespesa = reader.GetDateTime("data_des");
                    despesa.Valor = reader.GetDecimal("valor_des");
                    despesa.TipoDespesa = reader.GetString("tipo_des");
                    despesa.Status = reader.GetString("status_des");
                    despesa.Descricao = reader.IsDBNull(reader.GetOrdinal("descricao_des")) ? string.Empty : reader.GetString("descricao_des");
                    despesa.Parcelas = reader.IsDBNull(reader.GetOrdinal("parcela_des")) ? string.Empty : reader.GetString("parcela_des");
                }
                return despesa; // Retorna null se nenhum registro for encontrado
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar despesa por ID: {ex.Message}", ex);
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
       
        public List<Despesas> List()
        {
            MySqlDataReader reader = null;
            try
            {
                List<Despesas> list = new List<Despesas>();

                var query = conn.Query();
                query.CommandText = "SELECT id_despesa, data_des, valor_des, tipo_des, status_des, descricao_des, parcela_des FROM Despesas";

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Despesas()
                    {
                        Id = reader.GetInt32("id_despesa"),
                        DataDespesa = reader.GetDateTime("data_des"),
                        Valor = reader.GetDecimal("valor_des"),
                        TipoDespesa = reader.GetString("tipo_des"),
                        Status = reader.GetString("status_des"),
                        Descricao= reader.IsDBNull(reader.GetOrdinal("descricao_des")) ? string.Empty : reader.GetString("descricao_des"),
                        Parcelas = reader.IsDBNull(reader.GetOrdinal("parcela_des")) ? string.Empty : reader.GetString("parcela_des")
                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar despesas: {ex.Message}", ex);
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

        public void Insert(Despesas despesa)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "INSERT INTO Despesas (data_des, valor_des, tipo_des, status_des, descricao_des, parcela_des) VALUES (@data, @valor, @tipo, @status, @descricao, @parcela)";

                query.Parameters.AddWithValue("@data", despesa.DataDespesa);
                query.Parameters.AddWithValue("@valor", despesa.Valor);
                query.Parameters.AddWithValue("@tipo", despesa.TipoDespesa);
                query.Parameters.AddWithValue("@status", despesa.Status);
                query.Parameters.AddWithValue("@descricao", despesa.Descricao);
                query.Parameters.AddWithValue("@parcela", despesa.Parcelas);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("O registro da despesa não foi inserido. Verifique e tente novamente.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir despesa: {ex.Message}", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public void Update(Despesas despesa)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "UPDATE Despesas SET data_des = @data, valor_des = @valor, tipo_des = @tipo, status_des = @status, descricao_des = @descricao, parcela_des = @parcela WHERE id_despesa = @id";

                query.Parameters.AddWithValue("@data", despesa.DataDespesa);
                query.Parameters.AddWithValue("@valor", despesa.Valor);
                query.Parameters.AddWithValue("@tipo", despesa.TipoDespesa);
                query.Parameters.AddWithValue("@status", despesa.Status);
                query.Parameters.AddWithValue("@descricao", despesa.Descricao);
                query.Parameters.AddWithValue("@parcela", despesa.Parcelas);
                query.Parameters.AddWithValue("@id", despesa.Id);

                var resultado = query.ExecuteNonQuery();

                if (resultado == 0)
                {
                    throw new Exception("Registro da despesa não atualizado.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar despesa: {ex.Message}", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public void Delete(Despesas despesa)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "DELETE FROM Despesas WHERE id_despesa = @id";

                query.Parameters.AddWithValue("@id", despesa.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("O registro da despesa não foi excluído. Verifique e tente novamente.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir despesa: {ex.Message}", ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
    

