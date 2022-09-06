namespace BotMeter.Core.Types;

public struct Point
{
    public double X, Y;
    public Point(double x = 0.0, double y = 0.0) {
        X = x;
        Y = y;
    }

    public override string ToString() {
        return "(" + X + "; " + Y + ")";
    }
}