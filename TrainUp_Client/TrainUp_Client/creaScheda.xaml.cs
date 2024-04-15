using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
    /// Logica di interazione per creaScheda.xaml
    /// </summary>
    public partial class creaScheda : Page
    {
        private readonly string token;
        private readonly UserCard train_card;
        public creaScheda(string token, UserCard trainingCard)
        {

            this.train_card = trainingCard;
            this.token = token;
            InitializeComponent();
            loadExerciseFromDb();
        }

        private async void loadExerciseFromDb() {
            //carico esercizi nel listBox
            using (HttpClient client = new HttpClient())
            {
                string url = $"http://localhost:5000/loadExer";
                var content = new StringContent(JsonSerializer.Serialize("date"), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);

                string jsonResponse = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                ExerciseResponse data = JsonSerializer.Deserialize<ExerciseResponse>(jsonResponse, options);


                if (data.exercise_list != null)
                {
                    foreach (var exercise in data.exercise_list)
                    {
                        string name = exercise.name;
                        list_exercise.Items.Add(exercise.name);
                    }
                }

            }
        }

        private async void BackButton_Click(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = "http://localhost:5000/remove_from_list";
                var data = new { token };
                var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseObject = JsonSerializer.Deserialize<Dictionary<string, int>>(jsonString);
                //int state = (int)responseObject["state"];

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

        
        private async void AddExerciseButtonClick(object sender, RoutedEventArgs e) {
            string selectedText_day = "";
            if (list_day.SelectedItem != null)
            {
                ListBoxItem selectedItem_day = (ListBoxItem)list_day.SelectedItem;
                selectedText_day = selectedItem_day.Content.ToString();
            }


            string selectedItem_exe = "";
            if (list_exercise.SelectedItem != null)
            {
                selectedItem_exe = list_exercise.SelectedItem.ToString();
            }

            string set_ = set_testbox.Text;
            string rep_ = rep_testbox.Text;

            int set;
            int rep;
            // Verifica se i campi set e rep sono vuoti o contengono solo spazi bianchi
            if (string.IsNullOrWhiteSpace(set_) || string.IsNullOrWhiteSpace(rep_))
            {
                // Se i campi sono vuoti o contengono solo spazi bianchi, impostali a 0
                set = 0;
                rep = 0;
            }
            else
            {
                // Altrimenti, converte il testo in interi
                set = int.Parse(set_);
                rep = int.Parse(rep_);
            }

            //aggiungo gli esercizi alla card che ho ricevuto come parametro alla page
            Exercise exe = new Exercise();
            exe.Name = selectedItem_exe;
            exe.Sets = set;
            exe.Day = selectedText_day;
            exe.Reps = rep;
            // Aggiungi l'esercizio alla lista di esercizi nella scheda utente
            train_card.Exercises.Add(exe);


            using (HttpClient client = new HttpClient())
            {
                string url = $"http://localhost:5000/add_exe_card";
                var data = new { token, train_card };

               // Converti i dati in formato JSON
               string jsonData = JsonSerializer.Serialize(data);

                // Crea un oggetto StringContent con il JSON
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // Esegui la richiesta HTTP POST
                HttpResponseMessage response = await client.PostAsync(url, content);

                // Leggi la risposta come stringa
                string responseString = await response.Content.ReadAsStringAsync();
                var responseObject = JsonSerializer.Deserialize<Dictionary<string, int>>(responseString);
                int state = (int)responseObject["state"];

                if (state == 0) {
                    outLabelfault.Visibility = Visibility.Visible;
                }
                
            }


        }

        private async void ConfirmCreationCardButtonClick(object sender, RoutedEventArgs e)
        {
            string name = "";
            if (list_exercise.SelectedItem != null)
            {
                name = name_testbox.Text;
            }

            using (HttpClient client = new HttpClient())
            {

                string url = $"http://localhost:5000/confirm_creation_card";
                var data = new { token, name};

                // Converti i dati in formato JSON
                string jsonData = JsonSerializer.Serialize(data);

                // Crea un oggetto StringContent con il JSON
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // Esegui la richiesta HTTP POST
                HttpResponseMessage response = await client.PostAsync(url, content);

                // Leggi la risposta come stringa
                string responseString = await response.Content.ReadAsStringAsync();
                var responseObject = JsonSerializer.Deserialize<Dictionary<string, int>>(responseString);
                int state = (int)responseObject["state"];

                if (state == 0) {
                    outLabelfault.Visibility = Visibility.Visible;
                    outLabelsuccess.Visibility = Visibility.Hidden;
                }
                else if (state == 1)
                {
                    outLabelsuccess.Visibility = Visibility.Visible;
                    outLabelfault.Visibility = Visibility.Hidden;
                }


            }
        }
    }


}
