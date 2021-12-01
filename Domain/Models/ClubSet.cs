using System;
using Domain.Exceptions;

namespace Domain.Models
{
    public class ClubSet
    {
        public ClubSet(bool thuis, int versie)
        {
            Thuis = thuis;
            ZetVersie(versie);
        }

        public bool Thuis { get; set; }
        public int Versie { get; private set; }

        public void ZetVersie(int versie)
        {
            if (versie < 1) throw new ClubSetException("ZetVersie - versie < 1");
            Versie = versie;
        }


        public override bool Equals(object obj)
        {
            return obj is ClubSet set &&
                   Thuis == set.Thuis &&
                   Versie == set.Versie;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Thuis, Versie);
        }
        public override string ToString()
        {
            return Thuis ? $"Thuis - {Versie}" : $"Uit - {Versie}";
        }
    }
}