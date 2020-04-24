using System.Collections;
using Xamarin.Forms;

namespace CentralMangas.Controls
{
    public class FlexView : FlexLayout
    {
        public DataTemplate ItemTemplate { get; set; }

        public static readonly BindableProperty ItemsSourceProperty =
           BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(FlexView),
           defaultBindingMode: BindingMode.OneWay, propertyChanged: OnItemsSourceChanged);

        public IList ItemsSource
        {
            get { return (IList)this.GetValue(ItemsSourceProperty); }
            set { this.SetValue(ItemsSourceProperty, value); }
        }

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var self = (bindable as FlexView);
            var items = (newValue as IList);
            if (items != null)
                foreach (var item in items)
                {
                    var view = (View)self.ItemTemplate.CreateContent();
                    view.BindingContext = item;
                    self.Children.Add(view);
                }
            else
            {
                self.Children.Clear();
            }
        }
    }
}
