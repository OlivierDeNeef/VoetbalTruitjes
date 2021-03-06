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
        private readonly Dictionary<Voetbaltruitje, int> _producten = new();

        public Bestelling(int bestellingId, DateTime tijdstip) : this(tijdstip)
        {
            ZetBestellingId(bestellingId);
        }
        public Bestelling(int bestellingId, Klant klant, DateTime tijdstip) : this(bestellingId, tijdstip) 
        {
            ZetKlant(klant);
        }
        public Bestelling(int bestellingId, Klant klant, DateTime tijdstip, Dictionary<Voetbaltruitje, int> producten) : this(bestellingId, klant, tijdstip)
        {
            _producten = producten ?? throw new BestellingException("producten zijn leeg");
        }
        //constructor voor inlezen
        public Bestelling(int bestellingId, Klant klant, DateTime tijdstip, double prijs, bool betaald, Dictionary<Voetbaltruitje, int> producten) : this(bestellingId, klant, tijdstip, producten)
        {
            Prijs = prijs; //Todo zetprijs
            ZetBetaald(betaald);
        }
        public Bestelling(DateTime tijdstip)
        {
            ZetTijdstip(tijdstip);
            
        }

        public void VoegProductToe(Voetbaltruitje voetbaltruitje, int aantal)
        {
            if (voetbaltruitje == null) throw new KlantException("VoegVoetbaltruitjeToe - voetbaltruitje = null");
            if (aantal < 1) throw new BestellingException("VoegVoetbaltruitjeToe - aantal"); 
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
            if (voetbaltruitje == null) throw new KlantException("VoegVoetbaltruitjeToe - voetbaltruitje = null");
            if (aantal <= 0) throw new BestellingException("VerwijderVoetbaltruitje - aantal");
            if (!_producten.ContainsKey(voetbaltruitje)) throw new BestellingException("VerwijderVoetbaltruitje - product niet beschikbaar");
            if (_producten[voetbaltruitje] < aantal) throw new BestellingException("VerwijderVoetbaltruitje - beschikbaar aantal te klein");
            
            _producten[voetbaltruitje] -= aantal;
            if (_producten[voetbaltruitje] == 0) _producten.Remove(voetbaltruitje);
        }
        public IReadOnlyDictionary<Voetbaltruitje, int> GeefProducten()
        {
            return _producten;
        }
        public double Kostprijs() //procent
        {
            var prijs = 0.0;
            var korting = Klant?.Korting() ?? 0;
            foreach (var (voetbaltruitje, aantal) in _producten)
            {
                prijs += voetbaltruitje.Prijs * aantal * (100.0 - korting);
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
            if (newKlant.Equals(Klant)) throw new BestellingException("Bestelling - ZetKlant - not new");
            if (Klant != null)
            {
                if (Klant.HeeftBestelling(this)) Klant.VerwijderBestelling(this);
            }
            if (!newKlant.HeeftBestelling(this)) newKlant.VoegToeBestelling(this);
            Klant = newKlant;
        }
        public void ZetBestellingId(int id)
        {
            if (id <= 0) throw new BestellingException("Bestelling - invalid id");
            BestellingId = id;
        }
        public void ZetTijdstip(DateTime tijdstip)
        {
            Tijdstip = tijdstip;
        }
        public void ZetBetaald(bool betaald = true)
        {
            Betaald = betaald;
            Prijs = betaald ? Kostprijs() : 0.0;
        }
 
        public override string ToString()
        {
            var res = $"[Bestelling] {BestellingId},{Betaald},{Prijs},{Tijdstip},{Klant.KlantId},{Klant.Naam},{Klant.Adres},{_producten.Count}";
            return _producten.Aggregate(res, (current, p) => current + $"\n {p}");
        }
        public void Show()
        {
            Console.WriteLine(this);
            foreach (var (voetbaltruitje, value) in _producten)
                Console.WriteLine($"product:{voetbaltruitje},{value}");
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
