using DivinitySoftworks.Apps.Core.Data;
using DivinitySoftworks.Apps.TravelExpenses.Data.Enums;

namespace DivinitySoftworks.Apps.TravelExpenses.UI.ViewModels.TravelExpensesPage {

    public interface ITravelExpensesPageDetailsViewModel : IMainDetailContentViewModel<ITravelExpensesPageViewModel, ITravelExpensesPageDetailsViewModel> {
        CalendarRange CalendarRange { get; set; }
    }

    public class TravelExpensesPageDetailsViewModel : MainDetailContentViewModel<ITravelExpensesPageViewModel, ITravelExpensesPageDetailsViewModel>, ITravelExpensesPageDetailsViewModel {

        CalendarRange _calendarRange = CalendarRange.Month;
        public CalendarRange CalendarRange {
            get {
                return _calendarRange;
            }
            set {
                ChangeAndNotify(ref _calendarRange, value);
            }
        }
    }
}
