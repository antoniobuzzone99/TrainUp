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
using WpfApp1;

namespace TrainUp_Client
{
    /// <summary>
    /// Logica di interazione per Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        private readonly HttpClientService _httpClientService;
        public Page2(HttpClientService httpClientService, UserCard trainingCard)
        {
            
            InitializeComponent();
            _httpClientService = httpClientService;
            string currentDay = null;
            foreach (var exercise in trainingCard.Exercises)
            {
                if (exercise.Day != currentDay)
                {
                    // Se il giorno è diverso da quello attuale, crea un nuovo label per il giorno
                    currentDay = exercise.Day;

                    Label labelDay = new Label();
                    labelDay.Content = currentDay;
                    labelDay.FontSize = 14;
                    labelDay.FontWeight = FontWeights.Bold;
                    labelDay.FontFamily = new FontFamily("Georgia");
                    cardPanel.Children.Add(labelDay);

                }

                // Crea un label per l'esercizio
                Label labelExercise = new Label();
                labelExercise.Content = labelExercise.Content = $"{exercise.Name} - Set: {exercise.Sets} Rep: {exercise.Reps}";
                cardPanel.Children.Add(labelExercise);
                labelExercise.Margin = new Thickness(0, 0, 0, -5);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow && mainWindow.MainFrame != null)
            {
                // Accedi al NavigationService del Frame dalla finestra principale
                if (Application.Current.MainWindow is MainWindow && mainWindow.MainFrame != null)
                {
                    // Naviga verso una nuova pagina
                    mainWindow.MainFrame.NavigationService.Navigate(new Page1(_httpClientService));
                }
            }
        }
    }
}
