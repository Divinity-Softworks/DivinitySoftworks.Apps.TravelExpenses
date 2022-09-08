using DivinitySoftworks.Apps.Core.Data;

namespace DivinitySoftworks.Apps.TravelExpenses.UI.ViewModels.TravelExpensesPage {
    
    public interface ITravelExpensesPageCollectionViewModel : IMainDetailViewModel<ITravelExpensesPageViewModel, ITravelExpensesPageDetailsViewModel> {
    }


    public class TravelExpensesPageCollectionViewModel : MainDetailViewModel<ITravelExpensesPageViewModel, ITravelExpensesPageDetailsViewModel>, ITravelExpensesPageCollectionViewModel {
        public TravelExpensesPageCollectionViewModel(ITravelExpensesPageViewModel main, ITravelExpensesPageDetailsViewModel details) : base(main, details) {
        }
    }
}
