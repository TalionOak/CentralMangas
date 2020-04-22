using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;

namespace CentralMangas.Droid
{
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true, Label = "Central Mangas", Icon = "@mipmap/centralico")]
    public class SplashActivity : Activity
    {
        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
        }

        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
        }

        public override void OnBackPressed() { }

        async void SimulateStartup()
        {

            await Task.Delay(1000);

            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}