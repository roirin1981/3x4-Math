﻿using _3x4_Math.ModelDB;
using _3x4_Math.Results;
using _3x4_Math.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static SQLite.SQLite3;

namespace _3x4_Math.ViewModel;

public partial class ResultsViewModel : ObservableObject
{
    public ResultsViewModel()
    {
        //LoadScores();
    }

    [ObservableProperty]
    private ItemPesosMultiDB[] caracters;

    public async void LoadScores()
    {
        try
        {
            //Items.Clear();
            List<ItemPesosMultiDB> res2 = await App.RepositorySvc.GetAllPesosItems();
            Caracters = res2.ToArray();            
        }
        catch (Exception ex)
        {
            Excepcio.Excepcio.AddLog(ex);
        }
    }

    //public ObservableCollection<Puntuation> Items { get; } = new();

    


}
