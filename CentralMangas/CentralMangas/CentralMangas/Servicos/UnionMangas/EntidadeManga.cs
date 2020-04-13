using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CentralMangas.Servicos.UnionMangas
{
    public class EntidadeManga
    {
        public string MangaNome { get; }
        public string MangaLink { get; }
        public ImageSource MangaFotoLink { get; }
        public string ToolTip { get; }

        public EntidadeManga()
        {

        }

        public EntidadeManga(string nome, string mangaLink, string mangaFotoLink, string tooltip)
        {
            MangaNome = nome;
            ToolTip = tooltip;
            MangaLink = mangaLink;
            MangaFotoLink = ImageSource.FromUri(new Uri(mangaFotoLink)) ;
        }
    }
}
