using Apotek;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_Project.UC
{

    public partial class UClaporan : UserControl
    {
        machine machine = new machine();

        public UClaporan()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            
        }

        private void UClaporan_Load(object sender, EventArgs e)
        {
            // get data
            DataSet data = machine.GetData("select noTrans as 'Trans Number', tglTrans as 'Trans Date', totalBayar as 'Total Bill' from transaksi");

            // Buat kolom baru dengan tipe data string
            data.Tables[0].Columns.Add("Total Bills", typeof(string));

            foreach (DataRow row in data.Tables[0].Rows)
            {
                // Ambil nilai totalBayar dari setiap baris
                double totalBayar = Convert.ToDouble(row["Total Bill"]);

                // Konversi nilai totalBayar ke format Rupiah
                string formattedTotalBayar = string.Format("Rp. {0:N}", totalBayar);

                // Setel nilai yang telah diubah ke dalam kolom baru
                row["Total Bills"] = formattedTotalBayar;
            }

            // Hapus kolom asli "Total Bill" jika diperlukan
            data.Tables[0].Columns.Remove("Total Bill");

            guna2DataGridView2.DataSource = data.Tables[0];

            //cetak data
            //LaporanGrafikPenjualanDataContext lgpdc = new LaporanGrafikPenjualanDataContext();
            //chart1.DataSource = dataGridView1.DataSource;
            //  chart1.Series["Pendapatan"].XValueMember = "tglTrans";
            //chart1.Series["Pendapatan"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            //chart1.Series["Pendapatan"].YValueMembers = "totalBayar";
            //chart1.Series["Pendapatan"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;


        }

        private void chart1_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            //label3.Visible = false;
            auto auto = new auto();
            string selectquery = "SELECT noTrans as 'No Trans', tglTrans as 'Trans Date', CONCAT('Rp. ', FORMAT(CAST(totalBayar AS DECIMAL(18, 2)), 'N0')) as 'Total Bills' FROM transaksi WHERE tglTrans BETWEEN @fromdate AND @todate";
            SqlCommand command = new SqlCommand(selectquery, auto.GetCon());
            command.Parameters.AddWithValue("@fromdate", guna2DateTimePicker1.Value);
            command.Parameters.AddWithValue("@todate", guna2DateTimePicker2.Value);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            guna2DataGridView2.DataSource = table;


        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            label3.Visible = false;
            auto auto = new auto();
            string selectquery = "SELECT noTrans as 'No Trans', tglTrans as 'Trans Date', totalBayar as 'Total Bills' FROM transaksi WHERE tglTrans BETWEEN @fromdate AND @todate";
            SqlCommand command = new SqlCommand(selectquery, auto.GetCon());
            command.Parameters.AddWithValue("@fromdate", guna2DateTimePicker1.Value);
            command.Parameters.AddWithValue("@todate", guna2DateTimePicker2.Value);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            // Konfigurasi chart
            chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineWidth = 0;
            chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineWidth = 0;
            chart1.Series["Income"].XValueMember = "Trans Date";
            chart1.Series["Income"].YValueMembers = "Total Bills";
            chart1.Series["Income"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            chart1.Series["Income"]["showOpenClose"] = "Both";
            chart1.DataManipulator.IsStartFromFirst = true;

            // Bind chart ke sumber data
            chart1.DataSource = table;
            chart1.DataBind();

            // Format tampilan "Total Bills" dalam format Rupiah pada chart
            foreach (var point in chart1.Series["Income"].Points)
            {
                double totalBills = point.YValues[0];
                point.Label = string.Format("Rp. {0:N0}", totalBills);
            }


        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            label3.Visible = false;
            DailyIncomeViewer fcr = new DailyIncomeViewer();
            fcr.Show();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
