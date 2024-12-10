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
    public partial class ReservationTable : Form
    {
        public ReservationTable()
        {
            InitializeComponent();
        }
        private int TableID;
        string connectionString = "Data Source=LAPTOP-DP9CSODP\\SQLEXPRESS;Initial Catalog=Restaurant_1;Integrated Security=True";
        public ReservationTable(int tableID)
        {
            InitializeComponent();
            this.TableID = tableID;
            label1.Text = "Masa " + tableID.ToString(); // Masa numarasını göster
        }

        private bool CheckTableAvailability(int tableID, DateTime reservationDate)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(
                    "SELECT COUNT(*) FROM Reservation " +
                    "WHERE TableID = @TableID AND IsActive = 1 " +
                    "AND ReservationDate <= @ReservationDate " +
                    "AND DATEADD(MINUTE, Duration, ReservationDate) > @ReservationDate", connection);

                sqlCommand.Parameters.AddWithValue("@TableID", tableID);
                sqlCommand.Parameters.AddWithValue("@ReservationDate", reservationDate);

                int count = (int)sqlCommand.ExecuteScalar();
                return count == 0; // Masa uygunsa true döner
            }
        }
        private void AddReservationToDatabase(int tableID, string customerName, DateTime reservationDate, int duration)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(
                    "INSERT INTO Reservation (TableID, CustomerName, ReservationDate, Duration) " +
                    "VALUES (@TableID, @CustomerName, @ReservationDate, @Duration)", connection);

                sqlCommand.Parameters.AddWithValue("@TableID", tableID);
                sqlCommand.Parameters.AddWithValue("@CustomerName", customerName);
                sqlCommand.Parameters.AddWithValue("@ReservationDate", reservationDate);
                sqlCommand.Parameters.AddWithValue("@Duration", duration);

                sqlCommand.ExecuteNonQuery();
            }
        }

        private void ReservationTable_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string customerName = txtCustomerName.Text;
            DateTime reservationDate = dtpReservationTime.Value;
            int duration = 120; // Örneğin, rezervasyon süresi 2 saat
            bool isAvailable = CheckTableAvailability(TableID, reservationDate);

            if (isAvailable)
            {
                AddReservationToDatabase(TableID, customerName, reservationDate, duration);
                MessageBox.Show("Rezervasyon başarıyla eklendi!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Seçilen masa bu tarihte dolu. Lütfen başka bir zaman seçin.");
            }
        }
    }
}
