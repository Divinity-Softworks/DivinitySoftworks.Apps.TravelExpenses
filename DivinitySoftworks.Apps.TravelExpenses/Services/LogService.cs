using DivinitySoftworks.Apps.TravelExpenses.Core.Configuration;
using DivinitySoftworks.Apps.TravelExpenses.Data.Enums;
using DivinitySoftworks.Apps.TravelExpenses.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace DivinitySoftworks.Apps.TravelExpenses.Services {
    public interface ILogService {
        /// <summary>
        /// The logs of the application has changed.
        /// </summary>
        event EventHandler<EventArgs>? OnLogsChanged;

        public FixedObservableCollection<LogItem> LogItems { get; set; }

        public ValueTask LoadAsync();

        void LogException(Exception exception, string message);

        void LogInfo(string message, string details);

        void LogWarning(string message, string details);

        void LogError(string message, string details);

        void LogSuccess(string message, string details);
    }

    public class LogService : ILogService {
        readonly static int _maxItems = 500;

        readonly FileInfo _fileInfo;

        /// <inheritdoc/>
        public event EventHandler<EventArgs>? OnLogsChanged;

        public LogService(IAppSettings appSettings) {
            _fileInfo = new FileInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Divinity Softworks", "Travel Expenses", appSettings.LogsPath, "logs.dsdata"));
            if (_fileInfo.Directory is null || !_fileInfo.Directory.Exists) _fileInfo.Directory?.Create();
        }

        public FixedObservableCollection<LogItem> LogItems { get; set; } = new FixedObservableCollection<LogItem>(_maxItems);

        public async ValueTask LoadAsync() {
            if (!_fileInfo.Exists) return;

            LogItems = JsonConvert.DeserializeObject<FixedObservableCollection<LogItem>>(await File.ReadAllTextAsync(_fileInfo.FullName)) ?? new FixedObservableCollection<LogItem>(_maxItems);

            OnLogsChanged?.Invoke(this, new EventArgs());
        }

        private void Log(LogState state, string message, string details) {
            LogItems.Add(new LogItem() {
                State = state,
                Message = message,
                Details = details,
                DateTime = DateTime.Now
            });

            File.WriteAllText(_fileInfo.FullName, JsonConvert.SerializeObject(LogItems, Formatting.Indented));
        }

        public void LogException(Exception exception, string message) {
            LogError(message, $"{exception.Message}\r\n[StackTrace] {exception.StackTrace}");
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