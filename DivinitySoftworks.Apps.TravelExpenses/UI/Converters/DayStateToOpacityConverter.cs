using DivinitySoftworks.Apps.TravelExpenses.Data.Enums;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace DivinitySoftworks.Apps.TravelExpenses.UI.Converters {
    internal class DayStateToOpacityConverter : MarkupExtension, IValueConverter {

        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is not DayState)
                throw new InvalidCastException(nameof(value));

            DayState? state = (DayState)value;

            return state == DayState.Weekend ? 0.8 : 1.0;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
}
