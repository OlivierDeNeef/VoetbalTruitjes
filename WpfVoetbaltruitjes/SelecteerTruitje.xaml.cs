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
    /// Interaction logic for SelecteerTruitje.xaml
    /// </summary>
    public partial class SelecteerTruitje : Window
    {
        private readonly VoetbaltruitjeManager _voetbaltruitjeManager;
        private readonly ClubManager _clubManager;

        private List<Club> _alleClubs = new List<Club>();

        public bool IsOrder { get; private set; }

        public int Aantal { get; private set; }

        public SelecteerTruitje(VoetbaltruitjeManager voetbaltruitjeManager, ClubManager clubManager, bool isOrder)
        {
            _voetbaltruitjeManager = voetbaltruitjeManager;
            _clubManager = clubManager;
            InitializeComponent();
            ComboBoxMaat.ItemsSource = Enum.GetValues(typeof(Kledingmaat));
            _alleClubs = _clubManager.GeefAlleClubs();
            ComboBoxCompetitie.ItemsSource = _alleClubs.Select(c => c.Competitie);
            IsOrder = isOrder;
            if (!isOrder)
            {
                TextBoxAantal.Visibility = Visibility.Hidden;
                ButtonAddOne.Visibility = Visibility.Hidden;
                ButtonSubOne.Visibility = Visibility.Hidden;
                ButtonTruitjeSelecteren.Content = "Selecteer truitje";
                Grid.SetColumn(ButtonTruitjeSelecteren,1);
                Grid.SetColumnSpan(ButtonTruitjeSelecteren, 4);
            }

        }

        private void ComboBoxCompetitie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxCompetitie.SelectedItem != null)
            {
                ComboBoxClub.ItemsSource = _alleClubs
                    .Where(c => c.Competitie == ComboBoxCompetitie.SelectedItem.ToString())
                    .Select(c => c.Naam);
            }

            ;
        }

        private void ComboBoxClub_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }

        private void ButtonZoeken_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(!TruitjeZoekenIsValid()) return;
                List<Voetbaltruitje> voetbaltruitjes;
                if (TextBoxId.Text == "ALL")
                {
                    voetbaltruitjes = _voetbaltruitjeManager.GeefAlleVoetbaltruitjes();
                }
                else
                {
                    var id = string.IsNullOrWhiteSpace(TextBoxId.Text) ? 0 : int.Parse(TextBoxId.Text);
                    var versie = string.IsNullOrWhiteSpace(TextBoxVersie.Text) ? 0 : int.Parse(TextBoxVersie.Text);
                    var prijs = string.IsNullOrWhiteSpace(TextBoxPrijs.Text) ? 0 : double.Parse(TextBoxPrijs.Text);
                    var maat = ComboBoxMaat.SelectedItem == null ? default : Enum.Parse<Kledingmaat>(ComboBoxMaat.SelectedItem.ToString()) ;

                    voetbaltruitjes = _voetbaltruitjeManager.ZoekVoetbaltruitjes(id,
                        ComboBoxCompetitie.SelectionBoxItem.ToString(),
                        ComboBoxClub.SelectionBoxItem.ToString(), prijs, TextBoxSeizoen.Text, versie,
                        CheckBoxThuis.IsChecked.Value, CheckBoxUit.IsChecked.Value, maat);
                }

                ResultTruitjes.ItemsSource = voetbaltruitjes;

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Er ging iets mis");
            }
        }

        private bool TruitjeZoekenIsValid()
        {
            if (!string.IsNullOrWhiteSpace(TextBoxPrijs.Text) && double.TryParse(TextBoxPrijs.Text, out var _))
            {
                MessageBox.Show("Prijs kan alleen nummer bevatten", "Invalid field");
                return false;
            }
            if (!string.IsNullOrWhiteSpace(TextBoxVersie.Text) && int.TryParse(TextBoxVersie.Text, out var _))
            {
                MessageBox.Show("Versie kan alleen nummer bevatten", "Invalid field");
                return false;
            }
            if (!string.IsNullOrWhiteSpace(TextBoxVersie.Text) && !int.TryParse(TextBoxVersie.Text, out var _) &&  TextBoxVersie.Text != "ALL")
            {
                MessageBox.Show("Versie kan alleen nummer bevatten", "Invalid field");
                return false;
            }
            return true;
        }

        private void CheckBoxThuis_Checked(object sender, RoutedEventArgs e)
        {
            if (CheckBoxThuis.IsChecked == true)
            {
                CheckBoxUit.IsChecked = false;
            }
        }

        private void CheckBoxUit_Checked(object sender, RoutedEventArgs e)
        {
            if (CheckBoxUit.IsChecked == true)
            {
                CheckBoxThuis.IsChecked = false;
            }
        }

        private void ButtonAddOne_Click(object sender, RoutedEventArgs e)
        {
            Aantal++;
            TextBoxAantal.Text = Aantal.ToString();
        }

        private void ButtonSubOne_Click(object sender, RoutedEventArgs e)
        {
            if (Aantal > 1)
            {
                Aantal--;
                TextBoxAantal.Text = Aantal.ToString();
            }
        }

        private void ButtonTruitjeSelecteren_Click(object sender, RoutedEventArgs e)
        {
            if (ResultTruitjes.SelectedItem != null)
            {
                var truitje = (Voetbaltruitje) ResultTruitjes.SelectedItem;
                var main = Owner as MainWindow;
                main.SelectedVoetbaltruitje = truitje;
                if (!main.CurrentListVoetbaltruitjes.ContainsKey(truitje) && IsOrder)
                {
                    main.CurrentListVoetbaltruitjes.Add(truitje, int.Parse(TextBoxAantal.Text));
                }

                else if (main.CurrentListVoetbaltruitjes.ContainsKey(truitje) && IsOrder)
                {
                    var x = main.CurrentListVoetbaltruitjes[truitje];
                    main.CurrentListVoetbaltruitjes.Remove(truitje);
                    main.CurrentListVoetbaltruitjes.Add(truitje, int.Parse(TextBoxAantal.Text)+x);

                }
                this.Close();
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxMaat.SelectedValue = null;
            ComboBoxCompetitie.SelectedValue = null;
            ComboBoxClub.SelectedValue = null;
            TextBoxVersie.Text = "";
            TextBoxId.Text = "";
            TextBoxPrijs.Text = "";
            TextBoxAantal.Text = "";
            TextBoxSeizoen.Text = "";
        }
    }
}
