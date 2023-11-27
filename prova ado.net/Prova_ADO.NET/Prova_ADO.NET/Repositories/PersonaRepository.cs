using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Prova_ADO.NET.Interfaces;
using Prova_ADO.NET.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Prova_ADO.NET.Repositories
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<PersonaRepository> _logger;

        public PersonaRepository(ILogger<PersonaRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public List<Persona> GetPersone()
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            try
            {
                connection.Open();
                string query = "SELECT * FROM Persone;";
                SqlCommand cmd = new SqlCommand(query, connection);
                List<Persona> list = new List<Persona>();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Persona p = new Persona();
                    p.Id = reader.GetInt64(0);
                    p.Nome = reader.GetString(1);
                    p.Cognome = reader.GetString(2);
                    p.Professione = reader.GetString(3);
                    list.Add(p);
                }

                return list;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Errore durante la lettura dei dati: {ex.Message}");
                return new List<Persona>();
            }
            finally
            {
                connection.Close();
            }
        }

        public Persona GetPersonaById(long id)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            try
            {
                connection.Open();
                string query = "SELECT * FROM Persone WHERE Id=@Id;";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", id);
                Persona persona = new Persona();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    persona.Id = reader.GetInt64(0);
                    persona.Nome = reader.GetString(1);
                    persona.Cognome = reader.GetString(2);
                    persona.Professione = reader.GetString(3);
                    return persona;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Errore durante la lettura dei dati: {ex.Message}");
                return null; 
            }
            finally
            {
                connection.Close();
            }
        }

        public bool InserisciPersona(Persona persona)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            try
            {
                connection.Open();
                string query = "INSERT INTO Persone (Nome, Cognome, Professione) VALUES (@Nome, @Cognome, @Professione);";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Nome", persona.Nome);
                cmd.Parameters.AddWithValue("@Cognome", persona.Cognome);
                cmd.Parameters.AddWithValue("@Professione", persona.Professione);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Errore durante l'inserimento dei dati: {ex.Message}");
                return false; 
            }
            finally
            {
                connection.Close();
            }
        }

        public bool ModificaPersona(long id, Persona persona)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            try
            {
                connection.Open();
                string query = "UPDATE Persone SET Nome=@Nome, Cognome=@Cognome, Professione=@Professione WHERE ID=@Id;";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Nome", persona.Nome);
                cmd.Parameters.AddWithValue("@Cognome", persona.Cognome);
                cmd.Parameters.AddWithValue("@Professione", persona.Professione);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Errore durante la modifica dei dati: {ex.Message}");
                return false; 
            }
            finally
            {
                connection.Close();
            }
        }

        public bool EliminaPersona(long id)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            try
            {
                connection.Open();
                string query = "DELETE FROM Persone WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", id);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Errore durante la cancellazione dei dati: {ex.Message}");
                return false; 
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
