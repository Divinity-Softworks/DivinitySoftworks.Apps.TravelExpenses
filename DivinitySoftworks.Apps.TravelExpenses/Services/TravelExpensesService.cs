using DivinitySoftworks.Apps.Core.Configuration.Managers;
using DivinitySoftworks.Apps.TravelExpenses.Core.Configuration;
using DivinitySoftworks.Apps.TravelExpenses.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DivinitySoftworks.Apps.TravelExpenses.Services {

    public interface ITravelExpensesService {

        Task<MonthlyData?> LoadAsync(int year, int month);

        Task SaveAsync(MonthlyData monthlyData);
    }

    public class TravelExpensesService : ITravelExpensesService {
        IAppSettings _appSettings;
        IConfigurationManager _configurationManager;
        DirectoryInfo _directoryInfo;

        public TravelExpensesService(IAppSettings appSettings, IConfigurationManager configurationManager) {
            _appSettings = appSettings;
            _configurationManager = configurationManager;

            _directoryInfo = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Divinity Softworks", "Travel Expenses"));
            if (!_directoryInfo.Exists) _directoryInfo.Create();
        }

        /// <inheritdoc/>
        public Task<MonthlyData?> LoadAsync(int year, int month) {
            return Task.Run(() => {
                if (year < 2000 || year > 2100) return null;
                if (month < 1 || month > 12) return null;

                DirectoryInfo directoryInfo = new(Path.Combine(_directoryInfo.FullName, _appSettings.DataPath));
                if (!directoryInfo.Exists) _directoryInfo.Create();
                FileInfo fileInfo = new(Path.Combine(directoryInfo.FullName, $"{year}{month.ToString().PadLeft(2, '0')}.dsdata"));
                if (!fileInfo.Exists) return new MonthlyData(year, month);

                return JsonConvert.DeserializeObject<MonthlyData>(File.ReadAllText(fileInfo.FullName));
            });
        }

        public Task SaveAsync(MonthlyData monthlyData) {
            return Task.Run(() => {
                DirectoryInfo directoryInfo = new(Path.Combine(_directoryInfo.FullName, _appSettings.DataPath));
                if (!directoryInfo.Exists) directoryInfo.Create();
                FileInfo fileInfo = new(Path.Combine(directoryInfo.FullName, $"{monthlyData.Year}{monthlyData.Month.ToString().PadLeft(2, '0')}.dsdata"));
                
                File.WriteAllText(fileInfo.FullName, JsonConvert.SerializeObject(monthlyData, Formatting.Indented));
            });
        }
    }
}
