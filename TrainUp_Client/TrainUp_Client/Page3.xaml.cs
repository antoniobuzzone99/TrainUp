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

namespace WpfApp1
{
    /// <summary>
    /// Logica di interazione per Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();
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
                    mainWindow.MainFrame.NavigationService.Navigate(new Page0());
                }
            }
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;
            string password = PasswordBox.Text;
            string confirmPassword = ConfirmPasswordBox.Text;
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

                // Verifica se la risposta ha avuto successo
                //response.EnsureSuccessStatusCode();

                // Leggi la risposta come stringa
                string responseString = await response.Content.ReadAsStringAsync();

                // Controlla la risposta JSON per il successo
                var responseObject = JsonSerializer.Deserialize<Dictionary<string, string>>(responseString);
                string state = (string)responseObject["state"];


                if (state == "1")
                {
                    // Accedi al NavigationService del Frame dalla finestra principale
                    if (Application.Current.MainWindow is MainWindow mainWindow && mainWindow.MainFrame != null)
                    {
                        // Accedi al NavigationService del Frame dalla finestra principale
                        if (Application.Current.MainWindow is MainWindow && mainWindow.MainFrame != null)
                        {
                            // Naviga verso una nuova pagina
                            mainWindow.MainFrame.NavigationService.Navigate(new Page1());
                        }
                    }


                }
            }

        }
    }
}
