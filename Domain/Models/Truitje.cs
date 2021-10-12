using System;
using Domain.Exceptions;

namespace Domain.Models
{
    public class Truitje
    {

        public int Id
        {
            get => Id;
            init
            {
                if (value <= 0)
                {
                    throw new TruitjeException("De id van een truitje moet meer dan 0 zijn");
                }
            }
        }

        public Maat Maat
        {
            get => Maat; 
            private set
            {
                if (value == Maat) return; //ASK: Error geven?
                Maat = value;
            }
        }

        public string Seizoen
        {
            get => Seizoen;
            private set
            {
                if (string.IsNullOrEmpty(value.Trim())) throw new TruitjeException("Seizoen kan geen lege waarde bevatten");
                if (value == Seizoen) return; //ASK:Error geven?
                Seizoen = value;
            }
        }

        public decimal Prijs
        {
            get => Prijs;
            private set
            {
                if (value <= 1)throw new TruitjeException("De prijs van een truitje moet meer dan 0 zijn");
                if (value == Prijs) return; //ASK: Error geven?
                Prijs = value;
            }
        }

        public ClubSet ClubSet
        {
            get => ClubSet;
            init => ClubSet = value ?? throw new TruitjeException("Clubset kan niet null zijn");
        }

        public Ploeg Ploeg
        {
            get => Ploeg;
            init => Ploeg = value ?? throw new TruitjeException("Ploeg kan niet null zijn");
        }

      
        public Truitje(Maat maat, string seizoen, ClubSet clubSet, Ploeg ploeg, decimal prijs)
        {
            Maat = maat;
            Seizoen = seizoen;
            ClubSet = clubSet;
            Ploeg = ploeg;
            Prijs = prijs;
        }

        public void SetPrijs(decimal prijs)
        {
            Prijs = prijs;
        }

        public void SetSeizoen(string seizoen)
        {
            Seizoen = seizoen;
        }

        public void SetMaat(Maat maat)
        {
            Maat = maat;
        }

        
    }
}