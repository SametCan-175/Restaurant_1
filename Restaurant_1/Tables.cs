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
        public Tables()
        {
            InitializeComponent();
        }

        private void UpdateTableStatus()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Her masa için sorgu yapılacak
                for (int i = 1; i <= 10; i++)
                {
                    // Her masa için Is_Reserved değerini sorguluyoruz
                    SqlCommand sqlCommand = new SqlCommand("SELECT IsReserved FROM Tables WHERE TableNumber = @TableNumber", connection);
                    sqlCommand.Parameters.AddWithValue("@TableNumber", i);

                    object result = sqlCommand.ExecuteScalar();
                    if (result != null)
                    {
                        bool isReserved = (bool)result;

                        // PictureBox'ı duruma göre güncelliyoruz
                        UpdatePictureBoxStatus(i, isReserved);
                    }
                }
            }
        }

        private void UpdatePictureBoxStatus(int tableNumber, bool isReserved)
        {
            // Dinamik olarak PictureBox kontrolünü alıyoruz
            PictureBox pictureBox = (PictureBox)Controls.Find("pic" + tableNumber, true).FirstOrDefault();

            if (pictureBox != null)
            {
                if (isReserved)
                {
                    // Eğer masa doluysa, kırmızı renk veya dolu simgesi kullanabilirsiniz
                    pictureBox.BackColor = Color.Red;  // Dolu
                }
                else
                {
                    // Eğer masa boşsa, yeşil renk veya boş simgesi kullanabilirsiniz
                    pictureBox.BackColor = Color.Green;  // Boş
                }
            }
        }
        private bool IsTableReserved(int tableNumber)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT IsReserved FROM Tables WHERE TableNumber = @TableNumber", connection);
                sqlCommand.Parameters.AddWithValue("@TableNumber", tableNumber);
                object result = sqlCommand.ExecuteScalar();

                return result != null && (bool)result;
            }
        }

        private void UpdateTableStatusInDatabase(int tableNumber, bool isReserved)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("UPDATE Tables SET IsReserved = @IsReserved WHERE TableNumber = @TableNumber", connection);
                sqlCommand.Parameters.AddWithValue("@IsReserved", isReserved);
                sqlCommand.Parameters.AddWithValue("@TableNumber", tableNumber);
                sqlCommand.ExecuteNonQuery();
            }
        }

        string connectionString = "Data Source=LAPTOP-DP9CSODP\\SQLEXPRESS;Initial Catalog=Restaurant_1;Integrated Security=True";
        private void pic1_Click(object sender, EventArgs e)
        {
            int tableNumber = 1;
            bool isReserved = IsTableReserved(tableNumber);

            if (isReserved)
            {
                // Masa boşaltılacaksa
                UpdateTableStatusInDatabase(tableNumber, false);
                UpdatePictureBoxStatus(tableNumber, false);
            }
            else
            {
                // Masa rezerve edilecekse
                UpdateTableStatusInDatabase(tableNumber, true);
                UpdatePictureBoxStatus(tableNumber, true);
            }
            Siparis frmSiparis = new Siparis
            {
                secili_masa = 1
            };
            frmSiparis.Show();
            this.Hide();
        }

        private void pic2_Click(object sender, EventArgs e)
        {
            int tableNumber = 2;
            bool isReserved = IsTableReserved(tableNumber);

            if (isReserved)
            {
                // Masa boşaltılacaksa
                UpdateTableStatusInDatabase(tableNumber, false);
                UpdatePictureBoxStatus(tableNumber, false);
            }
            else
            {
                // Masa rezerve edilecekse
                UpdateTableStatusInDatabase(tableNumber, true);
                UpdatePictureBoxStatus(tableNumber, true);
            }
          
            Siparis frmSiparis = new Siparis
            {
                secili_masa = 2
            };
            frmSiparis.Show();
            this.Hide();
        }

        private void pic3_Click(object sender, EventArgs e)
        {
            Siparis frmSiparis = new Siparis
            {
                secili_masa = 3
            };
            frmSiparis.Show();
            this.Hide();
        }

        private void pic4_Click(object sender, EventArgs e)
        {
            Siparis frmSiparis = new Siparis
            {
                secili_masa = 4
            };
            frmSiparis.Show();
            this.Hide();
        }

        private void pic5_Click(object sender, EventArgs e)
        {
            Siparis frmSiparis = new Siparis
            {
                secili_masa = 5
            };
            frmSiparis.Show();
            this.Hide();
        }

        private void pic6_Click(object sender, EventArgs e)
        {
            Siparis frmSiparis = new Siparis
            {
                secili_masa = 6
            };
            frmSiparis.Show();
            this.Hide();
        }

        private void pic7_Click(object sender, EventArgs e)
        {
            Siparis frmSiparis = new Siparis
            {
                secili_masa = 7
            };
            frmSiparis.Show();
            this.Hide();
        }

        private void pic8_Click(object sender, EventArgs e)
        {
            Siparis frmSiparis = new Siparis
            {
                secili_masa = 8
            };
            frmSiparis.Show();
            this.Hide();
        }

        private void pic9_Click(object sender, EventArgs e)
        {
            Siparis frmSiparis = new Siparis
            {
                secili_masa = 9
            };
            frmSiparis.Show();
            this.Hide();
        }

        private void pic10_Click(object sender, EventArgs e)
        {
            Siparis frmSiparis = new Siparis
            {
                secili_masa = 10
            };
            frmSiparis.Show();
            this.Hide();
        }
       

        private void Tables_Load(object sender, EventArgs e)
        {
            UpdateTableStatus();
        }

    }
}
