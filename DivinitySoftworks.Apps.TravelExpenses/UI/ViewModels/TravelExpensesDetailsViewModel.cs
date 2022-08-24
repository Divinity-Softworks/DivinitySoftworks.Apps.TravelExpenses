using DivinitySoftworks.Apps.Core.Data;
using DivinitySoftworks.Apps.TravelExpenses.Data.Enums;

namespace DivinitySoftworks.Apps.TravelExpenses.UI.ViewModels {

    public interface ITravelExpensesDetailsViewModel {
        CalendarRange CalendarRange { get; set; }
        ITravelExpensesViewModel? Main { get; set; }
    }

    public class TravelExpensesDetailsViewModel : ViewModel, ITravelExpensesDetailsViewModel {

        CalendarRange _calendarRange = CalendarRange.Month;
        public CalendarRange CalendarRange {
            get {
                return _calendarRange;
            }
            set {
                ChangeAndNotify(ref _calendarRange, value);
            }
        }

        ITravelExpensesViewModel? _main;
        public ITravelExpensesViewModel? Main {
            get {
                return _main;
            }
            set {
                ChangeAndNotify(ref _main, value);
            }
        }
    }
}
