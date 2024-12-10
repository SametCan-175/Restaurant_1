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
    public partial class CustomInputReservation : Form
    {
        public CustomInputReservation()
        {
            InitializeComponent();
        }
        public int TableNumber { get; set; }  // Seçilen masa numarasını alacak
        string connectionString = "Data Source=LAPTOP-DP9CSODP\\SQLEXPRESS;Initial Catalog=Restaurant_1;Integrated Security=True";

        public CustomInputReservation(int tableNumber)
        {
            InitializeComponent();
            this.TableNumber = tableNumber;
            lblMasaNumarasi.Text = "Masa " + tableNumber.ToString();  // Masa numarasını göster
        }

        private bool CheckReservation(string customerName, DateTime reservationDate)
        {
            // Rezervasyonu veritabanından kontrol et
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT CustomerName, ReservationDate FROM Reservation WHERE TableID = @TableNumber AND ReservationDate = @ReservationDate", connection);
                sqlCommand.Parameters.AddWithValue("@TableNumber", TableNumber);
                sqlCommand.Parameters.AddWithValue("@ReservationDate", reservationDate);

                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["CustomerName"].ToString() == customerName)
                    {
                        return true;  // İsim ve tarih eşleşiyorsa doğru döndürüyoruz
                    }
                }
            }
            return false;  // Eşleşme yoksa yanlış döndürüyoruz
        }

        private void OpenOrderForm()
        {
            // Siparis formunu aç
            Siparis frmSiparis = new Siparis
            {
                secili_masa = TableNumber,  // Seçilen masa numarasını Siparis formuna gönderiyoruz
                MusteriAdi = txtCustomerName.Text  // Müşteri adını Siparis formuna gönderiyoruz
            };
            frmSiparis.ShowDialog();
        }
        private void CustomInputReservation_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string customerName = txtCustomerName.Text;  // TextBox'tan müşteri adı al
            DateTime selectedDate = dateTimePicker.Value;  // DateTimePicker'tan rezervasyon tarihini al

            // Rezervasyonu kontrol et
            if (CheckReservation(customerName, selectedDate))
            {
                // Eğer isim ve tarih eşleşiyorsa, Siparis formunu aç
                OpenOrderForm();
            }
            else
            {
                MessageBox.Show("Bu tarihte ve isimde bir rezervasyon bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
