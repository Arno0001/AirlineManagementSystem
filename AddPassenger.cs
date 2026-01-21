using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AirlineManagementSystem
{
    public partial class AddPassenger : Form
    {
        public AddPassenger()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(
            @"Data Source=(LocalDB)\MSSQLLocalDB;
              AttachDbFilename=C:\Users\ASUS\Documents\AirlineDb.mdf;
              Integrated Security=True;
              Connect Timeout=30;
              Encrypt=True"
        );

        private void AddPassenger_Load(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void label9_Click(object sender, EventArgs e)
        {
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (PassId.Text == "" ||
                PassAd.Text == "" ||
                PassName.Text == "" ||
                PassportTb.Text == "" ||
                PhonrTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();

                    string query = "insert into PassengerTbl values(" +
                                   PassId.Text + ", '" +
                                   PassName.Text + "', '" +
                                   PassportTb.Text + "', '" +
                                   PassAd.Text + "', '" +
                                   NationalityCb.SelectedItem.ToString() + "', '" +
                                   GenderCb.SelectedItem.ToString() + "', '" +
                                   PhonrTb.Text + "')";

                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Passenger Recorded Successfully");
                    Con.Close();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
    }
}
