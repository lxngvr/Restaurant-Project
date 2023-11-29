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
    public partial class FormLogin : Form
    {
        machine machine = new machine();
        UCbayar ub = new UCbayar();
        public static string kode_pegawai;
        public static string nama_pegawai;
        public static string level_pegawai;
        String nametext;
        public FormLogin()
        {
            InitializeComponent();
        }
       

        private void FormLogin_Load(object sender, EventArgs e)
        {
            warning.Visible = false;
            guna2TextBox2.Clear();
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text != "" && guna2TextBox2.Text != "")
            {
                DataTable data = machine.Login(guna2TextBox1.Text, guna2TextBox2.Text);

                if (data.Rows.Count == 1)
                {
                    kode_pegawai = data.Rows[0][0].ToString();
                    nama_pegawai = data.Rows[0][1].ToString();
                    level_pegawai = data.Rows[0][2].ToString();
                    machine.LogActivity("Login to this application");

                    if (level_pegawai == "Admin")
                    {
                        FormAdmin fa = new FormAdmin();
                        this.Hide();
                        fa.Show();
                        nametext = guna2TextBox1.Text;
                        //fa.textname.Text = "Welcome, " + nametext + "!";
                        //fa.textdate.Text = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");
                    }
                    else if (level_pegawai == "Kasir")
                    {
                        FormKasir fk = new FormKasir();
                        this.Hide();
                        fk.Show();
                        nametext = guna2TextBox1.Text;
                        ub.textname.Text = "Welcome, " + nametext + "!";
                        ub.textdate.Text = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");
                    }
                    else if (level_pegawai == "Manager")
                    {
                        FormManager fm = new FormManager();
                        this.Hide();
                        fm.Show();
                        nametext = guna2TextBox1.Text;
                        //fm.nametxt.Text = "Welcome, " + nametext + "!";
                        //fm.txttime.Text = DateTime.Now.ToLongTimeString(); 
                        //fm.txttime.Text = DateTime.Now.ToLongDateString();
                    }

                    else { }
                }
                else
                {
                    warning.Visible = true;
                    guna2TextBox1.Clear();
                    guna2TextBox2.Clear();

                    Timer myTimer = new Timer();
                    myTimer.Interval = (3 * 1000);
                    myTimer.Tick += new EventHandler(timer1_Tick);
                    myTimer.Start();
                }
            }
            else
            {
                machine.FillAllFields();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            warning.Visible = false;
        }

        private void guna2TextBox2_MouseHover(object sender, EventArgs e)
        {
            guna2TextBox2.PasswordChar = '\0';
        }

        private void guna2TextBox2_MouseLeave(object sender, EventArgs e)
        {
            guna2TextBox2.PasswordChar = '•';
        }

        private void warning_Click(object sender, EventArgs e)
        {

        }
    }
    
}
