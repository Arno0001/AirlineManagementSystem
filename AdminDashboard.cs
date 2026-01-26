using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace AirlineManagementSystem
{
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }


        private void AdminDashboard_Load(object sender, EventArgs e)
        {

        }

        // Employee Record button
        private void btnEmployeeRecord_Click(object sender, EventArgs e)
        {
            EmployeeDashboard emp = new EmployeeDashboard();
            emp.Show();
            this.Hide();
        }

        // Flight Details button
        private void btnFlightDetails_Click(object sender, EventArgs e)
        {
            FlightTbl flight = new FlightTbl();
            flight.Show();
            this.Hide();
        }

        // Flight Schedule button
        private void btnFlightSchedule_Click(object sender, EventArgs e)
        {
            ViewFlights schedule = new ViewFlights();
            schedule.Show();
            this.Hide();
        }

        // Logout button
        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }

        // Close 
        

        private void label11_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
