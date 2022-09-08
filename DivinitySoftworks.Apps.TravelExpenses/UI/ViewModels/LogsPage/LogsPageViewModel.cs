﻿using DivinitySoftworks.Apps.Core.Data;
using DivinitySoftworks.Apps.TravelExpenses.Data.Models;
using DivinitySoftworks.Apps.TravelExpenses.Services;
using System.Collections.ObjectModel;

namespace DivinitySoftworks.Apps.TravelExpenses.UI.ViewModels.LogsPage {

    public interface ILogsPageViewModel : IMainDetailContentViewModel<ILogsPageViewModel, ILogsPageDetailsViewModel> {
        ObservableCollection<LogItem> LogItems { get; }
    }

    public class LogsPageViewModel : MainDetailContentViewModel<ILogsPageViewModel, ILogsPageDetailsViewModel>, ILogsPageViewModel {

        readonly ILogService _logService;

        public LogsPageViewModel(ILogService logService) {
            _logService = logService;
            _logService.OnLogsChanged += LogService_OnLogsChanged;
            LogItems = _logService.LogItems;            
        }

        ObservableCollection<LogItem> _logItems = new();
        public ObservableCollection<LogItem> LogItems {
            get {
                return _logItems;
            }
            private set {
                ChangeAndNotify(ref _logItems, value);
            }
        }

        private void LogService_OnLogsChanged(object? sender, System.EventArgs e) {
            LogItems = _logService.LogItems;
        }

    }
}
