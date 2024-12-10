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
    public partial class Siparis : Form
    {
        public Siparis()
        {
            InitializeComponent();
        }
        string connectionString = "Data Source=LAPTOP-DP9CSODP\\SQLEXPRESS;Initial Catalog=Restaurant_1;Integrated Security=True";
        public int secili_masa { get; set; }



        decimal totalPrice;

        //private void SaveOrderToDatabase(int tableID, decimal totalPrice, bool isPaid)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        // SQL sorgusu: Orders tablosuna veri ekle
        //        string query = "INSERT INTO Orders (TableID, OrderDate, TotalPrice, IsPaid) VALUES (@TableID, @OrderDate, @TotalPrice, @IsPaid)";
        //        using (SqlCommand command = new SqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@TableID", tableID);
        //            command.Parameters.AddWithValue("@OrderDate", DateTime.Now); // Sipariş tarihi
        //            command.Parameters.AddWithValue("@TotalPrice", totalPrice);
        //            command.Parameters.AddWithValue("@IsPaid", isPaid);

        //            // Sorguyu çalıştır
        //            command.ExecuteNonQuery();
        //        }
        //    }

        //    MessageBox.Show("Sipariş başarıyla kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //}
        // SaveOrderToDatabase metodunu bir kez tanımlayın
        private int SaveOrderToDatabase(int tableID, decimal totalPrice, bool isPaid)
        {
            int orderId = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
            INSERT INTO Orders (TableID, OrderDate, TotalPrice, IsPaid) 
            VALUES (@TableID, @OrderDate, @TotalPrice, @IsPaid);
            SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TableID", tableID);
                    command.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                    command.Parameters.AddWithValue("@TotalPrice", totalPrice);
                    command.Parameters.AddWithValue("@IsPaid", isPaid);
                    orderId = Convert.ToInt32(command.ExecuteScalar());
                }
            }

            return orderId;
        }
         
        private void SaveOrderDetailsToDatabase(int orderId, ListView listViewOrders)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (ListViewItem item in listViewOrders.Items)
                {
                    // Ürün bilgilerini ListView'den al
                    string menuItemName = item.Text; // Ürün adı
                    int quantity = int.Parse(item.SubItems[1].Text); // Adet
                    decimal price = decimal.Parse(item.SubItems[2].Text, System.Globalization.NumberStyles.Currency); // Birim fiyat

                    // MenuItemID'yi bulmak için bir sorgu yap
                    string getMenuItemIdQuery = "SELECT MenuItemID FROM MenuItems WHERE Name = @Name";
                    int menuItemId;
                    using (SqlCommand getMenuItemCommand = new SqlCommand(getMenuItemIdQuery, connection))
                    {
                        getMenuItemCommand.Parameters.AddWithValue("@Name", menuItemName);
                        menuItemId = Convert.ToInt32(getMenuItemCommand.ExecuteScalar());
                    }

                    // OrderDetails tablosuna veri ekle
                    string insertDetailsQuery = @"
                INSERT INTO OrderDetails (OrderID, MenuItemID, Quantity, Price) 
                VALUES (@OrderID, @MenuItemID, @Quantity, @Price)";

                    using (SqlCommand insertCommand = new SqlCommand(insertDetailsQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@OrderID", orderId);
                        insertCommand.Parameters.AddWithValue("@MenuItemID", menuItemId);
                        insertCommand.Parameters.AddWithValue("@Quantity", quantity);
                        insertCommand.Parameters.AddWithValue("@Price", price * quantity); // Toplam fiyat
                        insertCommand.ExecuteNonQuery();
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text += "1";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text += "2";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text += "3";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text += "4";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text += "5";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text += "6";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text += "7";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text += "8";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text += "9";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.Text += "0";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {
            LoadAnaYemekItemsToListView("Çorba");
        }

        private void Siparis_Load(object sender, EventArgs e)
        {
            
        }
        private void LoadAnaYemekItemsToListView(string Category)
        {
            
           
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand=new SqlCommand("SELECT Name, Price FROM MenuItems WHERE Category =@Category AND IsAvailable = 1",connection); // Ana yemekleri getir
                sqlCommand.Parameters.AddWithValue("@Category", Category);
                

                SqlDataReader reader = sqlCommand.ExecuteReader();
                listView1.Items.Clear(); // Önceki verileri temizle
                while (reader.Read())
                {
                    
                    string itemName = reader["Name"].ToString();
                    decimal price = (decimal)reader["Price"];

                    // ListView'e ekleyin
                    ListViewItem item = new ListViewItem(itemName);
                    item.SubItems.Add(price.ToString("C2")); // Fiyatı para birimi formatında ekleyin
                    listView1.Items.Add(item);
                }
                reader.Close();
            }
        }
            private void button12_Click(object sender, EventArgs e)
        {
            LoadAnaYemekItemsToListView("Ana Yemek");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            LoadAnaYemekItemsToListView("İçecek");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            LoadAnaYemekItemsToListView("Tatlılar");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            LoadAnaYemekItemsToListView("Salata");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            LoadAnaYemekItemsToListView("FastFood");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            LoadAnaYemekItemsToListView("Makarna");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            LoadAnaYemekItemsToListView("Ara Sıcak");
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Seçili menü öğesini al
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];
                string itemName = selectedItem.Text; // Ürün adı
                decimal price = decimal.Parse(selectedItem.SubItems[1].Text, System.Globalization.NumberStyles.Currency); // Fiyat

                // Adet TextBox'ından değer al
                if (int.TryParse(textBox1.Text, out int quantity) && quantity > 0)
                {
                     totalPrice = price * quantity;

                    // Sipariş ListView'ine ekle
                    ListViewItem orderItem = new ListViewItem(itemName); // Ürün adı
                    orderItem.SubItems.Add(quantity.ToString()); // Adet
                    orderItem.SubItems.Add(price.ToString("C2")); // Birim fiyat
                    orderItem.SubItems.Add(totalPrice.ToString("C2")); // Toplam fiyat
                    listViewOrders.Items.Add(orderItem);
                }
                else
                {
                    MessageBox.Show("Lütfen geçerli bir adet giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (listViewOrders.SelectedItems.Count > 0)
            {
                // Seçili öğeyi kaldır
                listViewOrders.Items.Remove(listViewOrders.SelectedItems[0]);
                MessageBox.Show("Seçili sipariş iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lütfen iptal etmek için bir sipariş seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
       private int tableidgetir;
        private void button21_Click(object sender, EventArgs e)
        {
            if (listViewOrders.Items.Count == 0)
            {
                MessageBox.Show("Sipariş eklenmedi. Lütfen önce sipariş oluşturun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Masa numarasını almak
           // Masa numarasını kullanıcıdan alıyoruz
            int tableidgetir = 0;

            // Masa ID'sini veritabanından almak
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT TableID FROM Tables WHERE TableNumber = @secilimasa", connection);
                sqlCommand.Parameters.AddWithValue("@secilimasa", secili_masa);
                object result = sqlCommand.ExecuteScalar();

                if (result != null)
                {
                    tableidgetir = (int)result; // Veritabanından alınan TableID
                }
                else
                {
                    MessageBox.Show("Seçilen masa mevcut değil!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Toplam fiyatı hesapla
            decimal totalPrice = 0;
            foreach (ListViewItem item in listViewOrders.Items)
            {
                decimal itemTotalPrice = decimal.Parse(item.SubItems[3].Text, System.Globalization.NumberStyles.Currency); // Toplam fiyat sütunu
                totalPrice += itemTotalPrice;
            }

            // Ödeme durumu
            bool isPaid = false; // Ödeme yapılmadı (örnek olarak)

            // Siparişi Orders tablosuna kaydet ve OrderID'yi al
            int orderId = SaveOrderToDatabase(tableidgetir, totalPrice, isPaid);

            // Sipariş detaylarını OrderDetails tablosuna kaydet
            SaveOrderDetailsToDatabase(orderId, listViewOrders);

            // Sipariş ekranını temizle
            listViewOrders.Items.Clear();
            MessageBox.Show("Sipariş başarıyla kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            Tables tables = new Tables();
            tables.Show();
            this.Hide();

        }

    }
}
