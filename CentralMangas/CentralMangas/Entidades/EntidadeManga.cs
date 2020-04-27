using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CentralMangas.Entidades
{
    public class EntidadeManga
    {
        public string LinkBase { get; set; }
        public ImageSource LinkFoto { get; set; }
        public string Nome { get; set; }
        public List<EntidadeCapitulo> Capitulos { get; set; }

        public EntidadeManga()
        {
            Capitulos = new List<EntidadeCapitulo>();
        }

        public EntidadeManga(string nome, string mangaLink, string mangaFotoLink) : this()
        {
            Nome = nome;
            LinkBase = mangaLink;
            LinkFoto = ImageSource.FromUri(new Uri(mangaFotoLink));
        }

        public void AdicionarCapitulo(string nome, string link, DateTime dataPublicado)
        {
            Capitulos.Add(new EntidadeCapitulo(nome, link, dataPublicado));
        }

        public void AdicionarCapitulo(string nome, string link)
        {
            Capitulos.Add(new EntidadeCapitulo(nome, link));
        }
    }
}
