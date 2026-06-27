using Microsoft.Maui.Storage;

namespace ProjectAlprog3
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string user = EntryUsername.Text?.Trim() ?? string.Empty;
            string pass = EntryPassword.Text ?? string.Empty;

            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
            {
                await DisplayAlert("Gagal", "Username dan password wajib diisi.", "OK");
                return;
            }

            if (!Preferences.ContainsKey($"user_pass_{user}"))
            {
                await DisplayAlert("Ditolak", "Username tidak ditemukan. Silakan daftar terlebih dahulu.", "OK");
                return;
            }

            if (pass != Preferences.Get($"user_pass_{user}", string.Empty))
            {
                await DisplayAlert("Ditolak", "Password salah.", "OK");
                return;
            }

            string role = Preferences.Get($"user_role_{user}", "Pasien");
            Preferences.Set("session_username", user);
            Preferences.Set("session_role", role);

            Application.Current!.MainPage = new FaceVerificationPage(user, role);
        }

        private void OnGoToRegisterClicked(object sender, EventArgs e)
        {
            Application.Current!.MainPage = new RegisterPage();
        }
    }
}
