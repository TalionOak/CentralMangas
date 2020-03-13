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
    public partial class VulcanNovelForm : Form
    {
        public VulcanNovelForm()
        {
            InitializeComponent();
        }

        private void VulcanNovelForm_Load(object sender, EventArgs e)
        {
            //Console.WriteLine("HttpClient");
            //HttpClient client = new HttpClient();
            //var response = await client.GetAsync("https://google.com");
            //var pageContents = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(pageContents);
            //Console.WriteLine("Fim");
            //Console.ReadKey();


            //HttpClient client = new HttpClient();
            //var response = await client.GetAsync("https://vulcannovel.com.br/usaw-capitulo-02/");
            //var pageContents = await response.Content.ReadAsStringAsync();
            //var doc = new HtmlDocument();
            //doc.LoadHtml(pageContents);
            //string ff = "main";
            //var div = doc.DocumentNode.SelectNodes("//main[@id='" + ff + "']");
            //string html = div[0].InnerHtml.Replace("<br>", "\n");
            //doc.LoadHtml(html);

            ////foreach (HtmlNode p in div.DocumentNode.SelectNodes("//br"))
            ////{
            ////    Console.WriteLine(p.PreviousSibling.InnerText.Trim());
            ////}

            //string decoded = System.Net.WebUtility.HtmlDecode(doc.DocumentNode.InnerText);

            ////string mensagem = div[0].InnerText
            ////    .Replace("\n", "\n\n")
            ////    .Replace("&#8221", "”")
            ////    .Replace("&#8220", "“");

            //Console.WriteLine(decoded);
            //Console.WriteLine("Fim");
            //Console.ReadKey();

            // 
            // button3
            // 
            button3 = new Button();
            button3.Dock = DockStyle.Top;
            button3.Location = new Point(0, 0);
            button3.Name = "button3";
            button3.Size = new Size(800, 45);
            button3.TabIndex = 2;
            button3.Text = "button3";
            button3.UseVisualStyleBackColor = true;
            button3.Click += new EventHandler(button3_Click);
            Controls.Add(button3);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
