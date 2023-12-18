using DivinitySoftworks.Apps.TravelExpenses.Data.Enums;
using DivinitySoftworks.Apps.TravelExpenses.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;


namespace DivinitySoftworks.Apps.TravelExpenses.UI.Converters {

    internal class StaticsListToAmountConverter : MarkupExtension, IMultiValueConverter {
        /// <inheritdoc/>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            if (values.Length != 4) return DependencyProperty.UnsetValue;
            if (values[0] is null || values[0] is not IList<StaticsItem> staticsItems) return DependencyProperty.UnsetValue;
            if (values[1] is null || values[1] is not DateOnly dateOnly) return DependencyProperty.UnsetValue;
            if (values[2] is null || values[2] is not CalendarRange calendarRange) return DependencyProperty.UnsetValue;
            if (values[3] is null || values[3] is not StaticType staticType) return DependencyProperty.UnsetValue;

            if(staticType == StaticType.Unset) return DependencyProperty.UnsetValue;

            double value = 0.00;

            switch (calendarRange) {
                case CalendarRange.Month:
                    value = staticsItems.Where(s => s.DateTime.Year == dateOnly.Year &&  s.DateTime.Month == dateOnly.Month).Sum(s => (staticType == StaticType.Kilometers) ? s.Kilometers : s.Price) ?? 0;
                    break;
                case CalendarRange.Year:
                    value = staticsItems.Where(s => s.DateTime.Year == dateOnly.Year).Sum(s => (staticType == StaticType.Kilometers) ? s.Kilometers : s.Price) ?? 0;
                    break;
                case CalendarRange.All:
                    value = staticsItems.Sum(s => (staticType == StaticType.Kilometers) ? s.Kilometers : s.Price) ?? 0;
                    break;
                default:
                    return DependencyProperty.UnsetValue;
            }

            return (staticType == StaticType.Kilometers) ? (int)Math.Round(value) : Math.Round(value, 2);
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
