using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;

namespace Domain
{
    public class KlantManager
    {
        private readonly IKlantRepo _klantRepo;

        public KlantManager(IKlantRepo klantRepo)
        {
            _klantRepo = klantRepo;
        }

        

        public List<Klant> ZoekKlant(int id,  string naam,  string adres )
        {
            try
            {
               return _klantRepo.ZoekKlanten(id, naam, adres);
            }
            catch (Exception e)
            {
                throw new KlantManagerException("KlantManager - Fout bij opzoeken van de klant", e);
            }

          
        }

        public void VoegToeKlant(Klant klant)
        {
            try
            {
                _klantRepo.VoegKlantToe(klant);
            }
            catch (Exception e)
            {
                
                throw new KlantManagerException("KlantManager - Fout bij toevoegen van klant",e);
            }
        }

        public void VerwijderKlant(Klant klant)
        {
            try
            {
                if (_klantRepo.BestaatKlant(klant.KlantId))
                {
                    _klantRepo.VerwijderKlant(klant);
                }
            }
            catch (Exception e)
            {
                throw new KlantManagerException("KlantManager - Fout bij verwijderen van klant", e);
            }
        }

        
        public void UpdateKlant(Klant klant)
        {
            try
            {
                if (_klantRepo.BestaatKlant(klant.KlantId))
                {
                    _klantRepo.UpdateKlant(klant);
                }
               
            }
            catch (Exception e)
            {
                throw new KlantManagerException("KlantManager - Fout bij updaten van de klant", e);
            }
        }

        public List<Klant> GeefAlleKlanten()
        {
            try
            {
                return _klantRepo.GeefAlleKlanten();
            }
            catch (Exception e)
            {
                throw new KlantManagerException("KlantManager - Fout bij opzoeken van de klant", e);
            }
        }
    }
}