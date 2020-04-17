using CentralMangas.Entidades;
using CentralMangas.Servicos;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CentralMangas.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageUnionsMangaHud : ContentPage
    {
        public PageUnionsMangaHud()
        {
            InitializeComponent();
            vAsync();
        }

        public async void vAsync()
        {
            if (!DesignMode.IsDesignModeEnabled)
            {
                ServUnionMangas union = new ServUnionMangas();
                Flex.ItemsSource = await union.CarregarHudAsync();
                //MangasCarregando.IsRunning = false;
            }
        }

        public void OnTapped(object sender, EventArgs e)
        {
            var g = (View)sender;
            var manga = (EntidadeManga)g.BindingContext;

            Navigation.PushAsync(new PageMangaInfo(manga));
        }
    }
}