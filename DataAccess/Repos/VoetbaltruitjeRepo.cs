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
    public class VoetbaltruitjeRepo : IVoetbaltruitjeRepo
    {

        private readonly string _connectionString;

        public VoetbaltruitjeRepo(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("defaultConnection");
        }

        public List<Voetbaltruitje> ZoekVoetbaltruitjes(int id, string competitie, string club, double prijs,
            string seizoen, int versie, bool thuis, bool uit, Kledingmaat maat)
        {
            using var con = new SqlConnection(_connectionString);
            try
            {
                var cmd = new SqlCommand();
                var query =
                    "Select t.Id, t.Kledingmaat, t.Seizoen, t.Prijs, t.Thuis, t.Versie, t.ClubId , c.Naam , c.Competitie from dbo.truitjes t left join dbo.clubs c on t.ClubId = c.id where ";

                bool next = false;
                if (id > 0)
                {
                    query += "t.Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    next = true;
                }
                if (!string.IsNullOrWhiteSpace(competitie))
                {
                    if (next) query += " AND ";
                    query += "c.Competitie=@Competitie";
                    cmd.Parameters.AddWithValue("@Competitie", competitie);
                    next = true;
                }
                if (!string.IsNullOrWhiteSpace(club))
                {
                    if (next) query += " AND ";
                    query += "c.Naam=@Naam";
                    cmd.Parameters.AddWithValue("@Naam", club);
                    next = true;
                }

                if (prijs != 0)
                {
                    if (next) query += " AND ";
                    query += "t.Prijs=@Prijs";
                    cmd.Parameters.AddWithValue("@Prijs", prijs);
                    next = true;
                }
                if (versie != 0)
                {
                    if (next) query += " AND ";
                    query += "t.Versie=@Versie";
                    cmd.Parameters.AddWithValue("@Versie", versie);
                    next = true;
                }
                if (!string.IsNullOrWhiteSpace(seizoen))
                {
                    if (next) query += " AND ";
                    query += "t.Seizoen=@Seizoen";
                    cmd.Parameters.AddWithValue("@Seizoen", seizoen);
                    next = true;
                }

                if (thuis)
                {
                    if (next) query += " AND ";
                    query += "t.Thuis=@Thuis";
                    cmd.Parameters.AddWithValue("@Thuis", true);
                    next = true;
                }
                if (uit)
                {
                    if (next) query += " AND ";
                    query += "t.Thuis=@Thuis";
                    cmd.Parameters.AddWithValue("@Thuis", false);
                    next = true;
                }

                if (maat != 0)
                {
                    if (next) query += " AND ";
                    query += "t.Kledingmaat=@Kledingmaat";
                    cmd.Parameters.AddWithValue("@Kledingmaat",maat);
                    next = true;
                }
                if (!next)
                {
                    throw new VoetbaltruitjeRepoException(nameof(ZoekVoetbaltruitjes) +
                                                          " - Er zijn geen parameters meegegeven");
                }

                cmd.Connection = con;
                cmd.CommandText = query;
                con.Open();
                var reader = cmd.ExecuteReader();
                if (!reader.HasRows) throw new VoetbaltruitjeRepoException(nameof(ZoekVoetbaltruitjes) + " - Geen klanten gevonden");
                List<Voetbaltruitje> voetbaltruitjes = new List<Voetbaltruitje>();
                while (reader.Read())
                {
                    if (voetbaltruitjes.All(v=> v.Id != (int)reader["Id"]))
                    {
                        var voetbaltruitje = new Voetbaltruitje((int)reader["Id"],
                            new Club((int)reader["ClubId"] ,(string)reader["Competitie"], (string) reader["Naam"]),
                            (string) reader["Seizoen"],
                            (double) reader["Prijs"],
                            (Kledingmaat) reader["Kledingmaat"],
                            new ClubSet((bool) reader["Thuis"], (int) reader["Versie"]));
                        voetbaltruitjes.Add(voetbaltruitje);
                    }
                }

                return voetbaltruitjes;

            }
            catch (Exception e)
            {
                throw new VoetbaltruitjeRepoException(nameof(ZoekVoetbaltruitjes) + " - Er ging iets mis",e);
            }
        }

        public bool BestaatVoetbalTruitje(int id)
        {
            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                var cmd = new SqlCommand("SELECT *  FROM dbo.truitjes WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", id);
                var reader = cmd.ExecuteReader();
                return reader.HasRows;
            }
            catch (Exception e)
            {
                throw new VoetbaltruitjeRepoException(nameof(BestaatVoetbalTruitje) + " - Er ging iets mis", e);
            }
            finally
            {
                con.Close();
            }
        }


        public int VoegVoetbaltruitjeToe(Voetbaltruitje voetbaltruitje)
        {
            var connection = new SqlConnection(_connectionString);
            const string query = "INSERT INTO dbo.truitjes (Kledingmaat,Seizoen,Prijs,Thuis,Versie,ClubId) OUTPUT INSERTED.Id values (@Kledingmaat,@Seizoen,@Prijs,@Thuis,@Versie,@ClubId)  ";
            try
            {
                using var command = connection.CreateCommand();
                connection.Open();
                command.Parameters.AddWithValue("@Kledingmaat", voetbaltruitje.Kledingmaat);
                command.Parameters.AddWithValue("@Seizoen", voetbaltruitje.Seizoen);
                command.Parameters.AddWithValue("@Prijs", voetbaltruitje.Prijs);
                command.Parameters.AddWithValue("@Thuis", voetbaltruitje.ClubSet.Thuis);
                command.Parameters.AddWithValue("@Versie", voetbaltruitje.ClubSet.Versie);
                command.Parameters.AddWithValue("@ClubId", voetbaltruitje.Club.Id);
                command.CommandText = query;
                return (int)command.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw new VoetbaltruitjeRepoException(nameof(VoegVoetbaltruitjeToe) + " - Er ging iets mis", e);
            }
            finally
            {
                connection.Close();
            }
        }

        public void UpdateVoetbalTruitje(Voetbaltruitje voetbaltruitje)
        {
            var connection = new SqlConnection(_connectionString);
            const string query = "Update dbo.truitjes Set Kledingmaat=@Maat, Seizoen=@Seizoen, Prijs=@Prijs, Thuis=@Thuis, Versie=@Versie, ClubId=@ClubId  where Id = @Id  ";
            try
            {
                using var command = connection.CreateCommand();
                connection.Open();
                command.Parameters.AddWithValue("@Maat", voetbaltruitje.Kledingmaat);
                command.Parameters.AddWithValue("@Id", voetbaltruitje.Id);
                command.Parameters.AddWithValue("@Seizoen", voetbaltruitje.Seizoen);
                command.Parameters.AddWithValue("@Prijs", voetbaltruitje.Prijs);
                command.Parameters.AddWithValue("@Thuis", voetbaltruitje.ClubSet.Thuis);
                command.Parameters.AddWithValue("@Versie", voetbaltruitje.ClubSet.Versie);
                command.Parameters.AddWithValue("@ClubId", voetbaltruitje.Club.Id);
                command.CommandText = query;
                command.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw new VoetbaltruitjeRepoException(nameof(UpdateVoetbalTruitje) + " - Er ging iets mis", e);
            }
            finally
            {
                connection.Close();
            }
        }


        public void VerwijderVoetbaltruitje(int id)
        {
            var connection = new SqlConnection(_connectionString);
            const string query = "Delete from dbo.truitjes where Id = @Id  ";
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
                throw new VoetbaltruitjeRepoException(nameof(VerwijderVoetbaltruitje) + " - Er ging iets mis", e);
            }
            finally
            {
                connection.Close();
            }
        }


        public List<Voetbaltruitje> GeefAlleVoetbaltruitjes()
        {
            using var con = new SqlConnection(_connectionString);
            try
            {
                var cmd = new SqlCommand();
                var query =
                    "Select t.Id, t.Kledingmaat, t.Seizoen, t.Prijs, t.Thuis, t.Versie, t.ClubId , c.Naam , c.Competitie from dbo.truitjes t left join dbo.clubs c on t.ClubId = c.id";

                cmd.Connection = con;
                cmd.CommandText = query;
                con.Open();
                var reader = cmd.ExecuteReader();
                if (!reader.HasRows) throw new VoetbaltruitjeRepoException(nameof(ZoekVoetbaltruitjes) + " - Geen klanten gevonden");
                List<Voetbaltruitje> voetbaltruitjes = new List<Voetbaltruitje>();
                while (reader.Read())
                {
                    if (voetbaltruitjes.All(v => v.Id != (int)reader["Id"]))
                    {
                        var voetbaltruitje = new Voetbaltruitje((int)reader["Id"],
                            new Club((int)reader["ClubId"], (string)reader["Competitie"], (string)reader["Naam"]),
                            (string)reader["Seizoen"],
                            (double)reader["Prijs"],
                            (Kledingmaat)reader["Kledingmaat"],
                            new ClubSet((bool)reader["Thuis"], (int)reader["Versie"]));
                        voetbaltruitjes.Add(voetbaltruitje);
                    }
                }

                return voetbaltruitjes;

            }
            catch (Exception e)
            {
                throw new VoetbaltruitjeRepoException(nameof(ZoekVoetbaltruitjes) + " - Er ging iets mis",e);
            }
        }


    }
}