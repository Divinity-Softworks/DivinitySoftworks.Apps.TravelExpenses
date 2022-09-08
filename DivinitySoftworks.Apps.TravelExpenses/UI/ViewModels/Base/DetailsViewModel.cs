using DivinitySoftworks.Apps.Core.Data;

namespace DivinitySoftworks.Apps.TravelExpenses.UI.ViewModels.Base {

    public class DetailsViewModel<T> : ViewModel where T : class {
        protected readonly T _parent;

        public DetailsViewModel(T parent) {
            _parent = parent;
        }
    }
}
