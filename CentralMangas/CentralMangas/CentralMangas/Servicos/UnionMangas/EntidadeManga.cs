using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CentralMangas.Servicos.UnionMangas
{
    public class EntidadeManga
    {
        public string MangaNome { get; }
        public ImageSource MangaLink { get; }
        public string MangaFotoLink { get; }
        public string ToolTip { get; }

        public EntidadeManga(string nome, string mangaLink, string mangaFotoLink, string tooltip)
        {
            MangaNome = nome;
            ToolTip = tooltip;
            MangaLink = ImageSource.FromUri(new Uri(mangaLink));
            MangaFotoLink = mangaFotoLink;
        }
    }
}
