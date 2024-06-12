using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace _3x4_Math.ModelDB;

[Table("ItemPesosMultiDB")]
public class ItemPesosMultiDB
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
 
    public int val1 { get; set; }
    public int val2 { get; set; }
    public int fallos { get; set; }
    public int aciertos { get; set; }

    public int valance { get; set; }
    public string ultimos { get; set; }
    public string Res { get; set; }
    public string Percent { get; set; }
    public string Image { get; set; }
    public string Color { get; set; }

    public void AgregarValor(char valor)
    {
        // Agregar el nuevo valor al principio de la lista
        ultimos = valor + ultimos;

        // Verificar la longitud de la lista y truncar si es necesario
        if (ultimos.Length > 15)
        {
            ultimos = ultimos.Substring(0, 15);
        }
    }
    public int CalcularPuntaje
    {
        get
        {
            int puntaje = 0;

            for (int i = 0; i < ultimos.Length; i++)
            {
                int valor = ultimos[i] == '1' ? 0 : 1;

                // Asignar un peso creciente según la posición
                puntaje += valor * (ultimos.Length - i);
            }

            if (puntaje <= ultimos.Length) return 0;

            return puntaje;
        }
        set
        {

        }       
    }

    //public bool OperacionMuyMala()
    //{
    //    // Calcular el puntaje
    //    int puntaje = CalcularPuntaje();

    //    // Determinar el umbral relativo a la longitud de la cadena
    //    int umbral = (int)Math.Pow(ultimos.Length / 2, 2); // Este factor puede ajustarse según tus necesidades

    //    // Determinar si la operación es muy mala según el umbral calculado
    //    return puntaje >= umbral;
    //}

}
