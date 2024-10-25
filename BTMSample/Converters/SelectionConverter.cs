using System.Globalization;

namespace BTMSample
{
    public class SelectionConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            bool boolValue = false;
            if (parameter != null && parameter.ToString() == "img")
            {
                boolValue = (bool)value;
                return boolValue ? "homepink.png" : "home32.png";
            }
            else
            {
                // Convert the bool value to a Color
                boolValue = (bool)value;
                return boolValue ? Colors.BlueViolet : Colors.LightGray;
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            //throw new NotImplementedException();
            return null;
        }
    }
}
