using Domain;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Domain.Managers;

namespace WpfVoetbaltruitjes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Algemeen

        private readonly KlantManager _klantManager;
        private readonly ClubManager _clubManager;
        private readonly VoetbaltruitjeManager _voetbaltruitjeManager;


        private List<Club> _alleClubs = new List<Club>();
        public Bestelling SelectedBestelling { get; set; }
        public Klant SelectedKlant { get; set; }
        public Club SelectedClub { get; set; }
        public Dictionary<Voetbaltruitje, int> CurrentListVoetbaltruitjes = new Dictionary<Voetbaltruitje, int>();
        public Voetbaltruitje SelectedVoetbaltruitje { get; set; }



        public MainWindow(KlantManager klantManager, ClubManager clubManager, VoetbaltruitjeManager voetbaltruitjeManager)
        {
            _klantManager = klantManager;
            _clubManager = clubManager;
            _voetbaltruitjeManager = voetbaltruitjeManager;
            InitializeComponent();
            ComboBoxMaatTruitjeToevoegen.ItemsSource = Enum.GetValues<Kledingmaat>();
        }



        private void TabControlMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (TabControlMain.SelectedIndex)
            {
                case 0:
                    TextBoxKlantToevoegenBijbestelling.Text = "";
                    TextBoxPrijsToevoegen.Text = "";
                    CheckboxBetaaldToevoegen.IsChecked = false;
                    CurrentListVoetbaltruitjes = new Dictionary<Voetbaltruitje, int>();
                    BestellingToevoegenResult.ItemsSource = null;
                    break;
                case 1: 
                    break;
                case 2:
                    TextBoxNaamKlantToevoegen.Text = "";
                    TextBoxAdresKlantToevoegen.Text = "";
                    break;
                case 3:
                    TextBoxKlantIdAanpassen.Text = "";
                    TextBoxAdresAanpassenKlant.Text = "";
                    TextBoxNaamAanpassenKlant.Text = "";
                    break;
                case 4:
                    TextBoxPrijsTruitjeToevoegen.Text = "";
                    TextBoxSeizoenTruitjeToevoegen.Text = "";
                    TextBoxVersieTruitjeToevoegen.Text = "";
                    RadioButtonUitTruitjeToevoegen.IsChecked = true;
                    RadioButtonUitTruitjeToevoegen.IsChecked = false;
                    ComboBoxClubTruitjeToevoegen.SelectedItem = null;
                    ComboBoxCompetitieTruitjeToevoegen.SelectedItem = null;
                    ComboBoxMaatTruitjeToevoegen.SelectedItem = null;
                    _alleClubs = _clubManager.GeefAlleClubs();
                    ComboBoxCompetitieTruitjeToevoegen.ItemsSource = _alleClubs.Select(c => c.Competitie).Distinct();
                    break;
                case 5:
                    TextBoxIdTruitjeAanpassen.Text = "";
                    TextBoxPrijsTruitjeAanpassen.Text = "";
                    TextBoxSeizoenTruitjeAanpassen.Text = "";
                    TextBoxVersieTruitjeAanpassen.Text = "";
                    ComboBoxClubTruitjeAanpassen.SelectedItem = null;
                    ComboBoxCompetitieTruitjeAanpassen.SelectedItem = null;
                    ComboBoxMaatTruitjeAanpassen.SelectedItem = null;
                    break;
                case 6:
                    TextBoxNaamClubToevoegen.Text = "";
                    TextBoxCompetitieClubToevoegen.Text = "";
                    break;
                case 7:
                    TextBoxNaamAanpassenClub.Text = "";
                    TextBoxClubIdAanpassen.Text = "";
                    TextBoxCompetitieAanpassenClub.Text = "";
                    break;
            }
        }

        #endregion


        #region TabBestellingToevoegen

     


        #endregion

        #region TabKlantToevoegen
        private void ButtonKlantToevoegen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (KlantToevoegenIsValid())
                {
                    _klantManager.VoegToeKlant(new Klant(TextBoxNaamKlantToevoegen.Text, TextBoxAdresKlantToevoegen.Text));
                    TextBoxNaamKlantToevoegen.Text = "";
                    TextBoxAdresKlantToevoegen.Text = "";
                    MessageBox.Show("De klant is succesvol toegevoegd", "Bevestiging");

                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Er ging iets mis");
            }
        }

        private bool KlantToevoegenIsValid()
        {
            if (string.IsNullOrWhiteSpace(TextBoxNaamKlantToevoegen.Text))
            {
                MessageBox.Show("Naam veld moet worden ingevuld", "Invalid field");
                return false;
            }
            if (string.IsNullOrWhiteSpace(TextBoxAdresKlantToevoegen.Text))
            {
                MessageBox.Show("Adres veld moet worden ingevuld", "Invalid field");
                return false;
            }

            return true;
        }


        #endregion

        
        #region TabKlantAanpassen

        private void ButtonKlantSelecterenVoorKlantAanpassen_Click(object sender, RoutedEventArgs e)
        {
            SelectedKlant = null;

            new SelecteerKlant(_klantManager) { Owner = this }.ShowDialog();

            if (SelectedKlant != null)
            {
                TextBoxKlantIdAanpassen.Text = SelectedKlant.KlantId.ToString();
                TextBoxAdresAanpassenKlant.Text = SelectedKlant.Adres;
                TextBoxNaamAanpassenKlant.Text = SelectedKlant.Naam;
            }
        }

        private void ButtonKlantAanpassen_Click(object sender, RoutedEventArgs e)
        {
            if (!KlantAanpassenIsValid()) return;
            try
            {
                var updateKlant = new Klant(SelectedKlant.KlantId, TextBoxNaamAanpassenKlant.Text, TextBoxAdresAanpassenKlant.Text);
                if (updateKlant != SelectedKlant)
                {
                    _klantManager.UpdateKlant(updateKlant);
                    MessageBox.Show("De klant is aangepast", "bevestiging");
                    SelectedKlant = updateKlant;
                }
                else
                {
                    MessageBox.Show("Er zijn geen veranderingen aangebracht", "Opgelet");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Er ging iets mis");
            }
        }

        private bool KlantAanpassenIsValid()
        {

            if (string.IsNullOrWhiteSpace(TextBoxNaamAanpassenKlant.Text))
            {
                MessageBox.Show("Naam veld moet worden ingevuld", "Invalid field");
                return false;
            }
            if (string.IsNullOrWhiteSpace(TextBoxAdresAanpassenKlant.Text))
            {
                MessageBox.Show("Adres veld moet worden ingevuld", "Invalid field");
                return false;
            }

            return true;
        }

        private void ButtonKlantVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedKlant == null) return;
            try
            {
                if (MessageBox.Show("Bent u zeker dat u de klant wilt verwijderen?", "Bevestigen", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _klantManager.VerwijderKlant(SelectedKlant);
                    TextBoxNaamAanpassenKlant.Text = "";
                    TextBoxAdresAanpassenKlant.Text = "";
                    TextBoxKlantIdAanpassen.Text = "";
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Er ging iets mis");
            }

        }


        #endregion

        #region TabClubToevoegen

        private void ButtonClubToevoegen_Click(object sender, RoutedEventArgs e)
        {
            if (!ClubToevoegenIsValid()) return;
            try
            {
                _clubManager.VoegClubToe(new Club(TextBoxCompetitieClubToevoegen.Text,TextBoxNaamClubToevoegen.Text));
                TextBoxCompetitieClubToevoegen.Text = "";
                TextBoxNaamClubToevoegen.Text = "";
                MessageBox.Show("Club is toegevoegd ", "Bevestiging");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Er ging iets mis");
            }
        }

        private bool ClubToevoegenIsValid()
        {
            if (string.IsNullOrWhiteSpace(TextBoxNaamClubToevoegen.Text))
            {
                MessageBox.Show("Naam veld moet worden ingevuld", "Invalid field");
                return false;
            }
            if (string.IsNullOrWhiteSpace(TextBoxCompetitieClubToevoegen.Text))
            {
                MessageBox.Show("Competitie veld moet worden ingevuld", "Invalid field");
                return false;
            }

            return true;
        }


        #endregion

        #region TabClubAanpassen

        private void ButtonClubSelecterenVoorAanpassen_Click(object sender, RoutedEventArgs e)
        {
            SelectedClub = null;
            new SelecteerClub(_clubManager) { Owner = this }.ShowDialog();
            if (SelectedClub != null)
            {
                TextBoxClubIdAanpassen.Text = SelectedClub.Id.ToString();
                TextBoxNaamAanpassenClub.Text = SelectedClub.Naam;
                TextBoxCompetitieAanpassenClub.Text = SelectedClub.Competitie;
            }
        }

        private void ButtonClubVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedClub == null) return;
            try
            {
                if (MessageBox.Show("Bent u zeker dat u de club wilt verwijderen?", "Bevestigen", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _clubManager.VerwijderClub(SelectedClub.Id);
                    TextBoxNaamAanpassenClub.Text = "";
                    TextBoxCompetitieAanpassenClub.Text = "";
                    TextBoxClubIdAanpassen.Text = "";
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Er ging iets mis");
            }

        }

        private void ButtonClubAanpassen_Click(object sender, RoutedEventArgs e)
        {
            if (!ClubAanpassenIsValid()) return;
            try
            {
                var updateClub = new Club(SelectedClub.Id, TextBoxCompetitieAanpassenClub.Text, TextBoxNaamAanpassenClub.Text);
                if (updateClub != SelectedClub)
                {
                    _clubManager.UpdateClub(updateClub);
                    MessageBox.Show("De club is aangepast", "bevestiging");
                    SelectedClub = updateClub;
                }
                else
                {
                    MessageBox.Show("Er zijn geen veranderingen aangebracht", "Opgelet");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Er ging iets mis");
            }
        }

        private bool ClubAanpassenIsValid()
        {
            if (string.IsNullOrWhiteSpace(TextBoxNaamAanpassenClub.Text))
            {
                MessageBox.Show("Naam veld moet worden ingevuld", "Invalid field");
                return false;
            }
            if (string.IsNullOrWhiteSpace(TextBoxCompetitieAanpassenClub.Text))
            {
                MessageBox.Show("Competitie veld moet worden ingevuld", "Invalid field");
                return false;
            }

            return true;
        }


        #endregion

        #region TabTruitjeToevoegen

        private void ButtonTruitjeToevoegen_Click(object sender, RoutedEventArgs e)
        {
            if (TruitjeToevoegenIsVaid())
            {
                try
                {
                    var club = _alleClubs.FirstOrDefault(c =>
                        c.Naam == ComboBoxClubTruitjeToevoegen.SelectedValue.ToString() &&
                        c.Competitie == ComboBoxCompetitieTruitjeToevoegen.SelectedValue.ToString());


                    var clubset = new ClubSet(RadioButtonThuisTruitjeToevoegen.IsChecked.Value,
                        int.Parse(TextBoxVersieTruitjeToevoegen.Text));

                    var maat = (Kledingmaat)Enum.Parse(typeof(Kledingmaat), ComboBoxMaatTruitjeToevoegen.SelectedValue.ToString(), true);

                    var truitje = new Voetbaltruitje(club, TextBoxSeizoenTruitjeToevoegen.Text,
                        double.Parse(TextBoxPrijsTruitjeToevoegen.Text),maat ,clubset);
                    _voetbaltruitjeManager.VoegVoetbalTruitjeToe(truitje);
                    MessageBox.Show("Het voetbaltruitje is toegevoegd", "Bevestiging");
                    ClearTruitjeToevoegen();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Er ging iets mis");
                }

            }
        }

        private void ComboBoxCompetitieTruitjeToevoegen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cbx = sender as ComboBox;

            if (cbx.SelectedItem != null)
            {
                ComboBoxClubTruitjeToevoegen.ItemsSource =
                    _alleClubs.Where(c => c.Competitie == cbx.SelectedItem.ToString()).Select(c => c.Naam);
            }
            e.Handled = true;
        }

        private void ComboBoxClubTruitjeToevoegen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }

        private bool TruitjeToevoegenIsVaid()
        {

            if (ComboBoxCompetitieTruitjeToevoegen.SelectedItem == null)
            {
                MessageBox.Show("Er moet een competitie zijn geselecteerd", "invalid field");
                return false;
            }
            if (ComboBoxClubTruitjeToevoegen.SelectedItem == null)
            {
                MessageBox.Show("Er moet een club zijn geselecteerd", "invalid field");
                return false;
            }
            if (string.IsNullOrWhiteSpace(TextBoxSeizoenTruitjeToevoegen.Text))
            {
                MessageBox.Show("Er moet een seizoen worden ingevuld", "invalid field");
                return false;
            }
            if (string.IsNullOrWhiteSpace(TextBoxPrijsTruitjeToevoegen.Text))
            {
                MessageBox.Show("Er moet een prijs worden ingevuld", "invalid field");
                return false;
            }
            else if (!double.TryParse(TextBoxPrijsTruitjeToevoegen.Text,out var _))
            {
                MessageBox.Show("Een prijs kan alleen nummers bevatten", "invalid field");
                return false;
            }
            if (string.IsNullOrWhiteSpace(TextBoxVersieTruitjeToevoegen.Text))
            {
                MessageBox.Show("Er moet een versie worden ingevuld", "invalid field");
                return false;
            }
            else if (!int.TryParse(TextBoxVersieTruitjeToevoegen.Text, out var _))
            {
                MessageBox.Show("Een prijs kan alleen nummers bevatten", "invalid field");
                return false;
            }
            if (RadioButtonUitTruitjeToevoegen.IsChecked == false &&RadioButtonThuisTruitjeToevoegen.IsChecked == false )
            {
                MessageBox.Show("Er moet 1 van de twee radiobutton geslecteerd zijn uit of thuis", "invalid field");
                return false;
            }
            if (ComboBoxMaatTruitjeToevoegen.SelectedItem == null)
            {
                MessageBox.Show("Er moet een maat zijn geselecteerd", "invalid field");
                return false;
            }
            return true;
        }

        private void ClearTruitjeToevoegen()
        {
            TextBoxPrijsTruitjeToevoegen.Text = "";
            TextBoxSeizoenTruitjeToevoegen.Text = "";
            TextBoxVersieTruitjeToevoegen.Text = "";
            RadioButtonUitTruitjeToevoegen.IsChecked = true;
            RadioButtonUitTruitjeToevoegen.IsChecked = false;
            ComboBoxClubTruitjeToevoegen.SelectedItem = null;
            ComboBoxCompetitieTruitjeToevoegen.SelectedItem = null;
            ComboBoxMaatTruitjeToevoegen.SelectedItem = null;
        }


        #endregion

        #region TabTruitjeAanpassen

        private void ButtonSelecteerTruitjeAanpassen_Click(object sender, RoutedEventArgs e)
        {
            SelectedVoetbaltruitje = null;
            new SelecteerTruitje(_voetbaltruitjeManager, _clubManager, false) {Owner = this}.ShowDialog();
            if (SelectedVoetbaltruitje != null)
            {
                ComboBoxCompetitieTruitjeAanpassen.ItemsSource = _clubManager.GeefAlleClubs().Select(c => c.Competitie).Distinct();
                ComboBoxClubTruitjeAanpassen.ItemsSource = _clubManager.GeefAlleClubs()
                    .Where(c => c.Competitie == SelectedVoetbaltruitje.Club.Competitie).Select(c => c.Naam);
                ComboBoxMaatTruitjeAanpassen.ItemsSource = Enum.GetValues<Kledingmaat>();
                TextBoxIdTruitjeAanpassen.Text = SelectedVoetbaltruitje.Id.ToString(); 
                ComboBoxCompetitieTruitjeAanpassen.SelectedValue = SelectedVoetbaltruitje.Club.Competitie;
                ComboBoxClubTruitjeAanpassen.SelectedValue = SelectedVoetbaltruitje.Club.Naam;
                TextBoxSeizoenTruitjeAanpassen.Text = SelectedVoetbaltruitje.Seizoen;
                TextBoxPrijsTruitjeAanpassen.Text = SelectedVoetbaltruitje.Prijs.ToString(CultureInfo.InvariantCulture);
                TextBoxVersieTruitjeAanpassen.Text = SelectedVoetbaltruitje.ClubSet.Versie.ToString();
                RadioButtonThuisTruitjeAanpassen.IsChecked = SelectedVoetbaltruitje.ClubSet.Thuis;
                RadioButtonUitTruitjeAanpassen.IsChecked = !SelectedVoetbaltruitje.ClubSet.Thuis;
                ComboBoxMaatTruitjeAanpassen.SelectedValue = SelectedVoetbaltruitje.Kledingmaat;
                

            }
        }

        private void ComboBoxCompetitieTruitjeAanpassen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cbx = sender as ComboBox;

            if (cbx.SelectedItem != null)
            {
                ComboBoxClubTruitjeAanpassen.ItemsSource =
                    _clubManager.GeefAlleClubs().Where(c => c.Competitie == cbx.SelectedItem.ToString()).Select(c => c.Naam);
            }
            e.Handled = true;
        }

        private void ComboBoxMaatTruitjeAanpassen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }

        private void ButtonTruitjeVerwijderenAanpassen_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedVoetbaltruitje == null) return;
            try
            {
                if (MessageBox.Show("Bent u zeker dat u het truitje wilt verwijderen?", "Bevestigen", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _voetbaltruitjeManager.VerwijderVoetbaltruitje(SelectedVoetbaltruitje.Id);
                    TextBoxIdTruitjeAanpassen.Text = "";
                    TextBoxPrijsTruitjeAanpassen.Text = "";
                    TextBoxSeizoenTruitjeAanpassen.Text = "";
                    TextBoxVersieTruitjeAanpassen.Text = "";
                    RadioButtonUitTruitjeAanpassen.IsChecked = true;
                    RadioButtonUitTruitjeAanpassen.IsChecked = false;
                    ComboBoxClubTruitjeAanpassen.SelectedItem = null;
                    ComboBoxCompetitieTruitjeAanpassen.SelectedItem = null;
                    ComboBoxMaatTruitjeAanpassen.SelectedItem = null;
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Er ging iets mis");
            }
        }

        private void ButtonTruitjeAanpassen_Click(object sender, RoutedEventArgs e)
        {
            if (!TruitjeAanpassenIsValid()) return;
            try
            {

                var club = _clubManager.GeefAlleClubs().FirstOrDefault(c =>
                    c.Naam == ComboBoxClubTruitjeAanpassen.SelectedValue.ToString() &&
                    c.Competitie == ComboBoxCompetitieTruitjeAanpassen.SelectedValue.ToString());

                var clubset = new ClubSet(RadioButtonThuisTruitjeAanpassen.IsChecked.Value,
                    int.Parse(TextBoxVersieTruitjeAanpassen.Text));

                var maat = (Kledingmaat)Enum.Parse(typeof(Kledingmaat), ComboBoxMaatTruitjeAanpassen.SelectedValue.ToString(), true);

                var updatedTruitje = new Voetbaltruitje(SelectedVoetbaltruitje.Id, club, TextBoxSeizoenTruitjeAanpassen.Text,
                    double.Parse(TextBoxPrijsTruitjeAanpassen.Text), maat, clubset);
                
                if (!updatedTruitje.Equals(SelectedVoetbaltruitje))
                {
                    _voetbaltruitjeManager.UpdateVoetbaltruitje(updatedTruitje);
                    MessageBox.Show("Het truitje is aangepast", "bevestiging");
                    SelectedVoetbaltruitje = updatedTruitje;
                }
                else
                {
                    MessageBox.Show("Er zijn geen veranderingen aangebracht", "Opgelet");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Er ging iets mis");
            }
        }

        private bool TruitjeAanpassenIsValid()
        {
            if (ComboBoxCompetitieTruitjeAanpassen.SelectedItem == null)
            {
                MessageBox.Show("Er moet een competitie zijn geselecteerd", "invalid field");
                return false;
            }
            if (ComboBoxClubTruitjeAanpassen.SelectedItem == null)
            {
                MessageBox.Show("Er moet een club zijn geselecteerd", "invalid field");
                return false;
            }
            if (string.IsNullOrWhiteSpace(TextBoxSeizoenTruitjeAanpassen.Text))
            {
                MessageBox.Show("Er moet een seizoen worden ingevuld", "invalid field");
                return false;
            }
            if (string.IsNullOrWhiteSpace(TextBoxPrijsTruitjeAanpassen.Text))
            {
                MessageBox.Show("Er moet een prijs worden ingevuld", "invalid field");
                return false;
            }
            else if (!double.TryParse(TextBoxPrijsTruitjeAanpassen.Text, out var _))
            {
                MessageBox.Show("Een prijs kan alleen nummers bevatten", "invalid field");
                return false;
            }
            if (string.IsNullOrWhiteSpace(TextBoxVersieTruitjeAanpassen.Text))
            {
                MessageBox.Show("Er moet een versie worden ingevuld", "invalid field");
                return false;
            }
            else if (!int.TryParse(TextBoxVersieTruitjeAanpassen.Text, out var _))
            {
                MessageBox.Show("Een prijs kan alleen nummers bevatten", "invalid field");
                return false;
            }
            if (RadioButtonUitTruitjeAanpassen.IsChecked == false && RadioButtonThuisTruitjeAanpassen.IsChecked == false)
            {
                MessageBox.Show("Er moet 1 van de twee radiobutton geslecteerd zijn uit of thuis", "invalid field");
                return false;
            }
            if (ComboBoxMaatTruitjeAanpassen.SelectedItem == null)
            {
                MessageBox.Show("Er moet een maat zijn geselecteerd", "invalid field");
                return false;
            }
            return true;
        }

        #endregion

        

        private void SelecteerKlantButtonToevoegenAanbestelling_Click(object sender, RoutedEventArgs e)
        {

            SelectedKlant = null;
            new SelecteerKlant(_klantManager) { Owner = this }.ShowDialog();
            if (SelectedKlant != null)
            {
                TextBoxKlantToevoegenBijbestelling.Text = SelectedKlant.ToString();
            }
        }

        private void ButtonTruitjeToevoegenBijBestelling_Click(object sender, RoutedEventArgs e)
        {
            new SelecteerTruitje(_voetbaltruitjeManager, _clubManager, true) {Owner = this}.ShowDialog();
            BestellingToevoegenResult.ItemsSource = new Dictionary<Voetbaltruitje,int>(CurrentListVoetbaltruitjes);
            BestellingToevoegenResult.Items.Refresh();
            TextBoxPrijsToevoegen.Text = CurrentListVoetbaltruitjes.Sum(v => (v.Key.Prijs * v.Value)).ToString();
        }

        private void BestellingToevoegenResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }


        private void ButtonRemoveTruitje_OnClick(object sender, RoutedEventArgs e)
        {
            if (BestellingToevoegenResult.SelectedItem != null)
            {
                var truitje = (KeyValuePair<Voetbaltruitje,int>) BestellingToevoegenResult.SelectedItem;
                CurrentListVoetbaltruitjes.Remove(truitje.Key);
                BestellingToevoegenResult.ItemsSource = new Dictionary<Voetbaltruitje, int>(CurrentListVoetbaltruitjes);
                BestellingToevoegenResult.Items.Refresh();
                TextBoxPrijsToevoegen.Text = CurrentListVoetbaltruitjes.Sum(v => (v.Key.Prijs * v.Value)).ToString();
            }
        }

        private void ButtonPlaatBestelling_Click(object sender, RoutedEventArgs e)
        {
            
        }


        private void ButtonBestellingSelecterenAanpassen_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
