using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace _3x4_Math.Excepcio;

public class ExceptionResult
{
    private string gMessage = "";
    private string gStackTrace = "";
    private string gFunct = "";
    private string gLocation = "";
    private string gTime = "";

    public ExceptionResult(Exception e)
    {
        try
        {
            gMessage = e.Message;
            gStackTrace = e.StackTrace;
            try
            {
                gFunct = e.TargetSite.Name;
            }
            catch (Exception ex)
            {
            }
            try
            {
                gLocation = e.TargetSite.ReflectedType.FullName;
            }
            catch (Exception ex)
            {
            }

            DateTime date1 = DateTime.Now;

            gTime = date1.DayOfWeek.ToString() + " " + date1.ToString();
        }
        catch (Exception ex)
        {
        }
    }

    public string IMessage
    {
        get
        {
            return gMessage;
        }
    }

    public string IStackTrace
    {
        get
        {
            return gStackTrace;
        }
    }

    public string IFunction
    {
        get
        {
            return gFunct;
        }
    }

    public string ILocation
    {
        get
        {
            return gLocation;
        }
    }

    public string ITime
    {
        get
        {
            return gTime;
        }
    }

    public override string ToString()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        return JsonSerializer.Serialize(this, options);
    }
}

