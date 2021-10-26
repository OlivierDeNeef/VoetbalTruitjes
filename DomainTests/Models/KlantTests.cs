using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Exceptions;
using Domain.Models;
using Xunit;

namespace DomainTests.Models
{
    public class KlantTests
    {
        private readonly List<Bestelling> _bestellingen;
        private readonly Klant _klant;
        public KlantTests()
        {
            _bestellingen = new List<Bestelling>()
            {
                new Bestelling(1, DateTime.Now),
                new Bestelling(2, DateTime.Now),
                new Bestelling(3, DateTime.Now),
                new Bestelling(4, DateTime.Now),
                new Bestelling(5, DateTime.Now),

            };
            _klant = new Klant(1,"Olivier", "Dendermonde",_bestellingen);
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
        public void GetBestellingenTest_ReturnBestellingenVanKlant()
        {
            Assert.Equal(_bestellingen, _klant.GetBestellingen());
        }

        [Fact()]
        public void VerwijderBestellingTest_BestellingInKlant_VerwijderdBestellingBijKlantEnVeranderdKlantBijBestelling()
        {
            var bestelling = new Bestelling(8, DateTime.Now);
            _klant.VoegToeBestelling(bestelling);
            _klant.VerwijderBestelling(bestelling);

            Assert.False(_klant.HeeftBestelling(bestelling));
            Assert.Null(bestelling.Klant);
        }

        [Fact()]
        public void VerwijderBestellingTest_BestellingNietInKlant_ThrowsKlantException()
        {
            var bestelling = new Bestelling(8, DateTime.Now);
            
            Assert.Throws<KlantException>(() => _klant.VerwijderBestelling(bestelling));
        }

        [Fact()]
        public void VerwijderBestellingTest_BestellingIsNull_ThrowsKlantException()
        {
            Assert.Throws<KlantException>(() => _klant.VerwijderBestelling(null));
        }

        [Fact()]
        public void VoegToeBestellingTest_BestellingNietInLijst_BestellingWordtToegevoegt()
        {
            var bestelling = new Bestelling(8, DateTime.Now);
            _klant.VoegToeBestelling(bestelling);

            Assert.True(_klant.HeeftBestelling(bestelling));
            Assert.Equal(_klant, bestelling.Klant);
        }

        [Fact()]
        public void VoegToeBestellingTest_BestellingInLijst_ThrowsKlantException()
        {
            var bestelling = new Bestelling(8, DateTime.Now);
            _klant.VoegToeBestelling(bestelling);

            Assert.Throws<KlantException>(() => _klant.VoegToeBestelling(bestelling));
        }

        [Fact()]
        public void VoegToeBestellingTest_BestellingIsNull_ThrowsKlantException()
        {
            Assert.Throws<KlantException>(() => _klant.VoegToeBestelling(null));
        }

        [Fact()]
        public void HeeftBestellingTest_HeeftBestelling_ReturnsTrue()
        {
            var bestelling = new Bestelling(8, DateTime.Now);
            _klant.VoegToeBestelling(bestelling);

            Assert.True(_klant.HeeftBestelling(bestelling));
        }

        [Fact()]
        public void HeeftBestellingTest_HeeftBestellingNiet_ReturnsFalse()
        {
            var bestelling = new Bestelling(8, DateTime.Now);

            Assert.False(_klant.HeeftBestelling(bestelling));
        }

        [Fact()]
        public void HeeftBestellingTest_BestellingIsNull_ThrowsKlantException()
        {
            Assert.Throws<KlantException>((() => _klant.HeeftBestelling(null)));
        }

        [Fact()]
        public void KortingTest_KlantHeeft0Bestellingen_Returns0()
        {
            var klant = new Klant("Olivir","Dendermonde");

            Assert.Equal(0, klant.Korting());
        }

        [Fact()]
        public void KortingTest_KlantHeeft5Bestellingen_Returns10()
        {
            Assert.Equal(10, _klant.Korting());
        }

        [Fact()]
        public void KortingTest_KlantHeeft10Bestellingen_Returns20()
        {
            _klant.VoegToeBestelling(new Bestelling(6, DateTime.Now));
            _klant.VoegToeBestelling(new Bestelling(7, DateTime.Now));
            _klant.VoegToeBestelling(new Bestelling(8, DateTime.Now));
            _klant.VoegToeBestelling(new Bestelling(9, DateTime.Now));
            _klant.VoegToeBestelling(new Bestelling(10, DateTime.Now));

            Assert.Equal(20, _klant.Korting());
        }

        [Fact()]
        public void ToStringTest_ReturnString()
        {
            var date = DateTime.Now;
            var klant = new Klant(1,"Olivir", "Dendermonde", new List<Bestelling>(){new Bestelling(1,date)});

            Assert.Equal($"[Klant] 1,Olivir,Dendermonde,1\r\n[Bestelling] 1,False,0,{date},1,Olivir,Dendermonde,0", klant.ToString());
        }

        [Fact()]
        public void ToTextTest_UseDefaultTrue_ReturnsKorteVersie()
        {
            var date = DateTime.Now;
            var klant = new Klant(1, "Olivir", "Dendermonde", new List<Bestelling>() { new Bestelling(1, date) });

            Assert.Equal($"[Klant] 1,Olivir,Dendermonde,1", klant.ToText());
        }

        [Fact()]
        public void ToTextTest_UseFalse_ReturnsLangeVersie()
        {
            var date = DateTime.Now;
            var klant = new Klant(1, "Olivir", "Dendermonde", new List<Bestelling>() { new Bestelling(1, date) });

            Assert.Equal($"[Klant] 1,Olivir,Dendermonde,1\r\n[Bestelling] 1,False,0,{date},1,Olivir,Dendermonde,0", klant.ToText(false));
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