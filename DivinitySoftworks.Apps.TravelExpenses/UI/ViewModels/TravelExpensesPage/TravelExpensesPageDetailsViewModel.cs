using DivinitySoftworks.Apps.Core.Data;
using DivinitySoftworks.Apps.TravelExpenses.Data.Enums;
using DivinitySoftworks.Apps.TravelExpenses.Data.Models;
using DivinitySoftworks.Apps.TravelExpenses.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DivinitySoftworks.Apps.TravelExpenses.UI.ViewModels.TravelExpensesPage {

    public interface ITravelExpensesPageDetailsViewModel : IMainDetailContentViewModel<ITravelExpensesPageViewModel, ITravelExpensesPageDetailsViewModel>, IDisposable {
        CalendarRange CalendarRange { get; set; }

        ObservableCollection<StaticsItem> Statics { get; }

        Task LoadAsync();
    }

    public class TravelExpensesPageDetailsViewModel : MainDetailContentViewModel<ITravelExpensesPageViewModel, ITravelExpensesPageDetailsViewModel>, ITravelExpensesPageDetailsViewModel, IDisposable {
        readonly ILogService _logService;
        readonly IStaticsService _staticsService;

        public TravelExpensesPageDetailsViewModel(ILogService logService, IStaticsService staticsService) {
            _logService = logService;
            _staticsService = staticsService;
            _staticsService.OnStaticsChanged += OnStaticsChanged;
        }

        CalendarRange _calendarRange = CalendarRange.Month;
        public CalendarRange CalendarRange {
            get {
                return _calendarRange;
            }
            set {
                ChangeAndNotify(ref _calendarRange, value);
            }
        }

        public ObservableCollection<StaticsItem> Statics {
            get {
                return _staticsService.Statics;
            }
        }

        private void OnStaticsChanged(object? sender, EventArgs e) {
            Notify(nameof(Statics));
        }

        public Task LoadAsync() {
            return Task.Run(() => {
                _staticsService.Load();
            });
        }

        public void Dispose() {
            _staticsService.OnStaticsChanged -= OnStaticsChanged;
        }
    }
}
