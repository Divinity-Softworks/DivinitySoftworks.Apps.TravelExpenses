using DivinitySoftworks.Apps.TravelExpenses.Data.Enums;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace DivinitySoftworks.Apps.TravelExpenses.UI.Converters {
    internal class LogStateToIconConverter : MarkupExtension, IValueConverter {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is not LogState)
                throw new InvalidCastException(nameof(value));
            switch ((LogState)value) {
                case LogState.Error:
                case LogState.Info:
                case LogState.Warning:
                case LogState.Success:
                    return System.Windows.Application.Current.FindResource($"Icons.{value}");
                default:
                    return System.Windows.Application.Current.FindResource("Icons.Question");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
}
