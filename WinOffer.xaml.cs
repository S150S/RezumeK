using Rezume.DB;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Rezume
{
    /// <summary>
    /// Логика взаимодействия для WinOffer.xaml
    /// </summary>
    public partial class WinOffer : Window
    {
        private RezumeV _currentRez = new RezumeV();
        public WinOffer(RezumeV RezumeOffer)
        {
            InitializeComponent();
            _currentRez = RezumeOffer;
            InitializeComponent();
            InfRez.Text = RezumeOffer.Id_rezume.ToString() + " " + RezumeOffer.Kandidat.Familiy + " " + RezumeOffer.Kandidat.Imya + " " + RezumeOffer.Kandidat.Otchestvj + " (" + RezumeOffer.Vakanciy.Name_vakanciy + ")";
            DataContext = RezumeOffer;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _currentRez.Id_rezume = _currentRez.Id_rezume;
            if (OfferChec.IsChecked == true)
                _currentRez.Id_status = 7;
            
            
            RezumeEntities3.GetContext().SaveChanges();
            MessageBox.Show("Информация сохранена");
            this.Close();

        }
    }
}
