using _3x4_Math.Helpers;
using _3x4_Math.Results;
using _3x4_Math.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            SelectedItem = Convert.ToString(App.SettingsSvc.NumQuestions);
            Language = Config.GetCultureByKey(App.SettingsSvc.Language);

            Tables2 = new ObservableCollection<TableItem>();

            for (int i = 1; i <= 9; i++)
            {
                Tables2.Add(new TableItem()
                {
                    Name = $"Tabla {i}",
                    Id = i,
                    IsChecked = App.SettingsSvc[i]
                });
            }

            foreach (TableItem table in Tables2)
            {
                table.PropertyChanged += TableItem_PropertyChanged;
            }
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

        LocalizationResourceManager.Instance.SetCulture(new CultureInfo(value.Key));

        App.SettingsSvc.Language = value.Key;
    }
    public ObservableCollection<TableItem> Tables2 { get; set; }

    private void TableItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(TableItem.IsChecked))
        {
            var tableItem = sender as TableItem;
            App.SettingsSvc[tableItem.Id] = tableItem.IsChecked;
        }
    }

    #region Num Questions
    [ObservableProperty]
    String[] items = new string[] { "5", "10", "20", "30", "40" };

    [ObservableProperty]
    String selectedItem = "30";

    partial void OnSelectedItemChanged(string value)
    {
        try
        {
            App.SettingsSvc.NumQuestions = Convert.ToInt32(value);
        }
        catch (Exception ex)
        {
            Excepcio.Excepcio.AddLog(ex);
        }
    }
    #endregion


}

public partial class TableItem : ObservableObject
{
    [ObservableProperty]
    private bool isChecked;

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private int id;
}