using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
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
            if (e.RowIndex < 0) return;

            DataGridViewRow row = FlightDGV.Rows[e.RowIndex];

            FcodeTb.Text = row.Cells[0].Value?.ToString();
            SourceCb.SelectedItem = row.Cells[1].Value?.ToString();
            DstCb.SelectedItem = row.Cells[2].Value?.ToString();
            Seatnum.Text = row.Cells[4].Value?.ToString();
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

        private void button3_Click(object sender, EventArgs e)
        {
            FcodeTb.Text = "";
            Seatnum.Text = "";
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
                    if (Con.State == ConnectionState.Open)
                        Con.Close();

                    Con.Open();

                    string query = @"UPDATE FlightTbl
                                     SET Fsrc=@src, FDest=@dest, FDate=@date, FCap=@cap
                                     WHERE Fcode=@code";

                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.Parameters.AddWithValue("@src", SourceCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@dest", DstCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@date", FDate.Value.Date);
                    cmd.Parameters.AddWithValue("@cap", Seatnum.Text);
                    cmd.Parameters.AddWithValue("@code", FcodeTb.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Flight Updated Successfully");
                    populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Update failed: " + ex.Message);
                }
                finally
                {
                    if (Con.State == ConnectionState.Open)
                        Con.Close();
                }
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
                    if (Con.State == ConnectionState.Open)
                        Con.Close();

                    Con.Open();

                    string query = "DELETE FROM FlightTbl WHERE Fcode=@code";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.Parameters.AddWithValue("@code", FcodeTb.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Flight Deleted Successfully");
                    populate();
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

            FcodeTb.Text = "";
            Seatnum.Text = "";
            SourceCb.SelectedItem = null;
            DstCb.SelectedItem = null;
        }
    }
}
