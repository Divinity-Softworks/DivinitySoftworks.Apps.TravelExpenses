namespace DivinitySoftworks.Apps.TravelExpenses.Core.Configuration {
    public interface IAppSettings : Apps.Core.Configuration.IAppSettings {
        public string LogsPath { get; set; }
        public string ExportsPath { get; set; }
        public string DataPath { get; set; }
    }

    public class AppSettings : IAppSettings {
        public string LogsPath { get; set; } = string.Empty;
        public string ExportsPath { get; set; } = string.Empty;
        public string DataPath { get; set; } = string.Empty;
    }
}
