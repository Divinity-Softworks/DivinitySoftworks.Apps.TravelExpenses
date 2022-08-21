using DivinitySoftworks.Apps.Core.Configuration.Managers;
using DivinitySoftworks.Apps.TravelExpenses.Core.Handlers;
using MediatR;
using System.Threading.Tasks;

namespace DivinitySoftworks.Apps.TravelExpenses.Core.Configuration {

    public interface IUserSettings {

        string? Name { get; set; }
        string? Department { get; set; }
        string? Manager { get; set; }
        string? WorkAddress { get; set; }
        string? HomeAddress { get; set; }
        int? Kilometers { get; set; }
        double? Price { get; set; }

        ValueTask LoadAsync();
    }

    internal class UserSettings : IUserSettings {
        readonly IConfigurationManager _configurationManager;
        readonly IMediator _mediator;

        public UserSettings(IMediator mediator, IConfigurationManager configurationManager) {
            _mediator = mediator;
            _configurationManager = configurationManager;
        }

        string? _name;
        public string? Name {
            get {
                return _name;
            }
            set {
                _name = value;
                _mediator.Send(new ConfigurationItem(nameof(Name), _name));
            }
        }

        string? _department;
        public string? Department {
            get {
                return _department;
            }
            set {
                _department = value;
                _mediator.Send(new ConfigurationItem(nameof(Department), _department));
            }
        }

        string? _manager;
        public string? Manager {
            get {
                return _manager;
            }
            set {
                _manager = value;
                _mediator.Send(new ConfigurationItem(nameof(Manager), _manager));
            }
        }

        string? _workAddress;
        public string? WorkAddress {
            get {
                return _workAddress;
            }
            set {
                _workAddress = value;
                _mediator.Send(new ConfigurationItem(nameof(WorkAddress), _workAddress));
            }
        }

        string? _homeAddress;
        public string? HomeAddress {
            get {
                return _homeAddress;
            }
            set {
                _homeAddress = value;
                _mediator.Send(new ConfigurationItem(nameof(HomeAddress), _homeAddress));
            }
        }

        int? _kilometers;
        public int? Kilometers {
            get {
                return _kilometers;
            }
            set {
                _kilometers = value;
                _mediator.Send(new ConfigurationItem(nameof(Kilometers), _kilometers));
            }
        }

        double? _price;
        public double? Price {
            get {
                return _price;
            }
            set {
                _price = value;
                _mediator.Send(new ConfigurationItem(nameof(Price), _price));
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
    }
}
