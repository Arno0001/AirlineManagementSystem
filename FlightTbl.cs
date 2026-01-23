using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
              Encrypt=False"
                );
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (FcodeTb.Text == "" ||
                Fsrc.Text == "" ||
                FDest.Text == "" ||
                FDate.Text == "" ||
                SeatNum.Text == "") 
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();

                    string query = "insert into FlightTbl values( '" +
                                   FcodeTb.Text + "', '" +
                                   Fsrc.SelectedItem.ToString() + "', '" +
                                   FDest.SelectedItem.ToString() + "', '" +
                                   FDate.Text + "', '" +

                                   SeatNum.Text + "')";

                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Flight Recorded Successfully");

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                finally
                {
                    if (Con.State == ConnectionState.Open)
                        Con.Close();    

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FcodeTb.Text = "";
            SeatNum.Text = "";
            Fsrc.SelectedIndex = -1;

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
