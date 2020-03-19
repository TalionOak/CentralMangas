using System;
using System.Windows.Forms;
using VersaoDesktop.Forms.UnionMangas;

namespace VersaoDesktop.Forms
{
    public partial class FormMenuScans : Form
    {
        public FormMenuScans()
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

        private void btnUnionMangas_Click(object sender, EventArgs e)
        {
            new FormHudUnionMangas().Show();
        }
    }
}
