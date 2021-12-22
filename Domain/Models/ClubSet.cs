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

        public override string ToString()
        {
            return Thuis ? $"Thuis - {Versie}" : $"Uit - {Versie}";
        }

        protected bool Equals(ClubSet other)
        {
            return Thuis == other.Thuis && Versie == other.Versie;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ClubSet) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Thuis, Versie);
        }

        public static bool operator ==(ClubSet left, ClubSet right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ClubSet left, ClubSet right)
        {
            return !Equals(left, right);
        }
    }
}