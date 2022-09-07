using BotMeter.Core.Types;

namespace BotMeter.Core;

public class DataReader : IDisposable
{
    private const double RodLength = 500.0;
    private const double EndOfRopeDistance = 1000.0;

    private readonly char[] _sepChars = { ' ', '\t', ';' };

    private readonly StreamReader _stream;

    private double MakeCoord(double x) {            
        return -(Math.Pow((EndOfRopeDistance - RodLength) + x, 2) - Math.Pow(RodLength, 2) - Math.Pow(EndOfRopeDistance, 2)) 
                / (2 * EndOfRopeDistance);
    }

    private Vector ToVektor(string a, string b) {
        var ci = System.Globalization.CultureInfo.InvariantCulture;
        double aa = double.Parse(a, ci);
        double bb = double.Parse(b, ci);

        double x = MakeCoord(aa);
        double y = MakeCoord(bb);
        double z = Math.Sqrt(Math.Pow(RodLength, 2) - (x * x + y * y));

        Vector v = new Vector(x, y, z);
        return v * (1/v.Length);
    }

    public DataReader(string path) {
        _stream = new StreamReader(path);

        string line = _stream.ReadLine()!;
        string[] words = line.Split(_sepChars);
        string[] keyWords = new string[5] { "BLACK", "RED", "DATE", "TIME", "NAME" };
        for (int i = 0; i < 5; i++)
            if (words[i].ToUpper() != keyWords[i])
                throw new InvalidDataException("File Header not found, or invalid.");            
    }

    public bool EndOfStream => _stream.EndOfStream;

    private string _prevDateString = default!;
    public MeasuredData Next() {
        string line = _stream.ReadLine()!;
        int hasDate = Convert.ToInt32(HasDate(line));
        string[] dat;

        if (hasDate == 1) {
            dat = line.Split(_sepChars, 5);
            _prevDateString = dat[2];             
        } else {
            dat = line.Split(_sepChars, 4);
        }

        if (!hasSeconds(dat[2 + hasDate]))
            dat[2 + hasDate] += ":00";

        Star star;
        if (dat.Length == 4 + hasDate)
            star = new Star(ToVektor(dat[0], dat[1]), dat[3 + hasDate]);
        else
            star = new Star(ToVektor(dat[0], dat[1]), default!);

        DateTime time = DateTime.ParseExact(_prevDateString + ' ' + dat[2 + hasDate], "yyyy.MM.dd H:mm:ss", null);
        return new MeasuredData(star, time);
    }

    private bool hasSeconds(string s) {
        return (s.Length == 8);
    }

    private bool HasDate(string s) {
        s = s.Split(_sepChars, 4)[2];

        if (s.Length != 10)
            return false;
        for (int i = 0; i < 4; i++)
            if (!('0' <= s[i] && s[i] <= '9'))
                return false;
        if (s[4] != '.' && s[7] != '.')
            return false;
        return true;
    }

    public void Dispose() {
        _stream.Close();
        _stream.Dispose();
    }

    ~DataReader() {
        Dispose();
    }

}