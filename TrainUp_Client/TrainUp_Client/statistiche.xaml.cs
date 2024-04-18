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
            loadCardFavorites();
        }

        

        private async void loadCardFavorites() {

            string imagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "chart.png");

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://localhost:5000/cards_most_used");

                if (response.IsSuccessStatusCode)
                {
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        // Utilizza un blocco using per il flusso del file
                        using (var fileStream = File.Create(imagePath))
                        {
                            await stream.CopyToAsync(fileStream);

                        }

                        // Ora il file è stato chiuso, quindi puoi creare il bitmap
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                        bitmap.EndInit();


                        // Creare un oggetto Viewbox con dimensioni desiderate e allinearla a sinistra
                        Viewbox viewbox = new Viewbox();
                        viewbox.Stretch = Stretch.Uniform; // Per mantenere l'aspect ratio dell'immagine
                        viewbox.Width = 320; // Imposta la larghezza desiderata
                        viewbox.HorizontalAlignment = HorizontalAlignment.Left; // Allinea a sinistra

                        // Creare un oggetto Image e impostare la proprietà Source con l'immagine caricata
                        Image image = new Image();
                        image.Source = bitmap;

                        // Aggiungi l'immagine al Viewbox
                        viewbox.Child = image;

                        // Aggiungi il Viewbox allo StackPanel
                        cardPanel.Children.Add(viewbox);
                        
                    }

                }
                else
                {
                    Console.WriteLine("Failed to get the chart from the server.");
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
