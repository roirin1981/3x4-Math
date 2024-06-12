using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3x4_Math.Excepcio;

public static class Excepcio
{
    public static int errorI = 0;
    //public static void AddLog(string s)
    //{
    //    ErrorLogs.Add(errorI.ToString()+" : " + s + Environment.NewLine);
    //    errorI += 1;
    //}

    public static void AddLog(Exception e)
    {
        ErrorLogs.Add(new ExceptionResult(e));
        errorI += 1;
    }

    public static void AddLog(FileNotFoundException e) 
    {
        ErrorLogs.Add(new ExceptionResult(e));
        errorI += 1;
    }

    public static List<ExceptionResult> GetErrors() => ErrorLogs;

    private static List<ExceptionResult> ErrorLogs = new List<ExceptionResult>();
}
