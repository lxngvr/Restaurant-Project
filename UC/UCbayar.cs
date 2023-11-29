using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_Project.UC
{
    
    public partial class UCbayar : UserControl
    {
        machine machine = new machine();
        String kode_transaksi, kode_meja;
        public UCbayar()
        {
            InitializeComponent();
        }

        private void Clear()
        {
            txttagihan.Text = "";
            txtbayar.Clear();
            txtkembalian.Text = "";
        }

        private void UCbayar_Load(object sender, EventArgs e)
        {
            timer1.Start();
            textname.Text = "Welcome," + FormLogin.nama_pegawai;
            DataSet data = machine.GetData("select transaksi.noTrans, transaksi.mejaKd, pegawai.kdPegawai, pegawai.nama, transaksi.tglTrans, transaksi.totalBayar, transaksi.statusTrans from pegawai, transaksi, meja where transaksi.mejaKd = meja.kdMeja and transaksi.pegawaiKd = pegawai.kdPegawai and (statusTrans='Belum dibayar')");
            guna2DataGridView1.DataSource = data.Tables[0];
            guna2DataGridView1.CellFormatting += guna2DataGridView1_CellFormatting;
        }

        private DataTable GetDataFromDatabase()
        {
            DataTable dataTable = new DataTable();
            return dataTable;
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            

        }
        private void txtkembalian_TextChanged(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(report(), new Font("SimSun-ExtB", 5.5f, FontStyle.Bold), Brushes.Black, new Point(10, 10));
        }

        private void txttagihan_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                kode_transaksi = guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                kode_meja = guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                // Ambil harga dalam bentuk angka
                int harga = Convert.ToInt32(guna2DataGridView1.Rows[e.RowIndex].Cells[5].Value);

                // Format harga sebagai mata uang Rupiah tanpa angka desimal
                txttagihan.Text = "Rp. " + harga.ToString("N0", new CultureInfo("id-ID"));
            }
        }

        private void gunaButton5_Click(object sender, EventArgs e)
        {
           
        }

       

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == guna2DataGridView1.Columns["Column6"].Index && e.RowIndex >= 0)
            {
                if (e.Value is int || e.Value is long || e.Value is decimal || e.Value is double)
                {
                    decimal price = Convert.ToDecimal(e.Value);
                    CultureInfo culture = new CultureInfo("id-ID");
                    e.Value = "Rp. " + price.ToString("N2", culture); // Format angka dengan dua angka desimal
                    e.FormattingApplied = true;
                }
            }

        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtbayar.Text.Replace("Rp. ", "").Replace(".", "").Replace(",", "").Trim(), NumberStyles.Currency, new CultureInfo("id-ID"), out decimal bayar) &&
            decimal.TryParse(txttagihan.Text.Replace("Rp. ", "").Replace(".", "").Replace(",", "").Trim(), NumberStyles.Currency, new CultureInfo("id-ID"), out decimal tagihan))
            {
                if (bayar >= tagihan)
                {
                    decimal kembalian = bayar - tagihan;
                    // Format kembalian menjadi format Rupiah tanpa desimal
                    txtkembalian.Text = "Rp. " + kembalian.ToString("N0");


                    // Ubah status meja hanya untuk meja yang sesuai dengan transaksi yang sedang Anda proses
                    machine.SetDataWithOutMessageBox("update meja set status='Kosong' where kdMeja='" + kode_meja + "'");
                    // Tambahkan klausa WHERE untuk mengubah hanya transaksi yang sesuai
                    machine.SetData("update transaksi set statusTrans='Sudah dibayar' where noTrans = '" + kode_transaksi + "'", "Payment successful.");
                    UCbayar_Load(this, null);

                    // Lainnya seperti LogActivity, cetak dokumen, dan membersihkan
                    printPreviewDialog1.Document = printDocument1;
                    printPreviewDialog1.ShowDialog();
                    Clear();
                }
                else
                {
                    machine.ValidateCash();
                }
            }
            else
            {
                machine.FillAllFields();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //datetime brjalan
            texttime.Text = DateTime.Now.ToLongTimeString();
            textdate.Text = DateTime.Now.ToLongDateString();
        }

        private void textname_Click(object sender, EventArgs e)
        {

        }

        private void txtbayar_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtbayar.Text, "[^0-9]"))
            {
                MessageBox.Show("Payment Input must be filled in with numbers.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtbayar.Clear();
            }
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        public String report()
        {

            string setruk;
            DataSet barangDB = machine.GetData("select menu.nmMenu, detail.qty, menu.harga from menu, transaksi, detail, pegawai where transaksi.pegawaiKd = pegawai.kdPegawai and transaksi.noTrans = detail.transNo and detail.menuKd = menu.kdMenu and (transaksi.noTrans = '" + kode_transaksi + "')");
            DataTable infoTransaksi = machine.GetOneData("select transaksi.noTrans, pegawai.nama from transaksi, pegawai where transaksi.pegawaiKd = pegawai.kdPegawai and (transaksi.noTrans = '" + kode_transaksi + "')");

            setruk = "\n\n                      DEJA'YOU RESTAURANT    \n";
            setruk += "                   Fast Food • Drink • Snack    \n";
            setruk += "              Jl. Tanjungkerta No. 271 Cimalaka   \n";
            setruk += "                          085732340071  \n";
            setruk += "                                             \n";
            setruk += "                                             \n";
            setruk += "--------------------------------------------------------------------------------------------------\n";
            setruk += "--------------------------------------------------------------------------------------------------\n";
            setruk += "                                      " + DateTime.Now.ToLongDateString().PadLeft(5) + "\n";
            setruk += "                                             \n";
            setruk += "No Pesanan                                              ".PadLeft(20) + infoTransaksi.Rows[0][0].ToString() + "     \n";
            setruk += "Kasir                                             ".PadLeft(20) + infoTransaksi.Rows[0][1].ToString() + "\n";
            setruk += "--------------------------------------------------------------------------------------------------\n";
            setruk += "Qty ".PadLeft(2) + "Menu Pesanan".PadRight(30) + "Subtotal".PadLeft(27) + "\n";
            setruk += "--------------------------------------------------------------------------------------------------\n";
            //setruk += "Menu yang dibeli: \n";
            foreach (DataRow row in barangDB.Tables[0].Rows)
            {
                string menu = row["nmMenu"].ToString();
                string qty = row["qty"].ToString();
                string harga = (int.Parse(row["harga"].ToString()) * int.Parse(row["qty"].ToString())).ToString();
                setruk += qty.PadLeft(2) + "x " + menu.PadRight(30) + "Rp. ".PadLeft(23) + harga.PadRight(2) + "\n";
            }

            setruk += "--------------------------------------------------------------------------------------------------\n";
            setruk += "Total Tagihan :                                      ".PadLeft(23) + txttagihan.Text + "\n";

            if (decimal.TryParse(txtbayar.Text, out decimal bayarValue))
            {
                string bayarFormatted = "Rp. " + bayarValue.ToString("N0");
               setruk += "Tunai :                                              ".PadLeft(23) + bayarFormatted + "\n";  // Menggunakan variabel bayarFormatted
            }
            setruk += "Kembalian :                                          ".PadLeft(23) + txtkembalian.Text + "\n";
            setruk += "                                             \n";
            setruk += "                                             \n";
            setruk += "                                             \n";

            setruk += "                                             \n";
            setruk += "                                             \n";
            setruk += "                                             \n";
            setruk += "                                             \n";
            setruk += "                           Terima Kasih                          \n";
            return setruk;

        }

    }
}
