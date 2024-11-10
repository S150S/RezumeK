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
using Rezume.DB;

namespace Rezume.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddVak.xaml
    /// </summary>
    public partial class AddVak : Page
    {
        private Vakanciy _currentVak = new Vakanciy();
        
        public AddVak()
        {
            InitializeComponent();
            DataContext = _currentVak;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            _currentVak.Date_open = DateTime.Now;
            _currentVak.Id_status_vakanciy = 1;

            StringBuilder errors = new StringBuilder();

            if (_currentVak.Name_vakanciy == null)
                errors.AppendLine("Не задано название вакансии");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            RezumeEntities3.GetContext().Vakanciy.Add(_currentVak);
            RezumeEntities3.GetContext().SaveChanges();
            MessageBox.Show("Информация сохранена");
            NavigationService.GoBack();
        }
    }
}
