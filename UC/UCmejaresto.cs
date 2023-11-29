using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Restaurant_Project.UC
{
    public partial class UCmejaresto : UserControl
    {
        machine machine = new machine();
        String kode_meja;
        int dataNomorMejaAwal, dataNomorMejaBaru;
        public UCmejaresto()
        {
            InitializeComponent();
        }

        private void Clear()
        {
            txtnomormeja.Clear();
            guna2ComboBox1.Text = "";
            guna2ComboBox1.SelectedIndex = -1;
            UCmejaresto_Load(this, null);
        }

        private void UCmejaresto_Load(object sender, EventArgs e)
        {
            DataSet data = machine.GetData("select * from meja");
            guna2DataGridView1.DataSource = data.Tables[0];
            //txtnomormeja.Enabled = false;
            DataTable dataNomorMejaDB = machine.GetOneData("select noMeja from meja order by noMeja desc");
            dataNomorMejaAwal = int.Parse(dataNomorMejaDB.Rows[0][0].ToString());
            dataNomorMejaBaru = dataNomorMejaAwal + 1;
            txtnomormeja.Text = dataNomorMejaBaru.ToString();
        }

        private void gunaButton3_Click(object sender, EventArgs e)
        {
            
        }

        private void gunaButton4_Click(object sender, EventArgs e)
        {
            
        }

        private void txtcari_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtnomormeja_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtnomormeja.Text, "[^0-9]"))
            {
                MessageBox.Show("Table number data must be filled in with a number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtnomormeja.Clear();
            }
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                guna2Button2.Enabled = false;
                kode_meja = guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtnomormeja.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                guna2ComboBox1.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
        }

        private void gunaButton5_Click(object sender, EventArgs e)
        {
           
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            DataSet data = machine.GetData("select * from meja where kdMeja like '%" + txtcari.Text + "%' or noMeja like '%" + txtcari.Text + "%' or status like '%" + txtcari.Text + "%'");
            guna2DataGridView1.DataSource = data.Tables[0];
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (txtnomormeja.Text != "" && guna2ComboBox1.Text != "")
            {
                if (MessageBox.Show("Save new table data?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    kode_meja = machine.GenerateCode("kdMeja", "meja", "MJ", 3);
                    machine.SetData("insert into meja(kdMeja, noMeja, status) values('" + kode_meja + "', '" + txtnomormeja.Text + "', '" + guna2ComboBox1.Text + "')", "Successfully saved new table data.");
                    UCmejaresto_Load(this, null);
                    machine.LogActivity("Add New Table Data");
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
            if (txtnomormeja.Text != "" && guna2ComboBox1.Text != "")
            {
                if (MessageBox.Show("Save table data changes?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    machine.SetData("update meja set noMeja='" + txtnomormeja.Text + "', status='" + guna2ComboBox1.Text + "' where kdMeja='" + kode_meja + "'", "Successfully updated table data.");
                    UCmejaresto_Load(this, null);
                    machine.LogActivity("Update Table Data");
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
            guna2Button2.Enabled = true;
            Clear();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            
        }
    }
}

