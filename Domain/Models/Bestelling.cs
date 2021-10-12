using System;
using System.Collections.Generic;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Domain.Models
{
    public class Bestelling
    {
        public int BestellingId { get; private set; }
        private bool Betaald { get; set; }
        public Klant Klant { get; private set; }
        public double Prijs { get; set; }
        public DateTime Tijdstip { get; set; }
        public double? AangerekendePrijs { get; set; }
        
        private readonly Dictionary<Voetbaltruitje, int> _orders = new Dictionary<Voetbaltruitje, int>();


       

        public void ZetBetaald(bool betaald)
        {
            this.Betaald = betaald;
            if (Betaald)
            {
               var korting =  Klant.Korting();
               double subTotaal = 0;
               foreach (var (truitje, aantal) in _orders)
               {
                  subTotaal += truitje.Prijs * aantal;
               }

               AangerekendePrijs = subTotaal - (subTotaal / 100 * korting);
               return;
            }

            AangerekendePrijs = null;

        }

        public void VoegTruitjeToe(Voetbaltruitje voetbaltruitje, int aantal)
        {
            if (voetbaltruitje == null) throw new BestellingException("Kan voetbaltruitje niet toegvoegen aan bestelling voetbaltruitje is null");
            if (aantal <= 0) throw new ArgumentOutOfRangeException(nameof(aantal),"Er moet minstens 1 voetbaltruitje worden toegevoegd");
            if (_orders.ContainsKey(voetbaltruitje))
            {
                _orders[voetbaltruitje] += aantal; 
                return;
            }
            _orders.Add(voetbaltruitje,aantal);
        }

        public void VerwijderTruitje(Voetbaltruitje voetbaltruitje, int aantal)
        {
            if (voetbaltruitje == null) throw new BestellingException("Kan voetbaltruitje niet verwijderen want voetbaltruitje is null");
            if (!_orders.ContainsKey(voetbaltruitje)) return;
            if (_orders[voetbaltruitje]== aantal)
            {
                _orders.Remove(voetbaltruitje);
                return;
            }

            if (_orders[voetbaltruitje] <= aantal) throw new ArgumentOutOfRangeException(nameof(aantal), "Het aantal is te groter dan het aantal in de bestelling");
            _orders[voetbaltruitje] -= aantal;
            return;

        }


        public void SetKlant(Klant newKlant)
        {
           
            if (newKlant == Klant) throw new BestellingException("Dezelfde newKlant is opgegeven");
            if (Klant.HeeftBestelling(this)) Klant.VerwijderBestelling(this);
            Klant = newKlant;
            if (newKlant.HeeftBestelling(this))return;
            newKlant.VoegToeBestelling(this);
            
        }
    }
}