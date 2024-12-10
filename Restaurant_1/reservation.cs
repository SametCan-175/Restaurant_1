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
    public partial class reservation : Form
    {
        public reservation()
        {
            InitializeComponent();
        }
        string connectionString = "Data Source=LAPTOP-DP9CSODP\\SQLEXPRESS;Initial Catalog=Restaurant_1;Integrated Security=True";
        private void OpenReservationForm(int tableID)
        {
            using (reservation formReservation = new reservation(tableID))
            {
                if (formReservation.ShowDialog() == DialogResult.OK)
                {
                    // Rezervasyon başarıyla yapıldıktan sonra masa durumunu güncelle
                    UpdateTableStatus();
                }
            }
        }
        private void UpdateTableStatus()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                for (int i = 1; i <= 10; i++) // 10 masa için döngü
                {
                    SqlCommand sqlCommand = new SqlCommand(
                        "SELECT COUNT(*) FROM Reservation WHERE TableID = @TableID AND IsActive = 1", connection);
                    sqlCommand.Parameters.AddWithValue("@TableID", i);

                    int count = (int)sqlCommand.ExecuteScalar();
                    PictureBox pictureBox = (PictureBox)Controls.Find("pic" + i, true).FirstOrDefault();

                    if (pictureBox != null)
                    {
                        pictureBox.BackColor = count > 0 ? Color.Red : Color.Green; // Doluysa kırmızı, boşsa yeşil
                    }
                }
            }
        }

        private void pic1_Click(object sender, EventArgs e)
        {
            OpenReservationForm(1);
        }

        private void pic2_Click(object sender, EventArgs e)
        {
            OpenReservationForm(2);
        }

        private void pic3_Click(object sender, EventArgs e)
        {
            OpenReservationForm(3);
        }

        private void pic4_Click(object sender, EventArgs e)
        {
            OpenReservationForm(4);
        }

        private void pic5_Click(object sender, EventArgs e)
        {
            OpenReservationForm(5);
        }

        private void pic6_Click(object sender, EventArgs e)
        {
            OpenReservationForm(6);
        }

        private void pic7_Click(object sender, EventArgs e)
        {
            OpenReservationForm(7);
        }

        private void pic8_Click(object sender, EventArgs e)
        {
            OpenReservationForm(8);
        }

        private void pic9_Click(object sender, EventArgs e)
        {
            OpenReservationForm(9);
        }

        private void pic10_Click(object sender, EventArgs e)
        {
            OpenReservationForm(10);
        }

        private void reservation_Load(object sender, EventArgs e)
        {

        }
    }
}
