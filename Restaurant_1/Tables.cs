using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_1
{


    public partial class Tables : Form
    {
        public string user_role {  get; set; }


        string connectionString = "Data Source=LAPTOP-DP9CSODP\\SQLEXPRESS;Initial Catalog=Restaurant_1;Integrated Security=True";

        public Tables()
        {
            InitializeComponent();
        }

        // Masaların rezervasyon durumunu kontrol etmek
        private bool IsTableReserved(int tableNumber)
        {
            bool isReserved = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT IsReserved FROM Tables WHERE TableID = @TableNumber";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TableNumber", tableNumber);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        isReserved = (bool)result;
                    }
                }
            }
            return isReserved;
        }

        // Masa rezerve durumunu güncellemek
        private void UpdateTableStatusInDatabase(int tableNumber, bool isReserved)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Tables SET IsReserved = @IsReserved WHERE TableNumber = @TableNumber";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TableNumber", tableNumber);
                    command.Parameters.AddWithValue("@IsReserved", isReserved);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Sipariş formunu açmak
        private void OpenSiparisForm(int seciliMasa)
        {
            Siparis siparisForm = new Siparis();
            siparisForm.secili_masa = seciliMasa;
            siparisForm.Show();
            this.Hide();
        }

        // Masa tıklama event'leri
        private void pic1_Click(object sender, EventArgs e) => HandleTableClick(11, pic11);
        private void pic2_Click(object sender, EventArgs e) => HandleTableClick(12, pic12);
        private void pic3_Click(object sender, EventArgs e) => HandleTableClick(13, pic13);
        private void pic4_Click(object sender, EventArgs e) => HandleTableClick(14, pic14);
        private void pic5_Click(object sender, EventArgs e) => HandleTableClick(15, pic15);
        private void pic6_Click(object sender, EventArgs e) => HandleTableClick(16, pic16);
        private void pic7_Click(object sender, EventArgs e) => HandleTableClick(17, pic17);
        private void pic8_Click(object sender, EventArgs e) => HandleTableClick(18, pic18);
        private void pic9_Click(object sender, EventArgs e) => HandleTableClick(19, pic19);
        private void pic10_Click(object sender, EventArgs e) => HandleTableClick(20, pic20);

        // Ortak masa işlemleri
        private void HandleTableClick(int tableNumber, PictureBox pictureBox)
        {
            if (!IsTableReserved(tableNumber))
            {
                // Masa rezerve edilmedi, rezerve et
                UpdateTableStatusInDatabase(tableNumber, true);
                pictureBox.BackColor = System.Drawing.Color.Red; // Masa arka planını kırmızı yap
                OpenSiparisForm(tableNumber); // Sipariş formunu aç
            }
            else
            {
                // Masa zaten rezerve edilmiş
                MessageBox.Show("Bu masa zaten rezerve edilmiştir.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Form yüklendiğinde tüm masaların durumunu kontrol et
        private void Tables_Load(object sender, EventArgs e)
        {
            CheckTableStatus();
        }

        // Tüm masaların durumunu kontrol et ve PictureBox arka planlarını güncelle
        private void CheckTableStatus()
        {
            for (int i = 11; i <= 20; i++)
            {
                PictureBox pictureBox = (PictureBox)Controls["pic" + i];
                if (IsTableReserved(i))
                    pictureBox.BackColor = System.Drawing.Color.Red; // Dolu ise kırmızı
                else
                    pictureBox.BackColor = System.Drawing.Color.Green; // Boş ise yeşil
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show("Ana Sayfaya Dönmek İster Misin?", "Uygulama Çıkışı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result1 == DialogResult.Yes)
            {
                Menu menu = new Menu();
                menu.user_role = this.user_role;
                menu.Show();
                this.Hide();




            }
        }
    }
}
