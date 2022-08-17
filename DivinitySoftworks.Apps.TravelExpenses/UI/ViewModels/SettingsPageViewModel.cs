using DivinitySoftworks.Apps.Core.Configuration.Managers;
using DivinitySoftworks.Apps.Core.Data;
using System.Threading.Tasks;

namespace DivinitySoftworks.Apps.TravelExpenses.UI.ViewModels {

    public interface ISettingsPageViewModel {

        string? Name { get; set; }

        string? Department { get; set; }

        string? Manager { get; set; }

        string? WorkAddress { get; set; }

        string? HomeAddress { get; set; }

        int? Kilometers { get; set; }

        double? Price { get; set; }

        ValueTask LoadAsync();

        ValueTask SaveAsync();
    }

    public class SettingsPageViewModel : ViewModel, ISettingsPageViewModel {
        readonly IConfigurationManager _configurationManager;

        public SettingsPageViewModel(IConfigurationManager configurationManager) {
            _configurationManager = configurationManager;
        }

        string? _name = default;
        public string? Name {
            get {
                return _name;
            }
            set {
                ChangeAndNotify(ref _name, value);
            }
        }

        string? _department = default;
        public string? Department {
            get {
                return _department;
            }
            set {
                ChangeAndNotify(ref _department, value);
            }
        }

        string? _manager = default;
        public string? Manager {
            get {
                return _manager;
            }
            set {
                ChangeAndNotify(ref _manager, value);
            }
        }

        string? _workAddress = default;
        public string? WorkAddress {
            get {
                return _workAddress;
            }
            set {
                ChangeAndNotify(ref _workAddress, value);
            }
        }

        string? _homeAddress = default;
        public string? HomeAddress {
            get {
                return _homeAddress;
            }
            set {
                ChangeAndNotify(ref _homeAddress, value);
            }
        }

        int? _kilometers = default(int?);
        public int? Kilometers {
            get {
                return _kilometers;
            }
            set {
                ChangeAndNotify(ref _kilometers, value);
            }
        }

        double? _price = default(double?);
        public double? Price {
            get {
                return _price;
            }
            set {
                ChangeAndNotify(ref _price, value);
            }
        }

        public async ValueTask LoadAsync() {
            Name = await _configurationManager.GetUserSettingAsync<string?>(nameof(Name)) ?? default;
            Department = await _configurationManager.GetUserSettingAsync<string?>(nameof(Department)) ?? default;
            Manager = await _configurationManager.GetUserSettingAsync<string?>(nameof(Manager)) ?? default;
            WorkAddress = await _configurationManager.GetUserSettingAsync<string?>(nameof(WorkAddress)) ?? default;
            HomeAddress = await _configurationManager.GetUserSettingAsync<string?>(nameof(HomeAddress)) ?? default;
            Kilometers = await _configurationManager.GetUserSettingAsync<int?>(nameof(Kilometers)) ?? default(int?);
            Price = await _configurationManager.GetUserSettingAsync<double?>(nameof(Price)) ?? default(double?);
        }

        public async ValueTask SaveAsync() {
            await _configurationManager.SetUserSettingAsync(nameof(Name), Name);
            await _configurationManager.SetUserSettingAsync(nameof(Department), Department);
            await _configurationManager.SetUserSettingAsync(nameof(Manager), Manager);
            await _configurationManager.SetUserSettingAsync(nameof(WorkAddress), WorkAddress);
            await _configurationManager.SetUserSettingAsync(nameof(HomeAddress), HomeAddress);
            await _configurationManager.SetUserSettingAsync(nameof(Kilometers), Kilometers);
            await _configurationManager.SetUserSettingAsync(nameof(Price), Price);
        }
    }
}