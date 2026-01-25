using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AirlineManagementSystem
{
    public partial class FlightTbl : Form
    {
        public FlightTbl()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(
     @"Data Source=(LocalDB)\MSSQLLocalDB;
      AttachDbFilename=C:\Users\ASUS\Documents\AirlineDb.mdf;
      Integrated Security=True;
      Connect Timeout=30;
      Encrypt=False");


        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (FcodeTb.Text == "" ||
                Fsrc.SelectedIndex == -1 ||
                FDest.SelectedIndex == -1 ||
                SeatNum.Text == "" ||
                FDate.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    if (Con.State == ConnectionState.Open) Con.Close();
                    Con.Open();

                    string query = "insert into FlightTbl values('" +
                                   FcodeTb.Text + "','" +
                                   Fsrc.SelectedItem.ToString() + "','" +
                                   FDest.SelectedItem.ToString() + "','" +
                                   FDate.Text + "','" +
                                   SeatNum.Text + "')";

                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Flight Recorded Successfully");
                    Con.Close();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                    if (Con.State == ConnectionState.Open) Con.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FcodeTb.Text = "";
            SeatNum.Text = "";
            Fsrc.SelectedIndex = -1;
            FDest.SelectedIndex = -1;
            FDate.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ViewFlights viewFlights = new ViewFlights();
            viewFlights.Show();
            this.Hide();
        }

        private void FlightTbl_Load(object sender, EventArgs e)
        {
        }
    }
}
