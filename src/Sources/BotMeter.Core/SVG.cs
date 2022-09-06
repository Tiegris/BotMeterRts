using System.Drawing;
using System.Globalization;
using Point = BotMeter.Core.Types.Point;

namespace BotMeter.Core;

internal class Svg
{
    private int _height, _width;
    private static NumberFormatInfo _ni = new NumberFormatInfo() { NumberDecimalSeparator = "." };


    private string DotSize =>
        (((_height + _width) / 2.0) / 350.0)
        .ToString(_ni);

    private string LineSize =>
        (((_height + _width) / 2.0) / 2000.0)
        .ToString(_ni);

    public Svg(int width, int height) {
        this._height = height;
        this._width = width;
    }

    public string Header() {
        return "<svg height = \"" + _height + "\" width = \"" + _width + "\" xmlns = \"http://www.w3.org/2000/svg\" >" + 
               Environment.NewLine;
    }

    public string Footer() {
        return "</svg>" + Environment.NewLine;
    }

    public string Point(Point p, Color? color = null) {
        Color c = color ?? Color.Black;

        return "<circle cx=\"" + SvgCoordX(p) + "\" cy=\"" + SvgCoordY(p) + "\" r=\"" + DotSize + "\" style=\"fill:" + 
               Rgb(c) + ";\" />" + Environment.NewLine;
    }

    public string Line(Point p1, Point p2, Color? color = null) {
        Color c = color ?? Color.Black;

        return "<line x1=\"" + SvgCoordX(p1) + "\" y1=\"" + SvgCoordY(p1) +
               "\" x2=\"" + SvgCoordX(p2) + "\" y2=\"" + SvgCoordY(p2) + 
               "\" style=\"stroke: " + Rgb(c) + "; stroke-width:" + LineSize + "\" />" + 
               Environment.NewLine;
    }

    public string Circle(Point origin, double radius, Color? color = null) {
        Color c = color ?? Color.Black;

        return "<circle cx=\"" + SvgCoordX(origin) + 
               "\" cy=\"" + SvgCoordY(origin) + "\" r=\"" + radius.ToString(_ni) +
               "\" style=\"stroke: " + Rgb(c) + "; stroke-width:" + LineSize + "\" " +
               "fill = \"none\" />" + Environment.NewLine;
    }


    private string SvgCoordX(Point p) {
        return (p.X + _height / 2.0).ToString(_ni);
    }

    private string SvgCoordY(Point p) {
        return (-p.Y + _width / 2.0).ToString(_ni);
    }

    private string Rgb(Color c) {
        return "rgb(" + c.R + ',' + c.G + ',' + c.B + ")";
    }

}