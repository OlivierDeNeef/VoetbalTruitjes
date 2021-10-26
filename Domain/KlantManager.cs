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
            this._klantContext = klantContext;
        }

        public IEnumerable<Klant> GeefAlleKlanten()
        {
            try
            {
               return _klantContext.GeefAlleKlanten();
            }
            catch (Exception e)
            {
                throw new KlantManagerException("KlantManager - Fout bij opzoeken van de klanten", e);
            }
        }

        public Klant ZoekKlant([Optional]int id, [Optional] string naam, [Optional] string adres )
        {
            try
            {
                if(id != 0) return _klantContext.GeefKlant(id);
                if (!string.IsNullOrWhiteSpace(naam) && !string.IsNullOrWhiteSpace(adres)) _klantContext.GeefKlant(naam, adres);
                if(!string.IsNullOrWhiteSpace(naam)) return _klantContext
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