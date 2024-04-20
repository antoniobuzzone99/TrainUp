using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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
using WpfApp1;


namespace TrainUp_Client
{
    /// <summary>
    /// Logica di interazione per statistiche.xaml
    /// </summary>
    public partial class statistiche : Page
    {
        private readonly string token;
        public statistiche(string token)
        {
            InitializeComponent();
            this.token = token;
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

        private async void OpenImageViewerWindowButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Fai la richiesta al server per ottenere l'immagine
                string imageUrl = "http://localhost:5000/cards_most_used"; 

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(imageUrl);

                    // Controlla se la richiesta ha avuto successo
                    if (response.IsSuccessStatusCode)
                    {
                        // Ottieni l'immagine come array di byte
                        byte[] imageData = await response.Content.ReadAsByteArrayAsync();

                        // Visualizza l'immagine nella finestra ImageViewerWindow
                        CardMostUsedViewerWindows imageViewerWindow = new CardMostUsedViewerWindows(imageData);
                        imageViewerWindow.Show();
                    }
                    else
                    {
                        MessageBox.Show("Errore nel caricamento dell'immagine.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Si è verificato un errore: " + ex.Message);
            }
        }
    }
}
