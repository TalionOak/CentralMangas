using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VersaoDesktop.Forms
{
    public partial class NovelDescForm : Form
    {
        public NovelDescForm(string nome, string desc)
        {
            InitializeComponent();
            label1.Text = nome;
            richTextBox1.Text = desc;
        }

        private void NovelDescForm_Load(object sender, EventArgs e)
        {

        }
    }
}
