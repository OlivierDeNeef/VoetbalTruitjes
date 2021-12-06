using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Domain.Managers;
using Domain.Models;

namespace WpfVoetbaltruitjes
{
    /// <summary>
    /// Interaction logic for SelecteerClub.xaml
    /// </summary>
    public partial class SelecteerClub : Window
    {
        private readonly ClubManager _clubManager;
        public SelecteerClub(ClubManager clubManager)
        {
            _clubManager = clubManager;
            InitializeComponent();
        }

        private void ButtonClubZoeken_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ClubZoekenIsValid()) return;
                List<Club> clubs;
                if (TextBoxClubIdSelecteren.Text == "ALL")
                {
                    clubs = _clubManager.GeefAlleClubs();
                }
                else
                {
                    var id = string.IsNullOrWhiteSpace(TextBoxClubIdSelecteren.Text) ? 0 : int.Parse(TextBoxClubIdSelecteren.Text);
                    clubs = _clubManager.ZoekClubs(id, TextBoxNaamSelecteren.Text,
                        TextBoxCompitieSelecteren.Text);
                }

                ResultClubsZoeken.ItemsSource = clubs;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Er ging iets fout");
            }
        }

        private void ButtonClubSelecteren_Click(object sender, RoutedEventArgs e)
        {
            if (ResultClubsZoeken.SelectedItem != null)
            {
                var club = (Club)ResultClubsZoeken.SelectedItem;
                var main = Owner as MainWindow;
                main.SelectedClub = club;
                this.Close();
            }
        }
        private bool ClubZoekenIsValid()
        {
            if (!string.IsNullOrWhiteSpace(TextBoxClubIdSelecteren.Text) && !TextBoxClubIdSelecteren.Text.All(char.IsDigit) && TextBoxClubIdSelecteren.Text != "ALL")
            {
                MessageBox.Show("Id veld kan alleen nummers bevatten", "Invalid field");
                return false;
            }

            return true;
        }
    }
}
