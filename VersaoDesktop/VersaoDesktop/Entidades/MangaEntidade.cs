namespace VersaoDesktop.Entidades
{
    public class MangaEntidade
    {
        public string MangaNome { get; }
        public string MangaLink { get; }
        public string MangaFotoLink { get; }
        public string ToolTip { get; }

        public MangaEntidade(string nome, string mangaLink, string mangaFotoLink, string tooltip)
        {
            this.MangaNome = nome;
            this.ToolTip = tooltip;
            this.MangaLink = mangaLink;
            this.MangaFotoLink = mangaFotoLink;
        }
    }
}
