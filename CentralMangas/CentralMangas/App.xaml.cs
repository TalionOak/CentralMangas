using CentralMangas.Paginas;
using Xamarin.Forms;

namespace CentralMangas
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new Master();
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
