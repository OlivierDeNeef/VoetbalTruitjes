using System;
using System.Collections.Generic;
using Domain.Exceptions;
using Domain.Models;
using Xunit;

namespace DomainTests.Models
{
    public class BestellingTests
    {
        [Fact()]
        public void BestellingTest_GeldigeInput_VeranderdProperties()
        {
            var bestelling = new Bestelling(new DateTime());

            Assert.Equal(new DateTime(), bestelling.Tijdstip);
        }

        [Fact]
        public void Bestelling2Test_GeldigeInput_VeranderdProperties()
        {
            var bestelling = new Bestelling(1,new DateTime());

            Assert.Equal(1,bestelling.BestellingId);
            Assert.Equal(new DateTime(), bestelling.Tijdstip);
        }


        [Fact]
        public void Bestelling3Test_GeldigeInput_VeranderdProperties()
        {
            var klant = new Klant("Olivier", "Dendermonde");
            var bestelling = new Bestelling(1, klant, new DateTime());

            Assert.Equal(klant, bestelling.Klant);
            Assert.Equal(1, bestelling.BestellingId);
            Assert.Equal(new DateTime(), bestelling.Tijdstip);
        }


        [Fact]
        public void Bestelling4Test_GeldigeInput_VeranderdProperties()
        {
            var klant = new Klant("Olivier", "Dendermonde");
            var bestelling = new Bestelling(1, klant, new DateTime(),new Dictionary<Voetbaltruitje, int>());

            Assert.NotNull(bestelling.GeefProducten());
            Assert.Equal(klant, bestelling.Klant);
            Assert.Equal(1, bestelling.BestellingId);
            Assert.Equal(new DateTime(), bestelling.Tijdstip);
        }


        [Fact()]
        public void BestellingTest2_OnGeldigeInput_VeranderdProperties()
        {
            Assert.Throws<BestellingException>(() => new Bestelling(0, new DateTime()));
        }

        [Fact()]
        public void BestellingTest3_OnGeldigeInput_VeranderdProperties()
        {
            Assert.Throws<BestellingException>(() => new Bestelling(1,null, new DateTime()));
        }


        [Fact()]
        public void BestellingTest4_OnGeldigeInput_VeranderdProperties()
        {
            Assert.Throws<BestellingException>(() => new Bestelling(1, new Klant("olivier", "dendermonde"), new DateTime(), null));
        }

        [Fact()]
        public void BestellingTest1()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void BestellingTest2()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void BestellingTest3()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void BestellingTest4()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void VoegProductToeTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void VerwijderProductTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void GeefProductenTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void KostprijsTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void VerwijderKlantTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void ZetKlantTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void ZetBestellingIdTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void ZetTijdstipTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void ZetBetaaldTest()
        {
            throw new NotImplementedException();
        }

        [Fact()]
        public void ToStringTest()
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