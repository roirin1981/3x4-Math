﻿using _3x4_Math.ViewModel;
using System.ComponentModel;

namespace _3x4_Math.Views;

public partial class MainPage : ContentPage
{
   	public MainPage(MainPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
    protected override void OnAppearing()
    {
        if (this.BindingContext is MainPageViewModel vm)
        {
            vm.LoadDBFirstTime();
        }
    }

}

