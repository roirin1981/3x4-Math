using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3x4_Math.Converters;

public class ImageSourceConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is string imageName)
        {
            return ImageSource.FromResource($"_3x4_Math.Images.{imageName}.png"); // Ajusta el nombre del espacio de nombres y el formato de la imagen según tu configuración
        }
        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
