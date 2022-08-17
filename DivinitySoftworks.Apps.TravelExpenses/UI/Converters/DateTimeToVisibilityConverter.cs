using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace DivinitySoftworks.Apps.TravelExpenses.UI.Converters {
    internal class DateTimeToVisibilityConverter : MarkupExtension, IMultiValueConverter {

        /// <inheritdoc/>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            if (values.Length != 2) return Visibility.Hidden;
            if (values[0] is null || values[1] is null) return Visibility.Hidden;
            if (values[0] is not DateTime dateTime || values[1] is not int month) return Visibility.Hidden;
            return dateTime.Month == month ? Visibility.Visible : Visibility.Hidden;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
}
