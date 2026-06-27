using Microsoft.Maui.Media;
using Microsoft.Maui.Storage;

namespace ProjectAlprog3
{
    public partial class RegisterPage : ContentPage
    {
        private byte[]? _faceBytes;

        public RegisterPage()
        {
            InitializeComponent();
            PickerRole.ItemsSource = new List<string> { "Pasien", "Admin", "Dokter" };
            PickerRole.SelectedIndex = 0;
        }

        private async void OnScanFaceClicked(object sender, EventArgs e)
        {
            try
            {
                var status = await Permissions.RequestAsync<Permissions.Camera>();
                if (status != PermissionStatus.Granted)
                {
                    await DisplayAlert("Izin Kamera", "Izin kamera belum diberikan.", "OK");
                    return;
                }

                var photo = await MediaPicker.Default.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = "Scan wajah pendaftaran"
                });

                if (photo == null) return;

                await using var stream = await photo.OpenReadAsync();
                using var ms = new MemoryStream();
                await stream.CopyToAsync(ms);
                _faceBytes = ms.ToArray();

                FacePreview.Source = ImageSource.FromStream(() => new MemoryStream(_faceBytes));
                PlaceholderLabel.IsVisible = false;
            }
            catch (FeatureNotSupportedException)
            {
                await DisplayAlert("Kamera Tidak Didukung", "Perangkat ini tidak mendukung kamera.", "OK");
            }
            catch (PermissionException)
            {
                await DisplayAlert("Izin Kamera", "Aplikasi belum mendapat izin kamera.", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            string user = EntryUsername.Text?.Trim() ?? string.Empty;
            string pass = EntryPassword.Text ?? string.Empty;
            string role = PickerRole.SelectedItem?.ToString() ?? "Pasien";

            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
            {
                await DisplayAlert("Data Belum Lengkap", "Username dan password wajib diisi.", "OK");
                return;
            }

            if (_faceBytes == null || _faceBytes.Length == 0)
            {
                await DisplayAlert("Wajah Belum Discan", "Silakan scan wajah terlebih dahulu.", "OK");
                return;
            }

            Preferences.Set($"user_pass_{user}", pass);
            Preferences.Set($"user_role_{user}", role);

            string facePath = Path.Combine(FileSystem.AppDataDirectory, $"{user}_face.png");
            await File.WriteAllBytesAsync(facePath, _faceBytes);

            await DisplayAlert("Berhasil", "Akun berhasil didaftarkan. Silakan login.", "OK");
            Application.Current!.MainPage = new MainPage();
        }

        private void OnBackClicked(object sender, EventArgs e)
        {
            Application.Current!.MainPage = new MainPage();
        }
    }
}
