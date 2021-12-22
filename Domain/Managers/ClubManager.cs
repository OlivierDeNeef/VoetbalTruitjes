using System;
using System.Collections.Generic;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Managers
{
    public class ClubManager
    {
        private readonly IClubRepo _clubRepo;

        public ClubManager(IClubRepo clubRepo)
        {
            _clubRepo = clubRepo;
        }

        public List<Club> ZoekClubs(int id, string naam, string competitie)
        {
            try
            {
                return _clubRepo.ZoekClubs(id, naam, competitie);
            }
            catch (Exception e)
            {
                throw new ClubManagerException(nameof(ZoekClubs) + " - Er ging iets mis", e);
            }
        }

        public void VoegClubToe(Club club)
        {
            try
            {
                _clubRepo.VoegClubToe(club);
            }
            catch (Exception e)
            {
                throw new ClubManagerException(nameof(VoegClubToe) + " - Er ging iets mis",e);
            }
        }

        public void VerwijderClub(int id)
        {
            try
            {
                if (_clubRepo.BestaatClub(id))
                {
                    _clubRepo.VerwijderClub(id);
                }
            }
            catch (Exception e)
            {
                throw new ClubManagerException(nameof(VerwijderClub) + " - Er ging iets mis", e);
            }
        }


        public void UpdateClub(Club club)
        {
            try
            {
                if (_clubRepo.BestaatClub(club.Id))
                {
                    _clubRepo.UpdateClub(club);
                }
            }
            catch (Exception e)
            {
                throw new ClubManagerException(nameof(UpdateClub) + " - Er ging iets mis", e);
            }
        }

        public List<Club> GeefAlleClubs()
        {
            try
            {
                return _clubRepo.GeefAlleClubs();
            }
            catch (Exception e)
            {
                throw new ClubManagerException(nameof(GeefAlleClubs) + " - Er ging iets mis", e);
            }
        }

    }
}