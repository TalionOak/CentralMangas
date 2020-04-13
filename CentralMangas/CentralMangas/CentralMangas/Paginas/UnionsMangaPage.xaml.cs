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
    public partial class UnionsMangaPage : ContentPage
    {
        public UnionsMangaPage()
        {
            InitializeComponent();
            vAsync();
        }


        //public async void ItemTapped(object sender, ItemTappedEventArgs e)
        //{
        //    var myListView = (ListView)sender;
        //    var myItem = (EntidadeManga)myListView.SelectedItem;
        //    await DisplayAlert("Item Selecionado", myItem.ToolTip, "ok");
        //}

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
            //g.BindingContext
        }
    }
}