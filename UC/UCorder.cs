using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_Project.UC
{
    public partial class UCorder : UserControl
    {
        machine machine = new machine();
        String kode_menu, kode_meja, kode_transaksi;
        DataTable dataTable = new DataTable();
        DataRow dataRow;
        public UCorder()
        {
            InitializeComponent();
            dataTable.Columns.Add("kode menu");
            dataTable.Columns.Add("Nama Menu");
            dataTable.Columns.Add("Nomor Meja");
            dataTable.Columns.Add("Jumlah Menu");
            dataTable.Columns.Add("Total Harga");
        }

        private void Clear()
        {
            txtNamaMenu.Text = "";
            txtHargaMenu.Text = "";
            // txtNomorMeja.Text = "";
            txtJumlahMenu.Clear();
        }

        private void ClearWithNomorMeja()
        {
            txtNamaMenu.Text = "";
            txtHargaMenu.Text = "";
            txtNomorMeja.Text = "";
            txtJumlahMenu.Clear();
            txtTotalBayar.Clear();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtJumlahMenu_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtJumlahMenu.Text, "[^0-9]"))
            {
                MessageBox.Show("Quantity must be filled in with numbers.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtJumlahMenu.Clear();
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
           
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (txtNomorMeja.Text == "")
            {
                if (e.RowIndex != -1)
                {
                    kode_meja = guna2DataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtNomorMeja.Text = guna2DataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                }
            }
            else
            {
                MessageBox.Show("Can only choose one table.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                kode_menu = guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtNamaMenu.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

                // Ambil harga dalam bentuk angka
                int harga = Convert.ToInt32(guna2DataGridView1.Rows[e.RowIndex].Cells[2].Value);

                // Format harga sebagai mata uang Rupiah
                CultureInfo culture = new CultureInfo("id-ID");
                txtHargaMenu.Text = harga.ToString("C2", culture).Replace("Rp", "Rp. ");
            }

        }

        private void txtTotalBayar_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == guna2DataGridView1.Columns["Column4"].Index && e.RowIndex >= 0)
            {
                if (e.Value is int || e.Value is long || e.Value is decimal || e.Value is double)
                {
                    decimal price = Convert.ToDecimal(e.Value);
                    CultureInfo culture = new CultureInfo("id-ID");
                    e.Value = price.ToString("C0", culture).Replace("Rp", "Rp. ");
                    e.FormattingApplied = true;
                }
            }
        }

        private void txtcari_TextChanged(object sender, EventArgs e)
        {
            DataSet data = machine.GetData("select * from meja where kdMeja like '%" + txtcari.Text + "%' or noMeja like '%" + txtcari.Text + "%'");
            guna2DataGridView2.DataSource = data.Tables[0];
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {
            DataSet data = machine.GetData("select * from menu where kdMenu like '%" + txtSearch.Text + "%' or nmMenu like '%" + txtSearch.Text + "%' or harga like '%" + txtSearch.Text + "%' or kategori like '%" + txtSearch.Text + "%'");
            guna2DataGridView1.DataSource = data.Tables[0];
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            guna2Panel1.Visible = false;

            if (txtNamaMenu.Text != "" && txtNomorMeja.Text != "" && txtJumlahMenu.Text != "")
            {
                if (MessageBox.Show("Put it in cart?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    DataRow existingRow = dataTable.AsEnumerable()
                        .FirstOrDefault(row => row.Field<string>("Nama Menu") == txtNamaMenu.Text && row.Field<string>("Nomor Meja") == txtNomorMeja.Text);

                    if (existingRow != null)
                    {
                        // Jika menu sudah ada, tambahkan jumlah menu ke dalam jumlah yang sudah ada
                        if (int.TryParse(existingRow["Jumlah Menu"].ToString(), out int jumlahMenuExisting) && int.TryParse(txtJumlahMenu.Text, out int jumlahMenuInput) && int.TryParse(txtHargaMenu.Text.Replace("Rp. ", ""), NumberStyles.Currency, new CultureInfo("id-ID"), out int hargaMenu))
                        {
                            existingRow["Jumlah Menu"] = (jumlahMenuExisting + jumlahMenuInput).ToString();

                            // Hitung total harga untuk menu yang sudah ada
                            if (double.TryParse(existingRow["Total Harga"].ToString().Replace("Rp. ", "").Replace(",", ""), NumberStyles.Currency, new CultureInfo("id-ID"), out double hargaMenuExisting))
                            {
                                int totalHargaExisting = (int)(hargaMenuExisting + (jumlahMenuInput * hargaMenu));
                                existingRow["Total Harga"] = "Rp. " + totalHargaExisting.ToString("N0", new CultureInfo("id-ID"));
                            }
                            else
                            {
                                // Tampilkan pesan kesalahan jika konversi gagal
                                MessageBox.Show("Invalid Total Harga " + existingRow["Total Harga"].ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            // Tampilkan pesan kesalahan jika konversi gagal
                            MessageBox.Show("Invalid Jumlah Menu or Input Jumlah Menu.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        // Jika menu belum ada, tambahkan baris baru
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["kode menu"] = kode_menu;
                        dataRow["Nama Menu"] = txtNamaMenu.Text;
                        dataRow["Nomor Meja"] = txtNomorMeja.Text;
                        dataRow["Jumlah Menu"] = txtJumlahMenu.Text;

                        if (int.TryParse(txtHargaMenu.Text.Replace("Rp. ", ""), NumberStyles.Currency, new CultureInfo("id-ID"), out int hargaMenu) &&
                            int.TryParse(txtJumlahMenu.Text, out int jumlahMenu))
                        {
                            int totalHarga = hargaMenu * jumlahMenu;
                            dataRow["Total Harga"] = "Rp. " + totalHarga.ToString("N0", new CultureInfo("id-ID"));

                            dataTable.Rows.Add(dataRow);
                        }
                        else
                        {
                            MessageBox.Show("Invalid Quantity or Dish Price.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }

                    guna2DataGridView3.DataSource = dataTable;
                    guna2DataGridView3.Columns[0].Visible = false;

                    int sum = 0;
                    for (int i = 0; i < guna2DataGridView3.Rows.Count; ++i)
                    {
                        string hargaStr = guna2DataGridView3.Rows[i].Cells[4].Value.ToString();
                        hargaStr = hargaStr.Replace("Rp. ", "").Replace(",", "");

                        if (double.TryParse(hargaStr, NumberStyles.Currency, new CultureInfo("id-ID"), out double hargaDouble))
                        {
                            sum += (int)hargaDouble; // Ubah ke int jika Anda ingin menggunakan tipe data int
                        }
                        else
                        {
                            MessageBox.Show("Invalid Dish Price " + guna2DataGridView3.Rows[i].Cells[4].Value.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }

                    string formattedSum = "Rp. " + sum.ToString("N0", new CultureInfo("id-ID"));
                    txtTotalBayar.Text = formattedSum;

                    Clear();
                }
            }
            else
            {
                MessageBox.Show("Select the menu, table, and fill in the menu number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            dataTable.Clear();
            ClearWithNomorMeja();
            guna2Panel1.Visible = true;
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            if (dataTable.Rows.Count != 0)
            {
                if (MessageBox.Show("Order Now?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    kode_transaksi = machine.GenerateCode("noTrans", "transaksi", "TDY", 4);

                    // Konversi kembali txtTotalBayar dari format Rupiah ke nilai numerik
                    decimal totalBayar = decimal.Parse(txtTotalBayar.Text.Replace("Rp. ", "").Replace(".", "").Replace(",", ""), NumberStyles.Currency, new CultureInfo("id-ID"));

                    machine.SetDataWithOutMessageBox("insert into transaksi(noTrans, totalBayar, pegawaiKd, mejaKd) values('" + kode_transaksi + "', " + totalBayar + ", '" + FormLogin.kode_pegawai + "', '" + kode_meja + "')");

                    foreach (DataGridViewRow row in guna2DataGridView3.Rows)
                    {
                        machine.SetDataWithOutMessageBox("insert into detail(qty, transNo, menuKd) values('" + row.Cells["Jumlah Menu"].Value + "', '" + kode_transaksi + "', '" + row.Cells["kode menu"].Value + "')");
                    }

                    machine.SetDataWithOutMessageBox("update meja set status='Terisi' where kdMeja='" + kode_meja + "'");
                    dataTable.Clear();
                    Clear();
                    txtNomorMeja.Text = "";
                    txtTotalBayar.Text = ""; // Hapus format Rupiah sebelum membersihkan teks
                    UCorder_Load(this, null);
                    machine.LogActivity("Order Dish and Table");

                    MessageBox.Show("Successfully ordered the dish.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("The Cart cannot be empty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtJumlahMenu_TextChanged_1(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtJumlahMenu.Text, "[^0-9]"))
            {
                MessageBox.Show("Quantity must be filled in with numbers.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtJumlahMenu.Clear();
            }
        }

        private void txtTotalBayar_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void UCorder_Load(object sender, EventArgs e)
        {
            DataSet data = machine.GetData("select kdMenu,nmMenu,harga from menu");
            guna2DataGridView1.DataSource = data.Tables[0];
            guna2DataGridView1.CellFormatting += guna2DataGridView1_CellFormatting;

            DataSet data2 = machine.GetData("select * from meja where status = 'Kosong'");
            guna2DataGridView2.DataSource = data2.Tables[0];
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }
    }
}
