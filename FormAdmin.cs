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
   
    public partial class FormAdmin : Form
    {
        machine machine = new machine();
        UCdashboard ucd = new UCdashboard();
        UCpegawai upg = new UCpegawai();
        UClogactivity ula = new UClogactivity();
       
        
        public FormAdmin()
        {
            InitializeComponent();
        }

        private void showControl(Control control)
        {
            panelAdmin.Controls.Clear();

            control.Dock = DockStyle.Fill;
            control.BringToFront();
            control.Focus();

            panelAdmin.Controls.Add(control);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {
            UCdashadmin usda = new UCdashadmin();
            showControl(usda);
            timer1.Start();
            

        }

        private void guna2CircleButton4_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
           
        }

        private void guna2CircleButton3_Click(object sender, EventArgs e)
        {
           
        }

        private void guna2CircleButton5_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            showControl(ula);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            showControl(upg);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            infoApp ap = new infoApp();
            ap.ShowDialog();
        }

        private void guna2CircleButton2_Click_1(object sender, EventArgs e)
        {
            UCdashadmin usda = new UCdashadmin();
            showControl(usda);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
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

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            UCdashadmin usda = new UCdashadmin();
            showControl(usda);
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            ManualBook mnf = new ManualBook();
            mnf.Show();
        }
    }
}
