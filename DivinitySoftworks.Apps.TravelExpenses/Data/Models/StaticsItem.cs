using System;

namespace DivinitySoftworks.Apps.TravelExpenses.Data.Models {
    public sealed class StaticsItem {

        public DateTime DateTime { get; set; }

        public int WeekNumber {
            get {
                return DateTime.GetWeekNumber();
            }
        }

        public int Kilometers { get; set; }
        public double? Price { get; set; }
    }
}
