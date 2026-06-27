using ProjectAlprog3.Models;

namespace ProjectAlprog3
{
    public partial class InputDataPage : ContentPage
    {
        public InputDataPage()
        {
            InitializeComponent();
            FillDefaultData();
        }

        private void FillDefaultData()
        {
            HeartRateEntry.Text = "75";
            Spo2Entry.Text = "98";
            TempEntry.Text = "36.5";
            SystolicEntry.Text = "120";
            DiastolicEntry.Text = "80";
            AgeEntry.Text = "35";
            BmiEntry.Text = "24.5";
            NirEntry.Text = "0.65";
            PpgEntry.Text = "0.72";
            FamilyPicker.SelectedIndex = 0;
        }

        private async void OnAnalyzeClicked(object sender, EventArgs e)
        {
            try
            {
                var data = new PatientData
                {
                    HeartRate = ReadNumber(HeartRateEntry.Text),
                    Spo2 = ReadNumber(Spo2Entry.Text),
                    Temperature = ReadNumber(TempEntry.Text),
                    Systolic = ReadNumber(SystolicEntry.Text),
                    Diastolic = ReadNumber(DiastolicEntry.Text),
                    Age = ReadNumber(AgeEntry.Text),
                    Bmi = ReadNumber(BmiEntry.Text),
                    Nir = ReadNumber(NirEntry.Text),
                    Ppg = ReadNumber(PpgEntry.Text),
                    FamilyHistory = FamilyPicker.SelectedIndex == 1 ? 1 : 0
                };

                AIEngine.Analyze(data);
                await DisplayAlert("Sukses", "Analisis AI berhasil dijalankan.", "OK");
                Application.Current!.MainPage = new AIResultPage();
            }
            catch
            {
                await DisplayAlert("Input Salah", "Pastikan semua angka diisi dengan benar. Gunakan titik untuk desimal, contoh 36.5", "OK");
            }
        }

        private double ReadNumber(string? value)
        {
            value = (value ?? "0").Replace(',', '.');
            return double.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
        }

        private void OnBackClicked(object sender, EventArgs e)
        {
            Application.Current!.MainPage = new MainDashboardPage();
        }
    }
}

