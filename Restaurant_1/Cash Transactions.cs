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
    public partial class Cash_Transactions : Form
    {
        public Cash_Transactions()
        {
            InitializeComponent();
        }
        string connectionString = "Data Source=LAPTOP-DP9CSODP\\SQLEXPRESS;Initial Catalog=Restaurant_1;Integrated Security=True";
        private void MarkOrderAsPaid(int orderId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Orders SET IsPaid = 1 WHERE OrderID = @OrderID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderID", orderId);
                    command.ExecuteNonQuery();
                }
            }
        }
        private void UpdateTableStatus(int tableId, bool isReserved)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Tables SET IsReserved = @IsReserved WHERE TableID = @TableID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TableID", tableId);
                    command.Parameters.AddWithValue("@IsReserved", isReserved); // Boş olacak, bu yüzden false
                    command.ExecuteNonQuery();
                }
            }
        }
        private void LoadOrders()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT o.OrderID, o.TableID, o.OrderDate, o.TotalPrice, o.IsPaid, t.TableNumber " +
                               "FROM Orders o " +
                               "INNER JOIN Tables t ON o.TableID = t.TableID " +
                               "WHERE o.IsPaid = 0"; // Ödenmemiş siparişleri getir

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // DataGridView'e veriyi bind et
                dataGridView1.DataSource = dt;
            }
        }

        private void Cash_Transactions_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT o.OrderID, o.TableID, o.OrderDate, o.TotalPrice, o.IsPaid, t.TableNumber " +
                               "FROM Orders o " +
                               "INNER JOIN Tables t ON o.TableID = t.TableID " +
                               "WHERE o.IsPaid = 0"; // Ödenmemiş siparişleri getir

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // DataGridView'e veriyi bind et
                dataGridView1.DataSource = dt;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Seçili satırdan OrderID, TableID, TotalPrice gibi bilgileri alıyoruz
                int orderId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["OrderID"].Value);
                int tableId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["TableID"].Value);
                decimal totalPrice = Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells["TotalPrice"].Value);

                // Ödeme işlemi
                MarkOrderAsPaid(orderId);
                UpdateTableStatus(tableId, false);

                // Mesaj ve yeniden yükleme
                MessageBox.Show($"Ödeme alındı. Masa {tableId} boş olarak işaretlendi.", "Ödeme Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Siparişleri tekrar yükle
                LoadOrders();
            }
            else
            {
                MessageBox.Show("Lütfen ödeme yapılacak bir sipariş seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
