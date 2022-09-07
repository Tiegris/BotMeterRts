using System.Drawing;
using Point = BotMeter.Core.Types.Point;

namespace BotMeter.Core;

internal class SvgBuilder
{
    private static readonly string dotSize = "0.0057";
    private static readonly string lineSize = "0.001";
    
    private static readonly string header = $"<svg viewBox=\"-1 -1 2 2\" width=\"500\" xmlns=\"http://www.w3.org/2000/svg\">";
    private static readonly string footer = $"</svg>";
    private readonly List<string> _elements = new() { header };
    public string Build()
    {
        _elements.Add(footer);
        return string.Join(Environment.NewLine, _elements);
    }

    public void Dot(Point p, string? label, Color? color = null) {
        Color c = color ?? Color.Black;
        _elements.Add(
            (label is null) ?
                $"<circle cx=\"{(p.X.ToDotString())}\" cy=\"{(-p.Y).ToDotString()}\" r=\"{dotSize}\" style=\"fill:{rgb(c)};\"/>" :
                $"<circle cx=\"{(p.X.ToDotString())}\" cy=\"{(-p.Y).ToDotString()}\" r=\"{dotSize}\" style=\"fill:{rgb(c)};\"><title>{label}</title></circle>"
        );
    }

    public void Line(Point p1, Point p2, Color? color = null) {
        Color c = color ?? Color.Black;
        _elements.Add(
            $"<line x1=\"{(p1.X.ToDotString())}\" y1=\"{(-p1.Y).ToDotString()}\" x2=\"{(p2.X.ToDotString())}\" y2=\"{(-p2.Y).ToDotString()}\" style=\"stroke: {rgb(c)}; stroke-width:{lineSize}\" />"
            );
    }

    public void Circle(Point origin, double radius, Color? color = null) {
        Color c = color ?? Color.Black;
        _elements.Add(
            $"<circle cx=\"{(origin.X.ToDotString())}\" cy=\"{(-origin.Y).ToDotString()}\" r=\"{radius.ToDotString()}\" style=\"stroke: {rgb(c)}; stroke-width:{lineSize}\" fill = \"none\" />"
            );
    }
    
    private static string rgb(Color c) => $"rgb({c.R},{c.G},{c.B})";
}