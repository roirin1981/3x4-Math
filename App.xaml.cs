using _3x4_Math.Interface;
using _3x4_Math.ViewModel;

namespace _3x4_Math
{
    public partial class App : Application
    {
        public static IServiceProvider Services;
        public static IAlertService AlertSvc;

        public App(IServiceProvider provider)
        {
            InitializeComponent();

            Services = provider;

            AlertSvc = Services.GetService<IAlertService>();

            MainPage = new AppShell();
        }
    }
}
