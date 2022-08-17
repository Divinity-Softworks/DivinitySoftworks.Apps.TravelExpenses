using DivinitySoftworks.Apps.Core.Data;
using DivinitySoftworks.Apps.TravelExpenses.Data.Enums;
using System;

namespace DivinitySoftworks.Apps.TravelExpenses.Data.Models {
    public class DateItem : ViewModel {
        public DateTime Date { get; set; }

        private DayState _state = DayState.Unset;
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
    }
}
