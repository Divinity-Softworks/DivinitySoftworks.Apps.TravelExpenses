using DivinitySoftworks.Apps.Core.Data;
using DivinitySoftworks.Apps.TravelExpenses.Core.Configuration;
using DivinitySoftworks.Apps.TravelExpenses.Data.Models;
using DivinitySoftworks.Apps.TravelExpenses.Services;
using DivinitySoftworks.Apps.TravelExpenses.UI.Pages;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DivinitySoftworks.Apps.TravelExpenses.UI.ViewModels.TravelExpensesPage {

    public interface ITravelExpensesPageViewModel : IMainDetailContentViewModel<ITravelExpensesPageViewModel, ITravelExpensesPageDetailsViewModel> {

        DateOnly Date { get; set; }

        int Year { get; }

        int Month { get; }

        string Title { get; }

        DateTime? Exported { get; }

        void AddMonths(int amount);

        Task UpdateStateAsync(DateItem dayItem);

        Task ExportAsync();
    }

    public class TravelExpensesPageViewModel : MainDetailContentViewModel<ITravelExpensesPageViewModel, ITravelExpensesPageDetailsViewModel>, ITravelExpensesPageViewModel {
        readonly ILogService _logService;
        readonly ITravelExpensesService _travelExpensesService;
        readonly IUserSettings _userSettings;

        public TravelExpensesPageViewModel(ILogService logService, IUserSettings userSettings, ITravelExpensesService travelExpensesService, IServiceProvider serviceProvider) {
            _logService = logService;
            _userSettings = userSettings;
            _travelExpensesService = travelExpensesService;

            Date = DateOnly.FromDateTime(DateTime.Now);
        }

        DateOnly _date = DateOnly.MinValue;
        public DateOnly Date {
            get {
                return _date;
            }
            set {
                ChangeAndNotify(ref _date, value, additionalProperties: new[] { nameof(Month), nameof(Year), nameof(Title) });
            }
        }

        public int Year {
            get {
                return _date.Year;
            }
        }

        public int Month {
            get {
                return _date.Month;
            }
        }

        string _title = "Travel Expenses";
        public string Title {
            get {
                return $"{_title} - {Year}";
            }
        }

        public MonthlyData? MonthlyData {
            get; set;
        }

        ObservableCollection<DateItem> _days = new();
        public ObservableCollection<DateItem> Days {
            get {
                return _days;
            }
            set {
                ChangeAndNotify(ref _days, value);
            }
        }

        DateTime? _exported = null;
        public DateTime? Exported {
            get {
                return _exported;
            }
            set {
                ChangeAndNotify(ref _exported, value);
            }
        }

        public void AddMonths(int amount) {
            Date = Date.AddMonths(amount);
        }

        public Task UpdateStateAsync(DateItem dayItem) {
            if (string.IsNullOrWhiteSpace(_userSettings.HomeAddress) || string.IsNullOrWhiteSpace(_userSettings.HomeAddress) || _userSettings.Kilometers is null || _userSettings.Price is null) 
                return Task.CompletedTask;

            dayItem.State = dayItem.State == Data.Enums.DayState.Office ? Data.Enums.DayState.Unset : Data.Enums.DayState.Office;
            dayItem.Details = $"{_userSettings.HomeAddress}||{_userSettings.WorkAddress}||{_userSettings.Kilometers}||{_userSettings.Price}";
            return SaveAsync();
        }

        protected async override void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
            base.OnPropertyChanged(propertyName);

            if (propertyName != nameof(Date)) return;

            await LoadAsync();
        }

        public async Task ExportAsync() {
            if (MonthlyData is null) return;
            Exported = MonthlyData.Exported = DateTime.Now;
            await SaveAsync();
            await _travelExpensesService.ExportAsync(Date);
        }

        private async Task SaveAsync() {
            try {
                if (MonthlyData is null) return;
                MonthlyData.Days = new ObservableCollection<DateItem>(Days.Where(d => d.State == Data.Enums.DayState.Office));
                await _travelExpensesService.SaveAsync(MonthlyData);
                return;
            }
            catch (Exception exception) {
                _logService.LogException(exception, "An error occurred while trying to save the data.");

                await LoadAsync();
            }
        }

        private async Task LoadAsync() {
            try {
                MonthlyData = await _travelExpensesService.LoadAsync(Date.Year, Date.Month);

                Exported = MonthlyData?.Exported ?? default(DateTime?);

                Days.Clear();

                DateTime dateTime = new DateTime(Year, Month, 1).Date;
                while (dateTime.DayOfWeek is not DayOfWeek.Sunday)
                    dateTime = dateTime.AddDays(-1);

                while (dateTime < new DateTime(Year, Month, 1).AddMonths(1).Date) {
                    DateItem dateItem = new() {
                        Date = dateTime,
                        State = MonthlyData?.Days.FirstOrDefault(i => i.Date == dateTime)?.State ?? Data.Enums.DayState.Unset,
                        Details = MonthlyData?.Days.FirstOrDefault(i => i.Date == dateTime)?.Details ?? string.Empty
                    };

                    Days.Add(dateItem);

                    dateTime = dateTime.AddDays(1);
                }
            }
            catch (Exception exception) {
                _logService.LogException(exception, "An error occurred while trying to load the data.");
            }
        }
    }
}