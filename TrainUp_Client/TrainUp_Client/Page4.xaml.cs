using System;
using System.Collections.Generic;
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
using WpfApp1;

namespace TrainUp_Client
{
    /// <summary>
    /// Logica di interazione per Page4.xaml
    /// </summary>
    public partial class Page4 : Page
    {
        private readonly HttpClientService _httpClientService;
        public Page4(HttpClientService httpClientService)
        {
            InitializeComponent();
            Combobox.Items.Add("age");
            Combobox.Items.Add("weight");
            _httpClientService = httpClientService;
        }

        //private readonly HttpRequestService httpRequestService = new HttpRequestService();

        private void HomeButton_Click(object sender, RoutedEventArgs e)
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

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            string data = TestBox.Text;
            string data1 = Combobox.SelectedItem.ToString();

            using (HttpClient client = _httpClientService.Client)
            {
                string url = "http://localhost:5000/update_data";
                var data2 = new { data, data1 };

                // Converti i dati in formato JSON
                string jsonData = JsonSerializer.Serialize(data2);

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
                    outputLabe.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
