using System.Windows;

namespace WpfVoetbaltruitjes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelecteerKlantButtonToevoegen_Click(object sender, RoutedEventArgs e)
        {
            new SelecteerKlant(){Owner = this}.ShowDialog();
        }

        private void ButtonSelecteerTruitjeToevoegen_Click(object sender, RoutedEventArgs e)
        {
            new SelecteerTruitje(){Owner = this}.ShowDialog();
        }
    }
}
