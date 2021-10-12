using System;
using System.Collections.Generic;
using Domain.Exceptions;

namespace Domain.Models
{
    public class Klant
    {
        public int Klantnummer { get; init; }

        public string Naam
        {
            get => Naam;
            set
            {
                if (value.Trim().Length == 0)
                {
                    throw new KlantException( nameof(value) + " mag niet leeg zijn");
                }

                Naam = value;
            }
        }

        public string Adres
        {
            get => Adres;
            set
            {
                if (value.Trim().Length < 5)
                {
                    throw new KlantException( nameof(value) + " moet minstens 5 karakters zijn");
                }

                Adres = value;
            }
        }

        private readonly List<Besteling>  _bestellingen = new List<Besteling>();
        

        public Klant(string naam, string adres)
        {
            Naam = naam;
            Adres = adres;
            
        }

        public int BerekenKorting()
        {
            return _bestellingen.Count switch
            {
                < 5 => 0,
                > 10 => 10,
                _ => 20
            };
        }

        public void VoegbestellingToe(Besteling bestelling)
        {
            if (bestelling == null) throw new KlantException("Een bestelling die null is kan niet worden toegevoegt");
            if (_bestellingen.Contains(bestelling)) throw  new KlantException("De bestelling is al toegevoegt bij de klant");
            if (bestelling.Klant._bestellingen.Contains(bestelling))
            {
                bestelling.SetKlant(this);
                return;
            }

            bestelling.SetKlant(this);
            _bestellingen.Add(bestelling);

        }

        public void VerwijderdBestelling(Besteling bestelling)
        {
            if (bestelling == null) throw new KlantException("De bestelling die je probeert te verwijder is null");
            if (!_bestellingen.Contains(bestelling)) throw new KlantException("De bestelling die probeert te verwijderen hoort niet tot bij de klant ");
            _bestellingen.Remove(bestelling);
            bestelling.SetKlant(null);
        }

        public bool HeeftBestelling(Besteling besteling)
        {
            if (besteling == null) throw new KlantException("De besteling die probeert te zoeken kan niet null zijn");
            return _bestellingen.Contains(besteling);
        }
    }
}
