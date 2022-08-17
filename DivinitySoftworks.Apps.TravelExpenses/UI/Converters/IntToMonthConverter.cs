using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace DivinitySoftworks.Apps.TravelExpenses.UI.Converters {
    internal class IntToMonthConverter : MarkupExtension, IValueConverter {
        public int Length { get; set; } = 64;

        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is not int monthIndex)
                throw new InvalidCastException(nameof(value));
            
            string month = new DateTime(2000, monthIndex, 1).ToString("MMMM");

            if (month.Length < Length)
                return month;

            return month[..Length];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
}