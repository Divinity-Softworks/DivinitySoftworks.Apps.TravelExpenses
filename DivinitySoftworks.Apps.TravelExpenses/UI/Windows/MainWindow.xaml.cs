using DivinitySoftworks.Apps.Core.Components;
using DivinitySoftworks.Apps.TravelExpenses.UI.ViewModels;
using System;
using System.Windows;

namespace DivinitySoftworks.Apps.TravelExpenses.UI.Windows {

    public partial class MainWindow : Window {
        public MainWindow(IMainWindowViewModel mainWindowViewModel) {
            InitializeComponent();

            DataContext = mainWindowViewModel;
        }

        public IMainWindowViewModel ViewModel {
            get {
                return (IMainWindowViewModel)DataContext;
            }
        }

        private void OnMenuItemClick(object sender, RoutedEventArgs e) {
            ViewModel.SetPage(((MenuItem)sender).Target);
        }

        private void OnWindowStateChanged(object sender, EventArgs e) {
            BorderThickness = WindowState == WindowState.Maximized
                ? new Thickness(0, 0, 0, SystemParameters.PrimaryScreenHeight - SystemParameters.FullPrimaryScreenHeight - SystemParameters.WindowCaptionHeight)
                : new Thickness(0, 0, 0, 0);
        }

        private async void OnWindowLoaded(object sender, RoutedEventArgs e) {
            await ViewModel.LoadAsync();
        }

        private void OnMinimize(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            WindowState = WindowState.Minimized;

        }
        private void OnMaximize(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;

        }
        private void OnClose(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            Close();
        }
    }
}
