using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AirlineManagementSystem
{
    public partial class CancellationTbl : Form
    {
        public CancellationTbl()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(
     @"Data Source=(LocalDB)\MSSQLLocalDB;
      Initial Catalog=AirlineDb;
      Integrated Security=True;
      Encrypt=False;
      Connect Timeout=30");


        private bool isBinding = false;
        private bool isFetching = false;

        private void fillTicketId()
        {
            SqlDataReader rdr = null;

            try
            {
                isBinding = true;

                if (Con.State == ConnectionState.Open) Con.Close();
                Con.Open();

                SqlCommand cmd = new SqlCommand("select Tid from TicketTbl", Con);
                rdr = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Columns.Add("Tid", typeof(int));
                dt.Load(rdr);

                TidCb.ValueMember = "Tid";
                TidCb.DisplayMember = "Tid";
                TidCb.DataSource = dt;

                if (dt.Rows.Count > 0) TidCb.SelectedIndex = 0;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            finally
            {
                if (rdr != null) rdr.Close();
                if (Con.State == ConnectionState.Open) Con.Close();
                isBinding = false;
            }
        }

        private void fetchfcode()
        {
            if (isBinding) return;
            if (isFetching) return;

            SqlDataReader dr = null;

            try
            {
                isFetching = true;

                if (TidCb.SelectedValue == null)
                {
                    FcodeTb.Text = "";
                    return;
                }

                if (Con.State == ConnectionState.Open) Con.Close();
                Con.Open();

                int tid = 0;

                if (TidCb.SelectedValue != null && TidCb.SelectedValue.ToString() != "System.Data.DataRowView")
                    int.TryParse(TidCb.SelectedValue.ToString(), out tid);
                else
                    int.TryParse(TidCb.Text, out tid);

                SqlCommand cmd = new SqlCommand("select Fcode from TicketTbl where Tid = @Tid", Con);
                cmd.Parameters.Add("@Tid", SqlDbType.Int).Value = tid;

                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    FcodeTb.Text = dr[0].ToString();
                }
                else
                {
                    FcodeTb.Text = "";
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            finally
            {
                if (dr != null) dr.Close();
                if (Con.State == ConnectionState.Open) Con.Close();
                isFetching = false;
            }
        }

        





        private void populate()
        {
            try
            {
                if (Con.State == ConnectionState.Open) Con.Close();
                Con.Open();

                string query = "select * from CancelTbl";
                SqlDataAdapter sda = new SqlDataAdapter(query, Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                CancelDGV.DataSource = dt;
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







        private void CancellationTbl_Load(object sender, EventArgs e)
        {
            fillTicketId();
            
           populate();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TidCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            fetchfcode();
        }

        private void TidCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fetchfcode();
        }

        private void FcodeTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void TidCb_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            fetchfcode();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CanIdTb.Text = "";
            FcodeTb.Text = "";
        }


        private void deleteTicket()
        {
            if (CanIdTb.Text == "")
            {
                MessageBox.Show("Enter The Cancellation ID");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from CancelTbl where CanId=" + CanIdTb.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cancellation Deleted Successfully");
                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                    if (Con.State == ConnectionState.Open) Con.Close();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (CanIdTb.Text == "" || FcodeTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into CancelTbl values(" + CanIdTb.Text + "," + TidCb.Text + ",'" + FcodeTb.Text + "','" + CancDate.Value.Date + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Ticket Cancelled Successfully");
                    Con.Close();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                    if (Con.State == ConnectionState.Open) Con.Close();
                }
            }
        }

    }
}
