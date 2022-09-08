using DivinitySoftworks.Apps.Core.Components;
using DivinitySoftworks.Apps.TravelExpenses.Data.Enums;
using DivinitySoftworks.Apps.TravelExpenses.UI.Details.Base;
using DivinitySoftworks.Apps.TravelExpenses.UI.ViewModels.LogsPage;
using System.Windows;

namespace DivinitySoftworks.Apps.TravelExpenses.UI.Details {

    public class LogsPageBaseDetails : PageDetails<ILogsPageDetailsViewModel> { }

    public partial class LogsPageDetails : LogsPageBaseDetails {
        public LogsPageDetails() : base() {
            InitializeComponent();
        }

        private void OnToggleButtonClick(object sender, RoutedEventArgs e) {
            if (sender is null || sender is not ToggleButton toggleButton) return;
            if (toggleButton.Value is null || toggleButton.Value is not LogState logState) return;
            ViewModel.Toggle(logState);            
        }
    }
}
