PANDUAN CHATBOT GEMINI / NLP ASSISTANT

File yang dipakai:
1. Services/NlpAssistantApi.cs
   - Menghubungkan aplikasi .NET MAUI ke Gemini API.
   - Jika API key belum diisi atau internet gagal, aplikasi otomatis memakai jawaban offline.

2. Services/NlpAssistantService.cs
   - Chatbot offline/rule-based sebagai cadangan agar aplikasi tetap bisa jalan saat demo.

3. AiAssistantPage.xaml.cs
   - Halaman chatbot sekarang memanggil NlpAssistantApi.

Cara mengaktifkan Gemini API:
1. Buka Google AI Studio.
2. Buat API Key Gemini.
3. Buka file Services/NlpAssistantApi.cs.
4. Ganti:
   private const string ApiKey = "MASUKKAN_API_KEY_GEMINI_DI_SINI";
   menjadi API key milikmu.
5. Build ulang project.

Catatan penting:
- AIResultPage.xaml.cs JANGAN diganti, karena itu halaman hasil prediksi Decision Tree, Neural Network, dan Fuzzy.
- WebsiteBackend sudah dikeluarkan dari kompilasi MAUI melalui ProjectAlprog3.csproj supaya tidak memunculkan error DbContext/EntityFrameworkCore.
