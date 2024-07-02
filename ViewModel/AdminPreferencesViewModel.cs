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

    

    public async void LoadDBFirstTime()
    {
           
    }


}


