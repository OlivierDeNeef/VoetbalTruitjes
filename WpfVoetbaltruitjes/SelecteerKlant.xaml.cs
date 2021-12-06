using Domain;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace WpfVoetbaltruitjes
{
    /// <summary>
    /// Interaction logic for SelecteerKlant.xaml
    /// </summary>
    public partial class SelecteerKlant : Window
    {
        private readonly KlantManager _klantManager;
        public SelecteerKlant(KlantManager klantManager)
        {
            _klantManager = klantManager;
            InitializeComponent();
        }

        private void ButtonKlantSelecteren_Click(object sender, RoutedEventArgs e)
        {
            if (ResultKlantZoeken.SelectedItem != null)
            {
                var klant = (Klant)ResultKlantZoeken.SelectedItem;
                var main = Owner as MainWindow;
                main.SelectedKlant = klant;
                this.Close();
            }
        }

        private void ButtonKlantZoeken_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!KlantZoekenIsValid()) return;
                List<Klant> klanten;
                if (TextBoxKlantIdSelecteren.Text == "ALL")
                {
                    klanten = _klantManager.GeefAlleKlanten();
                }
                else
                {
                    var id = string.IsNullOrWhiteSpace(TextBoxKlantIdSelecteren.Text) ? 0 : int.Parse(TextBoxKlantIdSelecteren.Text);
                    klanten = _klantManager.ZoekKlant(id, TextBoxNaamSelecteren.Text,
                        TextBoxAdresSelecteren.Text);
                }

                ResultKlantZoeken.ItemsSource = klanten;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Er ging iets fout");
            }
        }

        private bool KlantZoekenIsValid()
        {
            if (!string.IsNullOrWhiteSpace(TextBoxKlantIdSelecteren.Text) && !TextBoxKlantIdSelecteren.Text.All(char.IsDigit) && TextBoxKlantIdSelecteren.Text != "ALL")
            {
                MessageBox.Show("Id veld kan alleen nummers bevatten", "Invalid field");
                return false;
            }

            return true;
        }
    }
}
