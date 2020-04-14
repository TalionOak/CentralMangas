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
        private EntidadeManga _mangaEntidade;
        public PageMangaInfo(EntidadeManga manga)
        {
            InitializeComponent();
            _mangaEntidade = manga;
            Carregar();
        }

        public async void Carregar()
        {
            flexcaps.ItemsSource = await ServUnionMangas.CarregarInfoManga(_mangaEntidade);
        }

        void Clicado(object sender, EventArgs args)
        {
            var f = (Button)sender;
        }

        public class manga
        {
            public string mangacapButton { get; set; }
        }
    }
}