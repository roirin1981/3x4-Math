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

public partial class ListadoPesosViewModel : ObservableObject
{
    public ListadoPesosViewModel()
    {
        //LoadScores();
    }

    public async void LoadScores()
    {
        try
        {
            Items.Clear();
            List<ItemPesosMultiDB> res2 = await App.RepositorySvc.GetAllPesosItems();
            foreach(ItemPesosMultiDB item in res2)
            {
                Items.Add(new Pesos() { 
                    Res = item.val1.ToString()+"x" +item.val2.ToString()+"="+Convert.ToString(item.val1*item.val2),
                    Aciertos = item.aciertos.ToString(),
                    Fallos = item.fallos.ToString(),
                    Valance = item.valance.ToString(),
                    Array = string.Join(",", item.ultimos),
                    CalculoPeso = item.CalcularPuntaje.ToString()}
                );
            }
        }
        catch (Exception ex)
        {
            Excepcio.Excepcio.AddLog(ex);
        }
    }

    public ObservableCollection<Pesos> Items { get; } = new();

    public class Pesos
    {
        public string Res { get; set; }
        public string Aciertos { get; set; }
        public string Fallos { get; set; }
        public string Valance { get; set; }
        public string Array { get; set; }
        public string CalculoPeso { get; set; }
    }


}
