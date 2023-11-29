using Restaurant_Project.UC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_Project
{
    public partial class FormKasir : Form
    {
        machine machine = new machine();
        public FormKasir()
        {
            InitializeComponent();
        }
        private void showControl(Control control)
        {
            panel1.Controls.Clear();

            control.Dock = DockStyle.Fill;
            control.BringToFront();
            control.Focus();

            panel1.Controls.Add(control);
        }

        private void guna2CircleButton3_Click(object sender, EventArgs e)
        {

        }

        private void FormKasir_Load(object sender, EventArgs e)
        {
            UCbayar ub = new UCbayar();
            showControl(ub);
            timer1.Start();
            //UCdashboard ucd = new UCdashboard();
            //showControl(ucd);
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
           
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2CircleButton3_Click_1(object sender, EventArgs e)
        {
            
        }

        private void guna2CircleButton5_Click(object sender, EventArgs e)
        {
           
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
           
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
           
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }

        private void guna2CircleButton4_Click(object sender, EventArgs e)
        {

        }

        private void texttime_Click(object sender, EventArgs e)
        {

        }

        private void textname_Click(object sender, EventArgs e)
        {

        }

        private void gunaLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            UCorder od = new UCorder();
            showControl(od);
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            UCbayar ub = new UCbayar();
            showControl(ub);
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            infoApp ap = new infoApp();
            ap.ShowDialog();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You will exit the application. Continue?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                FormLogin fl = new FormLogin();
                this.Hide();
                fl.Show();
                machine.LogActivity("logout");
            }
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            ManualBook mnf = new ManualBook();
            mnf.Show();
        }
    }
}
