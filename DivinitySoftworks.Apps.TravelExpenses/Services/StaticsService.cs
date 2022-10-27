using DivinitySoftworks.Apps.TravelExpenses.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DivinitySoftworks.Apps.TravelExpenses.Services {

    public interface IStaticsService {
        /// <summary>
        /// The statistics of the application have changed.
        /// </summary>
        event EventHandler<EventArgs>? OnStaticsChanged;
        ObservableCollection<StaticsItem> Statics { get; }
        void Load();
    }

    internal sealed class StaticsService : IStaticsService {
        /// <inheritdoc/>
        public event EventHandler<EventArgs>? OnStaticsChanged;

        readonly FileSystemWatcher _fileSystemWatcher;

        readonly DirectoryInfo _directoryInfo;

        public StaticsService() {
            _directoryInfo = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Divinity Softworks", "Travel Expenses", "Data"));
            if (!_directoryInfo.Exists) _directoryInfo.Create();

            _fileSystemWatcher = new FileSystemWatcher(_directoryInfo.FullName) {
                Filter = "*.dsdata",
                NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size
            };
            // Add Event Handlers.  
            _fileSystemWatcher.Changed += new FileSystemEventHandler(OnChanged);
            _fileSystemWatcher.Created += new FileSystemEventHandler(OnChanged);
            _fileSystemWatcher.Deleted += new FileSystemEventHandler(OnChanged);
            _fileSystemWatcher.Renamed += new RenamedEventHandler(OnRenamed);
        }
        public ObservableCollection<StaticsItem> Statics { get; set; } = new ObservableCollection<StaticsItem>();

        public void Load() {
            foreach (FileInfo fileInfo in _directoryInfo.GetFiles()) {
                LoadFile(fileInfo, false);
            }
            OnStaticsChanged?.Invoke(this, new EventArgs());
        }

        private void LoadFile(string fileName) {
            LoadFile(new FileInfo(fileName));
        }

        private void LoadFile(FileInfo fileInfo, bool raiseEvent = true) {
            try {
                //Stop Monitoring.  
                _fileSystemWatcher.EnableRaisingEvents = false;

                if (!fileInfo.Exists) return;
                if (fileInfo.Extension != ".dsdata") return;

                string data = string.Empty;

                using (FileStream fileStream = File.Open(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
                    byte[] bytes = new byte[10240];
                    UTF8Encoding encoding = new(true);

                    while (fileStream.Read(bytes, 0, bytes.Length) > 0)
                        data += encoding.GetString(bytes);
                }

                MonthlyData? monthlyData = JsonConvert.DeserializeObject<MonthlyData>(data);
                if (monthlyData is null) return;
                Application.Current.Dispatcher.Invoke(delegate {
                    Statics.RemoveAll(s => s.DateTime.Year == monthlyData.Year && s.DateTime.Month == monthlyData.Month);
                    foreach (DateItem dateItem in monthlyData.Days) {
                        if (dateItem.State != Data.Enums.DayState.Office) continue;
                        Statics.Add(new StaticsItem() {
                            DateTime = dateItem.Date,
                            Kilometers = int.Parse(dateItem.Kilometers) * 2,
                            Price = dateItem.Price * (int.Parse(dateItem.Kilometers) * 2)
                        });
                    }
                });

                if(raiseEvent)
                    OnStaticsChanged?.Invoke(this, new EventArgs());
            }
            finally {
                //Start Monitoring.  
                _fileSystemWatcher.EnableRaisingEvents = true;
            }
        }

        private void OnChanged(object sender, FileSystemEventArgs e) {
            LoadFile(e.FullPath);
        }

        private void OnRenamed(object sender, RenamedEventArgs e) {
            LoadFile(e.FullPath);
        }

    }
}
