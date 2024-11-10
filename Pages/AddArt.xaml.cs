using Microsoft.Win32;
using Rezume.DB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using static System.Net.Mime.MediaTypeNames;

namespace Rezume.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddArt.xaml
    /// </summary>
    public partial class AddArt : Page
    {
        private Artefact_rezume _currentArtRez = new Artefact_rezume();
        private RezumeV _currentRez = new RezumeV();
        private byte[] _mainData; 
        public AddArt(RezumeV RezumeArtAdd)
        {
            _currentRez= RezumeArtAdd;
            InitializeComponent();
            InfRez.Text = RezumeArtAdd.Id_rezume.ToString() + " "+ RezumeArtAdd.Kandidat.Familiy + " " + RezumeArtAdd.Kandidat.Imya + " " + RezumeArtAdd.Kandidat.Otchestvj + " (" + RezumeArtAdd.Vakanciy.Name_vakanciy+")";
            DataContext = RezumeArtAdd;

            List<Tip_artefact> allTip = RezumeEntities3.GetContext().Tip_artefact.ToList(); 
            TipComboBox.ItemsSource = allTip;
            TipComboBox.DisplayMemberPath = "Name_tip";
            TipComboBox.SelectedValuePath = "Id_tip";
        }

        

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _currentArtRez.Id_rezume = _currentRez.Id_rezume;
            _currentArtRez.Id_tip = Convert.ToInt32(TipComboBox.SelectedValue);
            _currentArtRez.Fail = _mainData;
            RezumeEntities3.GetContext().Artefact_rezume.Add(_currentArtRez);
            RezumeEntities3.GetContext().SaveChanges();
            MessageBox.Show("Информация сохранена");
            NavigationService.GoBack();
        }

        private void LoadFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog()
                {
                    Filter = "Документы (*.doc;*.pdf;*.xlsx)|*.doc;*.pdf;*.xlsx|Все файлы (*.*)|*.*", // Указываем фильтр
                    Title = "Выберите файл"
                };


            if (ofd.ShowDialog() == true)
            {
                fileListBox.Items.Clear();
                string filePath = ofd.FileName;
                _mainData = File.ReadAllBytes(ofd.FileName);
                fileListBox.Items.Add(filePath); // Добавляем путь к файлу в список
                LoadFile.Content = "Заменить файл";

                // Открываем файл в соответствующем приложении
                Process.Start(new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true // Используем оболочку для открытия файла
                });
            }
        }
    }
}
