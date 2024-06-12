using _3x4_Math.Interface;
using _3x4_Math.ViewModel;
using System.ComponentModel.Design;

namespace _3x4_Math
{
    public partial class App : Application
    {
        public static IServiceProvider Services;
        public static IAlertService AlertSvc;
        public static SettingsService SettingsSvc;
        public static IRepositoryService RepositorySvc;
        public static StartScreenViewViewModel StartScreenViewSvc;

        public App(IServiceProvider provider)
        {
            InitializeComponent();
            UserAppTheme = AppTheme.Light;
            Services = provider;
            AlertSvc = Services.GetService<IAlertService>();
            RepositorySvc = Services.GetService<IRepositoryService>();
            SettingsSvc = Services.GetService<SettingsService>();
            StartScreenViewSvc = Services.GetService<StartScreenViewViewModel>();

            MainPage = new AppShell();
        }
    }
}
