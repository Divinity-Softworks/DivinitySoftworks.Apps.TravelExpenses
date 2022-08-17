using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace DivinitySoftworks.Apps.TravelExpenses.UI.Converters {
    internal class IsTodayToVisibilityConverter : MarkupExtension, IValueConverter {
        public int Length { get; set; } = 64;

        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is not DateTime dateTime)
                throw new InvalidCastException(nameof(value));

            return dateTime.Date == DateTime.Now.Date ? Visibility.Visible : Visibility.Collapsed;
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