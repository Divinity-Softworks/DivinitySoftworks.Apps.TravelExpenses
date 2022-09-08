using DivinitySoftworks.Apps.TravelExpenses.UI.ViewModels;

namespace DivinitySoftworks.Apps.TravelExpenses.UI.Pages {
    public partial class SettingsPage : Base.ContentPage {
        public SettingsPage(ISettingsPageViewModel settingsPageViewModel) {
            InitializeComponent();

            DataContext = settingsPageViewModel;
        }

        public ISettingsPageViewModel ViewModel {
            get {
                return (ISettingsPageViewModel)DataContext;
            }
        }
    }
}
