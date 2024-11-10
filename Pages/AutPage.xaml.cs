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
    /// Логика взаимодействия для AutPage.xaml
    /// </summary>
    public partial class AutPage : Page
    {
        int count = 0;
        public AutPage()
        {
            InitializeComponent();
        }

        private async void AutClick(object sender, RoutedEventArgs e)
        {
            if (login.Text == "" || password.Password == "")
                MessageBox.Show("Не все поля заполнены");
            else
                if (RezumeEntities3.GetContext().HR.Select(item => item.Login_v_sistemu + " " + item.Parol_v_sistemu).Contains(login.Text + " " + password.Password))
            {
                HR currentHR = RezumeEntities3.GetContext().HR.Where(item => item.Login_v_sistemu.Equals(login.Text)).FirstOrDefault();
                MessageBox.Show("Вы авторизованы");
                NavigationService.Navigate(new KanbanRezumePage(currentHR.Id_HR));
            }
            else
            {
                count++;
                if (count == 3)
                {
                    login.IsEnabled = false;
                    password.IsEnabled = false;
                    MessageBox.Show("Неправильный пароль/логин. Ввод заблокирован на 60 секунд");
                    await Task.Delay(6000);
                    count = 0;
                    login.IsEnabled = true;
                    password.IsEnabled = true;
                }
                else
                    MessageBox.Show("Неправильный логин/пароль. У вас осталось попыток входа: " + Convert.ToString(3 - count));
            }


        }


        private void RegClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegPage());
        }
    }
}
