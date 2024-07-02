using _3x4_Math.ViewModel;
using System.ComponentModel;
using UraniumUI.Pages;

namespace _3x4_Math.Views;

public partial class ThreeX4View : UraniumContentPage
{
	public ThreeX4View(ThreeX4ViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;

    }

    protected override void OnAppearing()
    {
        if(this.BindingContext is ThreeX4ViewModel vm)
        {
            vm.LoadFirstTime();
        }
    }

    protected override void OnDisappearing()
    {
        if (this.BindingContext is ThreeX4ViewModel vm)
        {
            vm.OnDisappearing();
        }
    }
}