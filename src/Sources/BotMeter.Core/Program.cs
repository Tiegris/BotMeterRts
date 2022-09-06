using BotMeter.Core.Types;

namespace BotMeter.Core;

internal class Program
{
    private static int Main(string[] args) {
        if (args.Length == 0)
            return -1;

        CoordinateSystem cs;
        using (DataReader dr = new DataReader(args[0])) {
            MeasuredData m1 = dr.Next();
            MeasuredData m2 = dr.Next();
            MeasuredData m3 = dr.Next();

            Vector northVector = NorthVector(m1.Star.Vector, 
                m2.Star.Vector, 
                m3.Star.Vector);                

            cs = new CoordinateSystem(northVector, m1.DateTime);

            while (!dr.EndOfStream)                     
                cs.Add(dr.Next());                
        }

        Projection p = new Projection(cs);
        p.MirrorOnX();

        string svg = p.ToSvg();

        using (StreamWriter sw = new StreamWriter(NextFileName(".svg")))
            sw.Write(svg);

        return 0;
    }

    private static string NextFileName(string extension) {
        int i = 0;
        string s;
        while (File.Exists(s = Directory.GetCurrentDirectory().ToString() + "\\out" + (char)(i + '0') + extension))
            i++;
        return s;
    }

    private static Vector NorthVector(Vector v, Vector u, Vector w) {
        Vector e = (v - u) ^ (w - u);
        return (e * (1 / e.Length));
    }
}