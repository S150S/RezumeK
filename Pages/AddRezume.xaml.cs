using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using Rezume.DB;

namespace Rezume.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddRezume.xaml
    /// </summary>
    public partial class AddRezume : Page
    {
        private RezumeV _currentRez = new RezumeV();
        public AddRezume()
        {
            InitializeComponent();
            DataContext = _currentRez;
            List<Kandidat> allKan = RezumeEntities3.GetContext().Kandidat.ToList();
            KandComboBox.ItemsSource = allKan;
            KandComboBox.DisplayMemberPath = "Familiy";
            KandComboBox.SelectedValuePath = "Id_kandidat";
            List<Vakanciy> allVakanciy = RezumeEntities3.GetContext().Vakanciy.ToList(); 
            VakComboBox.ItemsSource = allVakanciy;
            VakComboBox.DisplayMemberPath = "Name_vakanciy";
            VakComboBox.SelectedValuePath = "Id_vakanciy";
            List<HR> allHR= RezumeEntities3.GetContext().HR.ToList();
            HRComboBox.ItemsSource = allHR;
            HRComboBox.DisplayMemberPath = "Familiy";
            HRComboBox.SelectedValuePath = "Id_HR";
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _currentRez.Id_kandidat = Convert.ToInt32(KandComboBox.SelectedValue);
            _currentRez.Id_vakanciy = Convert.ToInt32(VakComboBox.SelectedValue);
            _currentRez.Id_HR = Convert.ToInt32(HRComboBox.SelectedValue);
            _currentRez.Date_sozdaniy = DateTime.Now;
            _currentRez.Id_status = 1;
            
            StringBuilder errors = new StringBuilder();
            
            if (_currentRez.Id_kandidat == 0)
                errors.AppendLine("Не задан кандидат");
            if (_currentRez.Id_vakanciy == 0)
                errors.AppendLine("Не задана вакансия");



            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            RezumeEntities3.GetContext().RezumeV.Add(_currentRez);
            RezumeEntities3.GetContext().SaveChanges();
            MessageBox.Show("Информация сохранена");
            NavigationService.GoBack();
        }
    }
}
