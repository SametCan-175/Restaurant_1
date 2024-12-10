using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_1
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }
        public string user_name { get; set; }
        public string user_role {  get; set; }
        private void Menu_Load(object sender, EventArgs e)
        {
          
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Tables tables = new Tables
            {
                user_role = this.user_role
            };
            tables.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            reservation Reservation= new reservation();
            Reservation.Show();
            this.Hide();
        }
    }
}
