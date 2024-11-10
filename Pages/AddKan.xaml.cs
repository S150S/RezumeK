using Microsoft.Win32;
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
using System.Diagnostics;
using System.IO;

namespace Rezume.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddKan.xaml
    /// </summary>
    public partial class AddKan : Page
    {
        private Kandidat _currentKandidat = new Kandidat();
        private byte[] _mainImageData;
        public AddKan()
        {
            InitializeComponent();
            DataContext = _currentKandidat;
        }

        private void BtnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog(); ofd.Filter = "Image |*.png; *.jpg; *.jpeg";
            if (ofd.ShowDialog() == true)
            {
                _mainImageData = File.ReadAllBytes(ofd.FileName);
                ImageTv.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(_mainImageData);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _currentKandidat.Parol_v_sistemu = Convert.ToString(Password.Password);
            _currentKandidat.Photo = _mainImageData;

            StringBuilder errors = new StringBuilder();

            if (_currentKandidat.Familiy == null)
                errors.AppendLine("Не задана фамилия кандидата");
            if (_currentKandidat.Imya == null)
                errors.AppendLine("Не задано имя кандидата");
            if (_currentKandidat.Otchestvj == null)
                errors.AppendLine("Не задано отчество кандидата");
            if (_currentKandidat.Date_rogd == null)
                errors.AppendLine("Не задана дата рождения кандидата");
            if (_currentKandidat.Telefon == null)
                errors.AppendLine("Не задан телефон кандидата");
            if (_currentKandidat.Pochta == null)
                errors.AppendLine("Не задана почта кандидата");
            if (_currentKandidat.Parol_v_sistemu == null)
                errors.AppendLine("Не задан пароль кандидата");
            if (_currentKandidat.Login_v_sistemu == null)
                errors.AppendLine("Не задан логин кандидата");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            RezumeEntities3.GetContext().Kandidat.Add(_currentKandidat);
            RezumeEntities3.GetContext().SaveChanges();
            MessageBox.Show("Информация сохранена");
            NavigationService.GoBack();
        }
    }
}
