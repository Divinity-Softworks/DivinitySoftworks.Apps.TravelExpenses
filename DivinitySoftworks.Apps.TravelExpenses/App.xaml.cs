using DivinitySoftworks.Apps.TravelExpenses.Core.Configuration;
using DivinitySoftworks.Apps.TravelExpenses.Services;
using DivinitySoftworks.Apps.TravelExpenses.UI.Pages;
using DivinitySoftworks.Apps.TravelExpenses.UI.ViewModels;
using DivinitySoftworks.Apps.TravelExpenses.UI.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace DivinitySoftworks.Apps.TravelExpenses {

    /// <summary>
    /// Divinity Softworks Stream Loader Application.
    /// </summary>
    public class Application : Apps.Core.App<IAppSettings, AppSettings> {
    }

    /// <inheritdoc/>
    public partial class App : Application {

        /// <inheritdoc/>
        protected override void OnStartup(System.Windows.StartupEventArgs e) {
            base.OnStartup(e);

            ServiceProvider.GetRequiredService<MainWindow>().Show();
        }

        /// <inheritdoc/>
        override protected void ConfigureServices(IServiceCollection services) {
            base.ConfigureServices(services);
            services.AddSingleton<ILogService, LogService>();
            services.AddSingleton<ITravelExpensesService, TravelExpensesService>();

            services.AddTransient<IMainWindowViewModel, MainWindowViewModel>();
            services.AddTransient<ITravelExpensesDetailsViewModel, TravelExpensesDetailsViewModel>();
            services.AddTransient<ITravelExpensesViewModel, TravelExpensesViewModel>();
            services.AddTransient<ILogsPageViewModel, LogsPageViewModel>();
            services.AddTransient<ISettingsPageViewModel, SettingsPageViewModel>();

            services.AddTransient(typeof(MainWindow));
            services.AddTransient(typeof(TravelExpensesPage));
            services.AddTransient(typeof(LogsPage));
            services.AddTransient(typeof(SettingsPage));
        }
    }
}
