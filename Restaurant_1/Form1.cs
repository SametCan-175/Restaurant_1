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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void ShowPlaceholderUserName()
        {
            // Eğer TextBox boşsa placeholder göster
            if (string.IsNullOrEmpty(txtName.Text))
            {
                txtName.Text = "Kullanıcı Adı Giriniz";
                txtName.ForeColor = Color.Gray; // Placeholder rengi

            }
        }
        private void HidePlaceholderUserName()
        {
            // Eğer placeholder görünüyorsa temizle
            if (txtName.Text == "Kullanıcı Adı Giriniz")
            {
                txtName.Text = "";
                txtName.ForeColor = Color.Black; // Yazı rengini normal yap

            }
        }
        private void ShowPlaceholderPass()
        {
            // Eğer TextBox boşsa placeholder göster
            if (string.IsNullOrEmpty(txtPass.Text))
            {
                txtPass.Text = "Şifre giriniz";
                txtPass.ForeColor = Color.Gray; // Placeholder rengi
                txtPass.UseSystemPasswordChar = false; // Maskeyi kapat
            }
        }
        private void HidePlaceholderPass()
        {
            // Eğer placeholder görünüyorsa temizle
            if (txtPass.Text == "Şifre giriniz")
            {
                txtPass.Text = "";
                txtPass.ForeColor = Color.Black; // Yazı rengini normal yap
                txtPass.UseSystemPasswordChar = true; // Maskeyi aç
            }
        }
        string connectionString = "Data Source=LAPTOP-DP9CSODP\\SQLEXPRESS;Initial Catalog=Restaurant_1;Integrated Security=True";
        private void Form1_Load(object sender, EventArgs e)
        {
            txtName.Enter += txtName_Enter;
            txtName.Leave += txtName_Leave;
            txtPass.Enter += txtPass_Enter;
            txtPass.Leave += txtPass_Leave;
            ShowPlaceholderUserName();
            ShowPlaceholderPass();
        }

        private void txtName_Enter(object sender, EventArgs e)
        {
            HidePlaceholderUserName();
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            ShowPlaceholderUserName();
        }

        private void txtPass_Enter(object sender, EventArgs e)
        {
            HidePlaceholderPass();
        }

        private void txtPass_Leave(object sender, EventArgs e)
        {
            ShowPlaceholderPass();
        }

        private void picExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Uygulamadan Çıkıyorsun. Emin Misin?", "Uyarı!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void picLogin_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Kullanıcı giriş sorgusu
                    SqlCommand giris = new SqlCommand("SELECT * FROM Employees WHERE FirstName = @UserName AND Password = @UserPassword", connection);
                    giris.Parameters.AddWithValue("@UserName", txtName.Text.ToLower());
                    giris.Parameters.AddWithValue("@UserPassword", txtPass.Text);
                    SqlDataReader dr = giris.ExecuteReader();
                    if (dr.Read())
                    {
                        MessageBox.Show("Kullanıcı Girişi Doğru. Hoş Geldiniz " + txtName.Text);
                        dr.Close();
                        SqlCommand sqlCommand = new SqlCommand("select Role from Employees where FirstName=@userName", connection);
                        sqlCommand.Parameters.AddWithValue("@UserName", txtName.Text);
                        object result = sqlCommand.ExecuteScalar();
                        if(result!=null)
                        {
                            string role=(string) result;
                            if(role=="Admin")
                            {
                                Menu menu = new Menu
                                {
                                    user_name = txtName.Text,
                                    user_role = role
                                };
                                menu.Show();
                                this.Hide();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Yanlış Girdiniz.");
                       
                      
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hatanız: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                    
                }
            }
        }
    }
}
