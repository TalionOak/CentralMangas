using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CentralMangas.Entidades
{
    public class EntidadeManga
    {
        public string Nome { get; set; }
        public string Link { get; set; }
        public string ToolTip { get; set; }
        public ImageSource Foto { get; set; }
        public List<EntidadeCapitulo> Capitulos { get; set; }

        public EntidadeManga()
        {
            Capitulos = new List<EntidadeCapitulo>();
        }

        public EntidadeManga(string nome, string mangaLink, string mangaFotoLink, string tooltip)
        {
            Nome = nome;
            ToolTip = tooltip;
            Link = mangaLink;
            Foto = ImageSource.FromUri(new Uri(mangaFotoLink));
            Capitulos = new List<EntidadeCapitulo>();
        }

        public EntidadeManga(string nome, string mangaLink, Uri mangaFotoLink)
        {
            Nome = nome;
            Link = mangaLink;
            Foto = ImageSource.FromUri(mangaFotoLink);
            Capitulos = new List<EntidadeCapitulo>();
        }

        public void AdicionarCapitulo(string nome, string link, DateTime dataPublicado)
        {
            Capitulos.Add(new EntidadeCapitulo()
            {
                Nome = nome,
                Link = link,
                DataPublicado = dataPublicado
            });
        }

        public void AdicionarCapitulo(string nome, string link)
        {
            Capitulos.Add(new EntidadeCapitulo()
            {
                Nome = nome,
                Link = link,
            });
        }
    }
}
