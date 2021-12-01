using System;
using Domain.Exceptions;

namespace Domain.Models
{
    public class Club
    {
        public Club(string competitie, string ploeg)
        {
           ZetCompetitie(competitie);
           ZetPloeg(ploeg);
        }

        public string Competitie { get; private set; }
        public string Ploeg { get; private set; }

        public void ZetCompetitie(string competitie)
        {
            if (string.IsNullOrWhiteSpace(competitie)) throw new ClubException("ZetCompetitie - competitie null or empty");
            Competitie = competitie;
        }

        public void ZetPloeg(string ploeg)
        {
            if (string.IsNullOrWhiteSpace(ploeg)) throw new ClubException("ZetPloeg - ploeg null or empty");
            Ploeg = ploeg;
        }


        public override bool Equals(object obj)
        {
            return obj is Club club &&
                   Competitie == club.Competitie &&
                   Ploeg == club.Ploeg;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Competitie, Ploeg);
        }
    }
}