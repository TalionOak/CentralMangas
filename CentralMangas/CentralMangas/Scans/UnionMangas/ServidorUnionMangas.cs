using CentralMangas.Entidades;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace CentralMangas.Scans.UnionMangas
{
    public static class ServidorUnionMangas
    {
        static HttpClient client = new HttpClient();

        const string HudMangas = "https://unionleitor.top/lista-mangas/a-z/{0}/";
        const string PesquisaManga = "https://unionleitor.top/assets/busca.php?q={0}";

        public static async Task DownloadHudAsync(CancellationToken cancellationToken, ObservableCollection<EntidadeManga> listaMangas, int pagina)
        {
            try
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, string.Format(HudMangas, pagina)))
                using (var response = await client.SendAsync(request, cancellationToken))
                {
                    var stream = await response.Content.ReadAsStreamAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var doc = new HtmlDocument();
                        doc.Load(stream);
                        var div = doc.DocumentNode.SelectNodes("//div[@class='" + "col-md-3 col-xs-6 text-center bloco-manga" + "']");
                        foreach (var item in div)
                        {
                            string linkImagem = Regex.Match(item.ChildNodes[1].InnerHtml, @"(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|gif|png)", RegexOptions.IgnoreCase).Value;
                            string linkManga = item.ChildNodes[1].Attributes[0].Value;
                            string nomeManga = WebUtility.HtmlDecode(SpliceText(item.ChildNodes[4].InnerText, 10).Replace("/n", "").Trim());

                            EntidadeManga e = new EntidadeManga(nomeManga, linkManga, linkImagem);
                            listaMangas.Add(e);
                        }
                        return;
                    }
                }
            }
            catch { }
        }


        public static async Task<List<EntidadeManga>> CarregarHudAsync()
        {
            var listaMangas = new List<EntidadeManga>();
            using (var response = await client.GetAsync("https://unionleitor.top/lista-mangas"))
            {
                var page = await response.Content.ReadAsStringAsync();
                string decoded = System.Net.WebUtility.HtmlDecode(page);
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(decoded);
                string ff = "col-md-3 col-xs-6 text-center bloco-manga";
                var div = doc.DocumentNode.SelectNodes("//div[@class='" + ff + "']");
                foreach (var item in div)
                {
                    string linkImagem = Regex.Match(item.ChildNodes[1].InnerHtml, @"(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|gif|png)", RegexOptions.IgnoreCase).Value;
                    string nome = SpliceText(item.ChildNodes[4].InnerText, 10).Replace("/n", "").Trim();
                    string mangalink = item.ChildNodes[1].Attributes[0].Value;

                    EntidadeManga e = new EntidadeManga(nome, mangalink, linkImagem);
                    listaMangas.Add(e);
                }
            }

            return listaMangas;
        }





        public static async Task<List<EntidadeCapitulo>> CarregarInfoManga(EntidadeManga manga)
        {
            HttpClient client = new HttpClient();

            var response = await client.GetAsync(manga.LinkBase);
            var page = await response.Content.ReadAsStringAsync();
            string decoded = System.Net.WebUtility.HtmlDecode(page);
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(decoded);
            string ff = "col-md-8 col-xs-12";
            var div = doc.DocumentNode.SelectNodes("//div[@class='" + ff + "']");
            //lblTitulo.Text = _mangaEntidade.MangaNome;

            //lblTituloAlternativo.Text = div[1].ChildNodes[1].ChildNodes[1].InnerText;
            //lblDesc.Text = FormHudUnionMangas.NormalizeWhiteSpaceForLoop(div[7].ChildNodes[1].ChildNodes[1].InnerText);

            var caps = doc.DocumentNode.SelectNodes("//div[@class='" + "row lancamento-linha" + "']");
            if (caps == null) return null;
            List<EntidadeCapitulo> capitulos = new List<EntidadeCapitulo>();
            foreach (var item in caps)
            {

                string mangaLink = item.ChildNodes[1].ChildNodes[3].Attributes[0].Value;
                string mangaCap = item.ChildNodes[1].ChildNodes[3].InnerText;
                string mangaCapData = item.ChildNodes[1].ChildNodes[5].InnerText;

                DateTime.TryParse(mangaCapData, out DateTime data);
                capitulos.Add(new EntidadeCapitulo(mangaCap, mangaLink, data));
            }
            return capitulos;
        }

        public static async Task<List<string>> CarregarImagensAsync(EntidadeCapitulo entidadeCapitulo)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(entidadeCapitulo.Link);
            var page = await response.Content.ReadAsStringAsync();
            string decoded = System.Net.WebUtility.HtmlDecode(page);
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(decoded);
            var div = doc.DocumentNode.SelectSingleNode("//div[@class='" + "col-sm-12 text-center" + "']");
            List<string> linkImagens = new List<string>();
            foreach (var item in div.ChildNodes)
            {
                if (item.Name == "img")
                {

                    string link = item.Attributes[0].Value;
                    linkImagens.Add(link);
                }
            }

            return linkImagens;
        }

        public static async Task PesquisarManga(CancellationToken cancellationToken, string pesquisa, ObservableCollection<EntidadeManga> listaMangas)
        {
            try
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, string.Format(PesquisaManga, pesquisa)))
                using (var response = await client.SendAsync(request, cancellationToken))
                {
                    var stream = await response.Content.ReadAsStreamAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var json = DeserializeJsonFromStream<Pesquisa>(stream);
                        foreach (var item in json.Itens)
                        {
                            EntidadeManga e = new EntidadeManga(item.Titulo, $"https://unionleitor.top/perfil-manga/{item.Url}", item.Imagem.ToString());
                            listaMangas.Add(e);
                        }
                    }
                }
            }
            catch { }
        }

        private static T DeserializeJsonFromStream<T>(Stream stream)
        {
            if (stream == null || stream.CanRead == false)
                return default(T);
            using (var sr = new StreamReader(stream))
            using (var jtr = new JsonTextReader(sr))
            {
                var js = new JsonSerializer();
                var searchResult = js.Deserialize<T>(jtr);
                return searchResult;
            }
        }

        public static string SpliceText(string inputText, int lineLength)
        {
            string[] stringSplit = inputText.Split(' ');
            int charCounter = 0;
            string finalString = "";
            for (int i = 0; i < stringSplit.Length; i++)
            {
                finalString += stringSplit[i] + " ";
                charCounter += stringSplit[i].Length;
                if (charCounter > lineLength)
                {
                    finalString += "\n";
                    charCounter = 0;
                }
            }
            return finalString;
        }

        private class Pesquisa
        {
            [JsonProperty("items")]
            public List<Item> Itens { get; set; }
        }

        public class Item
        {
            public Uri Imagem { get; set; }
            public string Titulo { get; set; }
            public string Url { get; set; }
            public string Autor { get; set; }
            public string Artista { get; set; }
            public long Capitulo { get; set; }
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
    }
}
