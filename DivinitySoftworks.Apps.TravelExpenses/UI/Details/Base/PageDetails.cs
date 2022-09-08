using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace DivinitySoftworks.Apps.TravelExpenses.UI.Details.Base {

    public class PageDetails<T> : UserControl where T : class {

        public PageDetails() {
            DataContext = (System.Windows.Application.Current as App)?.ServiceProvider.GetRequiredService<T>();
        }

        public T ViewModel {
            get {
                return (T)DataContext;
            }
        }
    }
}