using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AirlineManagementSystem
{
    public partial class ViewFlights : Form
    {
        private readonly SqlConnection Con = new SqlConnection(
            @"Data Source=(LocalDB)\MSSQLLocalDB;
              AttachDbFilename=C:\Users\ASUS\Documents\AirlineDb.mdf;
              Integrated Security=True;
              Connect Timeout=30;
              Encrypt=False"
        );

        public ViewFlights()
        {
            InitializeComponent();
        }

        private void populate()
        {
            try
            {
                if (Con.State == ConnectionState.Open)
                    Con.Close();

                Con.Open();

                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM FlightTbl", Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                FlightDGV.AutoGenerateColumns = true;
                FlightDGV.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (Con.State == ConnectionState.Open)
                    Con.Close();
            }
        }

        private void ViewFlights_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void ViewFlights_Load_1(object sender, EventArgs e)
        {
            populate();
        }

        private void FlightDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FlightTbl Addfl = new FlightTbl();
            Addfl.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void label7_Click(object sender, EventArgs e)
        {
        }
    }
}
