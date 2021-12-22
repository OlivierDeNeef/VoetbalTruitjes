using System.Collections.Generic;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IVoetbaltruitjeRepo
    {
        List<Voetbaltruitje> ZoekVoetbaltruitjes(int id, string competitie, string club, double prijs,
            string seizoen, int versie, bool thuis, bool uit, Kledingmaat maat);

        bool BestaatVoetbalTruitje(int id);
        int VoegVoetbaltruitjeToe(Voetbaltruitje voetbaltruitje);
        void UpdateVoetbalTruitje(Voetbaltruitje voetbaltruitje);
        void VerwijderVoetbaltruitje(int id);
        List<Voetbaltruitje> GeefAlleVoetbaltruitjes();
    }
}