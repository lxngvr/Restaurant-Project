using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_Project.UC
{
    public partial class UClogactivity : UserControl
    {
        public UClogactivity()
        {
            InitializeComponent();
        }
        machine machine = new machine();
        private void UClogactivity_Load(object sender, EventArgs e)
        {
            String theDate = DateTime.Now.ToString("yyyy-MM-dd");
            DataSet data = machine.GetData("select pegawai.kdPegawai, pegawai.nama, logact.aktifitas, logact.tglLog, pegawai.lvl from logact, pegawai where logact.pegawaiKd = pegawai.kdPegawai and (tgl_filter = '" + theDate + "')");
            guna2DataGridView1.DataSource = data.Tables[0];
        }

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

            string selectedDate = guna2DateTimePicker1.Value.ToString("yyyy-MM-dd");
            DataSet data = machine.GetData("select pegawai.kdPegawai, pegawai.nama, logact.aktifitas, logact.tglLog, pegawai.lvl from logact, pegawai where logact.pegawaiKd = pegawai.kdPegawai and (tgl_filter = '" + selectedDate + "')");
            guna2DataGridView1.DataSource = data.Tables[0];

        }

        private void txtcari_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtcari_TextChanged_1(object sender, EventArgs e)
        {
            String theDate = DateTime.Now.ToString("yyyy-MM-dd");
            string query;

            if (isFirstQuery)
            {
                query = "select pegawai.kdPegawai, pegawai.nama, logact.aktifitas, logact.tglLog, pegawai.lvl from logact, pegawai where logact.pegawaiKd = pegawai.kdPegawai and tgl_filter = '" + theDate + "' and (pegawai.kdPegawai like '%" + txtcari.Text + "%' or pegawai.nama like '%" + txtcari.Text + "%' or pegawai.lvl like '%" + txtcari.Text + "%' or logact.aktifitas like '%" + txtcari.Text + "%')";
            }
            else
            {
                query = "select pegawai.kdPegawai, pegawai.nama, logact.aktifitas, logact.tglLog, pegawai.lvl from logact, pegawai where logact.pegawaiKd = pegawai.kdPegawai and (pegawai.kdPegawai like '%" + txtcari.Text + "%' or pegawai.nama like '%" + txtcari.Text + "%' or pegawai.lvl like '%" + txtcari.Text + "%' or logact.aktifitas like '%" + txtcari.Text + "%')";
            }

            DataSet data = machine.GetData(query);
            guna2DataGridView1.DataSource = data.Tables[0];
        }

        bool isFirstQuery = true;
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (isFirstQuery)
            {
                string firstQuery = "select pegawai.kdPegawai, pegawai.nama, logact.aktifitas, logact.tglLog, pegawai.lvl from logact, pegawai where logact.pegawaiKd = pegawai.kdPegawai";
                guna2DataGridView1.DataSource = machine.GetData(firstQuery).Tables[0];
            }
            else
            {
                string theDate = DateTime.Now.ToString("yyyy-MM-dd");
                string secondQuery = "select pegawai.kdPegawai, pegawai.nama, logact.aktifitas, logact.tglLog, pegawai.lvl from logact, pegawai where logact.pegawaiKd = pegawai.kdPegawai and (tgl_filter = '" + theDate + "')";
                guna2DataGridView1.DataSource = machine.GetData(secondQuery).Tables[0];
            }

            // Ubah status isFirstQuery
            isFirstQuery = !isFirstQuery;
        }
    }
}
 