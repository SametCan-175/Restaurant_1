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
      
        string connectionString = "Data Source=LAPTOP-DP9CSODP\\SQLEXPRESS;Initial Catalog=Restaurant_1;Integrated Security=True";
        //public ReservationTable(int tableID)
        //{
        //    InitializeComponent();
        //    this.TableID = tableID;
        //    label1.Text = "Masa " + tableID.ToString(); // Masa numarasını göster
        //}

        //private bool CheckTableAvailability(int tableID, DateTime reservationDate)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        SqlCommand sqlCommand = new SqlCommand(
        //            "SELECT COUNT(*) FROM Reservation " +
        //            "WHERE TableID = @TableID AND IsActive = 1 " +
        //            "AND ReservationDate <= @ReservationDate " +
        //            "AND DATEADD(MINUTE, Duration, ReservationDate) > @ReservationDate", connection);

        //        sqlCommand.Parameters.AddWithValue("@TableID", tableID);
        //        sqlCommand.Parameters.AddWithValue("@ReservationDate", reservationDate);

        //        int count = (int)sqlCommand.ExecuteScalar();
        //        return count == 0; // Masa uygunsa true döner
        //    }
        //}
        //private void AddReservationToDatabase(int tableID, string customerName, DateTime reservationDate, int duration)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        SqlCommand sqlCommand = new SqlCommand(
        //            "INSERT INTO Reservation (TableID, CustomerName, ReservationDate, Duration) " +
        //            "VALUES (@TableID, @CustomerName, @ReservationDate, @Duration)", connection);

        //        sqlCommand.Parameters.AddWithValue("@TableID", tableID);
        //        sqlCommand.Parameters.AddWithValue("@CustomerName", customerName);
        //        sqlCommand.Parameters.AddWithValue("@ReservationDate", reservationDate);
        //        sqlCommand.Parameters.AddWithValue("@Duration", duration);

        //        sqlCommand.ExecuteNonQuery();
        //    }
        //}

        public int TableID; // Masa ID'si
        private string CustomerName; // Müşteri adı
        private DateTime ReservationDate; // Rezervasyon tarihi

        // Constructor - Masa ID'sini alıyoruz
        public ReservationTable(int tableID)
        {
            InitializeComponent();
            this.TableID = tableID;

            // Masa ID'sini label üzerinde gösterelim
            label1.Text = "Masa " + TableID.ToString();
        }
        private bool IsTableReserved(int tableID, DateTime reservationDate)
        {
            bool isReserved = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(
                    "SELECT COUNT(*) FROM Reservation " +
                    "WHERE TableID = @TableID AND ReservationDate = @ReservationDate", connection);

                sqlCommand.Parameters.AddWithValue("@TableID", tableID);
                sqlCommand.Parameters.AddWithValue("@ReservationDate", reservationDate);

                int count = (int)sqlCommand.ExecuteScalar();
                if (count > 0)
                {
                    isReserved = true; // Eğer bu tarihte rezervasyon varsa masa dolu
                }
            }

            return isReserved;
        }

        private void SaveReservationToDatabase(int tableID, string customerName, DateTime reservationDate)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(
                    "INSERT INTO Reservation (TableID, CustomerName, ReservationDate) " +
                    "VALUES (@TableID, @CustomerName, @ReservationDate)", connection);

                sqlCommand.Parameters.AddWithValue("@TableID", tableID);
                sqlCommand.Parameters.AddWithValue("@CustomerName", customerName);
                sqlCommand.Parameters.AddWithValue("@ReservationDate", reservationDate);

                int rowsAffected = sqlCommand.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Rezervasyon başarıyla kaydedildi!");
                    this.Close(); // Rezervasyon kaydedildikten sonra formu kapatıyoruz
                }
                else
                {
                    MessageBox.Show("Bir hata oluştu. Lütfen tekrar deneyin.");
                }
            }
        }
    
    private void ReservationTable_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Müşteri adı ve tarih kontrolü
            string customerName = txtCustomerName.Text; // Müşteri adı
            DateTime reservationDate = dtpReservationDate.Value; // Rezervasyon tarihi

            // Müşteri adı boş mu kontrol et
            if (string.IsNullOrEmpty(customerName))
            {
                MessageBox.Show("Lütfen müşteri adını giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Geçerli tarih kontrolü
            //if (reservationDate <=DateTime.Now)
            //{
            //    MessageBox.Show("Geçmiş bir tarihe rezervasyon yapılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            // Masa dolu mu kontrol et
            if (IsTableReserved(TableID, reservationDate))
            {
                MessageBox.Show("Bu masa seçilen tarihte dolu. Lütfen başka bir tarih seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Rezervasyon kaydet
            SaveReservationToDatabase(TableID, customerName, reservationDate);
        }
    }
}
