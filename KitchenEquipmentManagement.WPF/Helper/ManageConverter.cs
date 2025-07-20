using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace KitchenEquipmentManagement.WPF.Helper
{
    public class ManageConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
         {
            if (values.Length <= 1) return Visibility.Collapsed;

            if(tryConvert(values[0]) == 0)
            {
                return Visibility.Visible;
            }

            return tryConvert(values[0]) == tryConvert(values[1]) ? Visibility.Visible : Visibility.Collapsed; ;
        }

        private int tryConvert(object val)
        {
            int x = 0;
            Int32.TryParse(val.ToString(), out x);

            return x;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
