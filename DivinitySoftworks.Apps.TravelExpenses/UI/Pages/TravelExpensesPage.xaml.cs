using DivinitySoftworks.Apps.Core.Components;
using DivinitySoftworks.Apps.TravelExpenses.Data.Enums;
using DivinitySoftworks.Apps.TravelExpenses.Data.Models;
using DivinitySoftworks.Apps.TravelExpenses.UI.Pages.Base;
using DivinitySoftworks.Apps.TravelExpenses.UI.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;

namespace DivinitySoftworks.Apps.TravelExpenses.UI.Pages {

    public partial class TravelExpensesPage : ContentPage {
        public TravelExpensesPage(ITravelExpensesViewModel travelExpensesViewModel) {
            InitializeComponent();

            DataContext = travelExpensesViewModel;
        }

        public ITravelExpensesViewModel ViewModel {
            get {
                return (ITravelExpensesViewModel)DataContext;
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

        private void OnToggleButtonClick(object sender, System.Windows.RoutedEventArgs e) {
            if (sender is not ToggleButton toggleButton) return;
            if (toggleButton.Value is not CalendarRange calendarRange) return;
            ViewModel.Details.CalendarRange = calendarRange;
        }

        private async void OnExport(object sender, System.Windows.RoutedEventArgs e) {
            await ViewModel.ExportAsync();
        }
    }
}
