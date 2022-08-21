using DivinitySoftworks.Apps.Core.Data;
using DivinitySoftworks.Apps.TravelExpenses.Core.Configuration;

namespace DivinitySoftworks.Apps.TravelExpenses.UI.ViewModels {

    public interface ISettingsPageViewModel {

        string? Name { get; set; }

        string? Department { get; set; }

        string? Manager { get; set; }

        string? WorkAddress { get; set; }

        string? HomeAddress { get; set; }

        int? Kilometers { get; set; }

        double? Price { get; set; }
    }

    public class SettingsPageViewModel : ViewModel, ISettingsPageViewModel {
        readonly IUserSettings _userSettings;

        public SettingsPageViewModel(IUserSettings userSettings) {
            _userSettings = userSettings;
        }

        public string? Name {
            get {
                return _userSettings.Name;
            }
            set {
                _userSettings.Name = value;
                Notify();
            }
        }

        public string? Department {
            get {
                return _userSettings.Department;
            }
            set {
                _userSettings.Department = value;
                Notify();
            }
        }

        public string? Manager {
            get {
                return _userSettings.Manager;
            }
            set {
                _userSettings.Manager = value;
                Notify();
            }
        }

        public string? WorkAddress {
            get {
                return _userSettings.WorkAddress;
            }
            set {
                _userSettings.WorkAddress = value;
                Notify();
            }
        }

        public string? HomeAddress {
            get {
                return _userSettings.HomeAddress;
            }
            set {
                _userSettings.HomeAddress = value;
                Notify();
            }
        }

        public int? Kilometers {
            get {
                return _userSettings.Kilometers;
            }
            set {
                _userSettings.Kilometers = value;
                Notify();
            }
        }

        public double? Price {
            get {
                return _userSettings.Price;
            }
            set {
                _userSettings.Price = value;
                Notify();
            }
        }
    }
}