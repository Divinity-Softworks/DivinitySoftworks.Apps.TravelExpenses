using DivinitySoftworks.Apps.Core.Components;
using DivinitySoftworks.Apps.TravelExpenses.Data.Enums;
using DivinitySoftworks.Apps.TravelExpenses.Data.Models;
using DivinitySoftworks.Apps.TravelExpenses.UI.ViewModels.TravelExpensesPage;
using System.Windows.Controls;
using System.Windows.Input;

namespace DivinitySoftworks.Apps.TravelExpenses.UI.Pages {

    public partial class TravelExpensesPage : Base.ContentPage {
        public TravelExpensesPage(ITravelExpensesPageCollectionViewModel collectionViewModel) {
            InitializeComponent();

            DataContext = collectionViewModel.Main;
        }

        public ITravelExpensesPageViewModel ViewModel {
            get {
                return (ITravelExpensesPageViewModel)DataContext;
            }
        }

        private async void OnDay_Clicked(object sender, MouseButtonEventArgs e) {
            if (sender is not Border border) return;
            if (border.DataContext is not DateItem dayItem) return;
            await ViewModel.UpdateStateAsync(dayItem);
        }

        private void OnSubstractMonth_Clicked(object sender, MouseButtonEventArgs e) {
            ViewModel.AddMonths(-1);
        }

        private void OnAddMonth_Clicked(object sender, MouseButtonEventArgs e) {
            ViewModel.AddMonths(1);
        }
    }
}
