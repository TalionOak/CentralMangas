using CentralMangas.Scans.UnionMangas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CentralMangas.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Master : MasterDetailPage
    {
        public Master()
        {
            InitializeComponent();
            Detail = new NavigationPage(new PageUnionsMangaHud());
        }

        private async void AcessarDiscord(object sender, EventArgs args)
        {
            await Launcher.OpenAsync("https://discord.gg/GH6GwRx");
        }

        private void AcessarConfig(object sender, EventArgs args)
        {
            DisplayAlert("Em criação", "Estamos criando uma configuração que seja agradavél para você.", "Aguardarei");
        }

        private void AcessarScans(object sender, EventArgs args)
        {
            DisplayAlert("Em criação", "Estamos procurando mais scans para você.", "Aguardarei");
        }
    }
}