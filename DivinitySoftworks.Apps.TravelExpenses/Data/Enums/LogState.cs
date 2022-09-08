using System;

namespace DivinitySoftworks.Apps.TravelExpenses.Data.Enums {
    [Flags]
    public enum LogState {
        Initial = 0,
        Success = 1,
        Info = 2,
        Warning = 4,
        Error = 8,
    }
}
