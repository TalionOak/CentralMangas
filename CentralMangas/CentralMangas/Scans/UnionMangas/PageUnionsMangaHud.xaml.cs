using CentralMangas.Entidades;
using CentralMangas.Paginas;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CentralMangas.Scans.UnionMangas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageUnionsMangaHud : ContentPage
    {

        ObservableCollection<EntidadeManga> listaMangas;
        ObservableCollection<EntidadeManga> listaMangasPesquisa;
        CancellationTokenSource tokenHud;
        CancellationTokenSource tokenPesquisa;
        int pagina = 1;
        bool isCarregouHud;
        bool isModoPesquisa;

        public PageUnionsMangaHud()
        {
            InitializeComponent();
            listaMangas = new ObservableCollection<EntidadeManga>();
            listaMangasPesquisa = new ObservableCollection<EntidadeManga>();
            tokenHud = new CancellationTokenSource();
            tokenPesquisa = new CancellationTokenSource();
            ListaMangas.ItemsSource = listaMangas;
            isCarregouHud = false;
            isModoPesquisa = false;

            CarregarMangasHud();
        }

        async void CarregarMangasHud()
        {
            await ServidorUnionMangas.DownloadHudAsync(tokenHud.Token, listaMangas, pagina);
            Carregando.IsVisible = false;
            isCarregouHud = true;
        }

        void ClickManga(object sender, EventArgs e)
        {
            Carregando.IsVisible = true;
            tokenHud.Cancel();
            tokenHud = new CancellationTokenSource();
            var mangaSelecionado = (EntidadeManga)((View)sender).BindingContext;
            Navigation.PushAsync(new PageMangaInfo(mangaSelecionado), true);
            Carregando.IsVisible = false;
        }

        void PesquisarMangaTextChanged(object sender, TextChangedEventArgs args)
        {
            if (string.IsNullOrEmpty(args.NewTextValue))
            {
                if (isCarregouHud == false)
                {
                    tokenPesquisa.Cancel();
                    tokenPesquisa = new CancellationTokenSource();

                    Carregando.IsVisible = false;
                    isCarregouHud = true;
                    isModoPesquisa = false;
                    ListaMangas.ItemsSource = null;
                    ListaMangas.ItemsSource = listaMangas;
                }
            }
        }

        async void PesquisarMangaButtonPressed(object sender, EventArgs args)
        {
            tokenHud.Cancel();
            tokenHud = new CancellationTokenSource();
            ListaMangas.ItemsSource = null;
            listaMangasPesquisa.Clear();
            Carregando.IsVisible = true;
            isCarregouHud = false;
            isModoPesquisa = true;
            await ServidorUnionMangas.PesquisarManga(tokenPesquisa.Token, ((SearchBar)sender).Text, listaMangasPesquisa);
            if (tokenPesquisa.IsCancellationRequested == false && isModoPesquisa == true)
            {
                ListaMangas.ItemsSource = listaMangasPesquisa;
                Carregando.IsVisible = false;
            }
        }

        void OnScrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            if (e.LastVisibleItemIndex == listaMangas.Count - 1)
            {
                if (isCarregandoOutraPagina == false && isModoPesquisa == false && isCarregouHud == true)
                    CarregarOutraPagina();
            };
        }

        bool isCarregandoOutraPagina;
        async void CarregarOutraPagina()
        {
            isCarregandoOutraPagina = true;
            pagina++;
            Carregando.IsVisible = true;
            await ServidorUnionMangas.DownloadHudAsync(tokenHud.Token, listaMangas, pagina);
            if (tokenHud.IsCancellationRequested == true && isModoPesquisa == false)
            {
                Carregando.IsVisible = false;
                isCarregouHud = true;
                pagina--;
            }
            else
            if (tokenHud.IsCancellationRequested == false && isModoPesquisa == false)
            {
                Carregando.IsVisible = false;
                isCarregouHud = true;
            }
            else if (tokenHud.IsCancellationRequested == true && isModoPesquisa == true)
                pagina--;
            isCarregandoOutraPagina = false;
        }
    }
}