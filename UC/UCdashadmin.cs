using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_Project.UC
{
   
    public partial class UCdashadmin : UserControl
    {
        machine machine = new machine();
        public UCdashadmin()
        {
            InitializeComponent();
        }

        private void UCdashadmin_Load(object sender, EventArgs e)
        {
            
            timer1.Start();
            textname.Text = "Welcome, " + FormLogin.nama_pegawai;
            
            DataTable empbaruDB = machine.GetOneData("SELECT nama FROM pegawai WHERE kdPegawai IN (SELECT MAX(kdPegawai) FROM pegawai)");
            newEmp.Text = empbaruDB.Rows[0][0].ToString();

            DataTable juminDB = machine.GetOneData("select CONCAT(count(lvl), ' People') from pegawai where lvl = 'Admin'");
            totadmin.Text = juminDB.Rows[0][0].ToString();

            DataTable jumanDB = machine.GetOneData("select CONCAT(count(lvl), ' People') from pegawai where lvl = 'Manager'");
            totmanage.Text = jumanDB.Rows[0][0].ToString();

            DataTable jukasDB = machine.GetOneData("select CONCAT(count(lvl), ' People') from pegawai where lvl = 'Kasir'");
            totkasir.Text = jukasDB.Rows[0][0].ToString();

            // best count
            DataTable basminDB = machine.GetOneData("select pegawai.nama, count(logact.idLog) as total from pegawai, logact,detail where logact.pegawaiKd = pegawai.kdPegawai and (pegawai.lvl = 'Admin') group by pegawai.nama order by total desc");
            badmin.Text = basminDB.Rows[0][0].ToString();

            DataTable basmanDB = machine.GetOneData("select pegawai.nama, count(logact.idLog) as total from pegawai, logact,detail where logact.pegawaiKd = pegawai.kdPegawai and (pegawai.lvl = 'Manager') group by pegawai.nama order by total desc");
            bmanage.Text = basmanDB.Rows[0][0].ToString();

            DataTable basirDB = machine.GetOneData("select pegawai.nama, count(logact.idLog) as total from pegawai, logact,detail where logact.pegawaiKd = pegawai.kdPegawai and (pegawai.lvl = 'Kasir') group by pegawai.nama order by total desc");
            bkasir.Text = basirDB.Rows[0][0].ToString();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //datetime brjalan
            texttime.Text = DateTime.Now.ToLongTimeString();
            textdate.Text = DateTime.Now.ToLongDateString();
        }
    }
}
