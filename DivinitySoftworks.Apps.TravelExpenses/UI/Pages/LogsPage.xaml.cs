using DivinitySoftworks.Apps.TravelExpenses.UI.ViewModels.LogsPage;

namespace DivinitySoftworks.Apps.TravelExpenses.UI.Pages {

    public partial class LogsPage : Base.ContentPage {
        public LogsPage(ILogsPageCollectionViewModel collectionViewModel) {
            InitializeComponent();

            DataContext = collectionViewModel.Main;
        }

        public ILogsPageViewModel ViewModel {
            get {
                return (ILogsPageViewModel)DataContext;
            }
        }
    }
}
