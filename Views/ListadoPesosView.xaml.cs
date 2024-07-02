using _3x4_Math.ViewModel;
using System.ComponentModel;

namespace _3x4_Math.Views;

public partial class ListadoPesosView : ContentPage
{
   	public ListadoPesosView(ListadoPesosViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
    protected override void OnAppearing()
    {
        if (this.BindingContext is ListadoPesosViewModel vm)
        {
            vm.LoadScores();
        }
    }
}

