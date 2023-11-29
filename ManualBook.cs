
using Patagames.Pdf.Net;
using Patagames.Pdf.Net.Controls.WinForms;
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
    public partial class ManualBook : Form 
    {
        

        private PdfViewer pdfViewer;
        public ManualBook()
        {
            InitializeComponent();
            pdfViewer = new PdfViewer();
            pdfViewer.Dock = DockStyle.Fill;
            Controls.Add(pdfViewer);

            loadPdf(@"C:\Users\HP\source\repos\Restaurant-Project\assets\pdf\USER MANUAL BOOK - RESTAURANT.pdf"); 
            
        }

        private void ManualBook_Load(object sender, EventArgs e)
        {

        }

        private void loadPdf(string filePath)
        {
            try
            {
               pdfViewer.Document = PdfDocument.Load(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat memuat PDF: " + ex.Message);
            }
        }

    }
}
