using System;
using System.Windows.Forms;

namespace VersaoDesktop.Forms
{
    public partial class ScanMenuForm : Form
    {
        public ScanMenuForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {



           
        }

        private void btnVulcanNovel_Click(object sender, EventArgs e)
        {
            new VulcanNovelForm().Show();
        }
    }
}
