using Domain.Exceptions;
using Domain.Interfaces;

namespace Domain.Models
{
    public class Voetbaltruitje : IVoetbaltruitje
    {

        public int Id { get; private set;} 

        public Kledingmaat Kledingmaat { get; set;}

        public string Seizoen { get; private set; }

        public double Prijs {get; private set; }

        public ClubSet ClubSet { get ; private set; }

        public Club Club { get; private set; }



        public void ZetId(int id)
        {
            if (id <= 0) throw new TruitjeException("De id van een voetbaltruitje moet meer dan 0 zijn");
            this.Id = id;

        }

        public void ZetPrijs(double prijs)
        {
            if (prijs <= 1) throw new TruitjeException("De prijs van een voetbaltruitje moet meer dan 0 zijn");
            if (prijs == this.Prijs) throw new TruitjeException("De nieuwe prijs kan niet dezelfde zijn als de huidige prijs");
            this.Prijs = prijs;
        }

        public void ZetSeizoen(string seizoen)
        {
            if (string.IsNullOrEmpty(seizoen.Trim())) throw new TruitjeException("Seizoen kan geen lege waarde bevatten");
            if (seizoen == this.Seizoen) throw new TruitjeException("Het seizoen kan niet het zelfde zijn");
            this.Seizoen = seizoen;
        }

        public void ZetClubSet(ClubSet clubSet)
        {
            this.ClubSet = clubSet ?? throw new TruitjeException("Clubset kan niet null zijn");
        }

        public void ZetClub(Club club)
        {
            this.Club = club ?? throw new TruitjeException("Club kan niet null zijn");
        }
      

        
    }
}