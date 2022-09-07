using System.Globalization;
using BotMeter.Core.Types;

namespace BotMeter.Core;

public static class Util
{
    private static readonly NumberFormatInfo numberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = "." };

    public static string ToDotString(this double num)
    {
        return num.ToString(numberFormatInfo);
    }
    
    public static Vector CalculateNorthVector(Vector v, Vector u, Vector w) {
        Vector e = (v - u) ^ (w - u);
        return (e * (1 / e.Length));
    }
}