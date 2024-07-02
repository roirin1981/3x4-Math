using _3x4_Math.ModelDB;
using _3x4_Math.Results;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static SQLite.SQLite3;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _3x4_Math.Interface;

public class RepositoryService : IRepositoryService
{
    private SQLiteAsyncConnection conn;

    public RepositoryService()
    {
        //SetupDatabase();
    }

    private async void Init()
    {
        try
        {
            if (conn is null)
            {
                conn = new SQLiteAsyncConnection(Config.DatabasePath, Config.Flags);
                var result = await conn.CreateTableAsync<ItemDB>();
                result = await conn.CreateTableAsync<ItemPesosMultiDB>();
                Console.WriteLine("DAS");
            }

        }
        catch (Exception e)
        {
            Excepcio.Excepcio.AddLog(e);
        }
    }

    public async Task<bool> AddItemPesoDB(int Num1, int Num2, bool val)
    {
        try
        {
            int result = 0;
            Init();
            ItemPesosMultiDB[]  aux = conn.Table<ItemPesosMultiDB>().Where(t => t.val1 == Num1 && t.val2 == Num2).ToArrayAsync().Result;
            if(aux.Length> 0)
            {
                /*Aciertos-Fallos*/
                if (val) aux[0].aciertos += 1;
                else aux[0].fallos += 1;
                aux[0].valance += (val) ? 1 : -1;
                /*Calcular Imagen-Percent*/
                calculateImagePercent(ref aux[0], val);
                result = await conn.UpdateAsync(aux[0]);
            }
            else
            {
                ItemPesosMultiDB o = new ItemPesosMultiDB()
                {
                    /*Constante*/
                    val1 = Num1,
                    val2 = Num2,
                    Res = Num1 + " X " + Num2 + " = " + Convert.ToString(Num1 * Num2),                    

                    /*Aciertos-Fallos*/
                    aciertos = (val) ? 1 : 0,
                    fallos = (val) ? 0 : 1,
                    valance = (val) ? 1 : -1                    
                };
                
                /*Calcular Imagen-Percent*/
                calculateImagePercent(ref o,val);
                result = await conn.InsertAsync(o);
            }

            if (result != 0)
            {
                return true;
            }
            return false;
        }
        catch (Exception e)
        {
            Excepcio.Excepcio.AddLog(e);
            return false;
        }
    }

    private void calculateImagePercent(ref ItemPesosMultiDB o, bool val)
    {
        o.AgregarValor(val ? '1' : '0');
        int aciertos = 0;
        foreach (char c in o.ultimos)
        {
            if (c == '1')
            {
                aciertos++;
            }
        }
        double porcentajeAciertos = (double)aciertos / o.ultimos.Length * 100;
        o.Percent = $"{(int)Math.Round(porcentajeAciertos)}% - {o.aciertos.ToString()}/{Convert.ToString(o.aciertos + o.fallos)}";
        switch (porcentajeAciertos)
        {
            case 100:
                o.Image = "EmoticonHappyOutline";
                o.Color = "00AF47";
                break;
            case >= 50 and < 100:
                o.Image = "EmoticonNeutralOutline";
                o.Color = "00AFF7";
                break;
            case < 50:
                o.Image = "EmoticonFrownOutline";
                o.Color = "FF5442";
                break;           
        }
    }

    public async Task<bool> AddItemDB(ItemDB c)
    {
        try
        {
            Init();
            int result = await conn.InsertAsync(c);
            
            if (result != 0)
            {
                return true;
            }
            return false;
        }
        catch (Exception e)
        {
            Excepcio.Excepcio.AddLog(e);
            return false;
        }
    }


    public async Task<bool> DeleteDataBase()
    {
        try
        {
            string path = Config.DatabasePath;
            if (File.Exists(path))
            {
                if (conn is not null)
                {
                    await conn.CloseAsync();
                    conn = null;
                }
                File.Delete(path);
            }
            return true;
        }
        catch (Exception e)
        {
            Excepcio.Excepcio.AddLog(e);
            Console.WriteLine(e.Message);
            return false;
        }

    }

    public async Task<List<ItemPesosMultiDB>> GetAllPesosItems()
    {
        try
        {
            Init();
            List<ItemPesosMultiDB> ret = await conn.Table<ItemPesosMultiDB>().ToListAsync();
            return ret;
        }
        catch (Exception e)
        {
            Excepcio.Excepcio.AddLog(e);
        }
        return new List<ItemPesosMultiDB>();
    }

    public async Task<ItemPesosMultiDB> GetItemPesosWorst()
    {
        try
        {
            Init();
            List<ItemPesosMultiDB> ret = await conn.Table<ItemPesosMultiDB>()
                                     .ToListAsync();

            // Filtrar la lista por operaciones muy malas
            ret = ret.OrderByDescending(item => item.CalcularPuntaje).ToList();
            if (ret.Count == 0) return new ItemPesosMultiDB();
            if (ret[0].CalcularPuntaje == 0) return new ItemPesosMultiDB();

            //Valores disponibles
            List<int> possibleValues = new List<int>();
            // Iterar sobre los índices para obtener los valores almacenados en preferencias
            for (int i = 1; i <= 9; i++)  // Asegúrate de ajustar MAX_INDEX según tus necesidades
            {
                if (App.SettingsSvc[i])  // Accede a la propiedad indexada para verificar si es verdadero
                {
                    possibleValues.Add(i);
                }
            }
            if (!possibleValues.Exists(x => x == ret[0].val1))
            {
                return new ItemPesosMultiDB();
            }

            int val = ret[0].CalcularPuntaje;
            return ret[0];
        }
        catch (Exception e)
        {
            Excepcio.Excepcio.AddLog(e);
        }
        return new ItemPesosMultiDB();
    }

    public async Task<List<ItemDB>> GetAllItems()
    {
        try
        {
            Init();
            List<ItemDB> ret = await conn.Table<ItemDB>()
                                       .OrderBy(item => item.SecondsElapsed)
                                       .ToListAsync();
            return ret;
        }
        catch (Exception e)
        {
            Excepcio.Excepcio.AddLog(e);
        }
        return new List<ItemDB>();
    }

    public async Task<ResultUpdateDB> LoadService_SaveDB()
    {
        ResultUpdateDB res = new ResultUpdateDB();
        try {
            res.ErrorMsg = "";
            res.InserInDB = true;
            Init();
            //List<ItemDB> ret = await conn.Table<ItemDB>().ToListAsync();
            return res;
        }
        catch (FileNotFoundException e)
        {
            Excepcio.Excepcio.AddLog(e);
            res.ErrorMsg = e.Message;
        }
        catch (Exception e)
        {
            Excepcio.Excepcio.AddLog(e);
            res.ErrorMsg = e.Message;
        }
        return res;
    }

    public async Task<bool> ClearScores()
    {
        try
        {
            Init();
            int result = await conn.DeleteAllAsync<ItemDB>();
            
            if (result != 0)
            {
                return true;
            }
            return false;
        }
        catch (Exception e)
        {
            Excepcio.Excepcio.AddLog(e);
            return false;
        }
    }

    public async Task<bool> ClearItemsPesos()
    {
        try
        {
            Init();
            int result = await conn.DeleteAllAsync<ItemPesosMultiDB>();

            if (result != 0)
            {
                return true;
            }
            return false;
        }
        catch (Exception e)
        {
            Excepcio.Excepcio.AddLog(e);
            return false;
        }
    }

    

}

