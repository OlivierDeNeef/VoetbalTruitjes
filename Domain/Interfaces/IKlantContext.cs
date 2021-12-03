using System.Collections.Generic;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IKlantContext
    {
        IEnumerable<Klant> GeefAlleKlanten();
        Klant GeefKlant(int id, string naam, string adres);
        bool BestaatKlant(int id);
        void VoegToeKlant(Klant klant);
        void VerwijderKlant(Klant klant);
        void UpdateKlant(Klant klant);
    }
}