using _3x4_Math.ModelDB;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _3x4_Math.Helpers;

public class AutoCalculator
{
    private static readonly ThreadLocal<Random> Random = new ThreadLocal<Random>(() => new Random());
    private static readonly ConcurrentBag<ResAutoCalculator> List = new ConcurrentBag<ResAutoCalculator>();

    public static void InitProcess()
    {
        while (!List.IsEmpty)
        {
            List.TryTake(out _);
        }
    }

    public static async Task<ResAutoCalculator> CreateMultiInit(int counted = 0)
    {
        ResAutoCalculator res;
        int x = Random.Value.Next(1, 3);
        if (x == 1)
        {
            ItemPesosMultiDB res2 = await App.RepositorySvc.GetItemPesosWorst().ConfigureAwait(false);
            if (res2.val1 == 0)
            {
                res = CreateRandom();
            }
            else
            {
                res = new ResAutoCalculator
                {
                    Num1 = res2.val1,
                    Num2 = res2.val2
                };
            }
        }
        else
        {
            res = CreateRandom();
        }

        if(List.Count(r => r.Num1 == res.Num1 && r.Num2 == res.Num2) > 2 && counted != 2)
        {
            return await CreateMultiInit(counted + 1);
        }

        List.Add(res);

        return res;
    }

    private static ResAutoCalculator CreateRandom()
    {
        return new ResAutoCalculator
        {
            Num1 = Random.Value.Next(2, 10), // Genera un número aleatorio entre 2 y 9
            Num2 = Random.Value.Next(1, 10)  // Genera un número aleatorio entre 1 y 9
        };
    }

    public class ResAutoCalculator
    {
        public int Num1 { get; set; }
        public int Num2 { get; set; }

    }
}
