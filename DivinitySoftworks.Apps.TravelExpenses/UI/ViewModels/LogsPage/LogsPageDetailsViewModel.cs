using DivinitySoftworks.Apps.Core.Data;
using DivinitySoftworks.Apps.TravelExpenses.Data.Enums;

namespace DivinitySoftworks.Apps.TravelExpenses.UI.ViewModels.LogsPage {
    public interface ILogsPageDetailsViewModel : IMainDetailContentViewModel<ILogsPageViewModel, ILogsPageDetailsViewModel> {

        LogState LogStates { get; }

        void Toggle(LogState logState);
    }

    public class LogsPageDetailsViewModel : MainDetailContentViewModel<ILogsPageViewModel, ILogsPageDetailsViewModel>, ILogsPageDetailsViewModel {

        LogState _logStates = LogState.Success | LogState.Warning | LogState.Error | LogState.Info;
        public LogState LogStates {
            get {
                return _logStates;
            }
            private set {
                ChangeAndNotify(ref _logStates, value);
            }
        }

        public void Toggle(LogState logState) {
            if (_logStates.HasFlag(logState))
                LogStates &= ~logState;
            else
                LogStates |= logState;
        }
    }
}
