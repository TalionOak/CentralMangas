using CentralMangas.Entidades;
using CentralMangas.Servicos;
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
    public partial class PageLeitorMangas : ContentPage
    {
        EntidadeCapitulo _capitulo;

        //public List<ImageSource> Links { get; set; }
        public List<string> Texto { get; set; }
        public PageLeitorMangas(EntidadeCapitulo capitulo)
        {
            InitializeComponent();
            _capitulo = capitulo;
            CarregarImagens();
            BindingContext = this;
        }

        public async void CarregarImagens()
        {
            var f = await ServUnionMangas.CarregarImagensAsync(_capitulo);
            Texto = f;
            //Links = new List<ImageSource>();
            //foreach (var item in f)
            //{
            //    Links.Add(ImageSource.FromUri(new Uri(item)));
            //}
            Imagens.ItemsSource = Texto;
        }
    }
}