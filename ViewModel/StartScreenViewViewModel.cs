using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using _3x4_Math.Interface;
using _3x4_Math.Views;
using _3x4_Math.Excepcio;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Windows.Input;

namespace _3x4_Math.ViewModel;

public partial class StartScreenViewViewModel : ObservableObject
{


    [ObservableProperty]
    private string messageUpdateDB = "";

    public StartScreenViewViewModel()
    {
        IsVisibleFirstPage = App.SettingsSvc.FirstTime;
    }

    [RelayCommand(CanExecute = nameof(CanIncrementCounter))]    
    private void ChangeMenu()
    {
        try
        {
            App.SettingsSvc.FirstTime = false;            
            //App.AlertSvc.ShowAlert("Result", "Se ha actualizado First Time to false");            

        }
        catch(Exception ex)
        {
            Excepcio.Excepcio.AddLog(ex);
            Console.WriteLine(ex.Message);
        }        
    }

    private bool CanIncrementCounter() => (!IsVisibleFirstPage && !IsBusy);

    private IDispatcherTimer gTimer;

    public async void LoadDBFirstTime()
    {
        IsBusy = true;
        MessageUpdateDB = "Loading, please wait...";
        gTimer = Application.Current.Dispatcher.CreateTimer();
        gTimer.Interval = TimeSpan.FromMilliseconds(10);
        gTimer.Tick += (s, e) => DoSomething();
        gTimer.Start();

        //ResultUpdateDB result = await App.RepositorySvc.LoadService_SaveDB();
        //if (result != null)
        //{
        //    if (!result.ErrorMsg.Trim().Equals("")) { App.AlertSvc.ShowAlert("Error", result.ErrorMsg); }
        //    else IsVisibleFirstPage = false;

        //    List<string> array_temp = new List<string>();
        //    array_temp.Add(result.LoadWebService ? "Load WebService OK" : "Load WebService Error");
        //    array_temp.Add(result.SaveJSONinFile ? "Save JSON in File OK" : "Save JSON in File Error");
        //    array_temp.Add(result.DeleteDB ? "Delete DB OK" : "Delete DB Error");
        //    array_temp.Add(result.InserInDB ? "Save in SQLite DB OK" : "Error in SQLite DB OK");
        //    MessageUpdateDB = string.Join(Environment.NewLine, array_temp);
        //}
        IsVisibleFirstPage = false;
        VisibleLoadImage = false;
        IsBusy = false;
        gTimer.Stop();
    }

    //[AlsoNotifyCanExecuteFor(nameof(ChangeMenu))]
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ChangeMenuCommand))]
    public bool isVisibleFirstPage;

    [ObservableProperty]
    public bool visibleLoadImage = true;


    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ChangeMenuCommand))]
    private bool isBusy = false;

    [ObservableProperty]
    private double rotationAux = 0;

    void DoSomething()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            RotationAux += 5;
        });
    }

}