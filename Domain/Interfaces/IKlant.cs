using System.Collections.Generic;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IKlant
    {
        string Adres { get; }
        int KlantId { get; }
        string Naam { get; }

        IReadOnlyList<Bestelling> GetBestellingen();
        bool HeeftBestelling(Bestelling bestelling);
        int Korting();
        void VerwijderBestelling(Bestelling bestelling);
        void VoegToeBestelling(Bestelling bestelling);
        void ZetAdres(string adres);
        void ZetKlantId(int id);
        void ZetNaam(string naam);
    }
}