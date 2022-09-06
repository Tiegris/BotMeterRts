using BotMeter.Core.Types;

namespace BotMeter.Core;

internal class CoordinateSystem {
    private readonly double _fiX, _fiZ;
    private readonly List<Star> _stars = new();
    private static readonly double EarthRotationAngularVelocity = 2.0 * Math.PI / new TimeSpan(0, 23, 56, 4, 1).TotalSeconds;
    private readonly DateTime _referenceTime;

    private Vector AlignNorth(Vector v) {
        v = Vector.RotateOnZ(v, _fiZ);
        v = Vector.RotateOnX(v, _fiX);
        return v;
    }

    private Vector Revolve(Vector v, TimeSpan t) {
        double fi = t.TotalSeconds * EarthRotationAngularVelocity;
        return Vector.RotateOnZ(v, fi);
    }

    public CoordinateSystem(Vector northVector, DateTime start) {
        _referenceTime = start;

        _fiZ = Math.Atan(northVector.X / northVector.Y);
        northVector = Vector.RotateOnZ(northVector, _fiZ);
        _fiX = Math.Atan(northVector.Y / northVector.Z);
    }
           
    public void Add(MeasuredData m) {
        m.Star.Vector = AlignNorth(m.Star.Vector);
        m.Star.Vector = Revolve(m.Star.Vector, m.DateTime - _referenceTime);
        _stars.Add(m.Star);            
    }
        
    public Star GetStar(int index) {
        return _stars[index];
    }

    public int Count => _stars.Count;
}