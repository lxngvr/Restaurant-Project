using System;
using System.Collections;
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
    public partial class UCpegawai : UserControl
    {
        machine machine = new machine();
        String kode_user;
        public UCpegawai()
        {
            InitializeComponent();
        }

        private void Clear()
        {
            txtemail.Clear();
            txtnamapegawai.Clear();
            txttelepon.Clear();
            txtalamat.Clear();
            txtusername.Clear();
            txtpass.Clear();
            guna2ComboBox1.Text= "";
            guna2ComboBox1.SelectedIndex = -1;
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            
        }

        private void UCpegawai_Load(object sender, EventArgs e)
        {
             DataSet data = machine.GetData("select * from pegawai");
             guna2DataGridView1.DataSource = data.Tables[0];

            guna2DataGridView1.Columns["Column7"].Visible = false;

        }

        private void gunaLabel9_Click(object sender, EventArgs e)
        {

        }

        private void gunaTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void gunaButton3_Click(object sender, EventArgs e)
        {
            
        }

        private void gunaButton4_Click(object sender, EventArgs e)
        {
           
        }

        private void gunaButton5_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                kode_user = guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtemail.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtnamapegawai.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txttelepon.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtalamat.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtusername.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtpass.Text = ""; // Mengosongkan txtpass
                guna2ComboBox1.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            }

        }

        private void txtcari_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtcari_TextChanged_1(object sender, EventArgs e)
        {
            DataSet data = machine.GetData("SELECT * FROM pegawai WHERE kdPegawai LIKE '%" + txtcari.Text + "%' OR nama LIKE '%" + txtcari.Text + "%' OR email LIKE '%" + txtcari.Text + "%' OR username LIKE '%" + txtcari.Text + "%' ");
            guna2DataGridView1.DataSource = data.Tables[0];
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (txtemail.Text != "" && txtnamapegawai.Text != "" && txttelepon.Text != "" && txtalamat.Text != "" && txtusername.Text != "" && txtpass.Text != "" && guna2ComboBox1.Text != "")
            {
                // Check if username, nama, or email already exists in the database
                if (IsDataAlreadyExists(txtusername.Text, txtnamapegawai.Text, txtemail.Text))
                {
                    MessageBox.Show("Username or email already exists in the database. Please use another one.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (MessageBox.Show("Save new employee data?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        kode_user = machine.GenerateCode("kdPegawai", "pegawai", "PG", 4);
                        Console.WriteLine(kode_user);
                        machine.SetData("insert into pegawai(kdPegawai, email, nama, telepon, alamat, username, pass, lvl) values('" + kode_user + "', '" + txtemail.Text + "', '" + txtnamapegawai.Text + "', '" + txttelepon.Text + "', '" + txtalamat.Text + "', '" + txtusername.Text + "', '" + txtpass.Text + "', '" + guna2ComboBox1.Text + "')", "New employee data has been successfully added.");
                        UCpegawai_Load(this, null);
                        machine.LogActivity("Add new employee data");
                        Clear();
                    }
                }
            }
            else
            {
                machine.FillAllFields();
            }
        }



        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (txtemail.Text != "" && txtnamapegawai.Text != "" && txttelepon.Text != "" && txtalamat.Text != "" && txtusername.Text != "" && guna2ComboBox1.Text != "")
            {
                // Cek apakah txtpass kosong atau tidak
                string passValue = (txtpass.Text != "") ? "'" + txtpass.Text + "'" : "pass";  // Ganti "pass" dengan nilai default yang sesuai

                if (MessageBox.Show("Save changes to employee data?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    machine.SetData("update pegawai set kdPegawai='" + kode_user + "', email='" + txtemail.Text + "', nama='" + txtnamapegawai.Text + "', telepon='" + txttelepon.Text + "', alamat='" + txtalamat.Text + "', username='" + txtusername.Text + "', pass=" + passValue + ", lvl='" + guna2ComboBox1.Text + "' where kdPegawai='" + kode_user + "'", "Successfully updated employee data.");

                    UCpegawai_Load(this, null);
                    machine.LogActivity("update employee data");
                    Clear();
                }
            }
            else
            {
                machine.SelectDataToEdit();
            }



        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            if (txtemail.Text != "" && txtnamapegawai.Text != "" && txttelepon.Text != "" && txtalamat.Text != "" && txtusername.Text != "" && guna2ComboBox1.Text != "")
            {
                if (MessageBox.Show("Are you sure you want to delete this employee's data?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    machine.SetData("delete from pegawai where kdPegawai='" + kode_user + "'", "Successfully deleted employee data.");
                    UCpegawai_Load(this, null);
                    machine.LogActivity("delete employee data");
                    Clear();
                }
            }
            else
            {
                machine.SelectDataToDelete();
            }
        }

        private void txttelepon_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txttelepon.Text, "[^0-9]"))
            {
                MessageBox.Show("Mobile Number must be filled in with numbers.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txttelepon.Clear();
            }

            if (txttelepon.Text.Length > 12)
            {
                // Jika panjang teks melebihi 12 karakter, kita akan memotongnya menjadi 12 karakter
                txttelepon.Text = txttelepon.Text.Substring(0, 12);
                MessageBox.Show("You have reached the input limit!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void txtnamapegawai_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtusername_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtusername.Text))
            {
                checkUser.Visible = false;
            }
            else
            {
                string query = "SELECT * FROM pegawai WHERE username='" + txtusername.Text + "'";
                DataSet ds = machine.GetData(query);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    checkUser.ImageLocation = @"C:\Users\HP\source\repos\Restaurant-Project\assets\icons\yes.png";
                }
                else
                {
                    checkUser.ImageLocation = @"C:\Users\HP\source\repos\Restaurant-Project\assets\icons\cancel.png";
                }

                checkUser.Visible = true;
            }

        }

        private bool IsDataAlreadyExists(string username, string nama, string email)
        {
            // Query to check if username, nama, or email already exists in the database
            string query = "SELECT COUNT(*) FROM pegawai WHERE username = '" + username + "' OR nama = '" + nama + "' OR email = '" + email + "'";

            // Execute the query and check if the count is greater than 0
            int count = machine.GetDataCount(query);
            return count > 0;
        }
    }
}
