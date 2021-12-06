using System.Collections.Generic;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IClubRepo
    {
        List<Club> ZoekClubs(int id, string naam, string competitie);
        List<Club> GeefAlleClubs();
        bool BestaatClub(int id);
        int VoegClubToe(Club club);
        void UpdateClub(Club club);
        void VerwijderClub(int id);
    }
}