using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Timers;
using System.Threading.Tasks;
using System.ComponentModel;
using UraniumUI.Dialogs;
using _3x4_Math.Views;
using static SQLite.SQLite3;
using _3x4_Math.ModelDB;
using _3x4_Math.Helpers;
using System.Runtime.CompilerServices;

namespace _3x4_Math.ViewModel
{
    public partial class ThreeX4ViewModel : ObservableObject
    {
        private System.Timers.Timer timer;
        private int secondsElapsed;
       
        private int resultedValue = 0;

        public IDialogService DialogService { get; }

        public ThreeX4ViewModel(IDialogService dialogService)
        {
            LoadFirstTime();
            DialogService = dialogService;
        }

        public void LoadFirstTime()
        {
            NumMultOk = 0;
            NumFallos = 0;
            secondsElapsed = 0;
            Resulted = "";
            TimeElapsed = "";
            IsVisibleTick = false;
            IsVisibleFail = false;
            ResultedColor = Colors.Black;
            AutoCalculator.InitProcess();
            CreateMultiInit();
            InitializeTimer();
        }

        public void OnDisappearing()
        {
            timer.Stop();
            timer.Enabled = false;
            timer.Elapsed -= OnTimedEvent;
            timer.Close();
            timer.Dispose();
            timer = null;
        }

        [ObservableProperty]
        private int numMultOk = 0;

        [ObservableProperty]
        private int numFallos = 0;

        [ObservableProperty]
        private string message2 = "";

        [ObservableProperty]
        private string timeElapsed;

        [ObservableProperty]
        private string resulted = "";

        [ObservableProperty]
        private int num1 = 1;

        [ObservableProperty]
        private int num2 = 1;

        [ObservableProperty]
        private bool isResultBold = false;

        [ObservableProperty]
        private bool isVisibleTick = false;

        [ObservableProperty]
        private bool isVisibleFail = false;

        [ObservableProperty]
        private Color resultedColor = Colors.Black;

        private bool isEnterInFunction = false;

        [ObservableProperty]
        private bool visibleHelp = false;

        [ObservableProperty]
        private string bottomSheetView1 = "";

        [ObservableProperty]
        private string bottomSheetView2 = "";

        [ObservableProperty]
        private string bottomSheetView3 = "";

        [RelayCommand]
        private void ShowHelp()
        {
            VisibleHelp = !visibleHelp;
        }

        [RelayCommand]
        private async void VeriyfyMultiplication(string parameter)
        {
            if (isEnterInFunction) return;
            if (string.IsNullOrWhiteSpace(Resulted)) return;
            isEnterInFunction = true;
            if (parameter == "OK")
            {
                if (resultedValue == Num1 * Num2)
                {
                    NumMultOk += 1;
                    IsResultBold = true;
                    IsVisibleTick = true;
                    ResultedColor = Colors.Green;
                    AddItemPesoDB(true);
                    await Task.Delay(750); // Espera 2 segundos antes de mostrar el resultado correcto
                }
                else
                {
                    NumFallos += 1;
                    IsResultBold = true;
                    IsVisibleFail = true;
                    ResultedColor = Colors.Red;
                    AddItemPesoDB(false);
                    await Task.Delay(500); // Espera 2 segundos antes de mostrar el resultado correcto
                    Resulted = (Num1 * Num2).ToString();
                    ResultedColor = Colors.Blue;
                    await Task.Delay(1000); // Espera 2 segundos antes de limpiar el resultado
                }

                if (NumMultOk == App.SettingsSvc.NumQuestions)
                {
                    timer.Stop();
                    int timeElapsed = secondsElapsed;
                    //Title = "{helpers:Translate menuPreferencias}"
                    string _puntuation = (string)Helpers.LocalizationResourceManager.Instance["puntuation"];
                    string _aceptar = (string)Helpers.LocalizationResourceManager.Instance["accept"];
                    string _cancelar = (string)Helpers.LocalizationResourceManager.Instance["cancel"];
                    string _name = (string)Helpers.LocalizationResourceManager.Instance["name?"];
                    var result = await DialogService.DisplayTextPromptAsync(_puntuation, _name, placeholder: "...", accept: _aceptar, cancel: _cancelar, maxLength: 15);
                    bool res2 = await App.RepositorySvc.AddItemDB(new ModelDB.ItemDB() { Name = result, SecondsElapsed = timeElapsed });
                    await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
                }
                else
                {
                    CreateMultiInit();
                    IsResultBold = false;
                    IsVisibleTick = false;
                    IsVisibleFail = false;
                    ResultedColor = Colors.Black;
                }
            }                
            isEnterInFunction = false;
        }

        private void AddItemPesoDB(bool val)
        {
            Task<bool> res2 = App.RepositorySvc.AddItemPesoDB(Num1, Num2, val);
        }

        [RelayCommand]
        private async void ChangeMenu(string parameter)
        {

            if (isEnterInFunction) return;
            isEnterInFunction = true;            
            if (Resulted.Length < 3)
            {
                Resulted += parameter;
                resultedValue = int.Parse(Resulted);
            }
            isEnterInFunction = false;
        }

        //protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName == "ResultColor")
        //    {
        //        Console.WriteLine("DAS");
        //    }
        //}

        [RelayCommand]
        private void RemoveCharacter(string parameter)
        {
            if (isEnterInFunction) return;
            isEnterInFunction = true;
            if (!string.IsNullOrEmpty(Resulted))
            {
                Resulted = Resulted.Substring(0, Resulted.Length - 1);
                resultedValue = string.IsNullOrEmpty(Resulted) ? 0 : int.Parse(Resulted);
            }
            isEnterInFunction = false;
        }

        private async void CreateMultiInit()
        {
            Resulted = "";
            resultedValue = 0;

            AutoCalculator.ResAutoCalculator res = await AutoCalculator.CreateMultiInit();
            Num1 = res.Num1;
            Num2 = res.Num2;
            //int x = _random.Next(1, 3);
            //if (x==1)
            //{
            //    ItemPesosMultiDB res2 = await App.RepositorySvc.GetItemPesosWorst();
            //    if (res2.val1 == 0)
            //    {
            //        Num1 = _random.Next(2, 10); // Genera un número aleatorio entre 2 y 9
            //        Num2 = _random.Next(1, 10); // Genera un número aleatorio entre 1 y 9
            //    }
            //    else
            //    {
            //        Num1 = res2.val1;
            //        Num2 = res2.val2;
            //    }                
            //}
            //else
            //{
            //    Num1 = _random.Next(2, 10); // Genera un número aleatorio entre 2 y 9
            //    Num2 = _random.Next(1, 10); // Genera un número aleatorio entre 1 y 9
            //}
            Message2 = $"{Num1} x {Num2} =";
            CreateBottomSheetView();
        }

        private void CreateBottomSheetView()
        {
            List<string> text1 = new List<string>();
            for (int i = 1; i <= 3; i++)
            {
               
                text1.Add(Num1.ToString()+"x"+i.ToString()+"="+Convert.ToString(Num1 * i));
               
            }
            BottomSheetView1 = string.Join("   ",text1);
            text1.Clear();
            for (int i = 4; i <= 6; i++)
            {

                text1.Add(Num1.ToString() + "x" + i.ToString() + "=" + Convert.ToString(Num1 * i));

            }
            BottomSheetView2 = string.Join("   ", text1);
            text1.Clear();
            for (int i = 7; i <= 9; i++)
            {

                text1.Add(Num1.ToString() + "x" + i.ToString() + "=" + Convert.ToString(Num1 * i));

            }
            BottomSheetView3 = string.Join("   ", text1);
        }

        private void InitializeTimer()
        {
            if (timer is not null) return;
            timer = new System.Timers.Timer(1000); // 1 second intervals
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            secondsElapsed++;
            TimeElapsed = $"{secondsElapsed / 60:D2}:{secondsElapsed % 60:D2}";
        }
    }
}
