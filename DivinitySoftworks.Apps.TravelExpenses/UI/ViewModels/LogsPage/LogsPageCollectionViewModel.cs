using DivinitySoftworks.Apps.Core.Data;

namespace DivinitySoftworks.Apps.TravelExpenses.UI.ViewModels.LogsPage {

    public interface ILogsPageCollectionViewModel : IMainDetailViewModel<ILogsPageViewModel, ILogsPageDetailsViewModel> {
    }


    public class LogsPageCollectionViewModel : MainDetailViewModel<ILogsPageViewModel, ILogsPageDetailsViewModel>, ILogsPageCollectionViewModel {
        public LogsPageCollectionViewModel(ILogsPageViewModel main, ILogsPageDetailsViewModel details) : base(main, details) {
        }
    }
}
