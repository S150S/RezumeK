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
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }

        private void RegClick(object sender, RoutedEventArgs e)
        {
            if (login.Text == "" || password.Password == "" || Familiy.Text == "" || FirstName.Text == "" || Otch.Text == "" || Pocht.Text=="" || Tel.Text == "")
                MessageBox.Show("Не все поля заполнены");
            else
            {
                HR newHR = new HR()
                {
                    Familiy = Familiy.Text,
                    Imya = FirstName.Text,
                    Otchestvj = Otch.Text,
                    Date_rogd = Convert.ToDateTime(DateB.SelectedDate),
                    Telefon = Tel.Text,
                    Pochta = Pocht.Text,
                    Login_v_sistemu = login.Text,
                    Parol_v_sistemu = password.Password,
                    
                };
                RezumeEntities3.GetContext().HR.Add(newHR);
                RezumeEntities3.GetContext().SaveChanges();
                NavigationService.Navigate(new AutPage());
                MessageBox.Show("Вы зарегистрированы");
            }
        }

        private void CanelClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AutPage());
        }
    }
}
