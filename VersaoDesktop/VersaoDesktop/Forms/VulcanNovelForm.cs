using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using VersaoDesktop.Data;

namespace VersaoDesktop.Forms
{
    public partial class VulcanNovelForm : Form
    {
        public VulcanNovelForm()
        {
            InitializeComponent();
        }

        public Dictionary<int, string> paginas;
        private async void VulcanNovelForm_LoadAsync(object sender, EventArgs e)
        {
            //Console.WriteLine("HttpClient");
            //HttpClient client = new HttpClient();
            //var response = await client.GetAsync("https://google.com");
            //var pageContents = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(pageContents);
            //Console.WriteLine("Fim");
            //Console.ReadKey();

            // 
            // button3
            // 

            HttpClient client = new HttpClient();
            foreach (var item in Link.Links)
            {
                Button button3 = new Button();
                button3.Dock = DockStyle.Top;
                button3.Location = new Point(0, 0);
                button3.Tag = item.Key;
                button3.Size = new Size(800, 45);
                var response = await client.GetAsync(item.Key);
                var page = await response.Content.ReadAsStringAsync();
                string title = Regex.Match(page, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>",
                               RegexOptions.IgnoreCase).Groups["Title"].Value;
                string decoded = System.Net.WebUtility.HtmlDecode(title);
                decoded = decoded.Substring(0, decoded.Length - 8);
                button3.Text = decoded;
                button3.UseVisualStyleBackColor = true;
                button3.Click += new EventHandler(button3_Click);
                Controls.Add(button3);
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            Button botao = (Button)sender;

            HttpClient client = new HttpClient();
            var response = await client.GetAsync(botao.Tag.ToString());
            var page = await response.Content.ReadAsStringAsync();
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(page);
            string ff = "elementor-element elementor-element-5ccbd00e elementor-widget elementor-widget-text-editor";
            var div = doc.DocumentNode.SelectNodes("//div[@class='" + ff + "']");
            string decoded = System.Net.WebUtility.HtmlDecode(div[0].InnerHtml);
            doc.LoadHtml(decoded);


            new NovelDescForm(botao.Text, doc.DocumentNode.InnerText).Show();
        }
    }
}
