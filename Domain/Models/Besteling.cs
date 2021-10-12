using System;
using System.Collections.Generic;
using Domain.Exceptions;

namespace Domain.Models
{
    public class Besteling
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public decimal? AangerekendePrijs { get; set; }
        private bool IsBetaald { get; set; }
        public Klant Klant { get; private set; }
        public Dictionary<Truitje, int> Orders { get; set; } = new Dictionary<Truitje, int>();


        public Besteling( Klant klant)
        {
            Klant = klant;
            Datum = DateTime.Now;
        }

        public void Betaald(bool isBetaald)
        {
            this.IsBetaald = isBetaald;
            if (IsBetaald)
            {
               var korting =  Klant.BerekenKorting();
               decimal subTotaal = 0;
               foreach (var (truitje, aantal) in Orders)
               {
                  subTotaal += truitje.Prijs * aantal;
               }

               AangerekendePrijs = subTotaal - (subTotaal / 100 * korting);
               return;
            }

            AangerekendePrijs = null;

        }

        public void VoegTruitjeToe(Truitje truitje, int aantal)
        {
            if (truitje == null) throw new BestellingException("Kan truitje niet toegvoegen aan bestelling truitje is null");
            if (aantal <= 0) throw new ArgumentOutOfRangeException(nameof(aantal),"Er moet minstens 1 truitje worden toegevoegd");
            if (Orders.ContainsKey(truitje))
            {
                Orders[truitje] += aantal; 
                return;
            }
            Orders.Add(truitje,aantal);
        }

        public void VerwijderTruitje(Truitje truitje, int aantal)
        {
            if (truitje == null) throw new BestellingException("Kan truitje niet verwijderen want truitje is null");
            if (!Orders.ContainsKey(truitje)) return;
            if (Orders[truitje]== aantal)
            {
                Orders.Remove(truitje);
                return;
            }
            if (Orders[truitje]>aantal)
            {
                Orders[truitje] -= aantal;
                return;
            }

            throw new ArgumentOutOfRangeException(nameof(aantal),"Het aantal is te groter dan het aantal in de bestelling");
        }


        public void SetKlant(Klant newKlant)
        {
           
            if (newKlant == Klant) throw new BestellingException("Dezelfde newKlant is opgegeven");
            if (Klant.HeeftBestelling(this)) Klant.VerwijderdBestelling(this);
            Klant = newKlant;
            if (newKlant.HeeftBestelling(this))return;
            newKlant.VoegbestellingToe(this);
            
        }
    }
}