using System;
using System.Collections.Generic;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Domain.Models
{
    public class Klant : IKlant
    {
        public int KlantId { get; private set; }
        public string Naam { get; private set; }
        public string Adres { get; private set; }
        private readonly List<Bestelling> _bestellingen = new List<Bestelling>();


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
            if (naam.Trim().Length < 1) throw new KlantException("Klant naam invalid");
            Naam = naam;
        }
        public void ZetAdres(string adres)
        {
            if (adres.Trim().Length < 5) throw new KlantException("Klant adres invalid");
            Adres = adres;
        }
        public IReadOnlyList<Bestelling> GetBestellingen()
        {
            return _bestellingen.AsReadOnly();
        }
        public void VerwijderBestelling(Bestelling bestelling)
        {
            if (bestelling == null) throw new KlantException("Klant : VerwijderBestelling - bestelling is null");
            if (!_bestellingen.Contains(bestelling))
            {
                throw new KlantException("Klant : RemoveBestelling - bestelling does not exists");
            }
            else
            {
                if (Equals(bestelling.Klant, this))
                    bestelling.VerwijderKlant();
            }
        }
        public void VoegToeBestelling(Bestelling bestelling)
        {
            if (bestelling == null) throw new KlantException("Klant : VerwijderBestelling - bestelling is null");
            if (_bestellingen.Contains(bestelling))
            {
                throw new KlantException("Klant : AddBestelling - bestelling already exists");
            }
            else
            {
                _bestellingen.Add(bestelling);
                if (!Equals(bestelling.Klant, this))
                    bestelling.ZetKlant(this);
            }
        }
        public bool HeeftBestelling(Bestelling bestelling)
        {
            if (_bestellingen.Contains(bestelling)) return true;
            else return false;
        }
        public int Korting() //procent
        {
            if (_bestellingen.Count <= 5) return 0;
            if (_bestellingen.Count <= 10) return 10;
            else return 20;
        }
        public override string ToString()
        {
            //return $"[Klant] {KlantId},{Naam},{Adres},{_bestellingen.Count}";
            string res = $"[Klant] {KlantId},{Naam},{Adres},{_bestellingen.Count}";
            foreach (var x in _bestellingen)
            {
                res += $"\n{x}";
            }
            return res;
        }
        public string ToText(bool kort = true)
        {
            if (kort)
                return $"[Klant] {KlantId},{Naam},{Adres},{_bestellingen.Count}";
            else
            {
                string res = $"[Klant] {KlantId},{Naam},{Adres},{_bestellingen.Count}";
                foreach (var x in _bestellingen)
                {
                    res += $"\n{x}";
                }
                return res;
            }
        }
        public void Show()
        {
            Console.WriteLine(this);
            foreach (Bestelling b in _bestellingen) Console.WriteLine($"    bestelling:{b}");
        }

        public override bool Equals(object obj)
        {
            return obj is Klant klant &&
                   Naam == klant.Naam &&
                   Adres == klant.Adres;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Naam, Adres);
        }
    }
}
