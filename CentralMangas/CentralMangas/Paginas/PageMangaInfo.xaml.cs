using CentralMangas.Entidades;
using CentralMangas.Servicos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CentralMangas.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMangaInfo : ContentPage
    {
        private EntidadeManga _manga;
        public PageMangaInfo(EntidadeManga manga)
        {
            InitializeComponent();
            _manga = manga;
            Carregar();
            MangaNome.Text = _manga.Nome;
            MangaFoto.Source = _manga.Foto;
            MangaDescricao.Text = _manga.ToolTip;
        }

        public async void Carregar()
        {
            _manga.Capitulos = await ServUnionMangas.CarregarInfoManga(_manga);
            CarregandoCapitulos.IsRunning = false;
            Capitulos.ItemsSource = _manga.Capitulos;
        }

        void Clicado(object sender, EventArgs args)
        {
            var f = (Button)sender;
            var manga = (EntidadeCapitulo)f.BindingContext;
            Navigation.PushModalAsync(new PageLeitorMangasTeste(manga), true);
        }
    }
}