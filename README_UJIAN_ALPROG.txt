PROJECTALPROG3 - VERSI UJIAN ALPROG

Yang sudah ditambahkan:
1. Input data pasien dengan keterangan lengkap:
   - Suhu tubuh
   - Denyut nadi / heart rate
   - Tekanan darah sistolik
   - Tekanan darah diastolik
   - SpO2
   - BMI
   - NIR
   - PPG
   - Usia
   - Riwayat keluarga diabetes

2. AI/ML dalam aplikasi MAUI:
   - Decision Tree
   - Neural Network simulasi MLP 9-12-6-3
   - Fuzzy Logic Mamdani
   - Ensemble voting untuk hasil akhir

3. NLP Assistant:
   - File AiAssistantPage.xaml dan AiAssistantPage.xaml.cs
   - Service NlpAssistantService.cs
   - Mode offline/rule-based agar aman untuk demo tanpa API key.

4. Database lokal:
   - Service LocalDatabaseService.cs
   - Menyimpan history ke prediction_history.json
   - Export data uji ke CSV.

5. Dokumentasi ujian:
   - Documentation/UML_FLOWCHART.md
   - Documentation/JURNAL_HKI_TARGET.md
   - Documentation/LITERATURE_REVIEW_TEMPLATE.csv
   - Simulasi/hasil_uji_contoh.csv

6. Website dan Backend:
   - WebsiteDashboard/index.html sebagai website dashboard demo.
   - WebsiteBackend berisi template ASP.NET Core API untuk koneksi MySQL.

Catatan penting:
- Untuk membuka aplikasi MAUI, buka ProjectAlprog3.csproj di Visual Studio.
- WebsiteBackend sengaja dipisah agar tidak mengganggu build MAUI.
- Jika ingin koneksi MySQL asli, jalankan WebsiteBackend sebagai project ASP.NET Core terpisah.
