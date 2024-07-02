using _3x4_Math.Helpers;
using _3x4_Math.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _3x4_Math.Converters;

public class IconConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is ItemPesosMultiDB itemPeso)
        {
            FieldInfo field = typeof(_3x4_Math.Helpers.IconFont).GetField(itemPeso.Image);
            if (field != null)
            {
                // Obtener el valor de la constante
                string unicodeValue = field.GetValue(null) as string;
                if (unicodeValue != null)
                {
                    return new FontImageSource
                    {
                        FontFamily = "MaterialDesignIcons", // Ajusta la familia de fuentes según tus necesidades
                        Glyph = unicodeValue,
                        Color = Color.FromArgb(itemPeso.Color),// Color.FromRgb(255, 255, 255), // Puedes ajustar el color si es necesario
                        Size = 14 // Puedes ajustar el tamaño si es necesario
                    };
                }
            }
            //return new FontImageSource
            //{
            //    FontFamily = "MaterialDesignIcons", // Ajusta la familia de fuentes según tus necesidades
            //    Glyph = iconName,
            //    Color = Color.FromRgb(0, 0, 0),// Color.White, // Puedes ajustar el color si es necesario
            //    Size = 12 // Puedes ajustar el tamaño si es necesario
            //};
        }
        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
