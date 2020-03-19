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
using System.Drawing.Drawing2D;
using System.Net;

namespace VersaoDesktop.Forms.UnionMangas
{
    public partial class FormMangaReaderUnionMangas : Form
    {
        private string _linkManga;
        public FormMangaReaderUnionMangas(string link)
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        private async void FormMangaReaderUnionMangas_Load(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(_linkManga);
            var page = await response.Content.ReadAsStringAsync();
            string decoded = System.Net.WebUtility.HtmlDecode(page);
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(decoded);
            var div = doc.DocumentNode.SelectSingleNode("//div[@class='" + "col-sm-12 text-center" + "']");
            foreach (var item in div.ChildNodes)
            {
                if (item.Name == "img")
                {

                    PictureBox pictureBox1 = new PictureBox();
                    pictureBox1.Dock = DockStyle.Bottom;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox1.TabStop = false;
                    pictureBox1.Size = new Size(787, 617);
                    pictureBox1.Location = new Point(3, 3);
                    Controls.Add(pictureBox1);
                    //Image img = await GetImageAsync(item.Attributes[0].Value);
                    pictureBox1.LoadAsync(item.Attributes[0].Value);

                    pictureBox1.LoadCompleted += PictureBox1_LoadCompleted;
                }
            }
        }

        private void PictureBox1_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            //if (e.Error != null)
            PictureBox pb = (PictureBox)sender;
            System.Drawing.Point CurrentPoint; CurrentPoint = AutoScrollPosition;
            pb.Size = new Size(787, Convert.ToInt32(pb.Image.Height / 2));
            AutoScrollPosition = new Point(Math.Abs(AutoScrollPosition.X), Math.Abs(CurrentPoint.Y));
        }

        public Image DownloadImageFromUrl(string imageUrl)
        {
            Image image = null;

            try
            {
                System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(imageUrl);
                webRequest.AllowWriteStreamBuffering = true;
                webRequest.Timeout = 30000;

                System.Net.WebResponse webResponse = webRequest.GetResponse();
                System.IO.Stream stream = webResponse.GetResponseStream();

                image = Image.FromStream(stream);

                webResponse.Close();
            }
            catch (Exception ex)
            {
                return null;
            }

            return image;
        }

        async Task<Image> GetImageAsync(string url)
        {
            Image image = null;
            await Task.Run(async () =>
            {
                try
                {
                    using (var webClient = new WebClient())
                    {
                        var stream = await webClient.OpenReadTaskAsync(url);
                        image = Image.FromStream(stream);
                    }
                }
                catch
                {
                }

            });
            return image;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool zoom = true;
            foreach (Control control in Controls)
            {
                switch (control)
                {
                    case PictureBox pb:
                        //Zoom ratio by which the images will be zoomed by default
                        int zoomRatio = 30;
                        //Set the zoomed width and height
                        int heightZoom = pb.Height * zoomRatio / 100;
                        //zoom = true --> zoom in
                        //zoom = false --> zoom out
                        if (!zoom)
                        {
                            heightZoom *= -1;
                        }
                        //Add the width and height to the picture box dimensions
                        pb.Height += heightZoom;

                        break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool zoom = false;
            foreach (Control control in Controls)
            {
                switch (control)
                {
                    case PictureBox pb:
                        //Zoom ratio by which the images will be zoomed by default
                        int zoomRatio = 30;
                        //Set the zoomed width and height
                        int heightZoom = pb.Height * zoomRatio / 100;
                        //zoom = true --> zoom in
                        //zoom = false --> zoom out
                        if (!zoom)
                        {
                            heightZoom *= -1;
                        }
                        //Add the width and height to the picture box dimensions
                        pb.Height += heightZoom;
                        pb.Width = pb.Width;

                        break;
                }
            }
        }
    }
}
