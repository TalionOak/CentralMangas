using CentralMangas.Entidades;
using CentralMangas.Scans.UnionMangas;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CentralMangas.Scans.UnionMangas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMangaInfo : ContentPage
    {
        private EntidadeManga _manga;
        public PageMangaInfo(EntidadeManga manga)
        {
            InitializeComponent();
            _manga = manga;
            MangaNome.Text = _manga.Nome;
            MangaFoto.Source = _manga.LinkFoto;
            Title = MangaNome.Text;
            Carregar();
        }

        public async void Carregar()
        {
            _manga.Capitulos = await ServidorUnionMangas.CarregarInfoManga(_manga);
            CarregandoCapitulos.IsRunning = false;
            Capitulos.ItemsSource = _manga.Capitulos;
        }

        void Clicado(object sender, EventArgs args)
        {
            var f = (Button)sender;
            var manga = (EntidadeCapitulo)f.BindingContext;
            Navigation.PushModalAsync(new PageLeitorMangas(manga), true);
        }
    }
}