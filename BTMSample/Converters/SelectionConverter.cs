using System.Globalization;

namespace BTMSample
{
    public class SelectionConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            // Convert the bool value to a Color
            bool boolValue = (bool)value;
            return boolValue ? Colors.BlueViolet : Colors.LightGray;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            //throw new NotImplementedException();
            return null;
        }
    }
}
