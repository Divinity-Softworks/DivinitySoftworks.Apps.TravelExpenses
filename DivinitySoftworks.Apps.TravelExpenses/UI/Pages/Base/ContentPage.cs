using System.Windows;
using System.Windows.Controls;

namespace DivinitySoftworks.Apps.TravelExpenses.UI.Pages.Base {
    public class ContentPage : Page {

        public object DetailsContent {
            get { return GetValue(DetailsContentProperty); }
            set { SetValue(DetailsContentProperty, value); }
        }

        public static readonly DependencyProperty DetailsContentProperty = DependencyProperty.Register(nameof(DetailsContent), typeof(object), typeof(ContentPage), null);

    }
}
