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
      Initial Catalog=AirlineDb;
      Integrated Security=True;
      Encrypt=False;
      Connect Timeout=30");



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
                PhonrTb.Text == "" ||
                NationalityCb.SelectedIndex == -1 ||
                GenderCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    if (Con.State == ConnectionState.Open) Con.Close();
                    Con.Open();

                    string query = "insert into PassengerTbl values(" +
                                   PassId.Text + ",'" +
                                   PassName.Text + "','" +
                                   PassportTb.Text + "','" +
                                   PassAd.Text + "','" +
                                   NationalityCb.SelectedItem.ToString() + "','" +
                                   GenderCb.SelectedItem.ToString() + "','" +
                                   PhonrTb.Text + "')";

                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Passenger Recorded Successfully");
                    Con.Close();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                    if (Con.State == ConnectionState.Open) Con.Close();
                }
            }
        }

        private void PassId_TextChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PassId.Text = "";
            PassName.Text = "";
            PassportTb.Text = "";
            PassAd.Text = "";
            PhonrTb.Text = "";
            NationalityCb.SelectedIndex = -1;
            GenderCb.SelectedIndex = -1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ViewPassenger viewpass = new ViewPassenger();
            viewpass.Show();
            this.Hide();
        }

        private void PassportTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
