namespace BotMeter.Core.Types;

internal class StringId
{
    private readonly static List<string> List = new List<string>();
    private readonly int _id;

    public StringId() {
        _id = -1;
    }

    public StringId(string s = null) {
        if (s == null)
            _id = -1;
        else {
            int tmp = List.FindIndex(v => v.Contains(s));
            if (tmp == -1) {
                List.Add(s);
                _id = List.Count - 1;
            } else {
                _id = tmp;
            }
        }
    }

    public static bool operator ==(StringId a, StringId b) {
        if (a._id == -1 || b._id == -1)
            return false;
        return a._id == b._id;
    }

    public static bool operator !=(StringId a, StringId b) {
        if (a._id == -1 && b._id == -1)
            return true;
        return a._id != b._id;
    }

    public string Value {
        get {
            if (_id == -1)
                return "Anonymus";
            else
                return List[_id];
        }
    }

    public int Id => _id;
}