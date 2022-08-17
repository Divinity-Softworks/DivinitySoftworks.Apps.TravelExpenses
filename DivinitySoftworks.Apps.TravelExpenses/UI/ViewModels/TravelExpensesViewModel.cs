using DivinitySoftworks.Apps.Core.Data;
using DivinitySoftworks.Apps.TravelExpenses.Data.Models;
using DivinitySoftworks.Apps.TravelExpenses.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DivinitySoftworks.Apps.TravelExpenses.UI.ViewModels {

    public interface ITravelExpensesViewModel {
        int Year { get; }

        int Month { get; }

        string Title { get; }

        void AddMonths(int amount);

        Task UpdateStateAsync(DateItem dayItem);
    }

    public class TravelExpensesViewModel : ViewModel, ITravelExpensesViewModel {
        readonly ITravelExpensesService _travelExpensesService;

        public TravelExpensesViewModel(ITravelExpensesDetailsViewModel travelExpensesDetailsViewModel, ITravelExpensesService travelExpensesService) {
            _travelExpensesService = travelExpensesService;

            Details = travelExpensesDetailsViewModel;
            Date = DateOnly.FromDateTime(DateTime.Now);
        }

        ITravelExpensesDetailsViewModel? _details;
        public ITravelExpensesDetailsViewModel Details {
            get {
                if (_details is null) throw new NullReferenceException(nameof(Details));
                return _details;
            }
            set {
                ChangeAndNotify(ref _details, value);
            }
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

        public void AddMonths(int amount) {
            Date = Date.AddMonths(amount);
        }

        public async Task UpdateStateAsync(DateItem dayItem) {
            dayItem.State = dayItem.State == Data.Enums.DayState.Office ? Data.Enums.DayState.Unset : Data.Enums.DayState.Office;
            if (MonthlyData is null) return;
            MonthlyData.Days = new ObservableCollection<DateItem>(Days.Where(d => d.State == Data.Enums.DayState.Office));
            await _travelExpensesService.SaveAsync(MonthlyData);
        }

        protected async override void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
            base.OnPropertyChanged(propertyName);

            if (propertyName != nameof(Date)) return;

            MonthlyData = await _travelExpensesService.LoadAsync(Date.Year, Date.Month);

            Days.Clear();

            DateTime dateTime = new DateTime(Year, Month, 1).Date;
            while (dateTime.DayOfWeek is not DayOfWeek.Sunday)
                dateTime = dateTime.AddDays(-1);

            while (dateTime < new DateTime(Year, Month, 1).AddMonths(1).Date) {
                DateItem dateItem = new() {
                    Date = dateTime,
                    State = MonthlyData?.Days.FirstOrDefault(i => i.Date == dateTime)?.State ?? Data.Enums.DayState.Unset

                };

                Days.Add(dateItem);

                dateTime = dateTime.AddDays(1);
            }
        }
    }
}