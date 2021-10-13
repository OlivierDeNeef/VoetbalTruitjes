using System;
using Domain.Exceptions;

namespace Domain.Models
{
    public class Club 
    {
        public Club(string competitie, string ploeg)
        {
            if ((string.IsNullOrWhiteSpace(competitie)) || (string.IsNullOrWhiteSpace(ploeg))) throw new ClubException("Club - null or empty");
            Competitie = competitie;
            Ploeg = ploeg;
        }

        public string Competitie { get; private set; }
        public string Ploeg { get; private set; }

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