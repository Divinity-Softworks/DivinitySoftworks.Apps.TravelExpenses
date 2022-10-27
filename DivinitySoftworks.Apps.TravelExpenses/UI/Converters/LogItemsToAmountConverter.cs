using DivinitySoftworks.Apps.TravelExpenses.Data.Enums;
using DivinitySoftworks.Apps.TravelExpenses.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;

namespace DivinitySoftworks.Apps.TravelExpenses.UI.Converters {
    internal class LogItemsToAmountConverter : MarkupExtension, IValueConverter {
        /// <summary>
        /// The <see cref="LogState"/> to count in the list.
        /// </summary>
        public LogState LogState { get; set; } = LogState.Initial;

        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is not IEnumerable<LogItem> logItems)
                throw new InvalidCastException(nameof(value));

            return logItems.Count(l => l.State == LogState);
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