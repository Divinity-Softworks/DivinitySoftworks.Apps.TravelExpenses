using DivinitySoftworks.Apps.Core.Data;
using DivinitySoftworks.Apps.TravelExpenses.Data.Enums;
using Newtonsoft.Json;
using System;

namespace DivinitySoftworks.Apps.TravelExpenses.Data.Models {
    public class DateItem : ViewModel {
        public DateTime Date { get; set; }

        DayState _state = DayState.Unset;
        public DayState State {
            get {
                if (Date.DayOfWeek is DayOfWeek.Sunday || Date.DayOfWeek is DayOfWeek.Saturday) return DayState.Weekend;
                return _state;
            }
            set {
                if (Date.DayOfWeek is DayOfWeek.Sunday || Date.DayOfWeek is DayOfWeek.Saturday) _state = DayState.Weekend;
                _state = value;
                Notify();
            }
        }

        string _details = string.Empty;
        public string Details {
            get { 
                return _details;
            }
            set { 
                _details = value;
                Notify(nameof(HomeAddress), nameof(WorkAddress), nameof(Kilometers), nameof(Price));
            }
        }

        [JsonIgnore]
        public string HomeAddress { 
            get { 
                if(string.IsNullOrWhiteSpace(_details)) return string.Empty;
                return _details.Split("||")[0];
            } 
        }

        [JsonIgnore]
        public string WorkAddress {
            get {
                if (string.IsNullOrWhiteSpace(_details)) return string.Empty;
                return _details.Split("||")[1];
            }
        }

        [JsonIgnore]
        public string Kilometers {
            get {
                if (string.IsNullOrWhiteSpace(_details)) return string.Empty;
                return _details.Split("||")[2];
            }
        }

        [JsonIgnore]
        public double Price {
            get {
                if (string.IsNullOrWhiteSpace(_details)) return 0.00;
                return double.Parse($"{_details.Split("||")[3]}");
            }
        }
    }
}
