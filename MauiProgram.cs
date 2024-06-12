using Microsoft.Extensions.Logging;
using _3x4_Math.Helpers;
using _3x4_Math.Interface;
using _3x4_Math.ViewModel;
using _3x4_Math.Views;
using System.ComponentModel.Design;
using UraniumUI;
using Mopups.Hosting;
using CommunityToolkit.Maui;

namespace _3x4_Math
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseUraniumUI()
                .UseUraniumUIMaterial()
                .ConfigureMopups() 
                .ConfigureFonts(fonts => 
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont(filename: "materialdesignicons-webfont.ttf", alias: "MaterialDesignIcons");
                    fonts.AddFontAwesomeIconFonts(); 
                });
            builder.Services.AddCommunityToolkitDialogs();


            //View Model
            builder.Services.AddSingleton<ThreeX4ViewModel>();
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<AdminPreferencesViewModel>();
            builder.Services.AddSingleton<LanguagePreferencesViewModel>();
            //builder.Services.AddTransient<StartScreenViewViewModel>();
            builder.Services.AddSingleton<StartScreenViewViewModel>();
            builder.Services.AddSingleton<ScoreViewModel>();
            builder.Services.AddSingleton<ListadoPesosViewModel>();
            builder.Services.AddSingleton<ResultsViewModel>();
            //builder.Services.AddSingleton<AppShellViewModel>();
            //Views
            builder.Services.AddSingleton<ThreeX4View>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<AdminPreferencesView>();
            builder.Services.AddSingleton<LanguagePreferencesView>();
            builder.Services.AddSingleton<StartScreenView>();
            builder.Services.AddSingleton<ScoreView>();
            builder.Services.AddSingleton<ListadoPesosView>();
            builder.Services.AddSingleton<ResultsView>();


            //Services
            builder.Services.AddSingleton<IAlertService, AlertService>();
            builder.Services.AddSingleton<SettingsService>();
            builder.Services.AddSingleton<IRepositoryService, RepositoryService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
