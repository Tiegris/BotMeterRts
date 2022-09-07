namespace BotMeter.Core.Types;

public class Star
{
    public Vector Vector { get; set; }
    private readonly StringId _name;

    public string Name => _name.Value;

    public Star(Vector v, string name) {
        Vector = v;
        _name = new StringId(name);
    }

    public override string ToString() {
        return $"{Vector}, {_name.Value}";
    }


}