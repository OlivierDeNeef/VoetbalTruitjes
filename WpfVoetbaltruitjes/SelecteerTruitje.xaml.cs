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

namespace WpfVoetbaltruitjes
{
    /// <summary>
    /// Interaction logic for SelecteerTruitje.xaml
    /// </summary>
    public partial class SelecteerTruitje : Window
    {
        private readonly VoetbaltruitjeManager _voetbaltruitjeManager;
        public SelecteerTruitje(VoetbaltruitjeManager voetbaltruitjeManager)
        {
            _voetbaltruitjeManager = voetbaltruitjeManager;
            InitializeComponent();
        }
    }
}
