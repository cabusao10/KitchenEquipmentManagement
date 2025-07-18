using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace KitchenEquipmentManagement.WPF.Helper
{
    public enum UserTypeEnum
    {
        Admin,
        SuperAdmin
    }

    public class UserTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Type enumType = (Type)parameter;
            return Enum.GetValues(enumType);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
