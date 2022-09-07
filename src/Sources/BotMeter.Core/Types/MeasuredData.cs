namespace BotMeter.Core.Types;

public readonly struct MeasuredData
{
    public Star Star { get; }
    public DateTime DateTime { get; }
    public MeasuredData(Star star, DateTime dateTime) {
        Star = star;
        DateTime = dateTime;
    }

    public override string ToString() {
        return $"{Star}, {DateTime.ToString("HH:mm")}";
    }
}