using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using DataAccess.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Repos
{
    public class ClubRepo : IClubRepo
    {
        private readonly string _connectionString;

        public ClubRepo(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("defaultConnection");
        }

        public List<Club> ZoekClubs(int id, string naam, string competitie)
        {
            using var con = new SqlConnection(_connectionString);
            try
            {
                var cmd = new SqlCommand();
                var query = "Select * from dbo.Clubs where ";

                bool next = false;
                if (id > 0)
                {
                    query += "Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    next = true;
                }

                if (!string.IsNullOrWhiteSpace(naam))
                {
                    if (next) query += " AND ";
                    query += "Naam=@Naam";
                    cmd.Parameters.AddWithValue("@Naam", naam);
                    next = true;
                }

                if (!string.IsNullOrWhiteSpace(competitie))
                {
                    if (next) query += " AND ";
                    query += "Competitie=@Competitie";
                    cmd.Parameters.AddWithValue("@Competitie", competitie);
                }

                cmd.Connection = con;
                cmd.CommandText = query;
                con.Open();
                var reader = cmd.ExecuteReader();
                if (!reader.HasRows) throw new ClubRepoException(nameof(ZoekClubs) + " - Geen clubs gevonden");
                var clubs = new List<Club>();
                while (reader.Read())
                {
                    clubs.Add(new Club((int)reader[0],(string)reader[1],(string)reader[2]));
                }
                return clubs;
            }
            catch (Exception e)
            {
                throw new ClubRepoException(nameof(ZoekClubs) + " - Er ging iets mis", e);
            }
        }

        public List<Club> GeefAlleClubs()
        {
            using var con = new SqlConnection(_connectionString);
            try
            {
                var cmd = new SqlCommand();
                var query = "Select * from dbo.Clubs ";
                cmd.Connection = con;
                cmd.CommandText = query;
                con.Open();
                var reader = cmd.ExecuteReader();
                if (!reader.HasRows) throw new KlantRepoException(nameof(ZoekClubs) + " - Geen clubs gevonden");
                var clubs = new List<Club>();
                while (reader.Read())
                {
                    clubs.Add(new Club((int)reader[0], (string)reader[1], (string)reader[2]));
                }
                return clubs;
            }
            catch (Exception e)
            {
                throw new KlantRepoException(nameof(GeefAlleClubs) + " - Er ging iets mis", e);
            }
        }

        public bool BestaatClub(int id)
        {

            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                var cmd = new SqlCommand("SELECT *  FROM dbo.Clubs WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", id);
                var reader = cmd.ExecuteReader();
                return reader.HasRows;
            }
            catch (Exception e)
            {
                throw new ClubRepoException(nameof(BestaatClub) + " - Er ging iets mis", e);
            }
            finally
            {
                con.Close();
            }
        }

        public int VoegClubToe(Club club)
        {
            var connection = new SqlConnection(_connectionString);
            const string query = "INSERT INTO dbo.Clubs (Naam,Competitie) OUTPUT INSERTED.Id values (@Naam,@Competitie)  ";
            try
            {
                using var command = connection.CreateCommand();
                connection.Open();
                command.Parameters.AddWithValue("@Naam", club.Naam);
                command.Parameters.AddWithValue("@Competitie", club.Competitie);
                command.CommandText = query;
                return (int)command.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw new ClubRepoException(nameof(VoegClubToe) + " - Er ging iets mis", e);
            }
            finally
            {
                connection.Close();
            }
        }

        public void UpdateClub(Club club)
        {
            var connection = new SqlConnection(_connectionString);
            const string query = "Update dbo.Clubs Set Naam=@Naam, Competitie=@Competitie  where Id = @Id  ";
            try
            {
                using var command = connection.CreateCommand();
                connection.Open();
                command.Parameters.AddWithValue("@Id", club.Id);
                command.Parameters.AddWithValue("@Naam", club.Naam);
                command.Parameters.AddWithValue("@Competitie", club.Competitie);
                command.CommandText = query;
                command.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw new ClubRepoException(nameof(UpdateClub) + " - Er ging iets mis", e);
            }
            finally
            {
                connection.Close();
            }
        }

        public void VerwijderClub(int id)
        {
            var connection = new SqlConnection(_connectionString);
            const string query = "Delete from dbo.Clubs where Id = @Id  ";
            try
            {
                using var command = connection.CreateCommand();
                connection.Open();
                command.Parameters.AddWithValue("@Id", id);

                command.CommandText = query;
                command.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw new KlantRepoException(nameof(VerwijderClub) + " - Er ging iets mis", e);
            }
            finally
            {
                connection.Close();
            }
        }

    }


}