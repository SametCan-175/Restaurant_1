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
    }
}
