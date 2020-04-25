using CentralMangas.Entidades;
using CentralMangas.Scans.UnionMangas;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CentralMangas.Scans.UnionMangas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageLeitorMangasTeste : ContentPage
    {
        EntidadeCapitulo _capitulo;

        //public List<ImageSource> Links { get; set; }
        public List<string> Texto { get; set; }
        public PageLeitorMangasTeste(EntidadeCapitulo capitulo)
        {
            InitializeComponent();
            _capitulo = capitulo;
            CarregarImagens();
            BindingContext = this;
            
        }

        public async void CarregarImagens()
        {
            var f = await ServidorUnionMangas.CarregarImagensAsync(_capitulo);
            Texto = f;
            //Links = new List<ImageSource>();
            //foreach (var item in f)
            //{
            //    Links.Add(ImageSource.FromUri(new Uri(item)));
            //}
            Imagens.ItemsSource = Texto;
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PopModalAsync(true);
            return base.OnBackButtonPressed();
        }
    }
}