﻿using DivinitySoftworks.Apps.Core.Configuration.Managers;
using DivinitySoftworks.Apps.Core.Data;
using DivinitySoftworks.Apps.TravelExpenses.Data.Models;
using DivinitySoftworks.Apps.TravelExpenses.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;

using Pages = DivinitySoftworks.Apps.TravelExpenses.UI.Pages;

namespace DivinitySoftworks.Apps.TravelExpenses.UI.ViewModels {

    public interface IMainWindowViewModel {

        string Name { get; set; }

        Page? Page { get; set; }

        void SetPage(Type target);

        ValueTask LoadAsync();
    }

    public class MainWindowViewModel : ViewModel, IMainWindowViewModel {
        readonly IConfigurationManager _configurationManager;
        readonly ILogService _logService;

        readonly Dictionary<Type, Page> _pages = new();

        public MainWindowViewModel(IConfigurationManager configurationManager, ILogService logService, Pages.TravelExpensesPage traveExpensesPage, Pages.LogsPage logsPage, Pages.SettingsPage settingsPage) {
            _configurationManager = configurationManager;
            _configurationManager.OnConfigurationChanged += ConfigurationManager_OnConfigurationChanged;

            _logService = logService;

            _pages.Add(typeof(Pages.TravelExpensesPage), traveExpensesPage);
            _pages.Add(typeof(Pages.LogsPage), logsPage);
            _pages.Add(typeof(Pages.SettingsPage), settingsPage);

            Page = traveExpensesPage;
        }


        public string _name = "Mystery Guest";
        public string Name {
            get {
                return _name;
            }
            set {
                ChangeAndNotify(ref _name, value);
            }
        }

        Page? _page;
        public Page? Page {
            get {
                return _page;
            }
            set {
                ChangeAndNotify(ref _page, value);
            }
        }

        public FixedObservableCollection<LogItem> LogItems {
            get {
                return _logService.LogItems;
            }
        }

        public void SetPage(Type target) {
            if (_pages.ContainsKey(target)) {
                Page = _pages[target];
                return;
            }

            _page = _pages.First().Value;
        }

        public async ValueTask LoadAsync() {
            string? name = await _configurationManager.GetUserSettingAsync<string?>(nameof(Name));
            string? department = await _configurationManager.GetUserSettingAsync<string?>("Department");
            string? manager = await _configurationManager.GetUserSettingAsync<string?>("Manager");
            string? workAddress = await _configurationManager.GetUserSettingAsync<string?>("WorkAddress");
            string? homeAddress = await _configurationManager.GetUserSettingAsync<string?>("HomeAddress");
            int? kilometers = await _configurationManager.GetUserSettingAsync<int?>("Kilometers");
            double? price = await _configurationManager.GetUserSettingAsync<double?>("Price");
            Name = name ?? "Mystery Guest";

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(department) || string.IsNullOrWhiteSpace(manager) || string.IsNullOrWhiteSpace(workAddress) || string.IsNullOrWhiteSpace(homeAddress) || kilometers is null || price is null)
                SetPage(typeof(Pages.SettingsPage));
        }

        private async void ConfigurationManager_OnConfigurationChanged(object? sender, ConfigurationChangedEventArgs e) {
            if (e.Key != nameof(Name)) return;
            Name = await _configurationManager.GetUserSettingAsync<string>(e.Key) ?? string.Empty;
        }
    }
}
