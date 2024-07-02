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

public partial class MainPageViewModel : ObservableObject
{
    [RelayCommand]
    private void ChangeMenu(string parameter)
    {
        IsBusy = true;
        System.Threading.Thread.Sleep(250);
        //Application.Current.MainPage.IsBusy = true;
        switch (parameter)
        {
            case "A":
                Shell.Current.GoToAsync(nameof(ThreeX4View));
                break;
            case "B":
                Shell.Current.GoToAsync(nameof(ResultsView));
                break;
            case "C":
                Shell.Current.GoToAsync(nameof(ScoreView));
                break;
        }
        IsBusy = false;
        //Application.Current.MainPage.IsBusy = false;
    }
    public async void LoadDBFirstTime()
    {
                ResultUpdateDB result = await App.RepositorySvc.LoadService_SaveDB();        
    }

    [ObservableProperty]
    private bool isBusy = false;

}
