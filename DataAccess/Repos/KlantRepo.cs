using DataAccess.Exceptions;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Domain.Interfaces;

namespace DataAccess.Repos
{
    public class KlantRepo : IKlantRepo
    {
        private readonly string _connectionString;

        public KlantRepo(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("defaultConnection");

        }



        public List<Klant> ZoekKlanten(int id, string naam, string adres)
        {
            using var con = new SqlConnection(_connectionString);
            try
            {
                var cmd = new SqlCommand();
                var query = "Select * from dbo.klanten k left join bestellingen b on k.Id = b.KlantId where ";

                bool next = false;
                if (id > 0)
                {
                    query += "k.Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    next = true;
                }

                if (!string.IsNullOrWhiteSpace(naam))
                {
                    if (next) query += " AND ";
                    query += "k.Naam=@Naam";
                    cmd.Parameters.AddWithValue("@Naam", naam);
                    next = true;
                }

                if (!string.IsNullOrWhiteSpace(adres))
                {
                    if (next) query += " AND ";
                    query += "k.Adres=@Adres";
                    cmd.Parameters.AddWithValue("@Adres", adres);
                }

                cmd.Connection = con;
                cmd.CommandText = query;
                con.Open();
                var reader = cmd.ExecuteReader();
                if (!reader.HasRows) throw new KlantRepoException(nameof(ZoekKlanten) + " - Geen klanten gevonden");
                var klanten = new List<Klant>();
                Klant klant = null;
                while (reader.Read())
                {
                    if (klanten.All(k => k.KlantId != (int)reader[0]))
                    {
                        klant = new Klant((int)reader[0], (string)reader[1], (string)reader[2],
                            new List<Bestelling>());

                        klanten.Add(klant);

                        if (reader[3] != DBNull.Value)
                        {
                            klant.VoegToeBestelling(new Bestelling((int)reader[3], klant, (DateTime)reader[4]));
                        }
                    }
                    else
                    {
                        if (reader[3] != DBNull.Value)
                        {
                            klant?.VoegToeBestelling(new Bestelling((int)reader[3], klant, (DateTime)reader[4]));
                        }
                    }
                }

                return klanten;
            }
            catch (Exception e)
            {
                throw new KlantRepoException(nameof(ZoekKlanten) + " - Er ging iets mis", e);
            }
        }



        public bool BestaatKlant(int id)
        {

            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                var cmd = new SqlCommand("SELECT *  FROM dbo.Klanten WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", id);
                var reader = cmd.ExecuteReader();
                return reader.HasRows;
            }
            catch (Exception e)
            {
                throw new KlantRepoException(nameof(BestaatKlant) + " - Er ging iets mis", e);
            }
            finally
            {
                con.Close();
            }
        }

        public int VoegKlantToe(Klant klant)
        {
            var connection = new SqlConnection(_connectionString);
            const string query = "INSERT INTO dbo.Klanten (Naam,Adres) OUTPUT INSERTED.Id values (@Naam,@Adres)  ";
            try
            {
                using var command = connection.CreateCommand();
                connection.Open();
                command.Parameters.AddWithValue("@Naam", klant.Naam);
                command.Parameters.AddWithValue("@Adres", klant.Adres);
                command.CommandText = query;
                return (int)command.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw new KlantRepoException(nameof(VoegKlantToe) + " - Er ging iets mis", e);
            }
            finally
            {
                connection.Close();
            }
        }

        public void UpdateKlant(Klant klant)
        {
            var connection = new SqlConnection(_connectionString);
            const string query = "Update dbo.Klanten Set Naam=@Naam, Adres=@Adres  where Id = @Id  ";
            try
            {
                using var command = connection.CreateCommand();
                connection.Open();
                command.Parameters.AddWithValue("@Id", klant.KlantId);
                command.Parameters.AddWithValue("@Naam", klant.Naam);
                command.Parameters.AddWithValue("@Adres", klant.Adres);
                command.CommandText = query;
                command.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw new KlantRepoException(nameof(UpdateKlant) + " - Er ging iets mis", e);
            }
            finally
            {
                connection.Close();
            }
        }

        public void VerwijderKlant(Klant klant)
        {
            var connection = new SqlConnection(_connectionString);
            const string query = "Delete from dbo.Klanten where Id = @Id  ";
            try
            {
                using var command = connection.CreateCommand();
                connection.Open();
                command.Parameters.AddWithValue("@Id", klant.KlantId);

                command.CommandText = query;
                command.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw new KlantRepoException(nameof(VerwijderKlant) + " - Er ging iets mis", e);
            }
            finally
            {
                connection.Close();
            }
        }

        public List<Klant> GeefAlleKlanten()
        {
            using var con = new SqlConnection(_connectionString);
            try
            {
                var cmd = new SqlCommand();
                var query = "Select * from dbo.klanten k left join bestellingen b on k.Id = b.KlantId  ";

                cmd.Connection = con;
                cmd.CommandText = query;
                con.Open();
                var reader = cmd.ExecuteReader();
                if (!reader.HasRows) throw new KlantRepoException(nameof(ZoekKlanten) + " - Geen klanten gevonden");
                var klanten = new List<Klant>();
                Klant klant = null;
                while (reader.Read())
                {
                    if (klanten.All(k => k.KlantId != (int)reader[0]))
                    {
                        klant = new Klant((int)reader[0], (string)reader[1], (string)reader[2],
                            new List<Bestelling>());

                        klanten.Add(klant);

                        if (reader[3] != DBNull.Value)
                        {
                           new Bestelling((int)reader[3], klant, (DateTime)reader[4]);
                        }
                    }
                    else
                    {
                        if (reader[3] != DBNull.Value)
                        {
                            new Bestelling((int)reader[3], klant, (DateTime)reader[4]);
                        }
                    }
                }

                return klanten;
            }
            catch (Exception e)
            {
                throw new KlantRepoException(nameof(ZoekKlanten) + " - Er ging iets mis", e);
            }
        }
    }

}