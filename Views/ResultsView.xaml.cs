using _3x4_Math.ViewModel;
using System.ComponentModel;

namespace _3x4_Math.Views;

public partial class ResultsView : ContentPage
{
   	public ResultsView(ResultsViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
    protected override void OnAppearing()
    {
        if (this.BindingContext is ResultsViewModel vm)
        {
            vm.LoadScores();
        }
    }

}

