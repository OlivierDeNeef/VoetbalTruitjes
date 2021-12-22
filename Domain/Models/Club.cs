using System;
using Domain.Exceptions;

namespace Domain.Models
{
    public class Club
    {

        public Club(int id,string competitie, string naam) : this(competitie,naam)
        {
            Id = id;
        }

        public Club(string competitie, string 
            naam)
        {
           ZetCompetitie(competitie);
           ZetPloeg(naam);
        }

        public int Id { get; private set; }
        public string Naam { get; private set; }
        public string Competitie { get; private set; }
       

        public void ZetCompetitie(string competitie)
        {
            if (string.IsNullOrWhiteSpace(competitie)) throw new ClubException("ZetCompetitie - competitie null or empty");
            Competitie = competitie;
        }

        public void ZetPloeg(string naam)
        {
            if (string.IsNullOrWhiteSpace(naam)) throw new ClubException("ZetPloeg - naam null or empty");
            Naam = naam;
        }


        protected bool Equals(Club other)
        {
            return Id == other.Id && Naam == other.Naam && Competitie == other.Competitie;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Club) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Naam, Competitie);
        }

        public static bool operator ==(Club left, Club right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Club left, Club right)
        {
            return !Equals(left, right);
        }
    }
}