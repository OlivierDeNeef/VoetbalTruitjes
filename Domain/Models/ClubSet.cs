using System;
using Domain.Exceptions;

namespace Domain.Models
{
    public class ClubSet
    {
        public int Versie { get; private set; }
        public bool IsUit { get; private set; }

        public Truitje Truitje
        {
            get => Truitje;
            private set => Truitje = value ?? throw new ClubSetException("Truitje kan niet null zijn.");
        }

        public ClubSet(int versie, bool isUit, Truitje truitje)
        {
            Versie = versie;
            IsUit = isUit;
            Truitje = truitje;
        }

        public void SetVersie(int versie)
        {
            Versie = versie;
        }

        public void SetIsUit(bool isUit)
        {
            IsUit = isUit;
        }

        public void SetTruitje(Truitje truitje)
        {
            Truitje = truitje;
        }
    }
}