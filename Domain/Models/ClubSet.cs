using System;
using Domain.Exceptions;

namespace Domain.Models
{
    public class ClubSet
    {
        public ClubSet(bool thuis, int versie)
        {
            Thuis = thuis;
            if (versie < 1) throw new ClubSetException("Clubset - versie < 1");
            Versie = versie;
        }

        public bool Thuis { get; private set; }
        public int Versie { get; private set; }

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