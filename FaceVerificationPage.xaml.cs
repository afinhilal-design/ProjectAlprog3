using Microsoft.Maui.Media;
using SkiaSharp;

namespace ProjectAlprog3
{
    public partial class FaceVerificationPage : ContentPage
    {
        private readonly string _username;
        private readonly string _role;
        private readonly string _dbPath;

        public FaceVerificationPage(string username, string role)
        {
            InitializeComponent();
            _username = username;
            _role = role;
            _dbPath = Path.Combine(FileSystem.AppDataDirectory, $"{username}_face.png");
            LabelTitle.Text = $"🔍 Verifikasi: {username} | {_role}";
        }

        private async void OnVerifyClicked(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(_dbPath))
                {
                    await DisplayAlert("Database Wajah Kosong", "Data wajah user ini tidak ditemukan.", "OK");
                    return;
                }

                var status = await Permissions.RequestAsync<Permissions.Camera>();
                if (status != PermissionStatus.Granted)
                {
                    await DisplayAlert("Izin Kamera", "Izin kamera belum diberikan.", "OK");
                    return;
                }

                var photo = await MediaPicker.Default.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = "Scan wajah verifikasi"
                });

                if (photo == null) return;

                using var newStreamForPreview = await photo.OpenReadAsync();
                using var previewMs = new MemoryStream();
                await newStreamForPreview.CopyToAsync(previewMs);
                byte[] newBytes = previewMs.ToArray();
                ScanPreview.Source = ImageSource.FromStream(() => new MemoryStream(newBytes));
                ScanText.IsVisible = false;

                double similarity = CompareFaces(newBytes, File.ReadAllBytes(_dbPath));
                SimilarityBar.Progress = similarity / 100.0;
                LabelSimilarity.Text = $"Kemiripan: {similarity:F1}%";

                if (similarity >= 85)
                {
                    BtnDashboard.IsVisible = true;
                    await DisplayAlert("AKSES DITERIMA", $"Wajah cocok. Kemiripan {similarity:F1}%", "OK");
                }
                else
                {
                    BtnDashboard.IsVisible = false;
                    await DisplayAlert("AKSES DITOLAK", $"Wajah belum cocok. Kemiripan {similarity:F1}%", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private double CompareFaces(byte[] newFace, byte[] dbFace)
        {
            using var newBitmap = SKBitmap.Decode(newFace);
            using var dbBitmap = SKBitmap.Decode(dbFace);
            if (newBitmap == null || dbBitmap == null) return 0;

            var info = new SKImageInfo(32, 32);
            using var resizedNew = newBitmap.Resize(info, SKFilterQuality.Medium);
            using var resizedDb = dbBitmap.Resize(info, SKFilterQuality.Medium);
            if (resizedNew == null || resizedDb == null) return 0;

            int matched = 0;
            int total = 32 * 32;

            for (int y = 0; y < 32; y++)
            {
                for (int x = 0; x < 32; x++)
                {
                    var a = resizedNew.GetPixel(x, y);
                    var b = resizedDb.GetPixel(x, y);
                    int ga = (int)(0.30 * a.Red + 0.59 * a.Green + 0.11 * a.Blue);
                    int gb = (int)(0.30 * b.Red + 0.59 * b.Green + 0.11 * b.Blue);
                    if (Math.Abs(ga - gb) < 55) matched++;
                }
            }

            return matched * 100.0 / total;
        }

        private void OnDashboardClicked(object sender, EventArgs e)
        {
            Application.Current!.MainPage = new MainDashboardPage();
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            Application.Current!.MainPage = new MainPage();
        }
    }
}

