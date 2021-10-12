using System;
using System.Collections.Generic;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Domain.Models
{
    public class Bestelling : IBestelling
    {
        public int BestellingId { get; private set; }
        public bool Betaald { get; private set; }
        public double Prijs { get; private set; }
        public Klant Klant { get; private set; }
        public DateTime Tijdstip { get; private set; }
        private Dictionary<Voetbaltruitje, int> _producten = new Dictionary<Voetbaltruitje, int>();

        public Bestelling(int bestellingId, DateTime tijdstip) : this(tijdstip)
        {
            ZetBestellingId(bestellingId);
        }
        public Bestelling(int bestellingId, Klant klant, DateTime tijdstip)
        {
            ZetKlant(klant);
        }
        public Bestelling(int bestellingId, Klant klant, DateTime tijdstip, Dictionary<Voetbaltruitje, int> producten) : this(bestellingId, klant, tijdstip)
        {
            if (producten is null) throw new BestellingException("producten zijn leeg");
            _producten = producten;
        }
        //constructor voor inlezen
        public Bestelling(int bestellingId, Klant klant, DateTime tijdstip, double prijs, bool betaald, Dictionary<Voetbaltruitje, int> producten) : this(bestellingId, klant, tijdstip, producten)
        {
            Prijs = prijs;
            Betaald = betaald;
        }
        public Bestelling(DateTime tijdstip)
        {
            ZetTijdstip(tijdstip);
            Betaald = false;
        }

        public void VoegProductToe(Voetbaltruitje voetbaltruitje, int aantal)
        {
            if (aantal <= 0) throw new BestellingException("VoegVoetbaltruitjeToe - aantal");
            if (_producten.ContainsKey(voetbaltruitje))
            {
                _producten[voetbaltruitje] += aantal;
            }
            else
            {
                _producten.Add(voetbaltruitje, aantal);
            }
        }
        public void VerwijderProduct(Voetbaltruitje voetbaltruitje, int aantal)
        {
            if (aantal <= 0) throw new BestellingException("VerwijderVoetbaltruitje - aantal");
            if (!_producten.ContainsKey(voetbaltruitje))
            {
                throw new BestellingException("VerwijderVoetbaltruitje - product niet beschikbaar");
            }
            else
            {
                if (_producten[voetbaltruitje] < aantal)
                {
                    throw new BestellingException("VerwijderVoetbaltruitje - beschikbaar aantal te klein");
                }
                else
                {
                    _producten[voetbaltruitje] -= aantal;
                }
            }
        }
        public IReadOnlyDictionary<Voetbaltruitje, int> GeefProducten()
        {
            return _producten;
        }
        public double Kostprijs() //procent
        {
            double prijs = 0.0;
            int korting;
            if (Klant is null)
            {
                korting = 0;
            }
            else
            {
                korting = Klant.Korting();
            }
            foreach (KeyValuePair<Voetbaltruitje, int> kvp in _producten)
            {
                prijs += kvp.Key.Prijs * kvp.Value * (100.0 - korting);
            }
            return prijs;
        }
        public void VerwijderKlant()
        {
            Klant = null;
        }
        public void ZetKlant(Klant newKlant)
        {
            if (newKlant == null) throw new BestellingException("Bestelling - invalid klant");
            if (newKlant == Klant) throw new BestellingException("Bestelling - ZetKlant - not new");
            if (Klant != null)
                if (Klant.HeeftBestelling(this))
                    Klant.VerwijderBestelling(this);
            if (!newKlant.HeeftBestelling(this))
                newKlant.VoegToeBestelling(this);
            Klant = newKlant;
        }
        public void ZetBestellingId(int id)
        {
            if (id <= 0) throw new BestellingException("Bestelling - invalid id");
            BestellingId = id;
        }
        public void ZetTijdstip(DateTime tijdstip)
        {
            if (tijdstip == null) throw new BestellingException("Bestelling - invalid tijdstip");
            Tijdstip = tijdstip;
        }
        public void ZetBetaald(bool betaald = true)
        {
            Betaald = betaald;
            if (betaald)
            {
                Prijs = Kostprijs();
            }
            else
            {
                Prijs = 0.0;
            }
        }
 
        public override string ToString()
        {
            string res = $"[Bestelling] {BestellingId},{Betaald},{Prijs},{Tijdstip},{Klant.KlantId},{Klant.Naam},{Klant.Adres},{_producten.Count}";
            foreach (var p in _producten)
            {
                res += $"\n {p}";
            }
            return res;
        }
        public void Show()
        {
            Console.WriteLine(this);
            foreach (KeyValuePair<Voetbaltruitje, int> kvp in _producten)
                Console.WriteLine($"    product:{kvp.Key},{kvp.Value}");
        }
        public override bool Equals(object obj)
        {
            return obj is Bestelling bestelling &&
                   BestellingId == bestelling.BestellingId &&
                   Betaald == bestelling.Betaald &&
                   Prijs == bestelling.Prijs &&
                   EqualityComparer<Klant>.Default.Equals(Klant, bestelling.Klant) &&
                   Tijdstip == bestelling.Tijdstip &&
                   EqualityComparer<Dictionary<Voetbaltruitje, int>>.Default.Equals(_producten, bestelling._producten);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(BestellingId, Betaald, Prijs, Klant, Tijdstip, _producten);
        }
    }
}
