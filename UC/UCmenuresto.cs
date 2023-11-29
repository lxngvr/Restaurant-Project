using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zen.Barcode;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Restaurant_Project.UC
{
    public partial class UCmenuresto : UserControl
    {
        machine machine = new machine();
        String imageLocation, kode_menu;
        bool isGenerated = false;
        
        public UCmenuresto()
        {
            InitializeComponent();
            
        }

        private void Clear()
        {
            txtnamamenu.Clear();
            txtdeskripsi.Clear();
            txtharga.Clear();
            txtfoto.Text = "";
            guna2ComboBox1.Text = "";
            guna2ComboBox1.SelectedIndex = -1;
            guna2PictureBox1.Image = null;
            guna2PictureBox2.Image = null;

        }

        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }

        private void UCmenuresto_Load(object sender, EventArgs e)
        {
            DataSet data = machine.GetData("select * from menu");
            guna2DataGridView1.DataSource = data.Tables[0];
            guna2DataGridView1.CellFormatting += guna2DataGridView1_CellFormatting_1;

            //guna2Button7.Visible = false;
        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            
            
        }
        private void gunaButton1_Click(object sender, EventArgs e)
        {
           
        }

        private void txtharga_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtharga.Text, "[^0-9]"))
            {
                MessageBox.Show("Price input must be filled with a number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtharga.Clear();
            }
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                kode_menu = guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtnamamenu.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtdeskripsi.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                guna2ComboBox1.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtharga.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtfoto.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                guna2PictureBox1.ImageLocation = guna2DataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
            else
            {
                // Jika sel yang diklik bukan merupakan sel data, kosongkan PictureBox
                guna2PictureBox1.ImageLocation = null;
            }

        }

        private void txtcari_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void gunaButton5_Click(object sender, EventArgs e)
        {
        
        }

        private void gunaButton4_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2DataGridView1_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
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

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (txtnamamenu.Text != "" && txtdeskripsi.Text != "" && guna2ComboBox1.Text != "" && txtharga.Text != "" && txtfoto.Text != "")
            {
                if (MessageBox.Show("Save New Dish Data?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {

                    kode_menu = machine.GenerateCode("kdMenu", "menu", "MN", 3);
                    Console.WriteLine(kode_menu);
                    machine.SetData("insert into menu(kdMenu, nmMenu, deskripsi, kategori, harga, fotoMenu) values('" + kode_menu + "', '" + txtnamamenu.Text + "', '" + txtdeskripsi.Text + "', '" + guna2ComboBox1.Text + "', '" + txtharga.Text + "', '" + txtfoto.Text + "')", "Successfully added new dish data.");
                    UCmenuresto_Load(this, null);
                    machine.LogActivity("Add New Dish Data");
                    Clear();

                }
            }
            else
            {
                machine.FillAllFields();
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (txtnamamenu.Text != "" && txtdeskripsi.Text != "" && guna2ComboBox1.Text != "" && txtharga.Text != "" && txtfoto.Text != "")
            {
                if (MessageBox.Show("Save new dish data changes?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    machine.SetData("update menu set nmMenu='" + txtnamamenu.Text + "', deskripsi='" + txtdeskripsi.Text + "', kategori='" + guna2ComboBox1.Text + "', harga='" + txtharga.Text + "', fotoMenu='" + txtfoto.Text + "' where kdMenu='" + kode_menu + "'", "Successfully edited dish data.");
                    UCmenuresto_Load(this, null);
                    machine.LogActivity("Update Dish Data");
                    Clear();
                }
            }
            else
            {
                machine.SelectDataToEdit();
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            if (txtnamamenu.Text != "" && txtdeskripsi.Text != "" && guna2ComboBox1.Text != "" && txtharga.Text != "" && txtfoto.Text != "")
            {
                if (MessageBox.Show("Save new dish data changes?", "Confimation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    machine.SetData("delete from menu where kdMenu='" + kode_menu + "'", "Successfully deleted dish data.");
                    UCmenuresto_Load(this, null);
                    machine.LogActivity("Delete Dish Data");
                    Clear();
                }
            }
            else
            {
                machine.SelectDataToDelete();
            }
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Image files(*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png;", Multiselect = false })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    imageLocation = openFileDialog.FileName;
                    txtfoto.Text = openFileDialog.FileName;
                    guna2PictureBox1.ImageLocation = imageLocation;
                }
            }
        }

        private void qrbutton_Click(object sender, EventArgs e)
        {
            //...

            // Menggabungkan semua data yang Anda inginkan dalam satu string
            string dataToEncode = $"{txtnamamenu.Text}\n{txtharga.Text}";

            // Memeriksa apakah txtnamamenu.Text atau txtharga.Text kosong
            if (string.IsNullOrWhiteSpace(txtnamamenu.Text) || string.IsNullOrWhiteSpace(txtharga.Text))
            {
                MessageBox.Show("Data tidak ditemukan. Gagal Mencetak QR Code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Jika txtnamamenu.Text dan txtharga.Text tidak kosong, lanjutkan dengan pembuatan QR Code
                CodeQrBarcodeDraw qrBarcode = BarcodeDrawFactory.CodeQr;
                guna2PictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
                guna2PictureBox2.Image = qrBarcode.Draw(dataToEncode, 200);
            }


        }

        private void barcodebutton_Click(object sender, EventArgs e)
        {
            isGenerated = true;

            // Memeriksa apakah txtnamamenu.Text kosong
            if (string.IsNullOrWhiteSpace(txtnamamenu.Text))
            {
                MessageBox.Show("Data tidak ditemukan. Gagal Mencetak Barcode", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Jika txtnamamenu.Text tidak kosong, lanjutkan dengan pembuatan barcode
                Code128BarcodeDraw barcode = BarcodeDrawFactory.Code128WithChecksum;
                guna2PictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
                guna2PictureBox2.Image = barcode.Draw(txtnamamenu.Text, 180);
            }

        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            if (isGenerated)
            {
                using (FolderBrowserDialog folderBrowser = new FolderBrowserDialog())
                {
                    if (guna2PictureBox2.Image != null)  // Periksa apakah guna2PictureBox2 tidak kosong
                    {
                        // Ubah visibilitas guna2Button7 menjadi true
                        guna2Button7.Visible = true;

                        using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                        {
                            saveFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All Files (*.*)|*.*";
                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                string selectedFilePath = saveFileDialog.FileName;

                                // Save the image to the selected path
                                guna2PictureBox2.Image.Save(selectedFilePath, ImageFormat.Jpeg);

                                // Show a success message
                                MessageBox.Show("Image saved successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Clear the image in guna2PictureBox2
                                guna2PictureBox2.Image = null;

                                // Set visibilitas guna2Button7 menjadi false setelah mengosongkan guna2PictureBox2
                               // guna2Button7.Visible = false;
                            }
                        }
                    }
                }
            



        }



    }

        private void txtcari_TextChanged_1(object sender, EventArgs e)
        {
            DataSet data = machine.GetData("select * from menu where kdMenu like '%" + txtcari.Text + "%' or nmMenu like '%" + txtcari.Text + "%' or harga like '%" + txtcari.Text + "%' or kategori like '%" + txtcari.Text + "%'");
            guna2DataGridView1.DataSource = data.Tables[0];
        }

        private void txtharga_TextChanged_1(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtharga.Text, "[^0-9]"))
            {
                MessageBox.Show("Price data must be filled in with numbers.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtharga.Clear();
            }
        }

        private void gunaButton3_Click(object sender, EventArgs e)
        {
           
        }
    }
}
