using BotMeter.Core.Types;

namespace BotMeter.Core;

internal class Projection
{
    private class ProjectedStar
    {
        public string Name;
        public Point Point;
    }

    private readonly List<ProjectedStar> _points = new List<ProjectedStar>();

    private Point Project(Vector v) {
        v += new Vector(0, 0, 1.0);

        double x = v.X * 2 / v.Z;
        double y = v.Y * 2 / v.Z;

        return new Point(x, y);
    }

    public void MirrorOnX() {
        for (int i = 0; i < _points.Count; i++) 
            _points[i].Point.X = -_points[i].Point.X;                       
    }

    public void MirrorOnY() {
        for (int i = 0; i < _points.Count; i++)
            _points[i].Point.Y = -_points[i].Point.Y;
    }

    public Projection(CoordinateSystem cs) {
        for (int i = 0; i < cs.Count; i++) {
            Point p = Project(cs.GetStar(i).Vector);
            _points.Add(new ProjectedStar { Point = p, Name = cs.GetStar(i).Name });
        }
    }

    private const int Height = 2000;
    private const int Width = 2000;
    public string ToSvg() {  
        Svg svg = new Svg(Width, Height);
        string s = svg.Header();

        s += svg.Line(new Point(-Width/2, 0), new Point(Width/2, 0));   //Draw abcissa
        s += svg.Line(new Point(0, -Height/2), new Point(0, Height/2));   //Draw ordinate

        for (int i = 10; i <= 90; i += 10) {
            Vector ct = new Vector(0, 0, 1.0);
            ct = Vector.RotateOnX(ct, ToRadian(i));
            Point pct = Project(ct);
            pct = Magnify(pct);
            s += svg.Circle(new Point(), Math.Abs(pct.Y));
        }

        foreach (var item in _points) 
            s += svg.Point(Magnify(item.Point), System.Drawing.Color.Red);

        s += svg.Footer();
            
        return s;
    }

    private double ToRadian(double a) {
        return a * Math.PI / 180;
    }

    private const double Lambda = ((Height + Width) / 2.0) / 4.0;

    private Point Magnify(Point p) {
        p.X *= Lambda;
        p.Y *= Lambda;
        return p;
    }


}