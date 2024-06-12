using _3x4_Math.Helpers;
using _3x4_Math.Resources.Languages;
using _3x4_Math.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3x4_Math.Interface;

public partial class SettingsService
{
    public bool FirstTime
    {
        get
        {
            try
            {
               return Preferences.Get("FirstTime", true);
            }
            catch (Exception ex)
            {
                Excepcio.Excepcio.AddLog(ex);
                return true;
            }
        }
        set
        {
            try
            {
                Preferences.Set("FirstTime", value);
                EvaluateShellMenu();
            }
            catch (Exception ex)
            {
                Excepcio.Excepcio.AddLog(ex);
            }
        }
    }

    public int NumQuestions
    {
        get
        {
            try
            {
                return Preferences.Get("NumQuestions", 30);
            }
            catch (Exception ex)
            {
                Excepcio.Excepcio.AddLog(ex);
                return 30;
            }
        }
        set
        {
            try
            {
                Preferences.Set("NumQuestions", value);
            }
            catch (Exception ex)
            {
                Excepcio.Excepcio.AddLog(ex);
            }
        }
    }

    public string Language
    {
        get
        {
            try
            {
                if (Preferences.ContainsKey("Language"))
                {
                    return Preferences.Get("Language", "ca");
                }
                else
                {
                    string key = "ca";// AppResources.Culture.TwoLetterISOLanguageName;
                    Cultures c = Config.GetCultureByKey(key);
                    return c.Key;
                }
            }
            catch (Exception ex)
            {
                Excepcio.Excepcio.AddLog(ex);
                return "es";
            }
        }
        set
        {
            try
            {
                Preferences.Set("Language", value);
            }
            catch (Exception ex)
            {
                Excepcio.Excepcio.AddLog(ex);
            }
        }
    }

    public async void EvaluateShellMenu()
    {
        try
        {
            if (FirstTime)
            {
                AppShell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
                AppShell.Current.Items.First().FlyoutItemIsVisible = true;

                //Localization
                Cultures language = Config.GetCultureByKey(App.SettingsSvc.Language);
                LocalizationResourceManager.Instance.SetCulture(new CultureInfo(language.Key));

                await Shell.Current.GoToAsync($"//{nameof(StartScreenView)}");
            }
            else
            {
                AppShell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
                AppShell.Current.Items.First().FlyoutItemIsVisible = false;

                if (Shell.Current.CurrentPage is MainPage) return;

                await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
                //await Shell.Current.GoToAsync("..");
                //await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
                //while (Navigation.NavigationStack.Count > 1)
                //{
                //    Navigation.RemovePage(Navigation.NavigationStack[1]);
                //}
                //await dispatcherProvider.DispatchAsync(() => Shell.Current.GoToAsync(".."));
            }
        }
        catch (Exception ex)
        {
            Excepcio.Excepcio.AddLog(ex);
        }
    }

    public async void ClearPreferences()
    {
        Preferences.Clear();
        FirstTime = true;
        await Shell.Current.GoToAsync($"//{nameof(StartScreenView)}");
    }
}
