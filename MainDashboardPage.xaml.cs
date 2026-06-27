using ProjectAlprog3.Models;
using ProjectAlprog3.Services;

namespace ProjectAlprog3
{
    public partial class MainDashboardPage : ContentPage
    {
        public MainDashboardPage()
        {
            InitializeComponent();
            RefreshDashboard();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshDashboard();
        }

        private void RefreshDashboard()
        {
            string user = Preferences.Get("session_username", "User");
            string role = Preferences.Get("session_role", "Pasien");
            LabelUser.Text = $"Login sebagai: {user} | Role: {role}";
            LabelTotal.Text = AIEngine.History.Count.ToString();

            if (AIEngine.History.Count > 0)
            {
                var last = AIEngine.History.Last();
                LabelGlucose.Text = $"{last.Result.EstimatedGlucose:F0} mg/dL";
                LabelRisk.Text = last.Result.FinalRisk;
            }
        }

        private void OnInputClicked(object sender, EventArgs e) => Application.Current!.MainPage = new InputDataPage();
        private void OnAIResultClicked(object sender, EventArgs e) => Application.Current!.MainPage = new AIResultPage();
        private void OnHistoryClicked(object sender, EventArgs e) => Application.Current!.MainPage = new HistoryPage();
        private void OnAiAssistantClicked(object sender, EventArgs e) => Application.Current!.MainPage = new AiAssistantPage();

        private async void OnExportClicked(object sender, EventArgs e)
        {
            string path = LocalDatabaseService.ExportCsv(AIEngine.History);
            await DisplayAlert("Export Berhasil", $"Data uji tersimpan di:\n{path}", "OK");
        }

        private void OnLogoutClicked(object sender, EventArgs e)
        {
            Preferences.Remove("session_username");
            Preferences.Remove("session_role");
            Application.Current!.MainPage = new MainPage();
        }
    }
}

