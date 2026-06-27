using ProjectAlprog3.Models;

namespace ProjectAlprog3
{
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
            HistoryCollection.ItemsSource = AIEngine.History.OrderByDescending(x => x.Time).ToList();
        }

        private void OnBackClicked(object sender, EventArgs e)
        {
            Application.Current!.MainPage = new MainDashboardPage();
        }
    }
}

