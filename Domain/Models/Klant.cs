using System;
using System.Collections.Generic;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Domain.Models
{
    public class Klant : IKlant
    {
        public string Adres { get; private set; }
        public int KlantId { get; private set; }
        public string Naam { get; private set; }

        private readonly List<Bestelling> _bestellingen = new List<Bestelling>();

        public void ZetAdres(string adres)
        {
            if (string.IsNullOrWhiteSpace(adres)) throw new KlantException("Adres kan niet leeg zijn");
            this.Adres = adres;
        }

        public void ZetKlantId(int id)
        {
            if (id < 0) throw new KlantException("Id kan niet negatief zijn");
            this.KlantId = id;
        }

        public void ZetNaam(string naam)
        {
            if (string.IsNullOrWhiteSpace(naam)) throw new KlantException("Klantnaam kan niet leef zijn");
            this.Naam = naam;
        }

        public int Korting()
        {
            return _bestellingen.Count switch
            {
                < 5 => 0,
                > 10 => 10,
                _ => 20
            };
        }

        public IReadOnlyList<Bestelling> GetBestellingen()
        {
            return _bestellingen;
        }
        public bool HeeftBestelling(Bestelling bestelling)
        {
            if (bestelling == null) throw new KlantException("De bestelling die probeert te zoeken kan niet null zijn");
            return _bestellingen.Contains(bestelling);
        }

        public void VoegToeBestelling(Bestelling bestelling)
        {
            if (bestelling == null) throw new KlantException("Een bestelling die null is kan niet worden toegevoegt");
            if (_bestellingen.Contains(bestelling)) throw new KlantException("De bestelling is al toegevoegt bij de klant");
            if (bestelling.Klant._bestellingen.Contains(bestelling))
            {
                bestelling.SetKlant(this);
                return;
            }
            bestelling.SetKlant(this);
            _bestellingen.Add(bestelling);
        }

        public void VerwijderBestelling(Bestelling bestelling)
        {
            if (bestelling == null) throw new KlantException("De bestelling die je probeert te verwijder is null");
            if (!_bestellingen.Contains(bestelling)) throw new KlantException("De bestelling die probeert te verwijderen hoort niet tot bij de klant ");
            _bestellingen.Remove(bestelling);
            bestelling.SetKlant(null);
        }
        
    }
}
