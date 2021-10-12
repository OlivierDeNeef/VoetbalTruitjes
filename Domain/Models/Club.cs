using System.Collections.Generic;
using Domain.Interfaces;

namespace Domain.Models
{
    public class Club
    {


        public string Naam { get; private set; }

        public string Competitie { get; private set; }

        public List<Voetbaltruitje> Truitjes { get; } = new List<Voetbaltruitje>();
        
        public Club(string naam, string competitie)
        {
            Naam = naam;
            Competitie = competitie;
        }

        public void ZetNaam(string naam)
        {
            Naam = naam;
        }

        public void ZetCompitie(string competitie)
        {
            Competitie = competitie;
        }
    }
}