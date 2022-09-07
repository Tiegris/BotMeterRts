using System.Globalization;

namespace BotMeter.Core.Types;

public class Vector
{
    public double X, Y, Z;
    private static readonly Random Ran = new Random();
    private static readonly NumberFormatInfo Ni = new NumberFormatInfo() { NumberDecimalSeparator = "." };

    public Vector(double x = 0, double y = 0, double z = 0) {
        if (double.IsNaN(x) || double.IsNaN(y) || double.IsNaN(z))
            throw new Exception("Vektor coordinate can not be NaN");

        this.X = x;
        this.Y = y;
        this.Z = z;
    }

    public Vector(Vector v) {
        if (double.IsNaN(v.X) || double.IsNaN(v.Y) || double.IsNaN(v.Z))
            throw new Exception("Vektor coordinate can not be NaN");

        X = v.X;
        Y = v.Y;
        Z = v.Z;
    }

    public static Vector operator -(Vector v1) {
        return new Vector(-v1.X, -v1.Y, -v1.Z);
    }

    public static Vector operator -(Vector v1, Vector v2) {
        return new Vector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
    }

    public static Vector operator +(Vector v1, Vector v2) {
        return new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
    }

    public double Length => Math.Sqrt(X * X + Y * Y + Z * Z);

    public static Vector operator <(Vector v1, Vector v2) {
        return v1 - v2;
    }

    public static Vector operator >(Vector v1, Vector v2) {
        return v2 - v1;
    }

    public static Vector operator *(Vector v1, double b) {
        return new Vector(v1.X * b, v1.Y * b, v1.Z * b);
    }

    public static double Distance(Vector v1, Vector v2) {
        return (v1 > v2).Length;
    }

    public static Vector operator^ (Vector v1, Vector v2) {
        return new Vector(
            v1.Y * v2.Z - v1.Z * v2.Y,
            -v1.X * v2.Z + v1.Z * v2.X,
            v1.X * v2.Y - v1.Y * v2.X 
        );
    }


    private static void Rotate(ref double a, ref double b, double fi) {
        double tempA = a * Math.Cos(fi) - b * Math.Sin(fi);
        b = a * Math.Sin(fi) + b * Math.Cos(fi);
        a = tempA;
    }

    public static Vector RotateOnX(Vector v, double a) {
        Rotate(ref v.Y, ref v.Z, a);
        return v;
    }

    public static Vector RotateOnY(Vector v, double a) {
        Rotate(ref v.X, ref v.Z, a);
        return v;
    }

    public static Vector RotateOnZ(Vector v, double a) {
        Rotate(ref v.X, ref v.Y, a);
        return v;
    }

    public override string ToString() {            
        return "(" + X.ToString(Ni) + "; " + Y.ToString(Ni) + "; " + Z.ToString(Ni) + ")";
    }

    public static explicit operator string(Vector v) {
        return v.ToString();
    }

    public static Vector Random(double length = 1.0) {
        Vector v = new Vector(1, 0, 0);            

        Vector.RotateOnX(v, Ran.NextDouble() * 2 * Math.PI);
        Vector.RotateOnY(v, Ran.NextDouble() * 2 * Math.PI);
        Vector.RotateOnZ(v, Ran.NextDouble() * 2 * Math.PI);

        return v * length;
    }

}