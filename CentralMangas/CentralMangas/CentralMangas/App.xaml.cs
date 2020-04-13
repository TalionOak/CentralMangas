using CentralMangas.Paginas;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CentralMangas
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new UnionsMangaPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
