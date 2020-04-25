using CentralMangas.Controls;
using CentralMangas.Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CentralMangas.Scans.UnionMangas
{
    public class ServidorUnionMangas
    {
        public static async Task<List<EntidadeManga>> CarregarHudAsync()
        {
            HttpClient client = new HttpClient();

            var response = await client.GetAsync("https://unionleitor.top/lista-mangas");
            var page = await response.Content.ReadAsStringAsync();
            string decoded = System.Net.WebUtility.HtmlDecode(page);
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(decoded);
            string ff = "col-md-3 col-xs-6 text-center bloco-manga";
            var div = doc.DocumentNode.SelectNodes("//div[@class='" + ff + "']");
            List<EntidadeManga> lista = new List<EntidadeManga>();
            foreach (var item in div)
            {
                string linkImagem = Regex.Match(item.ChildNodes[1].InnerHtml, @"(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|gif|png)", RegexOptions.IgnoreCase).Value;
                string nome = SpliceText(item.ChildNodes[4].InnerText, 10).Replace("/n","").Trim();
                string tooltip = NormalizeWhiteSpaceForLoop(item.ChildNodes[6].InnerText).Substring(1);
                string mangalink = item.ChildNodes[1].Attributes[0].Value;

                EntidadeManga e = new EntidadeManga(nome, mangalink, linkImagem, tooltip);
                lista.Add(e);
            }

            return lista;
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

        public static async Task<List<EntidadeCapitulo>> CarregarInfoManga(EntidadeManga manga)
        {
            HttpClient client = new HttpClient();

            var response = await client.GetAsync(manga.Link);
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
                capitulos.Add(new EntidadeCapitulo()
                {
                    Link = mangaLink,
                    Nome = mangaCap,
                    DataPublicado = data,
                });
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

        public static async Task<List<EntidadeManga>> PesquisarManga(string pesquisa)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"https://unionleitor.top/assets/busca.php?q={pesquisa}");
            var page = await response.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<Pesquisa>(page);
            List<EntidadeManga> mangas = new List<EntidadeManga>();
            foreach (var item in json.Itens)
            {
                EntidadeManga e = new EntidadeManga(item.Titulo, $"https://unionleitor.top/perfil-manga/{item.Url}", item.Imagem);
                mangas.Add(e);
            }
            return mangas;
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
    }
}
