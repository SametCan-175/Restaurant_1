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
        public int SelectedTableID { get; private set; }
        public reservation()
        {
            InitializeComponent();

            // PictureBox'lara tıklama olaylarını ekliyoruz.
            pic1.Click += (sender, e) => OpenReservationTable(11);
            pic2.Click += (sender, e) => OpenReservationTable(12);
            pic3.Click += (sender, e) => OpenReservationTable(13);
            pic4.Click += (sender, e) => OpenReservationTable(14);
            pic5.Click += (sender, e) => OpenReservationTable(15);
            pic6.Click += (sender, e) => OpenReservationTable(16);
            pic7.Click += (sender, e) => OpenReservationTable(17);
            pic8.Click += (sender, e) => OpenReservationTable(18);
            pic9.Click += (sender, e) => OpenReservationTable(19);
            pic10.Click += (sender, e) => OpenReservationTable(20);
        }


        private void OpenReservationTable(int tableID)
        {
            // Masa bilgilerini almak
           

            // ReservationTable formunu açıyoruz ve masa bilgilerini gönderiyoruz
            ReservationTable reservationTableForm = new ReservationTable(tableID);
            reservationTableForm.ShowDialog();
        }
        // Seçilen masayı kaydetme
       

        private void pic1_Click(object sender, EventArgs e)
        {
            //OpenReservationForm(1);
        }

        private void pic2_Click(object sender, EventArgs e)
        {
            //OpenReservationForm(2);
        }

        private void pic3_Click(object sender, EventArgs e)
        {
            //OpenReservationForm(3);
        }

        private void pic4_Click(object sender, EventArgs e)
        {
            //OpenReservationForm(4);
        }

        private void pic5_Click(object sender, EventArgs e)
        {
            //OpenReservationForm(5);
        }

        private void pic6_Click(object sender, EventArgs e)
        {
            //OpenReservationForm(6);
        }

        private void pic7_Click(object sender, EventArgs e)
        {
            //OpenReservationForm(7);
        }

        private void pic8_Click(object sender, EventArgs e)
        {
            //OpenReservationForm(8);
        }

        private void pic9_Click(object sender, EventArgs e)
        {
            //OpenReservationForm(9);
        }

        private void pic10_Click(object sender, EventArgs e)
        {
            //OpenReservationForm(10);
        }

        private void reservation_Load(object sender, EventArgs e)
        {

        }
    }
}
