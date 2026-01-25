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
    public partial class CancellationTbl : Form
    {
        public CancellationTbl()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(
     @"Data Source=(LocalDB)\MSSQLLocalDB;
      AttachDbFilename=C:\Users\ASUS\Documents\AirlineDb.mdf;
      Integrated Security=True;
      Connect Timeout=30;
      Encrypt=False");

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

                int tid = Convert.ToInt32(TidCb.SelectedValue);

                SqlCommand cmd = new SqlCommand("select Fcode from TicketTbl where Tid = @Tid", Con);
                cmd.Parameters.AddWithValue("@Tid", tid);

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

                CancellationDGV.DataSource = dt;
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
            fetchfcode();
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

        private void TidCb_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
