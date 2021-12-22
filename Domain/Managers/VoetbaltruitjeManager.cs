using System;
using System.Collections.Generic;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Managers
{
    public class VoetbaltruitjeManager
    {
        private readonly IVoetbaltruitjeRepo _voetbaltruitjeRepo;

        public VoetbaltruitjeManager(IVoetbaltruitjeRepo voetbaltruitjeRepo)
        {
            _voetbaltruitjeRepo = voetbaltruitjeRepo;
        }

        public List<Voetbaltruitje> ZoekVoetbaltruitjes(int id, string competitie, string club, double prijs,
            string seizoen, int versie, bool thuis, bool uit, Kledingmaat maat)
        {
            try
            {
               return _voetbaltruitjeRepo.ZoekVoetbaltruitjes(id, competitie, club, prijs, seizoen, versie, thuis, uit, maat);
            }
            catch (Exception e)
            {
                throw new VoetbalTruitjeManagerException(nameof(ZoekVoetbaltruitjes) + " - Er ging iets mis tijdens het opzoeken",e);
            }
        }

        public void VoegVoetbalTruitjeToe(Voetbaltruitje voetbaltruitje)
        {
            try
            {
                _voetbaltruitjeRepo.VoegVoetbaltruitjeToe(voetbaltruitje);
            }
            catch (Exception e)
            {
                throw new VoetbalTruitjeManagerException(
                    nameof(VoegVoetbalTruitjeToe) + " - Er ging iets mis tijdens het toevoegen", e);
            }
        }

        public void VerwijderVoetbaltruitje(int id)
        {
            try
            {
                if (_voetbaltruitjeRepo.BestaatVoetbalTruitje(id))
                {
                    _voetbaltruitjeRepo.VerwijderVoetbaltruitje(id);
                }
            }
            catch (Exception e)
            {
                throw new VoetbalTruitjeManagerException(
                    nameof(VoegVoetbalTruitjeToe) + " - Er ging iets mis tijdens het verwijderen", e);
            }
        }

        public void UpdateVoetbaltruitje(Voetbaltruitje voetbaltruitje)
        {
            try
            {
                if (_voetbaltruitjeRepo.BestaatVoetbalTruitje(voetbaltruitje.Id))
                {
                    _voetbaltruitjeRepo.UpdateVoetbalTruitje(voetbaltruitje);
                }
            }
            catch (Exception e)
            {
                throw new VoetbalTruitjeManagerException(
                    nameof(VoegVoetbalTruitjeToe) + " - Er ging iets mis tijdens het updaten", e);
            }
        }

        public List<Voetbaltruitje> GeefAlleVoetbaltruitjes()
        {
            try
            {
                return _voetbaltruitjeRepo.GeefAlleVoetbaltruitjes();
            }
            catch (Exception e)
            {
                throw new VoetbalTruitjeManagerException(
                    nameof(VoegVoetbalTruitjeToe) + " - Er ging iets mis tijdens het opvragen", e);
            }
        }
    }
}