using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3x4_Math.ViewModel;

public partial class MainPageViewModel : ObservableObject
{
    private int Contador = 0;

    [ObservableProperty]
    private string message2 = "";

    public MainPageViewModel()
    {

    }

    [RelayCommand]
    private void ShowMessage()
    {
        //AppShell.Current.IsVisible = false;

        //((AppShell)(AppShell.Current.Items.First().Parent)).FlyoutBehavior = FlyoutBehavior.Disabled;
        //AppShell.Current.Items.First().FlyoutItemIsVisible = false;

        //Preferences.Clear();
        App.AlertSvc.ShowConfirmation("Title", "Confirmation message.", (result =>
        {
            //App.AlertSvc.ShowAlert("Result", $"{result}");
        }));
    }

    [RelayCommand(CanExecute = nameof(CanIncrementCounter))]
    private void IncrementCounter()
    {
        Contador++;
        Message2 = $"Contador {Contador.ToString()}";
        IncrementCounterCommand.NotifyCanExecuteChanged();

        //await Shell.Current.GoToAsync($"//{nameof(ListOfGIJoe)}");
    }

    private bool CanIncrementCounter() => (Contador < 10);

    [RelayCommand]
    public static void ThemeChanged()
    {
        Application.Current.UserAppTheme =
            Application.Current.RequestedTheme is AppTheme.Dark
                ? AppTheme.Light
                : AppTheme.Dark;
    }
}
