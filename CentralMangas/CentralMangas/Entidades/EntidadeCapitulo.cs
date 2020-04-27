using System;

namespace CentralMangas.Entidades
{
    public class EntidadeCapitulo
    {
        public string NomeCapitulo { get; set; }
        public DateTime DataPublicado { get; set; }
        public string Link { get; set; }

        public EntidadeCapitulo(string nomeCapitulo, string link, DateTime data)
        {
            NomeCapitulo = nomeCapitulo;
            Link = link;
            DataPublicado = data;
        }

        public EntidadeCapitulo(string nomeCapitulo, string link)
        {
            NomeCapitulo = nomeCapitulo;
            Link = link;
        }
    }
}
