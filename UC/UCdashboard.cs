using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_Project.UC
{
    public partial class UCdashboard : UserControl
    {
        machine machine = new machine();
        public UCdashboard()
        {
            InitializeComponent();
        }

        private void UCdashboard_Load(object sender, EventArgs e)
        {
            timer1.Start();
            textname.Text = "Welcome," + FormLogin.nama_pegawai;

            DataTable jumlahMejaDB = machine.GetOneData("select CONCAT(count(kdMeja), ' Tables') from meja");
            totalmeja.Text = jumlahMejaDB.Rows[0][0].ToString();

            DataTable jumlahMenuDB = machine.GetOneData("select CONCAT(count(kdMenu), ' Dishes') from menu");
            totalmenu.Text = jumlahMenuDB.Rows[0][0].ToString();

            

            DataTable foodBaruDB = machine.GetOneData("SELECT nmMenu FROM menu WHERE kdMenu IN (SELECT MAX(kdMenu) FROM menu)");
            newdish.Text = foodBaruDB.Rows[0][0].ToString();

           

            string theDate = DateTime.Now.ToString("yyyy-MM-dd");
            DataTable totalHariIniDB = machine.GetOneData("select sum(menu.harga * detail.qty) from menu, detail, transaksi where detail.transNo = transaksi.noTrans and detail.menukd = menu.kdMenu and (transaksi.tglfilter = '" + theDate + "')");
            if (totalHariIniDB.Rows[0][0].ToString() == "")
            {
                incomeToday.Text = "Belum ada pemasukan";
                //incomeToday.Visible = false;
            }
            else
            {
                //incomeToday.Text = totalHariIniDB.Rows[0][0].ToString();

                decimal income = decimal.Parse(totalHariIniDB.Rows[0][0].ToString());
                string formattedIncome = string.Format("Rp. {0:N}", income);
                incomeToday.Text = formattedIncome;

            }

            DataTable makananTerlarisDB = machine.GetOneData("select menu.nmMenu, count(detail.idDetail) as total from menu, detail where detail.menuKd = menu.kdMenu and (menu.kategori = 'Makanan') group by menu.nmMenu order by total desc");
            if (makananTerlarisDB.Rows.Count != 0)
            {
                bestfood.Text = makananTerlarisDB.Rows[0][0].ToString();
            }
            else
            {
                bestfood.Text = "Belum tersedia";
            }

            DataTable minumanTerlarisDB = machine.GetOneData("select menu.nmMenu, count(detail.idDetail) as total from menu, detail where detail.menuKd = menu.kdMenu and (menu.kategori = 'Minuman') group by menu.nmMenu order by total desc");
            if (minumanTerlarisDB.Rows.Count != 0)
            {
                bestdrink.Text = minumanTerlarisDB.Rows[0][0].ToString();
            }
            else
            {
                bestdrink.Text = "Belum tersedia";
            }

            DataTable jumlahPegawaiDB = machine.GetOneData("select CONCAT(count(kdPegawai), ' People') from pegawai");
            totalpegawai.Text = jumlahPegawaiDB.Rows[0][0].ToString();

            DataTable jumlahreservedDB = machine.GetOneData("select CONCAT(count(status), ' Tables') from meja where status = 'Terisi'");
            rtot.Text = jumlahreservedDB.Rows[0][0].ToString();

            DataTable jumlahavaiDB = machine.GetOneData("select CONCAT(count(status), ' Tables') from meja where status = 'Kosong'");
            emtot.Text = jumlahavaiDB.Rows[0][0].ToString();

            DataTable jumlahmamDB = machine.GetOneData("select CONCAT(count(kategori), ' Foods') from menu where kategori = 'Makanan'");
            totfood.Text = jumlahmamDB.Rows[0][0].ToString();

            DataTable jumlahmimDB = machine.GetOneData("select CONCAT(count(kategori), ' Foods') from menu where kategori = 'Minuman'");
            totdrink.Text = jumlahmimDB.Rows[0][0].ToString();
        }

        private void totalmenu_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void newdrink_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //datetime brjalan
            texttime.Text = DateTime.Now.ToLongTimeString();
            textdate.Text = DateTime.Now.ToLongDateString();
        }
    }
}
