using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CentralMangas.Servicos.UnionMangas
{
    public class UnionHud
    {
        public async Task<List<EntidadeManga>> CarregarHudAsync()
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
                string nome = SpliceText(item.ChildNodes[4].InnerText, 10);
                string tooltip = NormalizeWhiteSpaceForLoop(item.ChildNodes[6].InnerText).Substring(1);
                string mangalink = item.ChildNodes[1].Attributes[0].Value;

                //Cria os valores.
                //CriarComponente(new MangaEntidade(nome, mangalink, linkImagem, tooltip));
                lista.Add(new EntidadeManga(nome, mangalink, linkImagem, tooltip));
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
    }
}
