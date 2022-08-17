using DivinitySoftworks.Apps.TravelExpenses.UI.Pages.Base;
using DivinitySoftworks.Apps.TravelExpenses.UI.ViewModels;

namespace DivinitySoftworks.Apps.TravelExpenses.UI.Pages {
    public partial class SettingsPage : ContentPage {
        public SettingsPage(ISettingsPageViewModel settingsPageViewModel) {
            InitializeComponent();

            DataContext = settingsPageViewModel;
        }

        public ISettingsPageViewModel ViewModel {
            get {
                return (ISettingsPageViewModel)DataContext;
            }
        }

        private async void OnPageLoaded(object sender, System.Windows.RoutedEventArgs e) {
            await ViewModel.LoadAsync();
        }

        private async void OnLostFocus(object sender, System.Windows.RoutedEventArgs e) {
            await System.Threading.Tasks.Task.Delay(100);
            await ViewModel.SaveAsync();
        }
    }
}
