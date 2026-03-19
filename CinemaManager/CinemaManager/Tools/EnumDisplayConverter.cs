using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using Microsoft.Maui.Controls;

namespace CinemaManager.Converters
{
    public class EnumDisplayConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not Enum enumValue)
                return value?.ToString() ?? string.Empty;

            Type enumType = enumValue.GetType();
            string enumName = enumValue.ToString();

            FieldInfo? field = enumType.GetField(enumName);
            if (field is null)
                return enumName;

            DescriptionAttribute? attribute = field.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? enumName;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotSupportedException($"{nameof(EnumDisplayConverter)} does not support ConvertBack.");
    }
}