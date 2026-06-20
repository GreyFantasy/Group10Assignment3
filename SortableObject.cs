namespace Assignment3;

class SortableObject : IComparable<SortableObject>, IEquatable<SortableObject>
{
    public int Key { get; set; }
    public double[] Data { get; }

    private readonly static int sizeOfObject = 1500;
    private readonly static Random random = new();

    public SortableObject(int key)
    {
        Key = key;
        Data = new double[sizeOfObject];
        for (int i = 0; i < sizeOfObject; i++)
            random.NextDouble();
    }

    public int CompareTo(SortableObject? other)
    {
        if (other is null)
            return 1;
        else
            return Key.CompareTo(other.Key);
    }

    public bool Equals(SortableObject? other)
    {
        if (other is null)
            return false;
        else
            return Key == other.Key;
    }

    // C# strongly recommends implementing these overrides for anything that implements IEquatable<T>:
    public override bool Equals(object? obj) => Equals(obj as SortableObject);
    public override int GetHashCode() => Key.GetHashCode();
    // (note: => is a short-hand for quickly returning something from a single-line method)

    public override string ToString()
    {
        return $"(Key: {Key:D4})";
    }
}