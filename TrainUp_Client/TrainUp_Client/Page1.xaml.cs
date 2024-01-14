using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
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
    /// Logica di interazione per Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
            LoadDataFromServer();

        }
        private async void LoadDataFromServer()
        {
            using (HttpClient client = new HttpClient())
            {

                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string url = "http://localhost:5000/home"; // Sostituisci con l'URL del tuo server Flask

                //HttpResponseMessage response = await client.GetAsync(url);

                var content = new StringContent(JsonSerializer.Serialize("date"), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);

                string jsonString = await response.Content.ReadAsStringAsync();

                var trainingCardsDict = JsonSerializer.Deserialize <Dictionary<string, List<TrainingCard>>>(jsonString);

                var trainingCards = trainingCardsDict["user_cards"];

                foreach (TrainingCard trainingCard in trainingCards){
                    Button button = new Button();
                    button.Content = trainingCard.CardId;
                    cardPanel.Children.Add(button);
                }

            }
        }
    }

    public class Exercise
    {
        public string Name { get; private set; }
        public int Sets { get; private set; }
        public int Reps { get; private set; }
        public string Day { get; private set; }

        public Exercise(string name, int sets, int reps, string day)
        {
            Name = name;
            Sets = sets;
            Reps = reps;
            Day = day;
        }
    }
    public class TrainingCard
    {
        public int CardId { get; private set; }
        public int UserId { get; private set; }
        public List<Exercise> Exercises { get; private set; }
    }

}