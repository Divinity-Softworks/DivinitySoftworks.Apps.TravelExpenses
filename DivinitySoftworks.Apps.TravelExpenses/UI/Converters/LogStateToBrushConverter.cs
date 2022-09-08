using DivinitySoftworks.Apps.TravelExpenses.Data.Enums;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace DivinitySoftworks.Apps.TravelExpenses.UI.Converters {
    internal class LogStateToBrushConverter : MarkupExtension, IValueConverter {
        public BrushType BrushType { get; set; } = BrushType.Regular;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (BrushType is BrushType.Unknown) throw new ArgumentNullException(nameof(BrushType));

            if (value is not LogState)
                throw new InvalidCastException(nameof(value));

            LogState? state = (LogState)value;

            return System.Windows.Application.Current.FindResource(GetResource(state ?? LogState.Initial));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }

        private string GetResource(LogState state) {
            string resource = "Brushes.";

            switch (state) {
                case LogState.Error:
                    resource += "Red";
                    break;
                case LogState.Info:
                    resource += "Blue";
                    break;
                case LogState.Warning:
                    resource += "Orange";
                    break;
                case LogState.Success:
                    resource += "Green";
                    break;
                default:
                    resource += "Gray";
                    break;
            }

            if (BrushType == BrushType.Light)
                return $"{resource}.Light";
            if (BrushType == BrushType.Dark)
                return $"{resource}.Dark";
            return resource;
        }
    }
}
