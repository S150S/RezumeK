using Syncfusion.UI.Xaml.Kanban;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Syncfusion.Data.Extensions;


namespace Rezume.Pages
{
    /// <summary>
    /// Логика взаимодействия для KanbanRezumePage.xaml
    /// </summary>
    public partial class KanbanRezumePage : Page
    {
        List<RezumeV> currentRezume = RezumeEntities3.GetContext().RezumeV.ToList();
        int idVhHR;
        ObservableCollection<KanbanModel> Tasks = new ObservableCollection<KanbanModel>();
        KanbanModel[,] matCard = new KanbanModel[RezumeEntities3.GetContext().RezumeV.Count(), RezumeEntities3.GetContext().RezumeV.Count()];

        public KanbanRezumePage(int idHR)
        {
            InitializeComponent();
            idVhHR = idHR;
            currentRezume = currentRezume.Where(x => x.Id_HR == idHR || x.Id_HR == null).ToList();
            string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            foreach (var file in Directory.GetFiles(System.IO.Path.Combine(projectDirectory, "temp")))
            {
                File.Delete(file);
            }
            UpdateKanban();
            if (currentRezume.Count == 0)
            {
                MessageBox.Show("У вас нет резюме для рассмотрения");
            }
            DeleteMatCard();
            UpdateKanban();

        }

        int countRez = 0;
        public void UpdateKanban()
        {
            Tasks.Clear();
            int Indmass = 0;
            string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            int numRow1 = 0, numRow2 = 0, numRow3 = 0, numRow4 = 0, numRow5 = 0, numRow6 = 0;
            foreach (RezumeV rezume in currentRezume)
            {
                Kandidat currentKandidat = RezumeEntities3.GetContext().Kandidat.Where(x => x.Id_kandidat.Equals(rezume.Id_kandidat)).FirstOrDefault();
                Vakanciy currentVakanciy = RezumeEntities3.GetContext().Vakanciy.Where(x => x.Id_vakanciy.Equals(rezume.Id_vakanciy)).FirstOrDefault();

                string namePath = "temp/temp" + Convert.ToString(++countRez) + ".jpg";
                string savePath = System.IO.Path.Combine(projectDirectory, namePath);
                File.WriteAllBytes(savePath, currentKandidat.Photo);

                Tasks.Add(new KanbanModel()
                {
                    Title = Convert.ToString(rezume.Kandidat.Familiy + "\n" + rezume.Kandidat.Imya + "\n" + rezume.Kandidat.Otchestvj),
                    ID = rezume.Id_rezume.ToString(),
                    Description = Convert.ToString(rezume.Kandidat.Telefon + "\n" + rezume.Kandidat.Pochta + "\n Опыт работы по должности: " + rezume.Opyt_rabot),
                    Category = rezume.Id_status,
                    Tags = new string[] { currentVakanciy.Name_vakanciy },
                    ImageURL = new Uri(savePath, UriKind.RelativeOrAbsolute)
                });
                switch (rezume.Id_status)
                {
                    case 1:
                        { matCard[numRow1, 0] = Tasks[Indmass]; numRow1++; Indmass++; break; }
                    case 2:
                        { matCard[numRow2, 1] = Tasks[Indmass]; numRow2++; Indmass++; break; }
                    case 3:
                        { matCard[numRow3, 2] = Tasks[Indmass]; numRow3++; Indmass++; break; }
                    case 4:
                        { matCard[numRow4, 3] = Tasks[Indmass]; numRow4++; Indmass++; break; }
                    case 5:
                        { matCard[numRow5, 4] = Tasks[Indmass]; numRow5++; Indmass++; break; }
                    case 6:
                        { matCard[numRow6, 5] = Tasks[Indmass]; numRow6++; Indmass++; break; }
                }
            }

            KanbaRez.ItemsSource = Tasks;
        }

        private void KanbaRez_CardDragStart(object sender, KanbanDragStartEventArgs e)
        {
        }

        private void KanbaRez_CardTapped(object sender, KanbanTappedEventArgs e)
        {
            int idRowCard = e.SelectedCardIndex;
            int idColCard = e.SelectedColumnIndex;
            if (idColCard == 5)
            {
                KanbanModel oneCard = matCard[idRowCard, idColCard];
                int idRez = Convert.ToInt32(oneCard.ID);
                RezumeV currentRezume = RezumeEntities3.GetContext().RezumeV.Where(x => x.Id_rezume.Equals(idRez)).FirstOrDefault();
                WinOffer winOffer_ = new WinOffer(currentRezume);
                winOffer_.Show();

                DeleteMatCard();
                UpdateKanban();
            }


        }

        private void KanbaRez_CardDragEnd(object sender, KanbanDragEndEventArgs e)
        {

            int idTarCard = e.TargetColumnIndex;
            int idRowCard = e.SelectedCardIndex;
            int idColCard = e.SelectedColumnIndex;
            if (idTarCard - idColCard != 1)
            {
                e.IsCancel = true;
                DeleteMatCard();
                UpdateKanban();
                MessageBox.Show("Не возможно выполнить перемещение карты, т.к. пропущен один из этапов рассмотрения резюме!");
            }
            else
            {
                KanbanModel oneCard = matCard[idRowCard, idColCard];
                int idRez = Convert.ToInt32(oneCard.ID);
                List<Artefact_rezume> currentArtRezume = RezumeEntities3.GetContext().Artefact_rezume.Where(x => x.Id_rezume.Equals(idRez)).ToList();
                RezumeV currentRezume = RezumeEntities3.GetContext().RezumeV.Where(x => x.Id_rezume.Equals(idRez)).FirstOrDefault();
                if (currentRezume.Id_HR == null)
                {

                    MessageBoxResult result = MessageBox.Show("Для данного резюме ответственным HR назначены Вы.\n Будете", "Предупреждение", MessageBoxButton.OK);
                    switch (result)
                    {
                        case MessageBoxResult.OK:

                            currentRezume.Id_HR = idVhHR;
                            RezumeEntities3.GetContext().SaveChanges();
                            break;
                        
                    }
                }
                if (idTarCard == 2 && currentArtRezume.Count <= 0)
                {

                    MessageBoxResult result = MessageBox.Show("Не прикреплен файл с результатами скрининга.\nЖелаете продолжить без прикрепления файла?", "Вопрос",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:

                            currentRezume.Id_status = 3;
                            RezumeEntities3.GetContext().SaveChanges();
                            break;
                        case MessageBoxResult.No:
                            NavigationService.Navigate(new AddArt(currentRezume));
                            break;

                    }
                }
                else if (idTarCard == 3 && currentArtRezume.Count <= 0)
                {

                    MessageBoxResult result = MessageBox.Show("Не прикреплен файл с результатами собеседования.\nЖелаете продолжить без прикрепления файла?", "Вопрос",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:

                            currentRezume.Id_status = 4;
                            RezumeEntities3.GetContext().SaveChanges();
                            break;
                        case MessageBoxResult.No:
                            NavigationService.Navigate(new AddArt(currentRezume));
                            break;

                    }
                }
                else if (idTarCard == 5 && currentArtRezume.Count <= 0)
                {

                    MessageBoxResult result = MessageBox.Show("Не прикреплен файл оффера.\nЖелаете прикрепить файл?", "Вопрос",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            NavigationService.Navigate(new AddArt(currentRezume));
                            break;

                        case MessageBoxResult.No:
                            e.IsCancel = false;
                            DeleteMatCard();
                            UpdateKanban();
                            break;

                    }
                }
                else
                {
                    currentRezume.Id_status = idTarCard+1;
                    RezumeEntities3.GetContext().SaveChanges();
                }
            }
            DeleteMatCard();
            UpdateKanban();
        }
        public void DeleteMatCard()
        {
            for (int i = 0; i < RezumeEntities3.GetContext().RezumeV.Count(); i++)
                for (int j = 0; j < RezumeEntities3.GetContext().RezumeV.Count(); j++)
                {
                    matCard[i, j] = null;
                }
        }

        private void KanbaRez_CardDoubleTapped(object sender, KanbanDoubleTappedEventArgs e)
        {
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddRezume());
        }

        private void AddVak_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddVak());
        }

        private void AddKan_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddKan());
        }
    }
}
