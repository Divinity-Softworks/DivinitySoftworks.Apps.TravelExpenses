using DivinitySoftworks.Apps.TravelExpenses.Core.Configuration;
using DivinitySoftworks.Apps.TravelExpenses.Services;
using DivinitySoftworks.Apps.TravelExpenses.UI.Pages;
using DivinitySoftworks.Apps.TravelExpenses.UI.ViewModels;
using DivinitySoftworks.Apps.TravelExpenses.UI.ViewModels.LogsPage;
using DivinitySoftworks.Apps.TravelExpenses.UI.ViewModels.TravelExpensesPage;
using DivinitySoftworks.Apps.TravelExpenses.UI.Windows;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Threading;

namespace DivinitySoftworks.Apps.TravelExpenses {

    /// <summary>
    /// Divinity Softworks Stream Loader Application.
    /// </summary>
    public class Application : Apps.Core.App<IAppSettings, AppSettings> {
    }

    /// <inheritdoc/>
    public partial class App : Application {

        /// <inheritdoc/>
        protected override async void OnStartup(System.Windows.StartupEventArgs e) {
            base.OnStartup(e);
            
            // Set culture to en-US
            CultureInfo culture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            await ServiceProvider.GetRequiredService<IUserSettings>().LoadAsync();
            await ServiceProvider.GetRequiredService<ILogService>().LoadAsync();

            ServiceProvider.GetRequiredService<MainWindow>().Show();
        }

        /// <inheritdoc/>
        override protected void ConfigureServices(IServiceCollection services) {
            base.ConfigureServices(services);
            services.AddSingleton<ILogService, LogService>();
            services.AddSingleton<IStaticsService, StaticsService>();
            services.AddSingleton<ITravelExpensesService, TravelExpensesService>();

            services.AddMediatR(typeof(App));

            services.AddSingleton<IUserSettings, UserSettings>();

            services.AddTransient<IMainWindowViewModel, MainWindowViewModel>();

            services.AddSingleton<ITravelExpensesPageCollectionViewModel, TravelExpensesPageCollectionViewModel>();
            services.AddSingleton<ITravelExpensesPageDetailsViewModel, TravelExpensesPageDetailsViewModel>();
            services.AddSingleton<ITravelExpensesPageViewModel, TravelExpensesPageViewModel>();

            services.AddSingleton<ILogsPageDetailsViewModel, LogsPageDetailsViewModel>();
            services.AddSingleton<ILogsPageViewModel, LogsPageViewModel>();
            services.AddSingleton<ILogsPageCollectionViewModel, LogsPageCollectionViewModel>();

            services.AddSingleton<ISettingsPageViewModel, SettingsPageViewModel>();

            services.AddSingleton(typeof(MainWindow));
            services.AddSingleton(typeof(TravelExpensesPage));
            services.AddSingleton(typeof(LogsPage));
            services.AddSingleton(typeof(SettingsPage));
        }
    }
}
