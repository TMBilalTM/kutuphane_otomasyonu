﻿using Bunifu.UI.WinForms;
using FontAwesome.Sharp;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KutuphaneSistemi
{
    public partial class Menu : Form
    {
        private Profile profileform; Form7 form7; YazarUpdate yazarupdate; UyelerUpdate uyelerupdate; Menu form5;
        bool sideBarExpand;
        private IconButton suankibuton;
        private Panel leftborderpanel;
        private MySqlConnection connection;
        private string tablo = "Null";
        Random rastgele = new Random();
        private string connectionString = "Server=localhost;Database=kütüphane sistemi;Uid=root;Pwd='';";
        private ContextMenuStrip contextMenuStrip1;

        private void Yedek()
        {
            string masaustuDizin = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string dosyaYolu = Path.Combine(masaustuDizin, "kitap.sql");

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    using (MySqlBackup backup = new MySqlBackup(command))
                    {
                        try
                        {
                            command.Connection = conn;
                            conn.Open();
                            if (!File.Exists(dosyaYolu))
                            {
                                using (File.Create(dosyaYolu)) { }
                            }
                            backup.ExportToFile(dosyaYolu);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Yedekleme hatası: " + ex.Message);
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }
        }

        private void YedekYukle()
        {
            string masaustuDizin = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string dosyaYolu = Path.Combine(masaustuDizin, "kitap.sql");
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    using (MySqlBackup backup = new MySqlBackup(command))
                    {
                        try
                        {
                            command.Connection = conn;
                            conn.Open();
                            backup.ImportFromFile(dosyaYolu);
                            // MessageBox.Show("Geri alma başarılı!", "Kütüphane Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Yedek geri yükleme hatası: " + ex.Message);
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }
        }
        public Menu(Form7 form7references, UyelerUpdate uyelerreferences)
        {
            form7 = form7references;
            uyelerupdate = uyelerreferences;
            Yedek();
            InitializeComponent();
            InitializeDatabaseConnection();
            GetAndDisplayComputerName();
            leftborderpanel = new Panel();
            profileform = new Profile(form5);
            leftborderpanel.Size = new Size(7, 102);
            panel1.Controls.Add(leftborderpanel);
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }
        private void EditMenuItem_Click(object sender, EventArgs e)
        {
            if (tablo == "üyeler")
            {
                ıconButton14.PerformClick();
            }
            if (tablo == "kitaplar")
            {
                ıconButton11.PerformClick();
            }
        }
        private void AddMenuItem_Click(object sender, EventArgs e)
        {
            if (tablo == "üyeler")
            {
                ıconButton13.PerformClick();
            }
            if (tablo == "kitaplar")
            {
                ıconButton12.PerformClick();
            }
            if (tablo == "ödünc_kitaplar")
            {
                ıconButton15.PerformClick();
            }
        }
        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            if(tablo == "üyeler")
            {
                ıconButton2.PerformClick();
            }
            if (tablo == "kitaplar")
            {
                ıconButton5.PerformClick();
            }
            if (tablo == "ödünc_kitaplar")
            {
                ıconButton8.PerformClick();
            }
            if (tablo == "gecikmeler")
            {
                ıconButton7.PerformClick();
            }
        }

        private void InitializeDatabaseConnection()
        {
            if (connection == null)
            {
                connection = new MySqlConnection(connectionString);
            }
        }
        private void GetAndDisplayComputerName()
        {
            try
            {
                string computerName = Environment.UserName;
                label8.Text = $"User: {computerName}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}");
            }
        }

        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(172, 126, 141);
            public static Color color2 = Color.FromArgb(24, 161, 251);
            public static Color color3 = Color.FromArgb(95, 77, 221);
            public static Color color4 = Color.FromArgb(244, 164, 96);
        }
        private void AktifButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                KapaliButton();
                suankibuton = (IconButton)senderBtn;
                suankibuton.ForeColor = color;
                suankibuton.TextAlign = ContentAlignment.MiddleCenter;
                suankibuton.IconColor = color;
                suankibuton.TextImageRelation = TextImageRelation.TextBeforeImage;
                suankibuton.ImageAlign = ContentAlignment.MiddleRight;
                leftborderpanel.BackColor = Color.Transparent;
                //leftborderpanel.Location = new Point(suankibuton.Location.Y);
                leftborderpanel.Visible = true;
                leftborderpanel.BringToFront();
                ıconPictureBox1.IconChar = suankibuton.IconChar;
                ıconPictureBox1.IconColor = color;
                label5.Text = suankibuton.Text;
                label5.ForeColor = color;
            }
        }
        private void KapaliButton()
        {
            if (suankibuton != null)
            {
                suankibuton.ForeColor = Color.Gainsboro;
                suankibuton.TextAlign = ContentAlignment.MiddleLeft;
                suankibuton.IconColor = Color.Gainsboro;
                suankibuton.TextImageRelation = TextImageRelation.ImageBeforeText;
                suankibuton.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            WampCalistirEgerCalismiyorsa();
            this.BackColor = Color.FromArgb(31, 30, 58);
        }
        private void WampCalistirEgerCalismiyorsa()
        {
            if (!IsWampServerRunning())
            {
                WampCalistir();
            }
        }
        private bool IsWampServerRunning()
        {
            Process[] processes = Process.GetProcessesByName("wampmanager");
            return processes.Length > 0;
        }
        private void WampCalistir()
        {
            try
            {
                string wampServerPath = @"C:\wamp64\wampmanager.exe";

                if (System.IO.File.Exists(wampServerPath))
                {
                    Process.Start(wampServerPath);
                    MessageBox.Show("Wampserveri başlattım!");
                }
                else
                {
                    MessageBox.Show("WampServer yüklü değil. Lütfen WampServer'ı yükleyip tekrar deneyin.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("WampServer başlatılırken bir hata oluştu: " + ex.Message);
            }
        }

        private void ToggleControls(bool visible)
        {
            panel3.Visible = true;
            label6.Visible = !visible;
            ıconButton2.Visible = visible;
            ıconButton5.Visible = visible;
            ıconButton12.Visible = visible;
            ıconButton11.Visible = visible;
            ıconButton14.Visible = visible;
            pictureBox1.Visible = visible;
            label7.Visible = !visible;
            label2.Visible = visible;
            ıconButton6.Visible = visible;
            ıconButton7.Visible = visible;
        }
        private async Task VeritabaniSorgula(string query, Action<MySqlDataAdapter, DataTable> onSuccess)
        {
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                try
                {
                    await connection.OpenAsync();
                    DataTable dt = new DataTable();
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        onSuccess(da, dt);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private async void ıconButton1_Click_1(object sender, EventArgs e)
        {
            tablo = "kitaplar";
            Sifirla();
            ıconButton5.Visible = true;
            label2.Visible = true;
            pictureBox1.Visible = true;
            ıconButton11.Visible = true;
            ıconButton13.Visible = false; ıconButton15.Visible = false;
            ıconButton12.Visible = true;
            AktifButton(sender, RGBColors.color1);
            dataGridView2.Visible = true;
            dataGridView2.Size = new Size(735, 416);
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            await VeritabaniSorgula("SELECT * FROM kitap", (adapter, dt) =>
            {
                dataGridView2.Size = new Size(735, 416);
                adapter.Fill(dt);
                dataGridView2.DataSource = dt;
                if (dataGridView2.Columns.Contains("Kitap_ID"))
                {
                    dataGridView2.Columns["Kitap_ID"].Visible = false;
                }
                if (dataGridView2.Columns.Contains("ID"))
                {
                    dataGridView2.Columns["ID"].Visible = false;
                }
                if (dataGridView2.Columns.Contains("resim"))
                {
                    dataGridView2.Columns["resim"].Visible = false;
                }
            });

            await VeritabaniSorgula("SELECT ID, COUNT(*) AS kitap_sayısı FROM kitap GROUP BY ID", (adapter, dt) =>
            {
                adapter.Fill(dt);
                if (dataGridView2.Columns.Contains("Kitap_ID"))
                {
                    dataGridView2.Columns["Kitap_ID"].Visible = false;
                }
                if (dataGridView2.Columns.Contains("ID"))
                {
                    dataGridView2.Columns["ID"].Visible = false;
                }
            });
        }
        private void Sifirla()
        {
            dataGridView2.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Light;
            ıconButton6.Visible = false;
            ıconButton7.Visible = false;
            label7.Visible = false;
            label6.Visible = false;
            ıconButton6.Visible = false;
            ıconButton7.Visible = false;
            ıconButton8.Visible = false;
            bunifuTextBox3.Visible = true;
            ToggleControls(false);
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int Msg, int wParam, int lParam);
        private void panel5_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void ıconPictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void ıconPictureBox4_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        private void ıconButton3_Click(object sender, EventArgs e)
        {
            tablo = "üyeler";
            Sifirla();
            AktifButton(sender, RGBColors.color2);
            int minimumWidth = dataGridView2.MaximumSize.Width;
            dataGridView2.Width = minimumWidth;
            dataGridView2.Visible = true;
            ıconButton13.Visible = true;
            ıconButton14.Visible = true;
            ıconButton2.Visible = true;
            ıconButton15.Visible = false;
            ıconButton12.Visible = false;
            string query = "SELECT uyeler.ID,uyeler.Ad,uyeler.Soyad,uyeler.Kk AS Kimlik_Kartı,uyeler.Okulno,statu.statu_adi,uyeler.DogumT FROM uyeler JOIN statu ON uyeler.statu = statu.ID";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    if (dataGridView2.Columns.Contains("ID"))
                    {
                        dataGridView2.Columns["ID"].Visible = false;
                    }
                    if (dataGridView2.Columns.Contains("Okulno"))
                    {
                        dataGridView2.Columns["Okulno"].Visible = false;
                    }
                    dataGridView2.DataSource = dt;
                }
                catch
                {
                }
                finally
                {
                    connection.Close();
                }
            }
            using (MySqlConnection connection = new MySqlConnection("Server=localhost;Database=kütüphane sistemi;Uid=root;Pwd='';"))
            {
                query = "SELECT ID, COUNT(*) AS üye_sayısı FROM uyeler GROUP BY ID";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        DataTable dt = new DataTable();
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            if (dataGridView2.Columns.Contains("ID"))
                            {
                                dataGridView2.Columns["ID"].Visible = false;
                            }
                            if (dataGridView2.Columns.Contains("Okulno"))
                            {
                                dataGridView2.Columns["Okulno"].Visible = false;
                            }
                            adapter.Fill(dt);

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message);
                    }
                }
            }
        }

        private void sideBarTimer_Tick(object sender, EventArgs e)
        {
            if (sideBarExpand)
            {
                panel1.Width -= 10;
                if (panel1.Width == panel1.MinimumSize.Width)
                {
                    sideBarExpand = false;
                    ıconButton1.Text = "";
                    ıconButton3.Text = "";
                    ıconButton4.Text = "";
                    ıconButton9.Text = "";
                    ıconButton10.Text = "";
                    labelAdminName.Visible = false;
                    sideBarTimer.Stop();
                }
            }
            else
            {
                panel1.Width += 10;
                if (panel1.Width == panel1.MaximumSize.Width)
                {
                    sideBarExpand = true;
                    ıconButton1.Text = "Kitaplar";
                    ıconButton4.Text = "Ödünç Kitaplar";
                    ıconButton3.Text = "Üyeler";
                    ıconButton9.Text = "İstatistikler";
                    ıconButton10.Text = "Yazarlar";
                    labelAdminName.Visible = true;
                    sideBarTimer.Stop();
                }
            }

        }

        private void ıconPictureBox5_Click(object sender, EventArgs e)
        {
            sideBarTimer.Start();
        }
        private void Resle()
        {
            bunifuTextBox3.Text = ""; label2.Visible = false; pictureBox1.Visible = false;
            ıconButton13.Visible = false;
            ıconButton5.Visible = false; ıconButton2.Visible = false; ıconButton14.Visible = false; panel3.Visible = true; ıconButton11.Visible = false; label6.Visible = true; label7.Visible = true;
            ıconButton6.Visible = false; ıconButton7.Visible = false; ıconButton8.Visible = false; bunifuTextBox3.Visible = false;
            dataGridView2.Visible = false; ıconPictureBox1.IconChar = IconChar.Home; ıconPictureBox1.IconColor = Color.MediumPurple; label5.Text = "Home"; label5.ForeColor = Color.Gainsboro;
            ıconButton15.Visible = false; ıconButton12.Visible = false;
            KapaliButton();
        }
        private void ıconButton4_Click(object sender, EventArgs e)
        {
            tablo = "ödünc_kitaplar";
            Sifirla();
            int minimumWidth = dataGridView2.MinimumSize.Width;
            dataGridView2.Width = minimumWidth;
            AktifButton(sender, RGBColors.color4);
            dataGridView2.Visible = true; label2.Visible = true;
            pictureBox1.Visible = true;
            ıconButton6.Visible = true;
            ıconButton15.Visible = true; ıconButton12.Visible = false; ıconButton13.Visible = false;
            ıconButton8.Visible = true;
            string query = "SELECT odunc_kitaplar.Verilen_Tarih,kitap.resim, odunc_kitaplar.Alinacak_Tarih, " +
       "kitap.Name AS KitapAdi, kitap.ID AS kitap_ID, kitap.Author, " +
       "uyeler.Ad, uyeler.Soyad " +
       "FROM odunc_kitaplar " +
       "JOIN kitap ON odunc_kitaplar.kitap_ID = kitap.ID " +
       "JOIN uyeler ON odunc_kitaplar.Uye_ID = uyeler.ID " +
       "WHERE odunc_kitaplar.Alinan_Tarih IS NULL";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    dataGridView2.DataSource = dt;
                }
                catch
                {
                }
                finally
                {
                    connection.Close();
                }
            }
            using (MySqlConnection connection = new MySqlConnection("Server=localhost;Database=kütüphane sistemi;Uid=root;Pwd='';"))
            {
                query = "SELECT ID, COUNT(*) AS üye_sayısı FROM odunc_kitaplar GROUP BY ID";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        DataTable dt = new DataTable();
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                            if (dataGridView2.Columns.Contains("Kitap_ID"))
                            {
                                dataGridView2.Columns["Kitap_ID"].Visible = false;
                            }
                            if (dataGridView2.Columns.Contains("resim"))
                            {
                                dataGridView2.Columns["resim"].Visible = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message);
                    }
                }
            }
        }

        private void IconButton6_Click(object sender, EventArgs e)
        {
            tablo = "gecikmeler";
            dataGridView2.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Maroon;
            ıconButton7.Visible = true;
            ıconButton8.Visible = false;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT odunc_kitaplar.Verilen_Tarih, odunc_kitaplar.Alinacak_Tarih, " +
      "Kitap_ID, kitap.Name AS KitapAdi, kitap.Author,kitap.resim, " +
      "uyeler.Ad, uyeler.Soyad " +
      "FROM odunc_kitaplar " +
      "JOIN kitap ON odunc_kitaplar.kitap_ID = kitap.ID " +
      "JOIN uyeler ON odunc_kitaplar.Uye_ID = uyeler.ID " +
      "WHERE Alinacak_Tarih < @CurrentDate AND Alinan_Tarih IS NULL";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CurrentDate", DateTime.Now);
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            if (dataGridView2.Columns.Contains("Kitap_ID"))
                            {
                                dataGridView2.Columns["Kitap_ID"].Visible = false;
                            }
                            if (dataGridView2.Columns.Contains("resim"))
                            {
                                dataGridView2.Columns["resim"].Visible = false;
                            }
                            dataGridView2.DataSource = dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }

        }
        private void ıconButton7_Click(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            if (labelAdminName.Text != "Giriş Yapılmamış")
            {
                int yetkilerValue = GetYetkilerValueForLoggedInUser(labelAdminName.Text);

                if (yetkilerValue != 4)
                {
                    if (dataGridView2.SelectedRows.Count > 0)
                    {
                        DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];
                        int selectedID = Convert.ToInt32(selectedRow.Cells["Kitap_ID"].Value);
                        try
                        {
                            using (MySqlConnection conn = new MySqlConnection(connectionString))
                            {
                                conn.Open();
                                string deleteQuery = "UPDATE odunc_kitaplar SET Alinan_Tarih = @date WHERE Kitap_ID = @kitapID";
                                using (MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, conn))
                                {
                                    deleteCmd.Parameters.AddWithValue("@kitapID", selectedID);
                                    deleteCmd.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
                                    deleteCmd.ExecuteNonQuery();
                                }
                                string updateQuery = "UPDATE odunc_kitaplar SET Alinan_Tarih = @date WHERE Kitap_ID = @kitapID";
                                using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                                {
                                    updateCmd.Parameters.AddWithValue("@kitapID", selectedID);
                                    updateCmd.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
                                    updateCmd.ExecuteNonQuery();
                                }
                            }

                            MessageBox.Show("Kitap başarıyla iade edildi.");
                            bunifuPanel1.Visible = true;
                            dataGridView2.Rows.Remove(selectedRow);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Hata: " + ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lütfen iade edilecek bir kitap seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Bu işlemi gerçekleştirmek için yeterli yetkiye sahip değilsiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Giriş yapmadan işlem yapamazsın!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void bunifuDropdown1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int x = rastgele.Next(70);
            int y = rastgele.Next(50);
            int z = rastgele.Next(150);
            if (bunifuDropdown1.SelectedIndex == 0)
            {
                SetUIBackgroundColor(Color.FromArgb(31, 30, 78));
            }
            else if (bunifuDropdown1.SelectedIndex == 1)
            {
                SetUIBackgroundColor(Color.FromArgb(31, 30, 58));
            }
            else if (bunifuDropdown1.SelectedIndex == 2)
            {
                SetUIBackgroundColor(Color.FromArgb(23, 15, 58));
            }
            else if (bunifuDropdown1.SelectedIndex == 3)
            {
                SetUIBackgroundColor(Color.FromArgb(x, y, z));
            }
        }

        private void SetUIBackgroundColor(Color color)
        {
            SetControlsBackColor(this.Controls, color);
            this.BackColor = color;
        }

        private void SetControlsBackColor(Control.ControlCollection controls, Color color)
        {
            foreach (Control control in controls)
            {
                if ((control is IconButton) && (control != ıconButton9 || control != ıconButton1 || control != ıconButton4 || control != ıconButton3))
                {
                    continue;
                }

                if (control is TextBox)
                {
                    TextBox textBox = (TextBox)control;
                    textBox.BackColor = color;
                    bunifuTextBox3.FillColor = Color.White;
                }
                else
                {
                    control.BackColor = color;
                    if (control.Controls.Count > 0)
                    {
                        SetControlsBackColor(control.Controls, color);
                    }
                }
            }
        }
        private void ıconButton8_Click(object sender, EventArgs e)
        {
            if (labelAdminName.Text != "Giriş Yapılmamış")
            {
                int yetkilerValue = GetYetkilerValueForLoggedInUser(labelAdminName.Text);

                if (yetkilerValue != 4)
                {
                    DateTime date = DateTime.Now;
                    if (dataGridView2.SelectedRows.Count > 0)
                    {
                        DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];
                        int selectedID = Convert.ToInt32(selectedRow.Cells["kitap_ID"].Value);

                        try
                        {
                            using (MySqlConnection conn = new MySqlConnection(connectionString))
                            {
                                conn.Open();
                                string updateQuery = "UPDATE odunc_kitaplar SET Alinan_Tarih = @alinanTarih WHERE kitap_ID = @kitapID";

                                using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                                {
                                    updateCmd.Parameters.AddWithValue("@alinanTarih", date.ToString("yyyy-MM-dd"));
                                    updateCmd.Parameters.AddWithValue("@kitapID", selectedID);
                                    updateCmd.ExecuteNonQuery();
                                }
                            }
                            MessageBox.Show("Kitap başarıyla iade edildi.");
                            bunifuPanel1.Visible = true;
                            dataGridView2.Rows.Remove(selectedRow);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Hata: " + ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lütfen iade edilecek bir satır seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Bu işlemi gerçekleştirmek için yeterli yetkiye sahip değilsiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Giriş yapmadan işlem yapamazsın!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void panel1_Click(object sender, EventArgs e)
        {
            Resle();
        }

        private void ıconButton9_Click(object sender, EventArgs e)
        {
            info form = new info();
            form.Show();
        }

        private void bunifuTextBox3_TextChange(object sender, EventArgs e)
        {
            string aramaKelimesi = bunifuTextBox3.Text.ToLower();
            DataView dv = ((DataTable)dataGridView2.DataSource).DefaultView;
            StringBuilder filterExpression = new StringBuilder();
            foreach (DataColumn column in dv.Table.Columns)
            {
                if (column.DataType == typeof(string))
                {
                    if (filterExpression.Length > 0)
                        filterExpression.Append(" OR ");
                    filterExpression.Append($"{column.ColumnName} LIKE '%{aramaKelimesi}%'");
                }
                else if (column.DataType == typeof(DateTime))
                {
                    if (filterExpression.Length > 0)
                        filterExpression.Append(" OR ");
                    filterExpression.Append($"CONVERT({column.ColumnName}, 'System.String') LIKE '%{aramaKelimesi}%'");
                }
            }
            dv.RowFilter = filterExpression.ToString();
        }

        private void ıconButton10_Click(object sender, EventArgs e)
        {
            if (labelAdminName.Text != "Giriş Yapılmamış")
            {
                int yetkilerValue = GetYetkilerValueForLoggedInUser(labelAdminName.Text);

                if (yetkilerValue == 1 || yetkilerValue == 2)
                {
                    YedekYukle();
                    Resle();
                }
                else
                {
                    MessageBox.Show("Bu işlemi gerçekleştirmek için yeterli yetkiye sahip değilsiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
            else
            {
                MessageBox.Show("Giriş yapmadan işlem yapamazsın!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void bunifuPictureBox1_Click(object sender, EventArgs e)
        {
            if (labelAdminName.Text == "Giriş Yapılmamış")
            {
                if (Application.OpenForms["Admin"] == null)
                {
                    this.Hide();
                    Admin form = new Admin(form5Reference: form5);
                    form.Show();
                }
            }
            if (labelAdminName.Text != "Giriş Yapılmamış")
            {
                Profile profileForm = new Profile(this);
                profileForm.Show();
            }
        }

        private void ıconButton5_Click_1(object sender, EventArgs e)
        {
            if (labelAdminName.Text != "Giriş Yapılmamış")
            {
                int yetkilerValue = GetYetkilerValueForLoggedInUser(labelAdminName.Text);

                if (yetkilerValue == 1 || yetkilerValue == 2)
                {
                    if (dataGridView2.SelectedRows.Count > 0)
                    {
                        DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];
                        int seciliID = Convert.ToInt32(selectedRow.Cells["ID"].Value);
                        string oduncCheckQuery = "SELECT COUNT(*) FROM odunc_kitaplar WHERE Kitap_ID = @KitapID AND (Alinan_Tarih IS NULL)";
                        using (MySqlCommand oduncCheckCmd = new MySqlCommand(oduncCheckQuery, connection))
                        {
                            oduncCheckCmd.Parameters.AddWithValue("@KitapID", seciliID);

                            try
                            {
                                connection.Open();
                                int oduncRowCount = Convert.ToInt32(oduncCheckCmd.ExecuteScalar());

                                if (oduncRowCount > 0)
                                {
                                    MessageBox.Show("Bu kitap ödünç verildiği için silinemez.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ödünç durumu kontrol edilirken hata oluştu: " + ex.Message);
                                return;
                            }
                            finally
                            {
                                connection.Close();
                            }
                        }
                        string deleteQuery = "DELETE FROM kitap WHERE ID = @id";
                        using (MySqlCommand cmd = new MySqlCommand(deleteQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@id", seciliID);

                            try
                            {
                                connection.Open();
                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Veri başarıyla silindi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    bunifuPanel1.Visible = true;
                                }
                                else
                                {
                                    MessageBox.Show("Silme işlemi başarısız oldu.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Hata: " + ex.Message);
                            }
                            finally
                            {
                                connection.Close();
                                ıconButton1.PerformClick();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lütfen silinecek bir satır seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Bu işlemi gerçekleştirmek için yeterli yetkiye sahip değilsiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Giriş yapmadan işlem yapamazsın!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void ıconButton11_Click(object sender, EventArgs e)
        {
            if (labelAdminName.Text != "Giriş Yapılmamış")
            {
                int yetkilerValue = GetYetkilerValueForLoggedInUser(labelAdminName.Text);

                if (yetkilerValue != 4)
                {
                    if (dataGridView2.SelectedRows.Count > 0)
                    {
                        DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];
                        string id = selectedRow.Cells["ID"].Value.ToString();
                        string isbn = selectedRow.Cells["ISBN"].Value.ToString();
                        string konu = selectedRow.Cells["Description"].Value.ToString();
                        string kitapAdi = selectedRow.Cells["Name"].Value.ToString();
                        string kategori = selectedRow.Cells["Category"].Value.ToString();
                        string yazar = selectedRow.Cells["Author"].Value.ToString();
                        string sayfaSayisi = selectedRow.Cells["No_of_pages"].Value.ToString();
                        string lokasyon = selectedRow.Cells["spot"].Value.ToString();
                        byte[] resimBytes = selectedRow.Cells["resim"].Value as byte[];
                        if (resimBytes != null && resimBytes.Length > 0)
                        {
                            using (MemoryStream ms = new MemoryStream(resimBytes))
                            {
                                pictureBox1.Image = Image.FromStream(ms);
                            }
                        }
                        else
                        {
                            pictureBox1.Image = null;
                        }
                        Form7 form7 = new Form7(this);
                        form7.Show();
                        form7.textBox8.Text = id;
                        form7.textBox1.Text = isbn;
                        form7.textBox4.Text = konu;
                        form7.textBox2.Text = kitapAdi;
                        form7.textBox5.Text = kategori;
                        form7.textBox3.Text = yazar;
                        form7.textBox6.Text = sayfaSayisi;
                        form7.textBox7.Text = lokasyon;
                        form7.pictureBox1.Image = pictureBox1.Image;
                    }
                    else
                    {
                        MessageBox.Show("Lütfen bir satır seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Bu işlemi gerçekleştirmek için yeterli yetkiye sahip değilsiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Giriş yapmadan işlem yapamazsın!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void labelAdminName_Click(object sender, EventArgs e)
        {
            if (labelAdminName.Text == "Giriş Yapılmamış")
            {
                if (Application.OpenForms["Admin"] == null)
                {
                    this.Hide();
                    Admin form = new Admin(form5Reference: form5);
                    form.Show();
                }
            }
            if (labelAdminName.Text != "Giriş Yapılmamış")
            {
                Profile profileForm = new Profile(this);
                profileForm.Show();
            }
        }
        private void ıconButton2_Click(object sender, EventArgs e)
        {
            if (labelAdminName.Text != "Giriş Yapılmamış")
            {
                int yetkilerValue = GetYetkilerValueForLoggedInUser(labelAdminName.Text);

                if (yetkilerValue == 1)
                {
                    if (dataGridView2.SelectedRows.Count > 0)
                    {
                        DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];
                        int seciliID = Convert.ToInt32(selectedRow.Cells["ID"].Value);
                        string oduncCheckQuery = "SELECT COUNT(*) FROM odunc_kitaplar WHERE Uye_ID = @KitapID AND (Alinan_Tarih IS NULL)";
                        using (MySqlCommand oduncCheckCmd = new MySqlCommand(oduncCheckQuery, connection))
                        {
                            oduncCheckCmd.Parameters.AddWithValue("@KitapID", seciliID);

                            try
                            {
                                connection.Open();
                                int oduncRowCount = Convert.ToInt32(oduncCheckCmd.ExecuteScalar());

                                if (oduncRowCount > 0)
                                {
                                    MessageBox.Show("Bu üyeye ödünç kitap verildiği için silinemez.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ödünç durumu kontrol edilirken hata oluştu: " + ex.Message);
                                return;
                            }
                            finally
                            {
                                connection.Close();
                            }
                        }
                        string deleteQuery = "DELETE FROM uyeler WHERE ID = @id";
                        using (MySqlCommand cmd = new MySqlCommand(deleteQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@id", seciliID);

                            try
                            {
                                connection.Open();
                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Veri başarıyla silindi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    bunifuPanel1.Visible = true;
                                }
                                else
                                {
                                    MessageBox.Show("Silme işlemi başarısız oldu.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Hata: " + ex.Message);
                            }
                            finally
                            {
                                connection.Close();
                                ıconButton3.PerformClick();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lütfen silinecek bir satır seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Bu işlemi gerçekleştirmek için yeterli yetkiye sahip değilsiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Giriş yapmadan işlem yapamazsın!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void ıconButton13_Click(object sender, EventArgs e)
        {
            if (labelAdminName.Text != "Giriş Yapılmamış")
            {
                int yetkilerValue = GetYetkilerValueForLoggedInUser(labelAdminName.Text);

                if (yetkilerValue == 2 || yetkilerValue == 1)
                {
                    YeniUye form = new YeniUye(this);
                    form.Show();
                }
                else
                {
                    MessageBox.Show("Bu işlemi gerçekleştirmek için yeterli yetkiye sahip değilsiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Giriş yapmadan işlem yapamazsın!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void ıconButton14_Click(object sender, EventArgs e)
        {
            if (labelAdminName.Text != "Giriş Yapılmamış")
            {
                int yetkilerValue = GetYetkilerValueForLoggedInUser(labelAdminName.Text);

                if (yetkilerValue == 2 || yetkilerValue == 1)
                {
                    if (dataGridView2.SelectedRows.Count > 0)
                    {
                        DateTime data;
                        DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];
                        string dogumTString = selectedRow.Cells["DogumT"].Value.ToString();
                        if (DateTime.TryParse(dogumTString, out data))
                        {
                            string id = selectedRow.Cells["ID"].Value.ToString();
                            string ad = selectedRow.Cells["Ad"].Value.ToString();
                            string soyad = selectedRow.Cells["Soyad"].Value.ToString();
                            string kk = selectedRow.Cells["Kimlik_Kartı"].Value.ToString();
                            string ok = selectedRow.Cells["Okulno"].Value.ToString();
                            string statu = selectedRow.Cells["statu_adi"].Value.ToString();
                            UyelerUpdate uyelerupdate = new UyelerUpdate(this);
                            uyelerupdate.Show();
                            uyelerupdate.textBox8.Text = id;
                            uyelerupdate.textBox1.Text = ad;
                            uyelerupdate.textBox4.Text = soyad;
                            uyelerupdate.comboBox1.Text = "statu seç";
                            uyelerupdate.textBox5.Text = kk;
                            uyelerupdate.textBox3.Text = ok;
                            uyelerupdate.dateTimePicker1.Value = data;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lütfen bir satır seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Bu işlemi gerçekleştirmek için yeterli yetkiye sahip değilsiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Giriş yapmadan işlem yapamazsın!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void ıconButton15_Click(object sender, EventArgs e)
        {
            if (labelAdminName.Text != "Giriş Yapılmamış")
            {
                int yetkilerValue = GetYetkilerValueForLoggedInUser(labelAdminName.Text);

                if (yetkilerValue != 4)
                {
                    OduncVer form = new OduncVer(this);
                    form.Show();
                }
                else
                {
                    MessageBox.Show("Bu işlemi gerçekleştirmek için yeterli yetkiye sahip değilsiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Giriş yapmadan işlem yapamazsın!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private int GetYetkilerValueForLoggedInUser(string username)
        {
            int yetkilerValue = -1;

            using (MySqlConnection connection = new MySqlConnection("Server=localhost;Database=kütüphane sistemi;Uid=root;Pwd='';"))
            {
                connection.Open();
                string query = "SELECT yetkiler FROM admin WHERE Name = @isim";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@isim", username);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            yetkilerValue = (int)reader["yetkiler"];
                        }
                    }
                }
            }
            return yetkilerValue;
        }
        private void bunifuPictureBox2_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            if (bunifuPictureBox2.Image != null && bunifuPictureBox2.Image.Tag != null && bunifuPictureBox2.Image.Tag.ToString() == "beta.png")
            {
                toolTip.SetToolTip(bunifuPictureBox2, "★ Şu an beta test aşamasındasınız.");
            }
            else
            {
                toolTip.SetToolTip(bunifuPictureBox2, "Sıcak bir kahve huzuru...");
            }
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            bunifuPanel1.Visible = false;
            Yedek();
            Resle();
            YedekYukle();
        }
        private void ıconPictureBox6_Click(object sender, EventArgs e)
        {
            bunifuPanel1.Visible = false;
            Resle();
            YedekYukle();
        }
        private void dataGridView2_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView2.Rows[e.RowIndex];
            int resimSutunuIndex = -1;

            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {
                if (column.Name == "resim")
                {
                    resimSutunuIndex = column.Index;
                    break;
                }
            }

            if (resimSutunuIndex != -1)
            {
                object cellValue = selectedRow.Cells[resimSutunuIndex].Value;

                if (cellValue != null && cellValue != DBNull.Value)
                {
                    byte[] imageBytes = cellValue as byte[];

                    if (imageBytes != null)
                    {
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            pictureBox1.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        pictureBox1.Image = null;
                    }
                }
                else
                {
                    pictureBox1.Image = null;
                }
            }
        }

        private void ıconButton12_Click(object sender, EventArgs e)
        {
            if (labelAdminName.Text != "Giriş Yapılmamış")
            {
                int yetkilerValue = GetYetkilerValueForLoggedInUser(labelAdminName.Text);

                if (yetkilerValue == 1 || yetkilerValue == 2)
                {
                    YeniKitap form = new YeniKitap(this);
                    form.Show();
                }
                else
                {
                    MessageBox.Show("Bu işlemi gerçekleştirmek için yeterli yetkiye sahip değilsiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Giriş yapmadan işlem yapamazsın!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ıconButton10_Click_1(object sender, EventArgs e)
        {
            if (labelAdminName.Text != "Giriş Yapılmamış")
            {
                int yetkilerValue = GetYetkilerValueForLoggedInUser(labelAdminName.Text);

                if (yetkilerValue != 4)
                {
                    Yazarlar yazarlar = new Yazarlar(yazarupdate);
                    yazarlar.Show();
                }
                else
                {
                    MessageBox.Show("Yazarlar tablosunu görmek için yeterli yetkiye sahip değilsiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Giriş yapmadan bu kısma giriş yapamazsın!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void dataGridView2_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == dataGridView2.Columns["İslemler"].Index)
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                    string editButtonText = "Düzenle";
                    string deleteButtonText = "Sil";

                    Font buttonFont = new Font("Arial", 10, FontStyle.Regular);

                    SizeF editButtonSize = e.Graphics.MeasureString(editButtonText, buttonFont);
                    SizeF deleteButtonSize = e.Graphics.MeasureString(deleteButtonText, buttonFont);

                    int buttonWidth = (int)Math.Max(editButtonSize.Width, deleteButtonSize.Width) + 10;
                    int buttonHeight = (int)Math.Max(editButtonSize.Height, deleteButtonSize.Height) + 5;

                    int editButtonX = e.CellBounds.Left + (e.CellBounds.Width - (2 * buttonWidth)) / 2;
                    int deleteButtonX = editButtonX + buttonWidth + 5;
                    int buttonY = e.CellBounds.Top + (e.CellBounds.Height - buttonHeight) / 2;

                    Rectangle editButtonRect = new Rectangle(editButtonX, buttonY, buttonWidth, buttonHeight);
                    Rectangle deleteButtonRect = new Rectangle(deleteButtonX, buttonY, buttonWidth, buttonHeight);

                    // Düzenle butonu yeşil renkte çizilir
                    using (SolidBrush greenBrush = new SolidBrush(Color.Green))
                    {
                        e.Graphics.DrawString(editButtonText, buttonFont, greenBrush, editButtonRect);
                    }

                    // Sil butonu kırmızı renkte çizilir
                    using (SolidBrush redBrush = new SolidBrush(Color.Red))
                    {
                        e.Graphics.DrawString(deleteButtonText, buttonFont, redBrush, deleteButtonRect);
                    }

                    e.Handled = true;
                }
            }
        }

        private void dataGridView2_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                int currentMouseOverRow = dataGridView2.HitTest(e.X, e.Y).RowIndex;
                int currentMouseOverColumn = dataGridView2.HitTest(e.X, e.Y).ColumnIndex;

                if (currentMouseOverRow >= 0 && currentMouseOverColumn >= 0)
                {
                    dataGridView2.ClearSelection();
                    dataGridView2.Rows[currentMouseOverRow].Selected = true;
                    dataGridView2.CurrentCell = dataGridView2[currentMouseOverColumn, currentMouseOverRow];

                    if(tablo == "üyeler") { 
                    contextMenuStrip1 = new ContextMenuStrip();

                    ToolStripMenuItem editMenuItem = new ToolStripMenuItem("Üye Düzenle");
                    editMenuItem.Click += EditMenuItem_Click;
                    contextMenuStrip1.Items.Add(editMenuItem);

                    ToolStripMenuItem deleteMenuItem = new ToolStripMenuItem("Üye Sil");
                    deleteMenuItem.Click += DeleteMenuItem_Click;
                    contextMenuStrip1.Items.Add(deleteMenuItem);

                        ToolStripMenuItem addMember = new ToolStripMenuItem("Üye Ekle");
                        addMember.Click += AddMenuItem_Click;
                        contextMenuStrip1.Items.Add(addMember);

                        contextMenuStrip1.Show(dataGridView2, new Point(e.X, e.Y));
                    }
                    if (tablo == "kitaplar")
                    {
                        contextMenuStrip1 = new ContextMenuStrip();

                        ToolStripMenuItem editMenuItem = new ToolStripMenuItem("Kitap Düzenle");
                        editMenuItem.Click += EditMenuItem_Click;
                        contextMenuStrip1.Items.Add(editMenuItem);

                        ToolStripMenuItem deleteMenuItem = new ToolStripMenuItem("Kitap Sil");
                        deleteMenuItem.Click += DeleteMenuItem_Click;
                        contextMenuStrip1.Items.Add(deleteMenuItem);

                        ToolStripMenuItem addBook = new ToolStripMenuItem("Kitap Ekle");
                        addBook.Click += AddMenuItem_Click;
                        contextMenuStrip1.Items.Add(addBook);

                        contextMenuStrip1.Show(dataGridView2, new Point(e.X, e.Y));
                    }
                    if (tablo == "ödünc_kitaplar")
                    {
                        contextMenuStrip1 = new ContextMenuStrip();
                        ToolStripMenuItem deleteMenuItem = new ToolStripMenuItem("Kitap iade");
                        deleteMenuItem.Click += DeleteMenuItem_Click;
                        contextMenuStrip1.Items.Add(deleteMenuItem);

                        ToolStripMenuItem odunc = new ToolStripMenuItem("Yeni Ödünç Kitap");
                        odunc.Click += AddMenuItem_Click;
                        contextMenuStrip1.Items.Add(odunc);

                        contextMenuStrip1.Show(dataGridView2, new Point(e.X, e.Y));
                    }
                    if (tablo == "gecikmeler")
                    {
                        contextMenuStrip1 = new ContextMenuStrip();
                        ToolStripMenuItem deleteMenuItem = new ToolStripMenuItem("Gecikme Sil");
                        deleteMenuItem.Click += DeleteMenuItem_Click;
                        contextMenuStrip1.Items.Add(deleteMenuItem);

                        contextMenuStrip1.Show(dataGridView2, new Point(e.X, e.Y));
                    }
                } else
                {
                    if (tablo == "üyeler")
                    {
                        contextMenuStrip1 = new ContextMenuStrip();
                        ToolStripMenuItem addMember = new ToolStripMenuItem("Üye Ekle");
                        addMember.Click += AddMenuItem_Click;
                        contextMenuStrip1.Items.Add(addMember);

                        contextMenuStrip1.Show(dataGridView2, new Point(e.X, e.Y));
                    }
                    if (tablo == "kitaplar")
                    {
                        contextMenuStrip1 = new ContextMenuStrip();
                        ToolStripMenuItem addBook = new ToolStripMenuItem("Kitap Ekle");
                        addBook.Click += AddMenuItem_Click;
                        contextMenuStrip1.Items.Add(addBook);

                        contextMenuStrip1.Show(dataGridView2, new Point(e.X, e.Y));
                    }
                    if (tablo == "ödünc_kitaplar")
                    {
                        contextMenuStrip1 = new ContextMenuStrip();

                        ToolStripMenuItem odunc = new ToolStripMenuItem("Yeni Ödünç Kitap");
                        odunc.Click += AddMenuItem_Click;
                        contextMenuStrip1.Items.Add(odunc);

                        contextMenuStrip1.Show(dataGridView2, new Point(e.X, e.Y));
                    }
                  
                }
            }
        }

    }
}
