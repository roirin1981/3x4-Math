using _3x4_Math.ViewModel;
using System.ComponentModel;


namespace _3x4_Math.Views;

public partial class StartScreenView : ContentPage
{
   	public StartScreenView(StartScreenViewViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
		
	}

    protected override void OnAppearing()
    {
        if (this.BindingContext is StartScreenViewViewModel vm)
        {
            vm.LoadDBFirstTime();
        }
    }
}

