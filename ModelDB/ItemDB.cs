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

[Table("ItemDB")]
public class ItemDB
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
 
    [MaxLength(250)]
    public string Name { get; set; }
    public int SecondsElapsed { get; set; }

}
