using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersaoDesktop.Entidades
{
    public class PesquisaEntidade
    {
        public List<Item> Items { get; set; }
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
