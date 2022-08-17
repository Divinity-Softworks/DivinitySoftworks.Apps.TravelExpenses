using DivinitySoftworks.Apps.TravelExpenses.Data.Enums;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace DivinitySoftworks.Apps.TravelExpenses.UI.Converters {
    internal class DayStateToBrushConverter : MarkupExtension, IValueConverter {

        public PropertyType PropertyType { get; set; } = PropertyType.Unknown;

        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (PropertyType is PropertyType.Unknown) throw new ArgumentNullException(nameof(PropertyType));

            if (value is not DayState)
                throw new InvalidCastException(nameof(value));

            DayState? state = (DayState)value;

            return System.Windows.Application.Current.FindResource(GetResource(state ?? DayState.Unset));
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }

        private string GetResource(DayState state) {
            string resource = "Brushes.";

            switch (PropertyType) {
                case PropertyType.Background:
                    resource += GetBackground(state);
                    break;
                case PropertyType.Forground:
                    resource += GetForground(state);
                    break;
                case PropertyType.Border:
                    resource += GetBorder(state);
                    break;
            }

            return resource;
        }

        private static string GetBackground(DayState state) {
            return state switch {
                DayState.Weekend => "Orange",
                DayState.Office => "Pink",
                _ => "White",
            };
        }

        private static string GetForground(DayState state) {
            return state switch {
                DayState.Office => "White",
                _ => "Purple.Light",
            };
        }

        private static string GetBorder(DayState state) {
            return state switch {
                DayState.Office => "Pink",
                _ => "Orange",
            };
        }
    }
}
