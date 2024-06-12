using CommunityToolkit.Mvvm.Input;
using _3x4_Math.Helpers;
using _3x4_Math.ViewModel;
using _3x4_Math.Views;
using System.Globalization;

namespace _3x4_Math;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(ThreeX4View), typeof(ThreeX4View));
        Routing.RegisterRoute(nameof(ScoreView), typeof(ScoreView));
        Routing.RegisterRoute(nameof(ResultsView), typeof(ResultsView));
        Routing.RegisterRoute(nameof(ListadoPesosView), typeof(ListadoPesosView));

        ////Esto se hace ya que DetailGIJoe no es visible por el menu del AppShell
        //Routing.RegisterRoute(nameof(DetailGIJoe), typeof(DetailGIJoe));

        //Poner idioma por defecto al catalan
        Cultures language = Config.GetCultureByKey(App.SettingsSvc.Language);
        LocalizationResourceManager.Instance.SetCulture(new CultureInfo(language.Key));
    }

    protected override void OnAppearing()
    {
        //if (App.SettingsSvc.FirstTime) 
        App.SettingsSvc.EvaluateShellMenu();
        //if (this.BindingContext is ListOfGIJoeViewModel vm)
        //{
        //    vm.LoadFirstTime();
        //}
    }

    

}


