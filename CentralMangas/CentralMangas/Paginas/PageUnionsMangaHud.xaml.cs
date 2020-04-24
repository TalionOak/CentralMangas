using CentralMangas.Entidades;
using CentralMangas.Servicos;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CentralMangas.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageUnionsMangaHud : ContentPage
    {

        private List<EntidadeManga> mangasHud = new List<EntidadeManga>();
        private List<EntidadeManga> mangasPesquisados = new List<EntidadeManga>();
        public PageUnionsMangaHud()
        {
            InitializeComponent();
            CarregarMangasHud();
        }

        public async void CarregarMangasHud()
        {
            if (!DesignMode.IsDesignModeEnabled)
            {
                //MangasCarregando.IsRunning = false;
                mangasHud = await ServUnionMangas.CarregarHudAsync();
                ListaMangas.ItemsSource = mangasHud;
            }
        }

        public void OnTapped(object sender, EventArgs e)
        {
            var g = (View)sender;
            var manga = (EntidadeManga)g.BindingContext;

            Navigation.PushAsync(new PageMangaInfo(manga), true);
        }

        public async void PesquisarMangaTextChanged(object sender, TextChangedEventArgs args)
        {
            mangasPesquisados.Clear();
            ListaMangas.ItemsSource = null;
            if (string.IsNullOrEmpty(args.NewTextValue))
            {
                ListaMangas.ItemsSource = mangasHud;
                return;
            }

            ListaMangas.ItemsSource = await ServUnionMangas.PesquisarManga(args.NewTextValue);
        }

        public async void PesquisarMangaButtonPressed(object sender, EventArgs args)
        {
            mangasPesquisados.Clear();
            ListaMangas.ItemsSource.Clear();
            if (string.IsNullOrEmpty(((SearchBar)sender).Text))
            {
                ListaMangas.ItemsSource = mangasHud;
                return;
            }
            ListaMangas.ItemsSource = await ServUnionMangas.PesquisarManga(((SearchBar)sender).Text);
        }
    }
}