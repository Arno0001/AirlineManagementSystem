using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AirlineManagementSystem
{
    public partial class ViewPassenger : Form
    {
        private readonly SqlConnection Con = new SqlConnection(
            @"Data Source=(LocalDB)\MSSQLLocalDB;
              AttachDbFilename=C:\Users\ASUS\Documents\AirlineDb.mdf;
              Integrated Security=True;
              Connect Timeout=30;
              Encrypt=False"
        );

        public ViewPassenger()
        {
            InitializeComponent();
            this.Load += ViewPassenger_Load;   // IMPORTANT: ensures Load event runs
        }

        private void populate()
        {
            try
            {
                if (Con.State == ConnectionState.Open)
                    Con.Close();

                Con.Open();

                string query = "SELECT * FROM PassengerTbl";
                SqlDataAdapter sda = new SqlDataAdapter(query, Con);

                DataTable dt = new DataTable();
                sda.Fill(dt);

                PassengerDGV.DataSource = null;
                PassengerDGV.AutoGenerateColumns = true;
                PassengerDGV.DataSource = dt;
                PassengerDGV.Refresh();

                Con.Close();
            }
            catch (Exception ex)
            {
                if (Con.State == ConnectionState.Open)
                    Con.Close();

                MessageBox.Show("Error loading passengers: " + ex.Message);
            }
        }

        private void ViewPassenger_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void label3_Click(object sender, EventArgs e) { }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void button4_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }

        private void button3_Click(object sender, EventArgs e)
        {
            AddPassenger addpas = new AddPassenger();
            addpas.Show();
            this.Hide();
        }
    }
}
