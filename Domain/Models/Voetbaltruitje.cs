using System;
using System.Collections.Generic;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Domain.Models
{
    public class Voetbaltruitje 
    {
        public Voetbaltruitje(int id, Club club, string seizoen, double prijs, Kledingmaat kledingmaat, ClubSet clubSet)
            : this(club, seizoen, prijs, kledingmaat, clubSet)
        {
            ZetId(id);
        }
        public Voetbaltruitje(Club club, string seizoen, double prijs, Kledingmaat kledingmaat, ClubSet clubSet)
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
            return $"[Id]:{Id} - [Club]: {Club.Naam} - [Seizoen]: {Seizoen} - [Versie]: {ClubSet.Versie} - [Thuis truitje]: {ClubSet.Thuis} - [Prijs/Stuk] {Prijs}";
        }

        protected bool Equals(Voetbaltruitje other)
        {
            return Id == other.Id && Club == other.Club && Seizoen == other.Seizoen && Prijs == other.Prijs && Kledingmaat == other.Kledingmaat && ClubSet == other.ClubSet;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Voetbaltruitje) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Club, Seizoen, Prijs, (int) Kledingmaat, ClubSet);
        }

        public static bool operator ==(Voetbaltruitje left, Voetbaltruitje right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Voetbaltruitje left, Voetbaltruitje right)
        {
            return !Equals(left, right);
        }
    }
}
