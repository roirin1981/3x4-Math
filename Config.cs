using _3x4_Math.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3x4_Math;

public static class Config
{
    public static bool ListenTogetherIsVisible => true;



    public static bool Desktop
    {
        get
        {
#if WINDOWS || MACCATALYST
            return true;
#else
            return false;
#endif
        }
    }

    public static string BaseWeb = $"{Base}:5002/";
    public static string Base = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2" : "http://localhost";

    public static string WebListImages = "http://www.hugovelilla.somee.com/GIJoeDB/";
    public static string WebJSONFile = "http://www.hugovelilla.somee.com/GIJoeDB/dataDBGiJOE.json";


    public static string GetLocalFilePath(string filename)
    {
        string ret = System.IO.Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, filename);
        return ret;
    }


 
    //public static string DatabasePath =>
    //    Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);


    public static Cultures[] languages = new Cultures[] {
        new Cultures{
            Name = "Español",
            Key = "es"
        },
        new Cultures{
            Name = "Català",
            Key = "ca"
        }
    };

    public static Cultures GetCultureByKey(string key)
    {
        Cultures c = Config.languages.ToList().Find(x => x.Key == key);
        if (c == null) return new Cultures() { Key = "es", Name = "Español" };
        return c;
    }

    public const string DatabaseFilename = "math3x4.db3";

    public const SQLite.SQLiteOpenFlags Flags =
       // open the database in read/write mode
       SQLite.SQLiteOpenFlags.ReadWrite |
       // create the database if it doesn't exist
       SQLite.SQLiteOpenFlags.Create |
       // enable multi-threaded database access
       SQLite.SQLiteOpenFlags.SharedCache;

    public static string DatabasePath =>
      Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
}


