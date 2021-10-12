using System;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Domain.Models
{
    public class ClubSet
    {
        public int Versie { get; private set; }
        public bool IsUit { get; private set; }

        public Voetbaltruitje Voetbaltruitje
        {
            get => Voetbaltruitje;
            private set => Voetbaltruitje = value ?? throw new ClubSetException("Voetbaltruitje kan niet null zijn.");
        }

        public ClubSet(int versie, bool isUit, Voetbaltruitje voetbaltruitje)
        {
            Versie = versie;
            IsUit = isUit;
            Voetbaltruitje = voetbaltruitje;
        }

        public void SetVersie(int versie)
        {
            Versie = versie;
        }

        public void SetIsUit(bool isUit)
        {
            IsUit = isUit;
        }

        public void SetTruitje(Voetbaltruitje voetbaltruitje)
        {
            Voetbaltruitje = voetbaltruitje;
        }
    }
}