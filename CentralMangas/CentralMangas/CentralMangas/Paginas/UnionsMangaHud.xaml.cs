using CentralMangas.Servicos.UnionMangas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CentralMangas.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UnionsMangaHud : ContentPage
    {
        public UnionsMangaHud()
        {
            InitializeComponent();
            vAsync();
        }

        public async void vAsync()
        {
            if (!DesignMode.IsDesignModeEnabled)
            {
                UnionHud union = new UnionHud();
                Flex.ItemsSource = await union.CarregarHudAsync();
                //MangasCarregando.IsRunning = false;
            }
        }

        public void OnTapped(object sender, EventArgs e)
        {
            var g = (View)sender;
            var manga = (EntidadeManga)g.BindingContext;

            Navigation.PushAsync(new Page1(manga));
        }
    }
}