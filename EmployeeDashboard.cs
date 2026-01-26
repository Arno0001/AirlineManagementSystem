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



namespace AirlineManagementSystem
{
    public partial class EmployeeDashboard : Form
    {
        public EmployeeDashboard()
        {
            InitializeComponent();
        }

       

        private void EmployeeDashboard_Load(object sender, EventArgs e)
        {

        }

        // View Passenger
        private void button1_Click(object sender, EventArgs e)
        {
            ViewPassenger vp = new ViewPassenger();
            vp.Show();
            this.Hide();
        }

        // Booked Ticket
        private void button2_Click(object sender, EventArgs e)
        {
            Ticket t = new Ticket();
            t.Show();
            this.Hide();
        }





        // Logout
        private void button3_Click(object sender, EventArgs e)
        {
            Login lg = new Login();
            lg.Show();
            this.Hide();
        }

        // Exit label
        private void label11_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
