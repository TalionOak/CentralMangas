using CentralMangas.Servicos.UnionMangas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CentralMangas.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        private EntidadeManga _mangaEntidade;
        public Page1(EntidadeManga manga)
        {
            InitializeComponent();
            _mangaEntidade = manga;
            Carregar();
        }

        public async void Carregar()
        {
            HttpClient client = new HttpClient();

            var response = await client.GetAsync(_mangaEntidade.MangaLink);
            var page = await response.Content.ReadAsStringAsync();
            string decoded = System.Net.WebUtility.HtmlDecode(page);
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(decoded);
            string ff = "col-md-8 col-xs-12";
            var div = doc.DocumentNode.SelectNodes("//div[@class='" + ff + "']");
            //lblTitulo.Text = _mangaEntidade.MangaNome;
            imageManga.Source = _mangaEntidade.MangaFotoLink;
            //lblTituloAlternativo.Text = div[1].ChildNodes[1].ChildNodes[1].InnerText;
            //lblDesc.Text = FormHudUnionMangas.NormalizeWhiteSpaceForLoop(div[7].ChildNodes[1].ChildNodes[1].InnerText);

            var caps = doc.DocumentNode.SelectNodes("//div[@class='" + "row lancamento-linha" + "']");
            List<manga> mangas = new List<manga>();
            foreach (var item in caps)
            {

                string mangaLink = item.ChildNodes[1].ChildNodes[3].Attributes[0].Value;
                string mangaCap = item.ChildNodes[1].ChildNodes[3].InnerText;
                string mangaCapData = item.ChildNodes[1].ChildNodes[5].InnerText;

                //button1.Text = $"{mangaCap} {mangaCapData}";
                //button1.Tag = mangaLink;
                //button1.MouseClick += Button1_MouseClick;
                mangas.Add(new manga()
                {
                    mangacapButton = mangaCap
                });
            }
            flexcaps.ItemsSource = mangas;
        }

        void Clicado(object sender, EventArgs args)
        {
            var f = (Button)sender;
        }

        public class manga
        {
            public string mangacapButton { get; set; }
        }
    }
}