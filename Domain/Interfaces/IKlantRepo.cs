using System.Collections.Generic;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IKlantRepo
    {
        List<Klant> ZoekKlanten(int id, string naam, string adres);
        bool BestaatKlant(int id);
        int VoegKlantToe(Klant klant);
        void UpdateKlant(Klant klant);
        void VerwijderKlant(Klant klant);
        List<Klant> GeefAlleKlanten();
    }
}