using DivinitySoftworks.Apps.Core.Data;
using System;
using System.Collections.ObjectModel;

namespace DivinitySoftworks.Apps.TravelExpenses.Data.Models {
    public class MonthlyData : ViewModel {

        public MonthlyData(int year, int month) {
            Year = year;
            Month = month;
        }

        DateTime? _exported = null;
        public DateTime? Exported {
            get {
                return _exported;
            }
            set {
                ChangeAndNotify(ref _exported, value);
            }
        }

        int _year = DateTime.Now.Year;
        public int Year {
            get {
                return _year;
            }
            set {
                ChangeAndNotify(ref _year, value);
            }
        }

        int _month = DateTime.Now.Month;
        public int Month {
            get {
                return _month;
            }
            set {
                ChangeAndNotify(ref _month, value);
            }
        }

        ObservableCollection<DateItem> _days = new();
        public ObservableCollection<DateItem> Days {
            get {
                return _days;
            }
            set {
                ChangeAndNotify(ref _days, value);
            }
        }
    }
}
