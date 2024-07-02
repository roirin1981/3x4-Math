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
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        
        if (this.BindingContext is ResultsViewModel vm)
        {
            await Task.Yield(); // Permite que la UI se actualice primero
            //await vm.LoadScores();
            _ = LoadDataAsync(vm);
        }
    }

    private async Task LoadDataAsync(ResultsViewModel vm)
    {
     
        await vm.LoadScores();
    }



}

