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
    internal class TipoQuartoDAO
    {
        private Conexao conn;

        public TipoQuartoDAO()
        {
            conn = new Conexao();
        }

        public TiposQuarto GetById(int id)
        {
            MySqlDataReader reader = null;
            try
            {
                var query = conn.Query();
                query.CommandText = "SELECT id_tipo_quarto, nome_tip, descricao_tip FROM TiposQuarto WHERE id_tipo_quarto = @id";
                query.Parameters.AddWithValue("@id", id);

                reader = query.ExecuteReader();
                TiposQuarto tipo = null;

                if (reader.Read())
                {
                    tipo = new TiposQuarto();
                    tipo.Id = reader.GetInt32("id_tipo_quarto");
                    tipo.Nome = reader.GetString("nome_tip");
                    tipo.Descricao = reader.IsDBNull(reader.GetOrdinal("descricao_tip")) ? string.Empty : reader.GetString("descricao_tip");
                }

                return tipo;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar tipo de quarto por ID.", ex);
            }
            finally
            {
                reader?.Close();
                conn.Close();
            }
        }

        public List<TiposQuarto> List()
        {
            MySqlDataReader reader = null;
            try
            {
                var lista = new List<TiposQuarto>();
                var query = conn.Query();
                query.CommandText = "SELECT id_tipo_quarto, nome_tip, descricao_tip FROM TiposQuarto";

                reader = query.ExecuteReader();
                while (reader.Read())
                {
                    var tipo = new TiposQuarto
                    {
                        Id = reader.GetInt32("id_tipo_quarto"),
                        Nome = reader.GetString("nome_tip"),
                        Descricao = reader.IsDBNull(reader.GetOrdinal("descricao_tip")) ? string.Empty : reader.GetString("descricao_tip")
                    };
                    lista.Add(tipo);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar tipos de quarto.", ex);
            }
            finally
            {
                reader?.Close();
                conn.Close();
            }
        }

        public void Insert(TiposQuarto tipo)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "INSERT INTO TiposQuarto (nome_tip, descricao_tip) VALUES (@nome, @descricao)";
                query.Parameters.AddWithValue("@nome", tipo.Nome);
                query.Parameters.AddWithValue("@descricao", tipo.Descricao);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("Tipo de quarto não inserido. Verifique os dados.");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir tipo de quarto.", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public void Update(TiposQuarto tipo)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "UPDATE TiposQuarto SET nome_tip = @nome, descricao_tip = @descricao WHERE id_tipo_quarto = @id";
                query.Parameters.AddWithValue("@nome", tipo.Nome);
                query.Parameters.AddWithValue("@descricao", tipo.Descricao);
                query.Parameters.AddWithValue("@id", tipo.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("Tipo de quarto não atualizado.");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar tipo de quarto.", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public void Delete(TiposQuarto tipo)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "DELETE FROM TiposQuarto WHERE id_tipo_quarto = @id";
                query.Parameters.AddWithValue("@id", tipo.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("Tipo de quarto não excluído.");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir tipo de quarto.", ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
