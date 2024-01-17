using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Logica di interazione per Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        private readonly HttpClientService _httpClientService;
        public Page3(HttpClientService httpClientService)
        {
            InitializeComponent();
            _httpClientService = httpClientService;
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
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

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;
            string password = PasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;
            string age = AgeBox.Text;
            string weight = WeightBox.Text;
            

            using (HttpClient client = new HttpClient())
            {
                string url = $"http://localhost:5000/register";
                var data = new { username, password, confirmPassword, age, weight};

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


                if (state == 0)
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
                else if (state == 2)
                {
                    outputLabel.Visibility = Visibility.Visible;
                }
                else {
                    outputLabel2.Visibility = Visibility.Visible;
                }
            }

        }
    }
}
