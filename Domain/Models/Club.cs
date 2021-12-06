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


        public override bool Equals(object obj)
        {
            return obj is Club club &&
                   Competitie == club.Competitie &&
                   Naam == club.Naam;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Competitie, Naam);
        }
    }
}