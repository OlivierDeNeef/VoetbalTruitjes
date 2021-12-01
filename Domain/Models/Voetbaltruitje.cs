using System;
using System.Collections.Generic;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Domain.Models
{
    public class Voetbaltruitje : IVoetbaltruitje
    {
        public Voetbaltruitje(int id, Club club, string seizoen, double prijs, Kledingmaat kledingmaat, ClubSet clubSet)
            : this(club, seizoen, prijs, kledingmaat, clubSet)
        {
            ZetId(id);
        }
        internal Voetbaltruitje(Club club, string seizoen, double prijs, Kledingmaat kledingmaat, ClubSet clubSet)
        {
            ZetClub(club);
            ZetSeizoen(seizoen);
            ZetPrijs(prijs);
            Kledingmaat = kledingmaat;
            ZetClubSet(clubSet);
        }
        public int Id { get; private set; }
        public Club Club { get; private set; }
        public string Seizoen { get; private set; }
        public double Prijs { get; private set; }
        public Kledingmaat Kledingmaat { get; set; }
        public ClubSet ClubSet { get; private set; }
       
        public void ZetId(int id)
        {
            if (id <= 0) throw new VoetbaltruitjeException("Voetbaltruitje - invalid id");
            Id = id;
        }
        public void ZetPrijs(double prijs)
        {
            if (prijs <= 0) throw new VoetbaltruitjeException("Voetbaltruitje - invalid prijs");
            Prijs = prijs;
        }
        public void ZetSeizoen(string seizoen)
        {
            Seizoen = seizoen;
        }
        public void ZetClub(Club club)
        {
            Club = club ?? throw new VoetbaltruitjeException("ZetClub = null");
        }
        public void ZetClubSet(ClubSet clubSet)
        {
            ClubSet = clubSet ?? throw new VoetbaltruitjeException("ZetClubSet - null");
        }
        public override string ToString()
        {
            return $"{Id} - {Club.Ploeg} - {Seizoen} - {Prijs} - {Kledingmaat} - {ClubSet}";
        }

        public override bool Equals(object obj)
        {
            return obj is Voetbaltruitje voetbaltruitje &&
                   Id == voetbaltruitje.Id &&
                   EqualityComparer<Club>.Default.Equals(Club, voetbaltruitje.Club) &&
                   Seizoen == voetbaltruitje.Seizoen &&
                   Prijs == voetbaltruitje.Prijs &&
                   Kledingmaat == voetbaltruitje.Kledingmaat &&
                   EqualityComparer<ClubSet>.Default.Equals(ClubSet, voetbaltruitje.ClubSet);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Club, Seizoen, Prijs, Kledingmaat, ClubSet);
        }
    }
}
