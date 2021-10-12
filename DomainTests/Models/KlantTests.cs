using System;
using System.Collections.Generic;
using Domain.Exceptions;
using Domain.Models;
using Xunit;

namespace DomainTests.Models
{
    public class KlantTests
    {
        private Klant _klant;
        public KlantTests()
        {

            _klant = new Klant("Olivier", "Dendermonde");
        }

        [Fact()]
        public void KlantTest_NaamEnAdresIngeven_NaamEnAdresVeranderenInKlant()
        {
            var klant = new Klant("Olivier", "Dendermonde");

            Assert.Equal("Olivier", klant.Naam);
            Assert.Equal("Dendermonde", klant.Adres);
        }

        [Theory]
        [InlineData("","Dendermonde")]
        [InlineData("Olivier","")]
        [InlineData("","")]
        public void KlantTest_LegeNaamEnAdresIngeven_ThrowKlantException(string naam , string adres)
        {
            Assert.Throws<KlantException>(() => new Klant(naam, adres));
        }


        [Fact()]
        public void KlantTest_KlantIdNaamEnAdresIngevegen_KlantIdNaamEnAdresVeranderenInKlant()
        {
            var klant = new Klant(1,"Olivier", "Dendermonde");

            Assert.Equal(1,klant.KlantId);
            Assert.Equal("Olivier", klant.Naam);
            Assert.Equal("Dendermonde", klant.Adres);
        }

        [Theory]
        [InlineData(1,"", "Dendermonde")]
        [InlineData(1,"Olivier", "")]
        [InlineData(0,"Olivier", "Dendermonde")]
        public void KlantTest_LegeKlantIdNaamEnAdresIngeven_ThrowKlantException(int klantId, string naam, string adres)
        {
            Assert.Throws<KlantException>(() => new Klant(klantId, naam, adres));
        }


        [Fact()]
        public void KlantTest_KlantIdNaamAdresEnBestellingenIngevegen_KlantIdNaamAdresBestellingenVeranderenInKlant()
        {
            var bestelling = new Bestelling(1, DateTime.Now);
            var klant = new Klant(1, "Olivier", "Dendermonde", new List<Bestelling>(){bestelling});

            Assert.Equal(1, klant.KlantId);
            Assert.Equal("Olivier", klant.Naam);
            Assert.Equal("Dendermonde", klant.Adres);
            Assert.True(klant.HeeftBestelling(bestelling));
        }

        [Theory]
        [InlineData(1, "", "Dendermonde", null)]
        [InlineData(1, "Olivier", "",null)]
        [InlineData(0, "Olivier", "Dendermonde",null)]
        [InlineData(1, "Olivier", "Dendermonde",null)]
        public void KlantTest_LegeKlantIdNaamAdresEnBestellingenIngeven_ThrowKlantException(int klantId, string naam, string adres , List<Bestelling> bestellingen)
        {
            Assert.Throws<KlantException>(() => new Klant(klantId, naam, adres, bestellingen));
        }


        [Fact()]
        public void ZetKlantIdTest_GeldigeId_KlantIdVeranderd()
        {
            _klant.ZetKlantId(50);

            Assert.Equal(50,_klant.KlantId);
        }

        [Fact()]
        public void ZetKlantIdTest_OngeldigeKlantId_ThrowsKlantException()
        {
            Assert.Throws<KlantException>(() => _klant.ZetKlantId(0));
        }

        [Fact()]
        public void ZetNaamTest_GeldigeNaam_KlantNaamVeranderd()
        {
            _klant.ZetNaam("Tom");

            Assert.Equal("Tom", _klant.Naam);
        }

        [Fact()]
        public void ZetNaamTest_OngeldigeNaam_ThrowsKlantException()
        {
            Assert.Throws<KlantException>(() => _klant.ZetNaam("    "));
        }

        [Fact()]
        public void ZetAdresTest_GeldigeAdres_KlantAdresVeranderd()
        {
            _klant.ZetAdres("Buggenhout");

            Assert.Equal("Buggenhout", _klant.Adres);
        }

        [Fact()]
        public void ZetAdresTest_OngeldigeAdres_ThrowsKlantException()
        {
            Assert.Throws<KlantException>(() => _klant.ZetAdres("1234   "));
        }

        [Fact()]
        public void GetBestellingenTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void VerwijderBestellingTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void VoegToeBestellingTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void HeeftBestellingTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void KortingTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void ToStringTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void ToTextTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void ShowTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void EqualsTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void GetHashCodeTest()
        {
            throw new NotImplementedException();
        }
    }
}