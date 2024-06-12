using _3x4_Math.ViewModel;
using System.ComponentModel;

namespace _3x4_Math.Views;

public partial class AdminPreferencesView : ContentPage
{
   	public AdminPreferencesView(AdminPreferencesViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
    protected override void OnAppearing()
    {
        if (this.BindingContext is AdminPreferencesViewModel vm)
        {
            vm.LoadDBFirstTime();
        }
    }

}

