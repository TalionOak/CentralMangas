using CentralMangas.Entidades;
using CentralMangas.Paginas;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CentralMangas.Scans.UnionMangas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageUnionsMangaHud : ContentPage
    {

        List<EntidadeManga> mangasHud = new List<EntidadeManga>();
        bool atualizou;
        public PageUnionsMangaHud()
        {
            InitializeComponent();
            CarregarMangasHud();
            atualizou = false;
            CarregandoIndicador.IsRunning = true;
            CarregandoIndicador.IsVisible = true;
        }

        public async void CarregarMangasHud()
        {
            mangasHud = await ServidorUnionMangas.CarregarHudAsync();
            ListaMangas.ItemsSource = mangasHud;
            CarregandoIndicador.IsVisible = false;
            atualizou = true;
        }

        public void OnTapped(object sender, EventArgs e)
        {
            var g = (View)sender;
            var manga = (EntidadeManga)g.BindingContext;

            Navigation.PushAsync(new PageMangaInfo(manga), true);
        }

        public async void PesquisarMangaTextChanged(object sender, TextChangedEventArgs args)
        {
            if (string.IsNullOrEmpty(args.NewTextValue))
            {
                if (atualizou == false)
                {
                    CarregandoIndicador.IsVisible = true;
                    ListaMangas.ItemsSource = null;
                    ListaMangas.ItemsSource = mangasHud;
                    CarregandoIndicador.IsVisible = false;
                }
            }
            await Task.CompletedTask;
        }

        public async void PesquisarMangaButtonPressed(object sender, EventArgs args)
        {
            ListaMangas.ItemsSource = null;
            CarregandoIndicador.IsVisible = true;
            atualizou = false;
            if (string.IsNullOrEmpty(((SearchBar)sender).Text))
            {
                ListaMangas.ItemsSource = mangasHud;
                return;
            }
            ListaMangas.ItemsSource = await ServidorUnionMangas.PesquisarManga(((SearchBar)sender).Text);
            CarregandoIndicador.IsVisible = false;
        }
    }
}