using DivinitySoftworks.Apps.Core.Components;
using DivinitySoftworks.Apps.TravelExpenses.Data.Enums;
using DivinitySoftworks.Apps.TravelExpenses.UI.Details.Base;
using DivinitySoftworks.Apps.TravelExpenses.UI.ViewModels.TravelExpensesPage;
using System.Windows;

namespace DivinitySoftworks.Apps.TravelExpenses.UI.Details {

    public class TravelExpensesPageBaseDetails : PageDetails<ITravelExpensesPageDetailsViewModel> { }
    
    public partial class TravelExpensesPageDetails : TravelExpensesPageBaseDetails {
        public TravelExpensesPageDetails() : base() {
            InitializeComponent();
        }

        private void OnToggleButtonClick(object sender, RoutedEventArgs e) {
            if (sender is not ToggleButton toggleButton) return;
            if (toggleButton.Value is not CalendarRange calendarRange) return;
            ViewModel.CalendarRange = calendarRange;
        }

        private async void OnExport(object sender, RoutedEventArgs e) {
            if (ViewModel.ContentGroup.Main is null) return;
            await ViewModel.ContentGroup.Main.ExportAsync();
        }

        private async void OnLoaded(object sender, RoutedEventArgs e) {
            await ViewModel.LoadAsync();
        }

        private void OnUnloaded(object sender, RoutedEventArgs e) {
            ViewModel.Dispose();
        }
    }
}
