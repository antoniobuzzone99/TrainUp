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
    /// Logica di interazione per stampaUserCard.xaml
    /// </summary>
    public partial class stampaUserCard : Page
    {
        private readonly string token;
        public stampaUserCard(string token, UserCard trainingCard)
        {
            this.token = token;
            InitializeComponent();

            Label CardId = new Label();
            CardId.Content = $"Card Id: {trainingCard.CardId}";
            CardId.FontSize = 22;
            CardId.Foreground = Brushes.DarkOliveGreen;
            CardId.FontWeight = FontWeights.Bold;
            CardId.FontFamily = new FontFamily("Georgia");
            cardPanel.Children.Add(CardId);

            // Raggruppa gli esercizi per giorno
            var groupedExercises = trainingCard.Exercises.GroupBy(e => e.Day);

            // Itera attraverso i gruppi e stampa gli esercizi
            foreach (var group in groupedExercises)
            {
                Label labelday = new Label();
                labelday.Content = $"Day: {group.Key}";
                labelday.FontSize = 14;
                labelday.FontWeight = FontWeights.Bold;
                labelday.FontFamily = new FontFamily("Georgia");
                cardPanel.Children.Add(labelday);
                foreach (var exercise in group)
                {
                    Label labelExercise = new Label();
                    labelExercise.Content = $"Name: {exercise.Name} - Set: {exercise.Sets} Rep: {exercise.Reps}";
                    cardPanel.Children.Add(labelExercise);
                }
            }
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow && mainWindow.MainFrame != null)
            {
                // Accedi al NavigationService del Frame dalla finestra principale
                if (Application.Current.MainWindow is MainWindow && mainWindow.MainFrame != null)
                {
                    // Naviga verso una nuova pagina
                    mainWindow.MainFrame.NavigationService.Navigate(new Page1(token));
                }
            }
        }
    }
}
