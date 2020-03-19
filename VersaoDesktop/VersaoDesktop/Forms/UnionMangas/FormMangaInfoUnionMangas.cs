using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VersaoDesktop.Entidades;

namespace VersaoDesktop.Forms.UnionMangas
{
    public partial class FormMangaInfoUnionMangas : Form
    {
        private MangaEntidade _mangaEntidade;
        public FormMangaInfoUnionMangas(MangaEntidade manga)
        {
            InitializeComponent();
            _mangaEntidade = manga;
        }

        private async void FormMangaInfoUnionMangas_Load(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();

            var response = await client.GetAsync(_mangaEntidade.MangaLink);
            var page = await response.Content.ReadAsStringAsync();
            string decoded = System.Net.WebUtility.HtmlDecode(page);
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(decoded);
            string ff = "col-md-8 col-xs-12";
            var div = doc.DocumentNode.SelectNodes("//div[@class='" + ff + "']");
            lblTitulo.Text = _mangaEntidade.MangaNome;
            pictureBox1.LoadAsync(_mangaEntidade.MangaFotoLink);
            lblTituloAlternativo.Text = div[1].ChildNodes[1].ChildNodes[1].InnerText;
            lblDesc.Text = FormHudUnionMangas.NormalizeWhiteSpaceForLoop(div[7].ChildNodes[1].ChildNodes[1].InnerText);

            var caps = doc.DocumentNode.SelectNodes("//div[@class='" + "row lancamento-linha" + "']");
            foreach (var item in caps)
            {

                string mangaLink = item.ChildNodes[1].ChildNodes[3].Attributes[0].Value;
                string mangaCap = item.ChildNodes[1].ChildNodes[3].InnerText;
                string mangaCapData = item.ChildNodes[1].ChildNodes[5].InnerText;
                Button button1 = new Button();
                button1.Location = new Point(3, 3);
                button1.Size = new Size(172, 29);
                button1.TabIndex = 0;
                button1.Text = $"{mangaCap} {mangaCapData}";
                button1.Tag = mangaLink;
                button1.UseVisualStyleBackColor = true;
                button1.MouseClick += Button1_MouseClick;
                flowLayoutPanel1.Controls.Add(button1);
            }
        }

        private void Button1_MouseClick(object sender, MouseEventArgs e)
        {
            Button bt = (Button)sender;
            new FormMangaReaderUnionMangas(bt.Tag.ToString()).Show();
        }
    }
}
