using _3x4_Math.ModelDB;
using _3x4_Math.Results;
using _3x4_Math.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SQLite.SQLite3;

namespace _3x4_Math.ViewModel;

public partial class ScoreViewModel : ObservableObject
{
    public ScoreViewModel()
    {
        //LoadScores();
    }

    public async void LoadScores()
    {
        try
        {
            Items.Clear();
            int i = 1;
            List<ItemDB> res2 = await App.RepositorySvc.GetAllItems();
            foreach(ItemDB item in res2)
            {
                string val = $"{item.SecondsElapsed / 60:D2}:{item.SecondsElapsed % 60:D2}";
                Items.Add(new Puntuation() { Id = i.ToString(), Name = item.Name, Time = val });
                i++;
            }
        }
        catch (Exception ex)
        {
            Excepcio.Excepcio.AddLog(ex);
        }
    }

    public ObservableCollection<Puntuation> Items { get; } = new();

    public class Puntuation
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Time { get; set; }
    }


}
