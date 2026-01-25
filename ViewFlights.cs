using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AirlineManagementSystem
{
    public partial class ViewFlights : Form
    {
        SqlConnection Con = new SqlConnection(
            @"Data Source=(LocalDB)\MSSQLLocalDB;
              AttachDbFilename=C:\Users\ASUS\Documents\AirlineDb.mdf;
              Integrated Security=True;
              Connect Timeout=30;
              Encrypt=False");

        public ViewFlights()
        {
            InitializeComponent();
        }

        private void populate()
        {
            try
            {
                if (Con.State == ConnectionState.Open) Con.Close();
                Con.Open();

                string query = "select * from FlightTbl";
                SqlDataAdapter sda = new SqlDataAdapter(query, Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                FlightDGV.DataSource = dt;
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
            if (e.RowIndex >= 0)
            {
                FcodeTb.Text = FlightDGV.Rows[e.RowIndex].Cells[0].Value.ToString();
                SourceCb.SelectedItem = FlightDGV.Rows[e.RowIndex].Cells[1].Value.ToString();
                DstCb.SelectedItem = FlightDGV.Rows[e.RowIndex].Cells[2].Value.ToString();
                Seatnum.Text = FlightDGV.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (FcodeTb.Text == "" || SourceCb.SelectedItem == null || DstCb.SelectedItem == null || Seatnum.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    if (Con.State == ConnectionState.Open) Con.Close();
                    Con.Open();

                    string query = "update FlightTbl set Fsrc='" + SourceCb.SelectedItem.ToString() +
                                   "', FDest='" + DstCb.SelectedItem.ToString() +
                                   "', FDate='" + FDate.Value.Date.ToString("yyyy-MM-dd") +
                                   "', FCap=" + Seatnum.Text +
                                   " where Fcode='" + FcodeTb.Text + "'";

                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Flight Updated Successfully");
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                finally
                {
                    if (Con.State == ConnectionState.Open) Con.Close();
                }

                populate();
            }

            FcodeTb.Text = "";
            Seatnum.Text = "";
            SourceCb.SelectedItem = null;
            DstCb.SelectedItem = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (FcodeTb.Text == "")
            {
                MessageBox.Show("Enter The Flight to Delete");
            }
            else
            {
                try
                {
                    if (Con.State == ConnectionState.Open) Con.Close();
                    Con.Open();

                    string query = "delete from FlightTbl where Fcode='" + FcodeTb.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Flight Deleted Successfully");
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                finally
                {
                    if (Con.State == ConnectionState.Open) Con.Close();
                }

                populate();
            }

            FcodeTb.Text = "";
            Seatnum.Text = "";
            SourceCb.SelectedItem = null;
            DstCb.SelectedItem = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FcodeTb.Text = "";
            Seatnum.Text = "";
            SourceCb.SelectedItem = null;
            DstCb.SelectedItem = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FlightTbl addfl = new FlightTbl();
            addfl.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
