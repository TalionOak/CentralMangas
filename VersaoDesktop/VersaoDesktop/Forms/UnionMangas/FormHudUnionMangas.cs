using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using VersaoDesktop.Entidades;

namespace VersaoDesktop.Forms.UnionMangas
{
    public partial class FormHudUnionMangas : Form
    {
        public FormHudUnionMangas()
        {
            InitializeComponent();

        }

        private async void FormHudUnionMangas_Load(object sender, EventArgs e)
        {
            flowLayoutPanel2.Visible = false;
            HttpClient client = new HttpClient();

            var response = await client.GetAsync("https://unionleitor.top/lista-mangas");
            var page = await response.Content.ReadAsStringAsync();
            string decoded = System.Net.WebUtility.HtmlDecode(page);
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(decoded);
            string ff = "col-md-3 col-xs-6 text-center bloco-manga";
            var div = doc.DocumentNode.SelectNodes("//div[@class='" + ff + "']");
            foreach (var item in div)
            {
                string linkImagem = Regex.Match(item.ChildNodes[1].InnerHtml, @"(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|gif|png)", RegexOptions.IgnoreCase).Value;
                string nome = item.ChildNodes[4].InnerText;
                string tooltip = NormalizeWhiteSpaceForLoop(item.ChildNodes[6].InnerText).Substring(1);
                string mangalink = item.ChildNodes[1].Attributes[0].Value;

                CriarComponente(new MangaEntidade(nome, mangalink, linkImagem, tooltip));
            }
        }

        private void CriarComponente(MangaEntidade MangaEntidade)
        {
            Panel panel1 = new Panel();
            PictureBox pictureBox1 = new PictureBox();
            Label label1 = new Label();
            panel1.SuspendLayout();
            flowLayoutPanel1.Controls.Add(panel1);
            // 
            // panel1
            // 
            panel1.Controls.Add(label1);
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(152, 236);
            panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Top;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(152, 181);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.LoadAsync(MangaEntidade.MangaFotoLink);
            pictureBox1.MouseHover += pictureBox1_MouseHover;
            pictureBox1.MouseClick += PictureBox1_MouseClick;
            pictureBox1.Tag = MangaEntidade;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Left;
            label1.Location = new Point(0, 181);
            label1.Name = "label1";
            label1.Size = new Size(152, 55);
            label1.TabIndex = 1;
            label1.Text = MangaEntidade.MangaNome;
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Tag = MangaEntidade;
            label1.MouseClick += Label1_MouseClick;
            panel1.ResumeLayout(false);
        }

        private void Label1_MouseClick(object sender, MouseEventArgs e)
        {
            Label lb = (Label)sender;
            new FormMangaInfoUnionMangas((MangaEntidade)lb.Tag).Show();
        }

        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            MangaEntidade pbe = (MangaEntidade)pb.Tag;
            new FormMangaInfoUnionMangas((MangaEntidade)pb.Tag).Show();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            PictureBox pb = (PictureBox)sender;
            MangaEntidade pbe = (MangaEntidade)pb.Tag;
            tt.SetToolTip(pb, pbe.ToolTip);
        }

        public static string NormalizeWhiteSpaceForLoop(string input)
        {
            int len = input.Length,
                index = 0,
                i = 0;
            var src = input.ToCharArray();
            bool skip = false;
            char ch;
            for (; i < len; i++)
            {
                ch = src[i];
                switch (ch)
                {
                    case '\u0020':
                    case '\u00A0':
                    case '\u1680':
                    case '\u2000':
                    case '\u2001':
                    case '\u2002':
                    case '\u2003':
                    case '\u2004':
                    case '\u2005':
                    case '\u2006':
                    case '\u2007':
                    case '\u2008':
                    case '\u2009':
                    case '\u200A':
                    case '\u202F':
                    case '\u205F':
                    case '\u3000':
                    case '\u2028':
                    case '\u2029':
                    case '\u0009':
                    case '\u000A':
                    case '\u000B':
                    case '\u000C':
                    case '\u000D':
                    case '\u0085':
                        if (skip) continue;
                        src[index++] = ch;
                        skip = true;
                        continue;
                    default:
                        skip = false;
                        src[index++] = ch;
                        continue;
                }
            }

            return new string(src, 0, index);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Visible = false;
            flowLayoutPanel2.Visible = true;
            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"https://unionleitor.top/assets/busca.php?q={textBox1.Text}");
            var page = await response.Content.ReadAsStringAsync();
            PesquisaEntidade json = JsonConvert.DeserializeObject<PesquisaEntidade>(page);

            foreach (var item in json.Items)
            {
                MangaEntidade manga = new MangaEntidade(item.Titulo, $"https://unionleitor.top/perfil-manga/{item.Url}", item.Imagem.ToString(), $"Autor: {item.Autor}\nCaps: {item.Capitulo}");
                Panel panel1 = new Panel();
                PictureBox pictureBox1 = new PictureBox();
                Label label1 = new Label();
                panel1.SuspendLayout();
                flowLayoutPanel2.Controls.Add(panel1);
                // 
                // panel1
                // 
                panel1.Controls.Add(label1);
                panel1.Controls.Add(pictureBox1);
                panel1.Location = new Point(3, 3);
                panel1.Name = "panel1";
                panel1.Size = new Size(152, 236);
                panel1.TabIndex = 0;
                // 
                // pictureBox1
                // 
                pictureBox1.Dock = DockStyle.Top;
                pictureBox1.Location = new Point(0, 0);
                pictureBox1.Name = "pictureBox1";
                pictureBox1.Size = new Size(152, 181);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.TabIndex = 0;
                pictureBox1.TabStop = false;
                pictureBox1.LoadAsync(item.Imagem.ToString());
                pictureBox1.MouseHover += pictureBox1_MouseHover;
                pictureBox1.MouseClick += PictureBox1_MouseClick;
                pictureBox1.Tag = manga;
                // 
                // label1
                // 
                label1.Dock = DockStyle.Left;
                label1.Location = new Point(0, 181);
                label1.Name = "label1";
                label1.Size = new Size(152, 55);
                label1.TabIndex = 1;
                label1.Text = manga.MangaNome;
                label1.TextAlign = ContentAlignment.MiddleCenter;
                label1.Tag = manga;
                label1.MouseClick += Label1_MouseClick;
                panel1.ResumeLayout(false);
            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                flowLayoutPanel1.Visible = true;
            }
        }
    }
}
