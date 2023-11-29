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
    public partial class Splash_Screen : Form
    {
        public Splash_Screen()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (guna2ProgressBar1.Value < 100)
            {
                guna2ProgressBar1.Value += 2;
                label1.Text = guna2ProgressBar1.Value.ToString() + "%";

            }
            else
            {
                timer1.Stop();
                this.Hide();
                FormLogin lg = new FormLogin();
                lg.Show();
            }

        }

        private void Splash_Screen_Load(object sender, EventArgs e)
        {

        }
    }
    
}
