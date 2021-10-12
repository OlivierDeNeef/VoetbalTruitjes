using System.Collections.Generic;

namespace Domain.Models
{
    public class Ploeg
    {


        public string Naam
        {
            get => Naam;
            private set
            {
                Naam = value;
            }
        }

        public string Competitie { get; private set; }

        public List<Truitje> Truitjes { get; } = new List<Truitje>();
        
        public Ploeg(string naam, string competitie)
        {
            Naam = naam;
            Competitie = competitie;
        }

        public void SetNaam(string naam)
        {
            Naam = naam;
        }

        public void SetCompitie(string competitie)
        {
            Competitie = competitie;
        }
    }
}