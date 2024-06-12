using _3x4_Math.Results;
using _3x4_Math.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3x4_Math.ViewModel;

public partial class AdminPreferencesViewModel : ObservableObject
{
    public AdminPreferencesViewModel()
    {
        try
        {
            SelectedItem = Convert.ToString(App.SettingsSvc.NumQuestions);
        }
        catch (Exception ex)
        {
            Excepcio.Excepcio.AddLog(ex);
        }
    }
    
    [RelayCommand]
    private async void ClearScore(string parameter)
    {
        bool res2 = await App.RepositorySvc.ClearScores();
        if(res2) App.AlertSvc.ShowAlert("Eliminada las puntuaciones", "Correctamente.");
        else App.AlertSvc.ShowAlert("Eliminada las puntuaciones", "Error al eliminar las puntuaciones.");
    }

    [RelayCommand]
    private async void ClearListadoPesos()
    {
        bool res2 = await App.RepositorySvc.ClearItemsPesos();
        if (res2) App.AlertSvc.ShowAlert("Eliminada listado pesos", "Correctamente.");
        else App.AlertSvc.ShowAlert("Eliminada listado pesos", "Error al eliminar las puntuaciones.");
    }

    [RelayCommand]
    private async void DeleteDatabase()
    {
        bool res2 = await App.RepositorySvc.DeleteDataBase();
        if (res2) App.AlertSvc.ShowAlert("Eliminada base de datos", "Correctamente.");
        else App.AlertSvc.ShowAlert("Eliminada base de datos", "Error al eliminar las puntuaciones.");
    }

    [RelayCommand]
    private void ListadoPesos()
    {
        Shell.Current.GoToAsync(nameof(ListadoPesosView));
    }

    [ObservableProperty]
    String[] items = new string[] {"5", "10", "20", "30", "40"};

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

    public async void LoadDBFirstTime()
    {
           
    }

  
}
