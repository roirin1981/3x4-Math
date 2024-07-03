using _3x4_Math.Results;
using _3x4_Math.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UraniumUI.Material.Controls;

namespace _3x4_Math.ViewModel;

public partial class AdminPreferencesViewModel : ObservableObject
{
    public AdminPreferencesViewModel()
    {
        try
        {           

            
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
        string _eliminateScore = (string)Helpers.LocalizationResourceManager.Instance["eliminateScore_"];
        string _correctamente = (string)Helpers.LocalizationResourceManager.Instance["correctamente_"];        
        string _error = (string)Helpers.LocalizationResourceManager.Instance["error_"];
        if (res2) App.AlertSvc.ShowAlert(_eliminateScore, _correctamente);
        else App.AlertSvc.ShowAlert(_eliminateScore, _error);
    }

    [RelayCommand]
    private async void ClearListadoPesos()
    {
        bool res2 = await App.RepositorySvc.ClearItemsPesos();
        string _correctamente = (string)Helpers.LocalizationResourceManager.Instance["correctamente_"];
        string _error = (string)Helpers.LocalizationResourceManager.Instance["error_"];
        if (res2) App.AlertSvc.ShowAlert("Eliminada listado pesos", _correctamente);
        else App.AlertSvc.ShowAlert("Eliminada listado pesos", _error);
    }

    [RelayCommand]
    private async void DeleteDatabase()
    {
        string _eliminateDB = (string)Helpers.LocalizationResourceManager.Instance["eliminateDB_"];
        string _correctamente = (string)Helpers.LocalizationResourceManager.Instance["correctamente_"];
        string _error = (string)Helpers.LocalizationResourceManager.Instance["error_"];
        bool res2 = await App.RepositorySvc.DeleteDataBase();
        if (res2) App.AlertSvc.ShowAlert(_eliminateDB, _correctamente);
        else App.AlertSvc.ShowAlert(_eliminateDB, _error);
    }

    [RelayCommand]
    private void ListadoPesos()
    {
        Shell.Current.GoToAsync(nameof(ListadoPesosView));
    }

    

    public async void LoadDBFirstTime()
    {
           
    }


}


