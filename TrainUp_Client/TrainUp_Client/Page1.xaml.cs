using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;
//using System.Text.Json;
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
using TrainUp_Client;

using Newtonsoft.Json;
using System.Diagnostics;

namespace WpfApp1
{
    /// <summary>
    /// Logica di interazione per Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        private readonly HttpClientService _httpClientService;
        public Page1(HttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            Debug.WriteLine($"page 1:HttpClient instance created: {_httpClientService.Client != null}");
            InitializeComponent();
            LoadDataFromServer();
            
        }
        private async void LoadDataFromServer()
        {
            Debug.WriteLine($"page 1bis:HttpClient instance created: {_httpClientService.Client != null}");
            using (HttpClient client = _httpClientService.Client)
            {
                Debug.WriteLine($"page 2bis:HttpClient instance created: {_httpClientService.Client != null}");

                string url = "http://localhost:5000/home"; 

      
                var content = new StringContent(System.Text.Json.JsonSerializer.Serialize("date"), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                string jsonString = await response.Content.ReadAsStringAsync();

                JsonResponse jsonResponse = JsonConvert.DeserializeObject<JsonResponse>(jsonString);
                var trainingCards = jsonResponse.UserCards;


                foreach (UserCard trainingCard in trainingCards){
                    // Verifica se un bottone con lo stesso CardId esiste già nel cardPanel
                    bool buttonExists = cardPanel.Children.OfType<Button>().Any(b => b.Name.Equals("bottone_" + trainingCard.CardId.ToString()));
                    if (!buttonExists) {
                        Button button = new Button();
                        button.Name = "bottone_"+trainingCard.CardId.ToString();
                        button.Content = "Scheda "+trainingCard.CardId;
                        button.BorderBrush = new SolidColorBrush(Colors.DarkBlue);
                        button.FontSize = 15;
                        button.Width = 200;
                        button.Height = 40;
                        button.FontFamily = new FontFamily("Georgia");
                        button.Margin = new Thickness(0,0,0,10);
                        cardPanel.Children.Add(button);
             
                        // Aggiunta del gestore dell'evento senza chiamarlo direttamente
                        button.Click += (sender, e) => CardButtonClick(sender, e, trainingCard);

                    }
                   
                }

            }
        }

        private async void CardButtonClick(object sender, RoutedEventArgs e, UserCard trainingCard)
        {

            if (Application.Current.MainWindow is MainWindow mainWindow && mainWindow.MainFrame != null)
            {
                // Accedi al NavigationService del Frame dalla finestra principale
                if (Application.Current.MainWindow is MainWindow && mainWindow.MainFrame != null)
                {
                    // Naviga verso una nuova pagina
                    mainWindow.MainFrame.NavigationService.Navigate(new Page2(_httpClientService, trainingCard));
                }
            }
        }

        private async void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = _httpClientService.Client)
            {
                string url = $"http://localhost:5000/logout";
                // Esegui la richiesta HTTP POST
                HttpResponseMessage response = await client.PostAsync(url, null);

                // Leggi la risposta come stringa
                string responseString = await response.Content.ReadAsStringAsync();


                // Controlla la risposta JSON per il successo
                //var responseObject = JsonSerializer.Deserialize<Dictionary<string, int>>(responseString);
                var responseObject = JsonConvert.DeserializeObject<Dictionary<string, int>>(responseString);
                int state = (int)responseObject["state"];
                

                if (state == 0)
                {
                    // Accedi al NavigationService del Frame dalla finestra principale
                    if (Application.Current.MainWindow is MainWindow mainWindow && mainWindow.MainFrame != null)
                    {
                        // Accedi al NavigationService del Frame dalla finestra principale
                        if (Application.Current.MainWindow is MainWindow && mainWindow.MainFrame != null)
                        {
                            // Naviga verso una nuova pagina
                            mainWindow.MainFrame.NavigationService.Navigate(new Page0(_httpClientService));
                        }
                    }
                }
            }

        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            //funzione aggirona dati

            if (Application.Current.MainWindow is MainWindow mainWindow && mainWindow.MainFrame != null)
            {
                // Accedi al NavigationService del Frame dalla finestra principale
                if (Application.Current.MainWindow is MainWindow && mainWindow.MainFrame != null)
                {
                    // Naviga verso una nuova pagina
                    mainWindow.MainFrame.NavigationService.Navigate(new Page4(_httpClientService));
                }
            }
        }

    }

   
    public class JsonResponse
    {
        [JsonProperty("user_cards")]
        public List<UserCard> UserCards { get; set; }
    }

    public class UserCard
    {
        [JsonProperty("card_id")]
        public int CardId { get; set; }

        [JsonProperty("exercises")]
        public List<Exercise> Exercises { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }
    }

    public class Exercise
    {
        [JsonProperty("day")]
        public string Day { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("reps")]
        public int Reps { get; set; }

        [JsonProperty("sets")]
        public int Sets { get; set; }
    }

}