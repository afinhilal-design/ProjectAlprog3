using ProjectAlprog3.Models;

namespace ProjectAlprog3
{
    public partial class AIResultPage : ContentPage
    {
        public AIResultPage()
        {
            InitializeComponent();
            ShowResult();
        }

        private void ShowResult()
        {
            if (AIEngine.History.Count == 0)
            {
                LabelGlucose.Text = "Belum ada data";
                LabelFinal.Text = "Silakan input data pasien terlebih dahulu.";
                return;
            }

            var r = AIEngine.History.Last().Result;
            LabelGlucose.Text = $"{r.EstimatedGlucose:F0} mg/dL";
            LabelFinal.Text = $"Risiko Akhir: {r.FinalRisk}";
            LabelDT.Text = $"Hasil: {r.DecisionTreeRisk} | Confidence: {r.DtConfidence:P0}";
            LabelNN.Text = $"Hasil: {r.NeuralNetworkRisk} | Rendah {r.NnLow:P0}, Sedang {r.NnMedium:P0}, Tinggi {r.NnHigh:P0}";
            LabelFuzzy.Text = $"Hasil: {r.FuzzyRisk} | Skor defuzzifikasi: {r.FuzzyScore:F1}";
            LabelRecommendation.Text = r.Recommendation;
        }

        private void OnInputClicked(object sender, EventArgs e) => Application.Current!.MainPage = new InputDataPage();
        private void OnBackClicked(object sender, EventArgs e) => Application.Current!.MainPage = new MainDashboardPage();
    }
}

