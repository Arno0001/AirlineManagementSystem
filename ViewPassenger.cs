using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AirlineManagementSystem
{
    public partial class ViewPassenger : Form
    {
        private const string z = "";
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
        private void button4_Click(object sender, EventArgs e) // Update
        {
            if (string.IsNullOrWhiteSpace(PidTb.Text))
            {
                MessageBox.Show("Select a passenger from the table first.");
                return;
            }

            try
            {
                Con.Open();

                string query = @"UPDATE PassengerTbl 
                         SET PassName=@name, Passport=@passport, PassAd=@addr, PassNat=@nat, PassGend=@gend, PassPhone=@phone
                         WHERE PassId=@id";

                using (SqlCommand cmd = new SqlCommand(query, Con))
                {
                    cmd.Parameters.AddWithValue("@id", PidTb.Text);
                    cmd.Parameters.AddWithValue("@name", PnameTb.Text);
                    cmd.Parameters.AddWithValue("@passport", PpassTb.Text);
                    cmd.Parameters.AddWithValue("@addr", PaddTb.Text);
                    cmd.Parameters.AddWithValue("@nat", natcb.Text);
                    cmd.Parameters.AddWithValue("@gend", GendCb.Text);
                    cmd.Parameters.AddWithValue("@phone", PphoneTb.Text);

                    int rows = cmd.ExecuteNonQuery();
                    MessageBox.Show(rows > 0 ? "Updated Successfully!" : "No row updated. Check PassId.");
                }

                Con.Close();
                populate();
            }
            catch (Exception ex)
            {
                if (Con.State == ConnectionState.Open) Con.Close();
                MessageBox.Show("Update failed: " + ex.Message);
            }
        }

        private void label5_Click(object sender, EventArgs e) { }

        private void button3_Click(object sender, EventArgs e)
        {
            AddPassenger addpas = new AddPassenger();
            addpas.Show();
            this.Hide();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (PidTb.Text == "")
            {
                MessageBox.Show("Enter The Passenger to Delete");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "DELETE FROM PassengerTbl WHERE PassId=" + PidTb.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Passenger Deleted Successfully");
                    Con.Close();
                    populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        

                  PnameTb.Text = "";
                    PpassTb.Text = "";
                    PaddTb.Text = "";
                    natcb.SelectedItem = "";
                    GendCb.SelectedItem = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void PassengerDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = PassengerDGV.Rows[e.RowIndex];

            PidTb.Text = row.Cells[0].Value?.ToString();
            PnameTb.Text = row.Cells[1].Value?.ToString();
            PpassTb.Text = row.Cells[2].Value?.ToString();
            PaddTb.Text = row.Cells[3].Value?.ToString();

            natcb.Text = row.Cells[4].Value?.ToString();
            GendCb.Text = row.Cells[5].Value?.ToString();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void ViewPassenger_Load_1(object sender, EventArgs e)
        {

        }
    }
}