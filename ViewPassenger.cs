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
              Encrypt=False");

        public ViewPassenger()
        {
            InitializeComponent();
        }

        private DataTable GetTable(string query)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter sda = new SqlDataAdapter(query, Con))
            {
                sda.Fill(dt);
            }
            return dt;
        }

        private int ExecuteDml(string query, params SqlParameter[] parameters)
        {
            int affected = 0;
            try
            {
                Con.Open();
                using (SqlCommand sql = new SqlCommand(query, Con))
                {
                    if (parameters != null && parameters.Length > 0)
                        sql.Parameters.AddRange(parameters);

                    affected = sql.ExecuteNonQuery();
                }
            }
            finally
            {
                if (Con.State == ConnectionState.Open) Con.Close();
            }
            return affected;
        }

        private void populate()
        {
            PassengerDGV.AutoGenerateColumns = true;
            PassengerDGV.DataSource = GetTable("select * from PassengerTbl");
        }

        private void ResetFields()
        {
            PidTb.Text = "";
            PnameTb.Text = "";
            PpassTb.Text = "";
            PaddTb.Text = "";
            PphoneTb.Text = "";
            natcb.SelectedIndex = -1;
            GendCb.SelectedIndex = -1;
        }

        private void ViewPassenger_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void ViewPassenger_Load_1(object sender, EventArgs e)
        {
            populate();
        }

        private void PassengerDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (PassengerDGV.SelectedRows.Count == 0) return;

            DataGridViewRow r = PassengerDGV.SelectedRows[0];

            PidTb.Text = Convert.ToString(r.Cells["PassId"].Value);
            PnameTb.Text = Convert.ToString(r.Cells["PassName"].Value);
            PpassTb.Text = Convert.ToString(r.Cells["Passport"].Value);
            PaddTb.Text = Convert.ToString(r.Cells["PassAd"].Value);
            natcb.Text = Convert.ToString(r.Cells["PassNat"].Value);
            GendCb.Text = Convert.ToString(r.Cells["PassGend"].Value);
            PphoneTb.Text = Convert.ToString(r.Cells["PassPhone"].Value);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (PidTb.Text == "" ||
                PnameTb.Text == "" ||
                PpassTb.Text == "" ||
                PaddTb.Text == "" ||
                PphoneTb.Text == "" ||
                natcb.SelectedIndex == -1 ||
                GendCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
                return;
            }

            int pid;
            if (!int.TryParse(PidTb.Text, out pid))
            {
                MessageBox.Show("Passenger Id must be a number");
                return;
            }

            string q =
                "update PassengerTbl set " +
                "PassName=@n, Passport=@pp, PassAd=@ad, PassNat=@nat, PassGend=@g, PassPhone=@ph " +
                "where PassId=@id";

            ExecuteDml(q,
                new SqlParameter("@n", PnameTb.Text),
                new SqlParameter("@pp", PpassTb.Text),
                new SqlParameter("@ad", PaddTb.Text),
                new SqlParameter("@nat", natcb.SelectedItem.ToString()),
                new SqlParameter("@g", GendCb.SelectedItem.ToString()),
                new SqlParameter("@ph", PphoneTb.Text),
                new SqlParameter("@id", pid)
            );

            MessageBox.Show("Passenger Updated Successfully");
            populate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (PidTb.Text == "")
            {
                MessageBox.Show("Select a passenger from the table first");
                return;
            }

            try
            {
                Con.Open();
                string query = "delete from PassengerTbl where PassId=" + PidTb.Text;
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                Con.Close();

                populate();

                PidTb.Text = "";
                PnameTb.Text = "";
                PpassTb.Text = "";
                PaddTb.Text = "";
                PphoneTb.Text = "";
                natcb.SelectedIndex = -1;
                GendCb.SelectedIndex = -1;

                MessageBox.Show("Passenger Deleted Successfully");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Con.Close();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            ResetFields();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddPassenger addpas = new AddPassenger();
            addpas.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void textBox4_TextChanged(object sender, EventArgs e) { }

        private void PassengerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}
