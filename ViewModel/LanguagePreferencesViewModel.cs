using _3x4_Math.Helpers;
using _3x4_Math.Results;
using _3x4_Math.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3x4_Math.ViewModel;

public partial class LanguagePreferencesViewModel : ObservableObject
{
    public LanguagePreferencesViewModel()
    {
        try
        {
            Language = Config.GetCultureByKey(App.SettingsSvc.Language);
        }
        catch (Exception ex)
        {
            Excepcio.Excepcio.AddLog(ex);
        }
    }

    [ObservableProperty]
    Cultures[] languages = Config.languages;

    [ObservableProperty]
    Cultures language;

    partial void OnLanguageChanged(Cultures value)
    {
        if (value.Key == App.SettingsSvc.Language) return;
        //var switchToCulture = AppResources.Culture.TwoLetterISOLanguageName
        //   .Equals("ca", StringComparison.InvariantCultureIgnoreCase) ?
        //   new CultureInfo("en-US") : new CultureInfo("ca");

        LocalizationResourceManager.Instance.SetCulture(new CultureInfo(value.Key));

        App.SettingsSvc.Language = value.Key;

        //SemanticScreenReader.Announce(CounterBtn.Text);
    }

}
