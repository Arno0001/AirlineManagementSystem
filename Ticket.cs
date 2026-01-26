using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AirlineManagementSystem
{
    public partial class Ticket : Form
    {
        public Ticket()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(
     @"Data Source=(LocalDB)\MSSQLLocalDB;
      Initial Catalog=AirlineDb;
      Integrated Security=True;
      Encrypt=False;
      Connect Timeout=30");





        private void populate()
        {
            try
            {
                if (Con.State == ConnectionState.Open) Con.Close();
                Con.Open();

                string query = "select * from TicketTbl";
                SqlDataAdapter sda = new SqlDataAdapter(query, Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                TicketDGV.DataSource = dt;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            finally
            {
                if (Con.State == ConnectionState.Open) Con.Close();
            }
        }


        private void fillPassenger()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("select PassId from PassengerTbl", Con);
                SqlDataReader rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("PassId", typeof(int));
                dt.Load(rdr);
                PIdCb.ValueMember = "PassId";
                PIdCb.DisplayMember = "PassId";
                PIdCb.DataSource = dt;
                Con.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                if (Con.State == ConnectionState.Open) Con.Close();
            }
        }

        private void fetchpassenger()
        {
            if (PIdCb.SelectedValue == null) return;

            try
            {
                Con.Open();
                string query = "select PassName, Passport, PassNat from PassengerTbl where PassId=" + PIdCb.SelectedValue.ToString() + "";
                SqlCommand cmd = new SqlCommand(query, Con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    PNameTb.Text = dr[0].ToString();
                    PPassTb.Text = dr[1].ToString();
                    PNatTb.Text = dr[2].ToString();
                }
                else
                {
                    PNameTb.Text = "";
                    PPassTb.Text = "";
                    PNatTb.Text = "";
                }
                Con.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                if (Con.State == ConnectionState.Open) Con.Close();
            }
        }

        private void fillFlightCode()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("select FCode from FlightTbl", Con);
                SqlDataReader rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("FCode", typeof(string));
                dt.Load(rdr);
                FCodeCb.ValueMember = "FCode";
                FCodeCb.DisplayMember = "FCode";
                FCodeCb.DataSource = dt;
                Con.Close();
                populate();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                if (Con.State == ConnectionState.Open) Con.Close();
            }
        }

        private void Ticket_Load(object sender, EventArgs e)
        {
            fillPassenger();
            fillFlightCode();
            fetchpassenger();
            populate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Tid.Text == "" || PIdCb.SelectedValue == null || PNameTb.Text == "" || PPassTb.Text == "" || PNatTb.Text == "" || PAmtTb.Text == "" || FCodeCb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into TicketTbl values(" + Tid.Text + ",'" + FCodeCb.Text + "'," + PIdCb.SelectedValue.ToString() + ",'" + PNameTb.Text + "','" + PPassTb.Text + "','" + PNatTb.Text + "'," + PAmtTb.Text + ")";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Ticket Booked Successfully");
                    Con.Close();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                    if (Con.State == ConnectionState.Open) Con.Close();
                }
            }
        }

        private void PIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fetchpassenger();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
        }

        private void label12_Click(object sender, EventArgs e)
        {
        }

        private void PNameTb_TextChanged(object sender, EventArgs e)
        {
        }

        private void PIdCb_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Tid.Text = "";
            PAmtTb.Text = "";
            PNameTb.Text = "";
            PPassTb.Text = "";
            PNatTb.Text = "";
            PNatTb.Text = "";
            if (PIdCb.Items.Count > 0) PIdCb.SelectedIndex = 0;
            if (FCodeCb.Items.Count > 0) FCodeCb.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
              PNameTb.Text = "";
             PPassTb.Text = "";
               PNatTb.Text = "";
            PAmtTb.Text = "";
            Tid.Text = "";
        }
    }
}
