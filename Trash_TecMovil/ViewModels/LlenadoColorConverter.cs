using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trash_TecMovil.ViewModels
{
    internal class LlenadoColorConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int llenado = (int)value;

            if (llenado <= 20)
                return Color.FromArgb("#FF0000"); 
            if (llenado <= 60)
                return Color.FromArgb("#FFFF00"); ;
            return Color.FromArgb("#008000"); 
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
