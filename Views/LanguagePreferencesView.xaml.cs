using _3x4_Math.ViewModel;
using System.ComponentModel;

namespace _3x4_Math.Views;

public partial class LanguagePreferencesView : ContentPage
{
   	public LanguagePreferencesView(LanguagePreferencesViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
    protected override void OnAppearing()
    {
        if (this.BindingContext is LanguagePreferencesViewModel vm)
        {
            
        }
    }

}

