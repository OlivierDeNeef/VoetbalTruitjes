using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Domain.Models
{
    public class Klant : IKlant
    {
        public int KlantId { get; private set; }
        public string Naam { get; private set; }
        public string Adres { get; private set; }

        private  List<Bestelling> _bestellingen = new();


        public Klant(int klantId, string naam, string adres, List<Bestelling> bestellingen) : this(klantId, naam, adres)
        {
            _bestellingen = bestellingen ?? throw new KlantException("Klant - bestellingen null");
            foreach (var bestelling in bestellingen) bestelling.ZetKlant(this);
        }
        public Klant(int klantId, string naam, string adres) : this (naam,adres)
        {
            ZetKlantId(klantId);
        }

        public Klant(string naam, string adres)
        {
            ZetNaam(naam);
            ZetAdres(adres);
        }

        public void ZetKlantId(int id)
        {
            if (id <= 0) throw new KlantException("Klant - invalid id");
            KlantId = id;
        }
        public void ZetNaam(string naam)
        {
            if (string.IsNullOrWhiteSpace(naam)) throw new KlantException("Klant - naam invalid");
            Naam = naam;
        }
        public void ZetAdres(string adres)
        {
            if (adres is null || adres.Trim().Length < 5) throw new KlantException("Klant - adres invalid");
            Adres = adres;
        }
        public IReadOnlyList<Bestelling> GetBestellingen()
        {
            return _bestellingen.AsReadOnly();
        }
        public void VerwijderBestelling(Bestelling bestelling)
        {
            if (bestelling == null) throw new KlantException("Klant : VerwijderBestelling - bestelling is null");
            if (!_bestellingen.Contains(bestelling)) throw new KlantException("Klant : RemoveBestelling - bestelling does not exists");
            if (Equals(bestelling.Klant, this))
            {
                bestelling.VerwijderKlant();
            }
            
            _bestellingen.Remove(bestelling);

        }

        public void ZetBestelling(List<Bestelling> bestellingen)
        {
            if (bestellingen == null) throw new KlantException("Klant : ZetBestellingen - bestellingen is null");
            _bestellingen = bestellingen;
        }

        public void VoegToeBestelling(Bestelling bestelling)
        {
            if (bestelling == null) throw new KlantException("Klant : VoegToeBestelling - bestelling is null");
            if (HeeftBestelling(bestelling)) throw new KlantException("Klant : AddBestelling - bestelling already exists");
            _bestellingen.Add(bestelling);
            if (!Equals(bestelling.Klant, this)) bestelling.ZetKlant(this);
            
        }
        public bool HeeftBestelling(Bestelling bestelling)
        {
            if (bestelling == null) throw new KlantException("Klant : HeeftBestelling - bestelling is null");
            return _bestellingen.Contains(bestelling);
        }
        public int Korting()
        {
            return _bestellingen.Count switch
            {
                < 5 => 0,
                < 10 => 10,
                _ => 20
            };
        }
        public override string ToString()
        {
           
            return $"[Id] {KlantId}, [Naam] {Naam}, [Adres] {Adres}, [Aantal bestellingen] {_bestellingen.Count}";
            
        }
        
        public void Show()
        {
            Console.WriteLine(this);
            foreach (var bestelling in _bestellingen) Console.WriteLine($"bestelling:{bestelling}"); 
        }

        protected bool Equals(Klant other)
        {
            return KlantId == other.KlantId && Naam == other.Naam && Adres == other.Adres;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Klant)) return false;
            return Equals((Klant) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(KlantId, Naam, Adres);
        }

        public static bool operator ==(Klant left, Klant right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Klant left, Klant right)
        {
            return !Equals(left, right);
        }
    }
}
