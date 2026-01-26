using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AirlineManagementSystem
{
    public partial class Login : Form
    {
        private readonly string _cs =
            @"Data Source=(LocalDB)\MSSQLLocalDB;
              Initial Catalog=AirlineDb;
              Integrated Security=True;
              Encrypt=False;
              Connect Timeout=30";

        public Login()
        {
            InitializeComponent();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtPassword.Multiline = false;
            txtPassword.UseSystemPasswordChar = true;
            txtUid.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUid.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Missing Information");
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            try
            {
                string uid = txtUid.Text.Trim();
                string pass = txtPassword.Text.Trim();

                string role = TryGetRole(uid, pass);

                if (string.IsNullOrEmpty(role))
                {
                    MessageBox.Show("Wrong User ID or Password");
                    return;
                }

                this.Hide();

                if (string.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase))
                {
                    new AdminDashboard().Show();
                }
                else
                {
                    new EmployeeDashboard().Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private string TryGetRole(string uid, string pass)
        {
            var candidates = new[]
            {
                new { Id = "UserId",     Pw = "UserPassword", Role = "UserRole" },
                new { Id = "Uid",        Pw = "Password",     Role = "Role"     },
                new { Id = "Username",   Pw = "Password",     Role = "Role"     },
                new { Id = "UserName",   Pw = "UserPassword", Role = "UserRole" },
            };

            using (SqlConnection con = new SqlConnection(_cs))
            {
                con.Open();

                foreach (var c in candidates)
                {
                    string sql = $"SELECT {c.Role} FROM UserTbl WHERE {c.Id}=@uid AND {c.Pw}=@pass";

                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {
                            cmd.Parameters.AddWithValue("@uid", uid);
                            cmd.Parameters.AddWithValue("@pass", pass);

                            object result = cmd.ExecuteScalar();
                            if (result != null && result != DBNull.Value)
                                return result.ToString();
                        }
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Message.IndexOf("Invalid column name", StringComparison.OrdinalIgnoreCase) >= 0 ||
                            ex.Message.IndexOf("Invalid object name", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            continue;
                        }

                        throw;
                    }
                }
            }

            throw new Exception(
                "Your UserTbl exists but column names don't match.\n" +
                "Open Server Explorer → AirlineDb → Tables → UserTbl → right click → Design\n" +
                "and check exact column names.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtUid.Text = "";
            txtPassword.Text = "";
            txtUid.Focus();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
        }

        private void linkResister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Register page is not added in this project.");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }
    }
}

