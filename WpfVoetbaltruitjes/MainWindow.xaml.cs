using Domain;
using Domain.Models;
using System;
using System.Collections.Generic;
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

        private List<Club> _alleClubs = new List<Club>();
        
        public Klant SelectedKlant { get; set; }
        public Club SelectedClub { get; set; }



        public MainWindow(KlantManager klantManager, ClubManager clubManager)
        {
            _klantManager = klantManager;
            _clubManager = clubManager;
            InitializeComponent();
        }



        private void TabControlMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TabControlMain.SelectedIndex == 2)
            {
                TextBoxKlantToevoegen.Text = "";
                TextBoxAdresKlantToevoegen.Text = "";
            }
            else if (TabControlMain.SelectedIndex == 3)
            {
                TextBoxKlantIdAanpassen.Text = "";
                TextBoxAdresAanpassenKlant.Text = "";
                TextBoxNaamAanpassenKlant.Text = "";
            }
            else if (TabControlMain.SelectedIndex == 4)
            {
                TextBoxPrijsTruitjeToevoegen.Text = "";
                TextBoxSeizoenTruitjeToevoegen.Text = "";
                TextBoxVersieTruitjeToevoegen.Text = "";
                RadioButtonUitTruitjeToevoegen.IsChecked = true;
                
                RadioButtonUitTruitjeToevoegen.IsChecked = false;
                ComboBoxClubTruitjeToevoegen.SelectedItem = null;
                ComboBoxCompetitieTruitjeToevoegen.SelectedItem = null;
                _alleClubs = _clubManager.GeefAlleClubs();
                ComboBoxCompetitieTruitjeToevoegen.ItemsSource = _alleClubs.Select(c => c.Competitie).Distinct();
            }
            else if (TabControlMain.SelectedIndex == 6)
            {
                TextBoxNaamClubToevoegen.Text = "";
                TextBoxCompetitieClubToevoegen.Text = "";
            }
            else if (TabControlMain.SelectedIndex == 7)
            {
                TextBoxNaamAanpassenClub.Text = "";
                TextBoxClubIdAanpassen.Text = "";
                TextBoxCompetitieAanpassenClub.Text = "";
            }
        }

        #endregion


        #region TabBestellingToevoegen

        private void SelecteerKlantButtonToevoegen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonSelecteerTruitjeToevoegen_Click(object sender, RoutedEventArgs e)
        {
            new SelecteerTruitje() { Owner = this }.ShowDialog();
        }

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
                var updateClub = new Club(SelectedClub.Id, TextBoxNaamAanpassenClub.Text, TextBoxCompetitieAanpassenClub.Text);
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
            throw new NotImplementedException();
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

        //todo : validation

        #endregion
    }
}
