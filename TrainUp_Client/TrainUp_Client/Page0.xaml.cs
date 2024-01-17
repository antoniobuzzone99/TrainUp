using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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

namespace WpfApp1
{
    /// <summary>
    /// Logica di interazione per Page0.xaml
    /// </summary>
    public partial class Page0 : Page
    {

        private readonly HttpClientService _httpClientService;
        public Page0(HttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            InitializeComponent();
            Debug.WriteLine($"page 0:HttpClient instance created: {_httpClientService.Client != null}");
        }


        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;
            string password = PasswordBox.Password;

            using (HttpClient client = _httpClientService.Client)
            {
                string url = $"http://localhost:5000/login";
                var data = new { username, password };

                // Converti i dati in formato JSON
                string jsonData = JsonSerializer.Serialize(data);

                // Crea un oggetto StringContent con il JSON
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // Esegui la richiesta HTTP POST
                HttpResponseMessage response = await client.PostAsync(url, content);

                // Leggi la risposta come stringa
                string responseString = await response.Content.ReadAsStringAsync();


                // Controlla la risposta JSON per il successo
                var responseObject = JsonSerializer.Deserialize<Dictionary<string, int>>(responseString);
                int state = (int)responseObject["state"];
    

                if (state == 1)
                {
                    // Accedi al NavigationService del Frame dalla finestra principale
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
                else {
                    outputLabel.Visibility = Visibility.Visible;
                }
            }

        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e) {
            // Accedi al NavigationService del Frame dalla finestra principale
            if (Application.Current.MainWindow is MainWindow mainWindow && mainWindow.MainFrame != null)
            {
                // Accedi al NavigationService del Frame dalla finestra principale
                if (Application.Current.MainWindow is MainWindow && mainWindow.MainFrame != null)
                {
                    // Naviga verso una nuova pagina
                    mainWindow.MainFrame.NavigationService.Navigate(new Page3(_httpClientService));
                }
            }
        }


    }
}
