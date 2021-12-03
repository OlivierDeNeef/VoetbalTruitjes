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
        private readonly IKlantContext _klantContext;

        public KlantManager(IKlantContext klantContext)
        {
            _klantContext = klantContext;
        }

        

        public Klant ZoekKlant(int id,  string naam,  string adres )
        {
            try
            {
                
                
            }
            catch (Exception e)
            {
                throw new KlantManagerException("KlantManager - Fout bij opzoeken van de klant", e);
            }

            return new Klant(1, "test", "sfsdgfsedr");
        }

        public void VoegToeKlant(Klant klant)
        {
            try
            {
                if (!_klantContext.BestaatKlant(klant.KlantId))
                {
                   _klantContext.VoegToeKlant(klant); 
                }
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
                if (_klantContext.BestaatKlant(klant.KlantId))
                {
                    _klantContext.VerwijderKlant(klant);
                }
            }
            catch (Exception e)
            {
                throw new KlantManagerException("KlantManager - Fout bij verwijderen van klant", e);
            }
        }

        public bool BestaatKlant(int id)
        {
            try
            {
                
                return _klantContext.BestaatKlant(id);
            }
            catch (Exception e)
            {
                throw new KlantManagerException("KlantManager - Fout bij opzoeken van klant", e);
            }
        }

        public void UpdateKlant(Klant klant)
        {
            try
            {
                _klantContext.UpdateKlant(klant);
               
            }
            catch (Exception e)
            {
                throw new KlantManagerException("KlantManager - Fout bij updaten van de klant", e);
            }
        }
    }
}