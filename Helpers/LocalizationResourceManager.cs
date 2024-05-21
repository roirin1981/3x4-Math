﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3x4_Math.Resources.Languages;

namespace _3x4_Math.Helpers;

public class LocalizationResourceManager : INotifyPropertyChanged
{
    private LocalizationResourceManager()
    {
        AppResources.Culture = CultureInfo.CurrentCulture;
    }

    public static LocalizationResourceManager Instance { get; } = new();

    public object this[string resourcekey]
        => AppResources.ResourceManager.GetObject(resourcekey, AppResources.Culture) ?? Array.Empty<byte>();

    public event PropertyChangedEventHandler PropertyChanged;

    public void SetCulture(CultureInfo culture)
    {
        AppResources.Culture = culture;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
    }
}
