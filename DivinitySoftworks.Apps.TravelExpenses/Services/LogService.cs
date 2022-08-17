using DivinitySoftworks.Apps.TravelExpenses.Data.Enums;
using DivinitySoftworks.Apps.TravelExpenses.Data.Models;
using System.Collections.ObjectModel;

namespace DivinitySoftworks.Apps.TravelExpenses.Services {
    public interface ILogService {

        public FixedObservableCollection<LogItem> LogItems { get; set; }

        void LogInfo(string message, string details);

        void LogWarning(string message, string details);

        void LogError(string message, string details);

        void LogSuccess(string message, string details);
    }

    public class LogService : ILogService {
        readonly static int _maxItems = 500;

        public LogService() { }

        public FixedObservableCollection<LogItem> LogItems { get; set; } = new FixedObservableCollection<LogItem>(_maxItems);

        private void Log(LogState state, string message, string details) {
            LogItems.Add(new LogItem() {
                State = state,
                Message = message,
                Details = details,
                DateTime = System.DateTime.Now
            });
        }

        public void LogError(string message, string details) {
            Log(LogState.Error, message, details);
        }

        public void LogInfo(string message, string details) {
            Log(LogState.Info, message, details);
        }

        public void LogSuccess(string message, string details) {
            Log(LogState.Success, message, details);
        }

        public void LogWarning(string message, string details) {
            Log(LogState.Warning, message, details);
        }
    }
}