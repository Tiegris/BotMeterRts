using BotMeter.Core.Types;

namespace BotMeter.Core;

public class Projection
{
    private class ProjectedStar
    {
        public string Name;
        public Point Point;
    }

    private readonly List<ProjectedStar> _points = new List<ProjectedStar>();

    private static Point project(Vector v) {
        v += new Vector(0, 0, 1.0);

        double x = v.X / v.Z;
        double y = v.Y / v.Z;

        return new Point(x, y);
    }

    public void MirrorOnX()
    {
        foreach (var t in _points)
            t.Point.X = -t.Point.X;
    }

    public void MirrorOnY()
    {
        foreach (var t in _points)
            t.Point.Y = -t.Point.Y;
    }

    public Projection(CoordinateSystem cs) {
        for (int i = 0; i < cs.Count; i++) {
            Point p = project(cs.GetStar(i).Vector);
            _points.Add(new ProjectedStar { Point = p, Name = cs.GetStar(i).Name });
        }
    }
    
    public string ToSvg()
    {
        var builder = new SvgBuilder();
        builder.Line(new Point(-1, 0), new Point(1, 0));   //Draw abscissa
        builder.Line(new Point(0, -1), new Point(0, 1));   //Draw ordinate

        for (int i = 10; i <= 90; i += 10) {
            Vector ct = new Vector(0, 0, 1.0);
            ct = Vector.RotateOnX(ct, ToRadian(i));
            Point pct = project(ct);
            builder.Circle(Point.Origin, Math.Abs(pct.Y));
        }

        foreach (var item in _points)
            builder.Dot(item.Point, item.Name, System.Drawing.Color.Red);
        
        return builder.Build();
    }

    private static double ToRadian(double a) {
        return a * Math.PI / 180.0;
    }
    

}