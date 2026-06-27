using ProjectAlprog3.Services;

namespace ProjectAlprog3
{
    public partial class AiAssistantPage : ContentPage
    {
        private readonly NlpAssistantApi _assistant = new();

        public AiAssistantPage()
        {
            InitializeComponent();
        }

        private async void OnAskClicked(object sender, EventArgs e)
        {
            string question = QuestionEditor.Text ?? string.Empty;
            AnswerLabel.Text = await _assistant.AskAsync(question);
        }

        private void OnBackClicked(object sender, EventArgs e)
        {
            Application.Current!.MainPage = new MainDashboardPage();
        }
    }
}
